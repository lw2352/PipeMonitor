using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Timers;

//ToDo: 1.在所有设备的22发上来后，关闭定时器，避免在上传过程中占用CPU资源（完成）
//      2.上层收到AD采样结束的22--55之后，把采样完成时间写入数据库，而不是记录上传时间
//       （22指令的接收数据并发大，不考虑直接处理存入数据库，而是作为dataitem属性暂存）
//      3.对于命令解析用switch效率高，对于所有设备的状态判断最好一次性查找哈希表弄完
//      4.数据批量分析用队列，先从数据库读取符合要求的ID对，再一个一个地分析
//      5.不能保证所有设备的工作按预期进行，要做好处理和记录

namespace pipemonitor
{
    public class DataItem
    {
        //21个
        public int datalength;
        public string strIP;
        public string strPort;      //设备的端口号(掉线后改变)
        public byte[] byteAllData; //所有数据，算一个完整的数据。
        public byte[] byteDeviceID; //这个是设备的ID的byte数组
        public int intDeviceID;    //转化为int后的ID
        public int currentsendbulk; //当前发送的包数
        //public int totalsendbulk; //总发送的包数
        public Socket socket;   //Socket of the client
        public bool isSendDataToServer;//发送数据到服务器
        //public bool isChoosed;
        public int CmdNum;//当前发到第几条命令
        //**************
        //从数据库读取的参数
        public int CmdbulkHex;//要发送的命令数目--16进制
        public int CapTimeHour;//采样时刻--小时
        public int CapTimeMinute;//采样时刻--分钟
        public int OpenTime;//开启时长（分钟）
        public int CloseTime;//关闭时长（分钟）
        //***********
        public int CmdStage;//0-没发命令；1-除上传以外的命令发完；2-分段时间设定完；3-数据上传并存储完
        public int isWaked;//设备醒来
        public int uploadGroup;//用来判断对哪一组设备发送上传命令//test
        public bool isGetADNow; //加一个立即采样属性,用来上传

        public byte[] SingleBuffer;
        public string strAddress;
    }

    public class CmdItem
    {
        public byte[] CmdReadGpsTime = new byte[] { 0xA5, 0xA5, 0x25, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x04, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x5A, 0x5A };//读取GPS采样时间
        public byte[] CmdSetCapTime = new byte[] { 0xA5, 0xA5, 0x25, 0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0x04, 0x0A, 0x1E, 0x00, 0x00, 0xFF, 0x5A, 0x5A }; //设定GPS采样时间,byte[9]是小时，byte[10]是分钟
        //public byte[] CmdSetOpenTime  = new byte[] { 0xA5, 0xA5, 0x26, 0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0x02, 0xFF, 0xFF, 0xFF, 0x5A, 0x5A };//设置开启时长
        //public byte[] CmdSetCloseTime = new byte[] { 0xA5, 0xA5, 0x27, 0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0x02, 0x00, 0x00, 0xFF, 0x5A, 0x5A };//设置关闭时长
        //public byte[] CmdADNow        = new byte[] { 0xA5, 0xA5, 0x22, 0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0x01, 0xFF, 0xFF, 0x5A, 0x5A };//立即采样
        public byte[] CmdSetOpenAndCloseTime = new byte[] { 0xA5, 0xA5, 0x26, 0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0x04, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x5A, 0x5A };//设置开启和关闭时长
    }

    public class Net_Device
    {
        //public delegate void UpdateStatusDelegate(string status);//使用安全方法来更新控件
        //public string IP = "192.168.1.104";//服务端的IP地址
        public string IP = "120.25.229.254";//服务端的IP地址
        public int Port = 8085; //服务器端口地址
        //public int Port = 8080;
        //public byte[] buffer = new byte[1002]; //临时缓冲区
        //public byte[] buffer = new byte[1008]; //临时缓冲区
        public int perPackageLength = 1008;//每包的长度
        public static int g_datafulllength = 600000; //完整数据包的一个长度
        public static int g_totalPackageCount = 600; //600个包
        public static Hashtable htClient = new Hashtable(); //创建一个Hashtable实例，保存所有设备信息，key存储是ID号，value是DataItem;
        public static Socket ServerSocket; //The main socket on which the server listens to the clients
        public static System.Timers.Timer cmdTimer = new System.Timers.Timer();
        public static CmdItem cmdItem = new CmdItem();//实例化
        public static int currentUploadGroup = 0;//当前第几组上传

        public static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Net_Device));

        #region 开启和关闭服务
        //开启服务
        public void OpenServer()
        {
            try
            {
                //初始化定时器
                cmdTimer.Interval = 5000; //执行间隔时间,单位为毫秒; 这里为5秒  
                cmdTimer.Elapsed += new System.Timers.ElapsedEventHandler(SendCmdOnTime);

                ServerSocket = new Socket(AddressFamily.InterNetwork,
                                                 SocketType.Stream,
                                                 ProtocolType.Tcp);

                //Assign the any IP of the machine and listen on port number 8080
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);

                //Bind and listen on the given address
                ServerSocket.Bind(ipEndPoint);
                ServerSocket.Listen(8080);

                //Accept the incoming clients
                ServerSocket.BeginAccept(new AsyncCallback(OnAccept), null);

                Log.Debug("服务开启成功");

            }
            catch (Exception ex)
            {
                string error = DateTime.Now.ToString() + "出错信息：" + "---" + ex.Message + "\n";
                Log.Debug(error);
                Log.Debug("服务开启失败");
                System.Diagnostics.Debug.WriteLine(error);
            }
        }

        public void CloseServer()
        {
            //ServerSocket.Close();//用循环关闭哈希表中的socket
            try
            {
                foreach (DictionaryEntry de in htClient)
                {
                    DataItem dataitem = (DataItem)de.Value;
                    dataitem.socket.Shutdown(SocketShutdown.Both);
                    dataitem.socket.Close();
                }
                ServerSocket.Close();
                htClient.Clear();//清除哈希表
            }
            catch (Exception ex)
            {
                string error = DateTime.Now.ToString() + "出错信息：" + "---" + ex.Message + "\n";
                Log.Debug(error);
                System.Diagnostics.Debug.WriteLine(error);
            }
        }
        #endregion 开启和关闭服务

        #region 接收设备的连接请求和数据
        //接收来自客户端的请求
        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = ServerSocket.EndAccept(ar);

                //Start listening for more clients
                ServerSocket.BeginAccept(new AsyncCallback(OnAccept), null);

                //add 5-6
                string strIP = (clientSocket.RemoteEndPoint as IPEndPoint).Address.ToString();
                string strPort = (clientSocket.RemoteEndPoint as IPEndPoint).Port.ToString();
                string strAddress = clientSocket.RemoteEndPoint.ToString();
                if (!htClient.ContainsKey(strAddress))
                {
                    DataItem dataitem = new DataItem();
                    dataitem.byteDeviceID = new byte[4]; ;
                    dataitem.intDeviceID = 0;
                    dataitem.strIP = strIP;
                    dataitem.strPort = strPort;
                    dataitem.datalength = 0;
                    dataitem.socket = clientSocket;
                    dataitem.byteAllData = new byte[g_datafulllength];
                    dataitem.currentsendbulk = 0;
                    dataitem.uploadGroup = 0;
                    dataitem.isGetADNow = false;
                    dataitem.isWaked = 1;
                    dataitem.CmdStage = 0;
                    dataitem.isSendDataToServer = false;
                    dataitem.CmdNum = 0;
                    dataitem.SingleBuffer = new byte[perPackageLength];
                    dataitem.strAddress = strAddress;
                    htClient.Add(strAddress, dataitem);
                    //Once the client connects then start receiving the commands from her
                    //开始从连接的socket异步接收数据
                    clientSocket.BeginReceive(dataitem.SingleBuffer, 0, dataitem.SingleBuffer.Length, SocketFlags.None,
                    new AsyncCallback(OnReceive), clientSocket);
                }
            }
            catch (Exception ex)
            {
                string error = DateTime.Now.ToString() + "出错信息：" + "---" + ex.Message + "\n";
                Log.Debug(error);
                System.Diagnostics.Debug.WriteLine(error);
            }
        }

        //接收数据
        private void OnReceive(IAsyncResult ar)
        {
            byte[] ID = new byte[4];//设备的ID号
            int intdeviceID = 0;
            int[] cfg = new int[5];//存储从数据库读取的设备配置参数
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;//此处获取数据大小              

                //获取客户端信息，包括了IP地址、端口
                string strIP = (clientSocket.RemoteEndPoint as IPEndPoint).Address.ToString();
                string strPort = (clientSocket.RemoteEndPoint as IPEndPoint).Port.ToString();
                //test
                string strAddress = clientSocket.RemoteEndPoint.ToString();

                DataItem dataitem = (DataItem)htClient[strAddress];//取出address对应的dataitem
                //获取接收的数据长度,注意此处的停止接收，后面必须继续接收，否则不会接收数据的。
                int bytesRead = clientSocket.EndReceive(ar);//接收到的数据长度
                if (bytesRead > 0)     //打印数据
                {
                    string str = byteToHexStr(dataitem.SingleBuffer);
                    string strrec = str.Substring(0, bytesRead * 2);
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "从硬件" + strAddress + "设备号--" + dataitem.intDeviceID + "接收到的数据长度是" + bytesRead.ToString() + "数据是" + strrec + "\n"); //把接收的数据打印出来

                    if (checkIsHeartPackage(dataitem.SingleBuffer))
                    {
                        if (dataitem.intDeviceID == 0)//只判断新地址的心跳包，避免重复检测
                        {
                            //设备的ID字符串
                            ID[0] = dataitem.SingleBuffer[3];
                            ID[1] = dataitem.SingleBuffer[4];
                            ID[2] = dataitem.SingleBuffer[5];
                            ID[3] = dataitem.SingleBuffer[6];
                            intdeviceID = byteToInt(ID);

                            string oldAddress = checkIsHaveID(intdeviceID);//得到当前ID对应的旧地址

                            if (oldAddress != null)//若存在，把旧地址的属性复制到新地址上
                            {//！！！由于掉线，新dataitem属性要继承旧设备，只需要更新网络属性，如IP、port、socket等
                                DataItem olddataitem = (DataItem)htClient[oldAddress];//取出当前数据IP对应的dataitem

                                dataitem.byteDeviceID = ID;
                                dataitem.intDeviceID = intdeviceID;
                                dataitem.strIP = strIP;
                                dataitem.strPort = strPort;
                                dataitem.datalength = olddataitem.datalength;//继承旧属性
                                dataitem.socket = clientSocket;
                                dataitem.byteAllData = olddataitem.byteAllData;//继承旧属性
                                dataitem.currentsendbulk = olddataitem.currentsendbulk;//继承旧属性
                                dataitem.uploadGroup = olddataitem.uploadGroup;//继承旧属性
                                dataitem.isGetADNow = olddataitem.isGetADNow;
                                dataitem.isWaked = 1;
                                dataitem.CmdStage = olddataitem.CmdStage;//继承旧属性
                                dataitem.isSendDataToServer = olddataitem.isSendDataToServer;//继承旧属性
                                dataitem.CmdNum = olddataitem.CmdNum;//继承旧属性
                                dataitem.SingleBuffer = new byte[perPackageLength];
                                dataitem.strAddress = strAddress;
                                //cfg = Net_DB.readsensorcfg(intdeviceID);//从数据库读取设备的配置参数
                                //if (cfg != null)
                                //{
                                dataitem.CmdbulkHex = olddataitem.CmdbulkHex;//要发送的命令数目--16进制//继承旧属性
                                dataitem.CapTimeHour = olddataitem.CapTimeHour;//继承旧属性
                                dataitem.CapTimeMinute = olddataitem.CapTimeMinute;//继承旧属性
                                dataitem.OpenTime = olddataitem.OpenTime;//继承旧属性
                                dataitem.CloseTime = olddataitem.CloseTime;//继承旧属性
                                //}
                                htClient.Remove(oldAddress);//删除旧地址的键值对
                                htClient[strAddress] = dataitem;

                                string time = DateTime.Now.ToString();
                                //把信息存入数据库
                                Net_DB.addsensorinfo(intdeviceID, strIP, strPort, time, dataitem.isWaked);

                                Log.Debug("ID为"+intdeviceID+"--地址" +strAddress+"已存储");
                            }
                            else
                            {
                                //若不存在，属于全新地址，更新ID号
                                dataitem.intDeviceID = intdeviceID;
                                dataitem.byteDeviceID = ID;
                                cfg = Net_DB.readsensorcfg(intdeviceID);//从数据库读取设备的配置参数
                                if (cfg != null)
                                {
                                    dataitem.CmdbulkHex = cfg[0];//要发送的命令数目--16进制
                                    dataitem.CapTimeHour = cfg[1];
                                    dataitem.CapTimeMinute = cfg[2];
                                    dataitem.OpenTime = cfg[3];
                                    dataitem.CloseTime = cfg[4];
                                }
                                //开启命令发送定时器
                                if (cmdTimer.Enabled != true)
                                    cmdTimer.Start();

                                string time = DateTime.Now.ToString();
                                //把信息存入数据库
                                Net_DB.addsensorinfo(intdeviceID, strIP, strPort, time, dataitem.isWaked);
                            }
                        }//if (dataitem.intDeviceID == 0)
                    }//end if (checkIsHeartPackage()) 
      
                    //设备进入低功耗模式后，断开网络，从哈希表中移除
                    //如果是暂时关闭，则不能从哈希表中移除
                    if (checkIsOffLine(dataitem.SingleBuffer))
                    {
                        //更新数据库，把isWaked置0
                        dataitem.isWaked = 0;
                        Net_DB.addsensorinfo(dataitem.intDeviceID, strIP, strPort, DateTime.Now.ToString(), dataitem.isWaked);
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                        htClient.Remove(intdeviceID);
                        System.Diagnostics.Debug.WriteLine("设备已断开连接,进入低功耗模式");
                    }
                    if (checkIsGPSsetOK(dataitem.SingleBuffer))
                    {
                        dataitem.CmdNum++;
                        System.Diagnostics.Debug.WriteLine("设定GPS采样时间成功");
                        Log.Debug("设备号为"+dataitem.intDeviceID+"设定GPS采样时间成功");
                    }
                    if (checkIsGPSReadOK(dataitem.SingleBuffer))
                    {
                        dataitem.CmdNum++;
                        System.Diagnostics.Debug.WriteLine("读取GPS当前时间成功");
                        Log.Debug("设备号为" + dataitem.intDeviceID + "读取GPS当前时间成功");
                    }
                    /*if (checkIsOpenTimeSetOK())
                    {
                        dataitem.CmdNum++;
                        System.Diagnostics.Debug.WriteLine("开启时长设置成功");
                    }*/
                    if (checkIsOpenAndCloseTime(dataitem.SingleBuffer))
                    {
                        dataitem.CmdNum++;
                        System.Diagnostics.Debug.WriteLine("关闭时长设置成功");
                        Log.Debug("设备号为" + dataitem.intDeviceID + "关闭时长设置成功");

                        //该设备需要设置或者查询的命令已发完，剩下的是上传
                        dataitem.CmdbulkHex = 0;

                    }
                    if (checkIsADstart(dataitem.SingleBuffer))
                    {
                        //dataitem.CmdNum++;
                        System.Diagnostics.Debug.WriteLine("AD采样开始");
                    }

                    //检查AD采样是否结束
                    if (checkIsADfinished(dataitem.SingleBuffer))
                    {
                        System.Diagnostics.Debug.WriteLine("AD采样结束,准备上传AD数据");
                        //！！！在此处记录该设备的一套命令已发完，把CmdbulkHex置0；若所有设备的命令发完（包括AD采样），则关闭定时器
                        dataitem.CmdStage = 1;
                        
                        Log.Debug("设备号为" + dataitem.intDeviceID + "AD采样结束,CmdStage设为1");
                        //如果是立即采样，则采样完成后上传AD数据
                        if (dataitem.isGetADNow == true)
                        {                           
                            UploadADdata(dataitem.strAddress);
                        }
                        
                    }

                    //处理AD数据（收到纯数据时保存到一个60万大小的数组中）
                    if (checkIsPureData(dataitem.SingleBuffer))
                    {
                        dataitem.currentsendbulk++;

                        for (int i = 7; i < perPackageLength - 1; i++)//将上传的包去掉头和尾的两个字节后，暂时存储在TotalData[]中
                        {
                            dataitem.byteAllData[dataitem.datalength++] = dataitem.SingleBuffer[i];
                        }
                        if (dataitem.datalength == g_datafulllength)//1000*600 = 600000;
                        {
                            StoreDataToFile(dataitem.intDeviceID, dataitem.byteAllData);
                            //复位变量
                            //dataitem.totalsendbulk = 0;
                            dataitem.currentsendbulk = 0;
                            dataitem.isSendDataToServer = false;
                            System.Diagnostics.Debug.WriteLine("数据采集完毕");
                            //progressBar1.Value = 0;
                            //！！！若所有设备的命令发完（包括AD采样），设置一个属性
                            //！！！还要加一个判断，若哈希表中的所有设备都已发完命令。就可以关闭定时器了
                            dataitem.CmdStage = 3;
                            //test
                            Log.Debug("设备号为" + dataitem.intDeviceID + "数据采集完毕,CmdStage设为3");
                            if (dataitem.isGetADNow == true)
                            {
                                //立即采样结束后恢复采样时刻
                                byte[] cmdCapTime = cmdItem.CmdSetCapTime;
                                cmdCapTime[9] = (byte)dataitem.CapTimeHour;
                                cmdCapTime[10] = (byte)dataitem.CapTimeMinute;
                                SendCmdSingle(cmdCapTime, dataitem.byteDeviceID, dataitem.socket, 1);

                                dataitem.isGetADNow = false;//立即采样的数据上传完成后，将属性复位为false
                            }
                        }
                    }

                    if (checkIsAllCmdStage1())//所有设备采样完成，关闭定时器，可以进行上传了
                    {
                        cmdTimer.Stop();//add 5-12
                        Log.Debug("所有设备采样完成，关闭定时器,开始分组");
                        setTaskGroup();

                        if (checkIsAllCmdStage2())//所有设备的分组属性设置完，上传第0组，激活分段上传过程
                        {
                            Log.Debug("分组完成，开始上传");
                            UploadADdataByGroup(currentUploadGroup);
                        }
                    }

                    //检查是否当前组都下线且下一组都上线
                    if (checkCurrentOffLineAndNextOnLine(currentUploadGroup))
                    {
                        //当前组已经发完并下线,可以给下一组发送上传命令了
                        Log.Debug("当前组已经发完并下线,可以给下一组发送上传命令了");
                        
                        //将当前组(第0组不用恢复)设备恢复关闭时长
                        byte[] cmdSetOpenAndCloseTime = cmdItem.CmdSetOpenAndCloseTime;
                        cmdSetOpenAndCloseTime[9] = (byte)(2 * dataitem.OpenTime >> 8);
                        cmdSetOpenAndCloseTime[10] = (byte)(2 * dataitem.OpenTime & 0xFF);
                        cmdSetOpenAndCloseTime[11] = (byte)(2 * dataitem.CloseTime >> 8);
                        cmdSetOpenAndCloseTime[12] = (byte)(2 * dataitem.CloseTime & 0xFF);
                        SendCmdSingle(cmdSetOpenAndCloseTime, dataitem.byteDeviceID, dataitem.socket, 1);

                        currentUploadGroup += 1;

                        UploadADdataByGroup(currentUploadGroup);
                    }

                    if (checkIsAllCmdStage3())
                    {
                        //可以进行数据分析了
                        //htClient.Clear();
                        Log.Debug("所有设备数据传完");
                    }

                    if (checkIsAllOffLine())//add 5-11
                    {
                        //cmdTimer.Stop();
                        htClient.Clear();
                        Log.Debug("清空哈希表");
                    }

                    //把数据上传到服务器
                    if (dataitem.isSendDataToServer == true)
                    {
                        SendCmdSingle(SetADcmd(dataitem.currentsendbulk), dataitem.byteDeviceID, dataitem.socket, 1);//发送下一包的命令                                                                                                                                
                        System.Diagnostics.Debug.WriteLine("第" + dataitem.currentsendbulk + "包");
                        Log.Debug("设备号为" + dataitem.intDeviceID + "第" + dataitem.currentsendbulk + "包");
                    }

                }

                else if (bytesRead == 0)//设备自己关闭socket
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    Log.Debug(dataitem.strAddress+"设备自己关闭socket");
                }
                //需要持续接收 
                clientSocket.BeginReceive(dataitem.SingleBuffer, 0, dataitem.SingleBuffer.Length, SocketFlags.None, new AsyncCallback(OnReceive), clientSocket);
            }
            catch (Exception ex)
            {
                string error = DateTime.Now.ToString() + "出错信息：" + "---" + ex.Message + "\n";
                Log.Debug(ex);
                System.Diagnostics.Debug.WriteLine(error);
            }
        }
        #endregion 接收设备的连接请求和数据

        #region 16进制转换和check函数

        //检查哈希表中是否已存在当前ID
        public string checkIsHaveID(int id)
        {
            foreach (DictionaryEntry de in htClient)
            {
                DataItem dataitem = (DataItem)de.Value;
                if (dataitem.intDeviceID == id)//设备掉线后IP改变而ID号不变
                    return dataitem.strAddress;
            }
            return null;
        }

        public bool checkIsOffLine(byte[] buffer)
        {//buffer[4]指示1pps信息
            if (buffer[0] == 0xA5 && buffer[1] == 0xA5 && buffer[2] == 0xFF && buffer[3] == 0xFF && buffer[9] == 0xFF && buffer[10] == 0x5A && buffer[11] == 0x5A)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判断是否所有设备已经离线
        public static bool checkIsAllOffLine()//add 5-11
        {
            //此处进行遍历操作
            foreach (DictionaryEntry de in htClient)
            {
                DataItem dataitem = (DataItem)de.Value;
                if (dataitem.isWaked !=0 && dataitem.intDeviceID != 0)//add 5-9
                    return false;
            }
            return true;
        }

        // 字节数组转16进制字符串 
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (long i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        //字节数组转int值
        public static int byteToInt(byte[] bytes)
        {
            int returnInt = 0;
            if (bytes != null)
            {
                for (long i = 0; i < bytes.Length; i++)
                {
                    if (i == 3)
                        returnInt += (int)bytes[i];
                    else if (i == 2)
                        returnInt += (int)bytes[i] * 256;
                    else if (i == 1)
                        returnInt += (int)bytes[i] * 65536;
                    else if (i == 0)
                        returnInt += (int)bytes[i] * 16777216;
                }
            }
            return returnInt;
        }

        /*// 字符串转16进制字节数组(1-->1,没用到)
        private static byte[] strToHexByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length];
            for (int i = 0; i < returnBytes.Length; i++)
            { returnBytes[i] = Convert.ToByte(hexString.Substring(i, 1), 16); }
            return returnBytes;
        }*/


        /*//字符串转数组(1-->49)
        private static byte[] strToByte(string str)
        {
            byte[] bytes = new byte[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                bytes[i] = Convert.ToByte(str[i]);
            }
            return bytes;
        }*/

        //将int数值转换为占byte数组
        public static byte[] intToBytes(int value)
        {
            byte[] src = new byte[2];

            src[0] = (byte)((value >> 8) & 0xFF);
            src[1] = (byte)(value & 0xFF);
            return src;
        }

        //检查是否是心跳包，主要检查命令码即可
        private bool checkIsHeartPackage(byte[] buffer)
        {
            if (buffer[0] == 0xA5 && buffer[1] == 0xA5 && buffer[2] == 0xFF && buffer[3] == 0x00 && buffer[9] == 0xFF && buffer[10] == 0x5A && buffer[11] == 0x5A)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //检查AD采样结束是否开始
        private bool checkIsADstart(byte[] buffer)
        {
            if (buffer[0] == 0xA5 && buffer[1] == 0xA5 && buffer[2] == 0x22 && buffer[9] == 0xAA && buffer[10] == 0xFF && buffer[11] == 0x5A && buffer[12] == 0x5A)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //检查AD采样结束是否结束
        private bool checkIsADfinished(byte[] buffer)
        {
            if (buffer[0] == 0xA5 && buffer[1] == 0xA5 && buffer[2] == 0x22 && buffer[9] == 0x55 && buffer[10] == 0xFF && buffer[11] == 0x5A && buffer[12] == 0x5A)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //设定GPS采样时间成功
        private bool checkIsGPSsetOK(byte[] buffer)
        {
            if (buffer[0] == 0xA5 && buffer[1] == 0xA5 && buffer[2] == 0x25 && buffer[7] == 0x01 && buffer[9] == 0x55 && buffer[10] == 0xFF && buffer[11] == 0x5A && buffer[12] == 0x5A)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //读取GPS当前时间成功
        private bool checkIsGPSReadOK(byte[] buffer)
        {
            if (buffer[0] == 0xA5 && buffer[1] == 0xA5 && buffer[2] == 0x25 && buffer[7] == 0x00 && buffer[13] == 0xFF && buffer[14] == 0x5A && buffer[15] == 0x5A)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //开启时长是否设置成功
        /* private bool checkIsOpenTimeSetOK()
         {
             if (buffer[0] == 0xA5 && buffer[1] == 0xA5 && buffer[2] == 0x26 && buffer[7] == 0x01 && buffer[11] == 0xFF && buffer[12] == 0x5A && buffer[13] == 0x5A)
             {
                 return true;
             }
             else
             {
                 return false;
             }
         }*/

        //开启和关闭时长是否设置成功
        private bool checkIsOpenAndCloseTime(byte[] buffer)
        {
            if (buffer[0] == 0xA5 && buffer[1] == 0xA5 && buffer[2] == 0x26 && buffer[7] == 0x01 && buffer[13] == 0xFF && buffer[14] == 0x5A && buffer[15] == 0x5A)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //是否完全是采集数据，而不是命令码或者应答或者心跳包之类的。
        private bool checkIsPureData(byte[] buffer)
        {
            int DataLength = buffer.Length;
            if (buffer[0] == 0xA5 && buffer[1] == 0xA5 && buffer[2] == 0xAA && buffer[DataLength - 1] == 0x55 && DataLength == perPackageLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //是否为当前GPS时间
        private bool checkIsGPSTime(byte[] buffer)
        {
            if (buffer[0] == 0xA5 && buffer[1] == 0xA5 && buffer[2] == 0x24 && buffer[7] == 0x00 && buffer[12] == 0xFF && buffer[13] == 0x5A && buffer[14] == 0x5A)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //检查所有设备处于第几命令阶段--stage1--除上传外的命令已发完，可以进行上传了
        private bool checkIsAllCmdStage1()
        {
            //此处进行遍历操作
            foreach (DictionaryEntry de in htClient)
            {
                DataItem dataitem = (DataItem)de.Value;
                if (dataitem.CmdStage != 1 && dataitem.intDeviceID != 0)//add 5-9
                    return false;
            }
            return true;
        }

        //检查所有设备处于第几命令阶段--stage2--分组属性设置好了，可以分段上传
        private bool checkIsAllCmdStage2()
        {
            //此处进行遍历操作
            foreach (DictionaryEntry de in htClient)
            {
                DataItem dataitem = (DataItem)de.Value;
                if (dataitem.CmdStage != 2 && dataitem.intDeviceID != 0)//add 5-9
                    return false;
            }
            return true;
        }

        //检查所有设备处于第几命令阶段--stage3--所有命令发完，可以进行数据分析了
        private bool checkIsAllCmdStage3()
        {
            //此处进行遍历操作
            foreach (DictionaryEntry de in htClient)
            {
                DataItem dataitem = (DataItem)de.Value;
                if (dataitem.CmdStage != 3 && dataitem.intDeviceID != 0)//add 5-9
                    return false;
            }
            return true;
        }

        //设置任务组，先要给设备分组
        public void setTaskGroup()
        {
            //！！！！！！！
            //第一组1--10号，（设临时关闭时长为0），可以直接开始上传
            //第二组11--20号，设临时关闭时长为30，（默认一组设备耗时30分钟）
            //第三组21--30号，设临时关闭时长为60；
            //第三组31--40号，设临时关闭时长为90；
            //........

            int i = 0;
            int j = 0;
            //此处进行遍历操作
            foreach (DictionaryEntry de in htClient)
            {
                DataItem dataitem = (DataItem)de.Value;
                if (dataitem.CmdStage == 1)
                {
                    if (i < 10)
                    {
                        i++;
                        dataitem.uploadGroup = j;//设置组属性
                        dataitem.CmdStage = 2;
                        if (j >= 1)//add 5-12 第0组不设置暂时关闭时长
                        {
                            SendCmdSingle(CmdSetTempCloseTime(j, dataitem.strAddress), dataitem.byteDeviceID, dataitem.socket, 1);//设置暂时关闭时长
                        }
                    }
                    else
                    {
                        i = 0;
                        j++;
                    }
                }
            }
            //分组完成后，默认当前上传组为第0组
            currentUploadGroup = 0;

            /* Old code
            int i = 0;
            //此处进行遍历操作
            foreach (DictionaryEntry de in htClient)
            {
                DataItem dataitem = (DataItem)de.Value;
                if (dataitem.CmdStage == 1)
                {
                    //一个任务组有10个设备
                    i++;
                    dataitem.isSendDataToServer = true;
                    dataitem.datalength = 0;
                    dataitem.totalsendbulk = 0;
                    dataitem.currentsendbulk = 0;
                    SendCmdSingle(SetADcmd(0), dataitem.byteDeviceID, dataitem.socket, 1);//发送第0包的命令;采集AD数据时第四个参数默认为1
                    dataitem.currentsendbulk++;
                    //test
                    WriteLog(DateTime.Now.ToString() + "设备号--" + dataitem.intDeviceID + "--AD数据开始发送" + "\r\n"); 
                }
                if (i == 9)
                    break;
            }
            */
        }

        //检查是否当前组都下线且下一组都上线
        private bool checkCurrentOffLineAndNextOnLine(int currentUploadGroup)
        {
            //bool currentGroupOffline = false;
            //bool nextGroupOnline = false;
            foreach (DictionaryEntry de in htClient)
            {
                DataItem dataitem = (DataItem)de.Value;
                if (dataitem.uploadGroup == currentUploadGroup && dataitem.isWaked == 0)
                {
                    //currentGroupOffline = true;
                }
                else return false;

                if (dataitem.uploadGroup == (currentUploadGroup + 1) && dataitem.isWaked == 1)
                {
                    //nextGroupOnline = true;
                }
                else return false;
            }
            return true;
        }

        #endregion 16进制转换和check函数

        #region 发送命令       
        /*//(多个)将命令从服务端发送到每一个选中的设备（普通命令）
        public void SendCmdAll(byte[] cmd)
        {
            byte[] ChoosedDeviceID = new byte[4]; //已选择设备的ID号
            try
            {
                //此处进行遍历操作
                foreach (DictionaryEntry de in htClient)
                {
                    DataItem dataitem = (DataItem)de.Value;
                    if (dataitem.isWaked == 1)
                    {
                        ChoosedDeviceID = dataitem.byteDeviceID;
                        cmd[3] = ChoosedDeviceID[0];
                        cmd[4] = ChoosedDeviceID[1];
                        cmd[5] = ChoosedDeviceID[2];
                        cmd[6] = ChoosedDeviceID[3];
                        dataitem.socket.BeginSend(cmd, 0, cmd.Length, SocketFlags.None, new AsyncCallback(OnSend), dataitem.socket);
                        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        System.Diagnostics.Debug.WriteLine(timestamp + "向设备" + dataitem.intDeviceID + "发送命令是：" + byteToHexStr(cmd) + "\n");
                    }
                }
            }
            catch (Exception ex)
            {
                string error = DateTime.Now.ToString() + "出错信息：" + "---" + ex.Message + "\n";
                WriteLog(error);
                System.Diagnostics.Debug.WriteLine(error);
            }
        }*/

        //(单个)将命令从服务端发送到特定的设备（数据采集）
        public void SendCmdSingle(byte[] cmd, byte[] id, Socket deviceSocket, int IsSend)
        {
            if (IsSend != 0)
            { 
                    cmd[3] = id[0];
                    cmd[4] = id[1];
                    cmd[5] = id[2];
                    cmd[6] = id[3];
                    try
                    {
                        deviceSocket.BeginSend(cmd, 0, cmd.Length, SocketFlags.None, new AsyncCallback(OnSend), deviceSocket);
                        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        System.Diagnostics.Debug.WriteLine(timestamp + "向设备" + byteToHexStr(id) + "发送命令是：" + byteToHexStr(cmd) + "\n");
                    }

                    catch (Exception ex)
                    {
                        string error = DateTime.Now.ToString() + "出错信息：" + "---" + ex.Message + "\n";
                        Log.Debug(error);
                        System.Diagnostics.Debug.WriteLine(error);
                    }
            }
            else
            {
                //IPAddress clientIPAddress = (deviceSocket.RemoteEndPoint as IPEndPoint).Address;
                //string ip = clientIPAddress.ToString();
                //int intID = byteToInt(id);
                string strAddress = deviceSocket.RemoteEndPoint.ToString();
                DataItem dataitem = (DataItem)htClient[strAddress];//取出当前数据ID对应的dataitem
                dataitem.CmdNum++;//不需要发送命令，仅CmdNum++
            }
        }

        //发送数据
        public void OnSend(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);
            }
            catch (Exception ex)
            {
                string error = DateTime.Now.ToString() + "出错信息：" + "---" + ex.Message + "\n";
                Log.Debug(error);
                System.Diagnostics.Debug.WriteLine(error);
            }
        }
        #endregion 发送命令

        #region 保存文件
        //保存文件，16进制，封装开头是0xAA，结尾是0x55
        public void StoreDataToFile(int intDeviceID, byte[] bytes)
        {
            string filename = DateTime.Now.ToString("yyyy-MM-dd") + "--" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "--" + intDeviceID.ToString();//以日期时间命名，避免文件名重复
            byte[] fileStartAndEnd = new byte[2] { 0xAA, 0x55 };//保存文件的头是AA，尾是55
            string path = @"D:\\Data\\" + filename + ".dat";
            FileStream F = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            F.Write(fileStartAndEnd, 0, 1);
            F.Write(bytes, 0, bytes.Length);
            F.Write(fileStartAndEnd, 1, 1);
            F.Flush();
            F.Close();
            //StatusBox.Text = "文件保存成功";
            Net_DB.addsensorad(intDeviceID, DateTime.Now.ToString(), path);
        }

        #endregion 保存文件

        #region AD立即采样、获取进度（分阶段）、数据上传
        //立即采样
        public string GetADNow(int id)
        {
            byte[] cmd = cmdItem.CmdSetCapTime;

            cmd[9] = (byte)DateTime.Now.Hour;
            cmd[10] = (byte)(DateTime.Now.Minute + 5);//当前时刻加5分钟
            try
            {//此处进行遍历操作
                foreach (DictionaryEntry de in htClient)
                {
                    DataItem dataitem = (DataItem)de.Value;
                    if (dataitem.intDeviceID == id)
                    {
                        dataitem.isGetADNow = true;
                        SendCmdSingle(cmd, dataitem.byteDeviceID, dataitem.socket, 1);
                        return "OK";
                    }
                }
                return "Fail";
            }
            catch (Exception ex)
            {
                return "Fail";
            }
        }

        //获取上传进度
        public int GetUploadStatus(int id)
        {
            //此处进行遍历操作
            foreach (DictionaryEntry de in htClient)
            {
                DataItem dataitem = (DataItem)de.Value;
                if (dataitem.intDeviceID == id && dataitem.CmdStage == 1)
                {
                    return dataitem.currentsendbulk;//如果上传完成，返回值为600，方便前天处理进度条
                }
                else if (dataitem.intDeviceID == id && dataitem.CmdStage == 3)
                {
                    return g_totalPackageCount;//此时数据全部上传完毕并且已存入数据库
                }
            }
            return 0;
        }

        //让设备上传数据到服务器      
        private void UploadADdata(string address)
        {
            DataItem dataitem = (DataItem)htClient[address];
            dataitem.isSendDataToServer = true;
            dataitem.datalength = 0;
            dataitem.currentsendbulk = 0;
            SendCmdSingle(SetADcmd(0), dataitem.byteDeviceID, dataitem.socket, 1);//发送第0包的命令;采集AD数据时第四个参数默认为1
            System.Diagnostics.Debug.WriteLine(dataitem.currentsendbulk.ToString());
        }

        //向一组设备发送上传命令
        private void UploadADdataByGroup(int group)
        {
            //此处进行遍历操作
            foreach (DictionaryEntry de in htClient)
            {
                DataItem dataitem = (DataItem)de.Value;
                if (dataitem.uploadGroup == group && dataitem.intDeviceID != 0)
                {
                    dataitem.isSendDataToServer = true;
                    dataitem.datalength = 0;
                    dataitem.currentsendbulk = 0;
                    SendCmdSingle(SetADcmd(0), dataitem.byteDeviceID, dataitem.socket, 1);//发送第0包的命令;采集AD数据时第四个参数默认为1
                }
            }
        }

        //构造暂时关闭时长命令
        public byte[] CmdSetTempCloseTime(int group, string address)
        {
            byte[] Cmd = cmdItem.CmdSetOpenAndCloseTime; ;//设置暂时关闭时长
            byte[] bytetime = new byte[2];
            DataItem dataitem = (DataItem)htClient[address];
            Cmd[9] = (byte)(2 * dataitem.OpenTime >> 8);
            Cmd[10] = (byte)(2 * dataitem.OpenTime & 0xFF);

            if (group == 0)
            {
                //bytetime = intToBytes(group * 30 * 2);
                //bytetime = intToBytes(2);//第0组关闭时长1分钟
                //Cmd[11] = bytetime[0];
                //Cmd[12] = bytetime[1];
                //测试，第0组关闭时长不变 5-12
                Cmd[11] = (byte)(2 * dataitem.CloseTime >> 8);
                Cmd[12] = (byte)(2 * dataitem.CloseTime & 0xFF);
            }
            else
            {
                bytetime = intToBytes(group * 30 * 2);
                Cmd[11] = bytetime[0];
                Cmd[12] = bytetime[1];
            }
            return Cmd;
        }

        //构造ADcmd
        public byte[] SetADcmd(int bulkCount)
        {
            byte[] Cmd = new byte[] { 0xA5, 0xA5, 0x23, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x06, 0x02, 0x57, 0xFF, 0xFF, 0x03, 0xE8, 0xFF, 0x5A, 0x5A };
            byte[] bytesbulkCount = new byte[2];
            bytesbulkCount = intToBytes(bulkCount);

            Cmd[11] = bytesbulkCount[0];
            Cmd[12] = bytesbulkCount[1];

            if (Cmd[11] == 0x00 && Cmd[12] == 0x3A)
            {
                Cmd[11] = 0x03;
                Cmd[12] = 0x0A;
            }
            else if (Cmd[11] == 0x01 && Cmd[12] == 0x3A)
            {
                Cmd[11] = 0x13;
                Cmd[12] = 0x0A;
            }
            else if (Cmd[11] == 0x02 && Cmd[12] == 0x3A)
            {
                Cmd[11] = 0x23;
                Cmd[12] = 0x0A;
            }
            string strtest = byteToHexStr(Cmd);
            return (Cmd);
        }
        #endregion AD采样

        #region 定时器
        /*//发送定时器
        public void SendTimer()
        {    
            
        }*/

        //定时发送命令
        public void SendCmdOnTime(object source, ElapsedEventArgs e)
        {
            try
            {
                foreach (DictionaryEntry de in htClient)
                {
                    DataItem dataitem = (DataItem)de.Value;
                    if (dataitem.isWaked == 1 && dataitem.intDeviceID != 0)
                    {
                        switch (dataitem.CmdNum)
                        {
                            case 0:
                                SendCmdSingle(cmdItem.CmdReadGpsTime, dataitem.byteDeviceID, dataitem.socket, dataitem.CmdbulkHex & 0x01);
                                break;
                            case 1:
                                byte[] cmdCapTime = cmdItem.CmdSetCapTime;
                                cmdCapTime[9] = (byte)dataitem.CapTimeHour;
                                cmdCapTime[10] = (byte)dataitem.CapTimeMinute;
                                SendCmdSingle(cmdCapTime, dataitem.byteDeviceID, dataitem.socket, dataitem.CmdbulkHex & 0x02);
                                break;
                            case 2:
                                byte[] cmdSetOpenAndCloseTime = cmdItem.CmdSetOpenAndCloseTime;
                                cmdSetOpenAndCloseTime[9] = (byte)(2 * dataitem.OpenTime >> 8);
                                cmdSetOpenAndCloseTime[10] = (byte)(2 * dataitem.OpenTime & 0xFF);
                                cmdSetOpenAndCloseTime[11] = (byte)(2 * dataitem.CloseTime >> 8);
                                cmdSetOpenAndCloseTime[12] = (byte)(2 * dataitem.CloseTime & 0xFF);
                                SendCmdSingle(cmdSetOpenAndCloseTime, dataitem.byteDeviceID, dataitem.socket, dataitem.CmdbulkHex & 0x04);
                                break;
                            /*case 2:
                                byte[] cmdSetOpenTime = cmdItem.CmdSetOpenTime;
                                cmdSetOpenTime[9] = (byte)(dataitem.OpenTime >> 8);
                                cmdSetOpenTime[10] = (byte)(dataitem.OpenTime & 0xFF);
                                SendCmdSingle(cmdSetOpenTime, dataitem.byteDeviceID, dataitem.socket, dataitem.CmdbulkHex & 0x04);
                                break;
                            case 3:
                                byte[] cmdSetCloseTime = cmdItem.CmdSetCloseTime;
                                cmdSetCloseTime[9] = (byte)(dataitem.CloseTime >> 8);
                                cmdSetCloseTime[10] = (byte)(dataitem.CloseTime & 0xFF);
                                SendCmdSingle(cmdSetCloseTime, dataitem.byteDeviceID, dataitem.socket, dataitem.CmdbulkHex & 0x08);
                                break;*/
                            //case 4:
                            //SendCmdSingle(cmdItem.CmdADNow, dataitem.byteDeviceID, dataitem.socket, dataitem.CmdbulkHex & 0x10);
                            //break;
                            default:
                                //
                                break;

                        }//end switch
                    }//end if
                }//End foreach
            }
            catch (Exception ex)
            {
                string error = DateTime.Now.ToString() + "出错信息：" + "---" + ex.Message + "\n";
                Log.Debug(error);
                System.Diagnostics.Debug.WriteLine(error);
            }

        }
        #endregion 定时器

    }
}