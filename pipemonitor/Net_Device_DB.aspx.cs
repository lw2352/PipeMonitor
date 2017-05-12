using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.Script.Services;


namespace pipemonitor
{
    public partial class Net_DB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static Net_Device netdevice = new Net_Device();//实例化

        //add 3-14
        [WebMethod(Description = "添加连接设备IP,Port,连接时刻,ID")]
        public static string addsensorinfo(int sensorintdeviceID, string sensorIP, string sensorPort, string sensorloginTime, int sensorStatus)
        {
            MySQLDB.InitDb();
            string sensorid = "0";
            //从数据库中查找当前ID是否存在
            try
            {
                DataSet ds1 = new DataSet("tsensorinfo");
                string strSQL1 = "SELECT intdeviceID FROM tsensorinfo where intdeviceID=" + sensorintdeviceID;
                ds1 = MySQLDB.SelectDataSet(strSQL1, null);
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        sensorid = ds1.Tables[0].Rows[0][0].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

            //************************************************************
            if (sensorid == "0")//若不存在,则添加
            {
                DataSet ds = new DataSet("dssensorinfo");
                string strResult = "";
                MySqlParameter[] parmss = null;
                string strSQL = "";
                bool IsDelSuccess = false;
                strSQL = " insert into tsensorinfo (intdeviceID,IP,Port,loginTime,Status) values" +
                    "(?sensorintdeviceID,?sensorIP,?sensorPort,?sensorloginTime,?sensorStatus);";

                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?sensorintdeviceID", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorIP", MySqlDbType.VarChar),
                                         new MySqlParameter("?sensorPort", MySqlDbType.VarChar),
                                         new MySqlParameter("?sensorloginTime", MySqlDbType.DateTime),
                                         new MySqlParameter("?sensorStatus", MySqlDbType.Int32)
                                     };
                parmss[0].Value = sensorintdeviceID;
                parmss[1].Value = sensorIP;
                parmss[2].Value = sensorPort;
                parmss[3].Value = Convert.ToDateTime(sensorloginTime);
                parmss[4].Value = sensorStatus;

                try
                {
                    IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                    if (IsDelSuccess != false)
                    {
                        return PublicMethod.OperationResultJson("0", "ok");
                    }
                    else
                    {
                        return PublicMethod.OperationResultJson("207", "fail");
                    }
                }

                catch (Exception ex)
                {
                    return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                }
            }

            else//若ID存在,就更新update
            {
                DataSet ds = new DataSet("dssensorinfo");
                string strResult = "";
                MySqlParameter[] parmss = null;
                string strSQL = "";
                bool IsDelSuccess = false;
                strSQL = "Update tsensorinfo SET IP=?sensorIP,Port=?sensorPort,loginTime=?sensorloginTime,Status=?sensorStatus WHERE intdeviceID=?sensorintdeviceID";

                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?sensorintdeviceID", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorIP", MySqlDbType.VarChar),
                                         new MySqlParameter("?sensorPort", MySqlDbType.VarChar),
                                         new MySqlParameter("?sensorloginTime", MySqlDbType.DateTime),
                                         new MySqlParameter("?sensorStatus", MySqlDbType.Int32)
                                     };
                parmss[0].Value = sensorintdeviceID;
                parmss[1].Value = sensorIP;
                parmss[2].Value = sensorPort;
                parmss[3].Value = Convert.ToDateTime(sensorloginTime);
                parmss[4].Value = sensorStatus;

                try
                {
                    IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                    if (IsDelSuccess != false)
                    {
                        return PublicMethod.OperationResultJson("0", "ok");
                    }
                    else
                    {
                        return PublicMethod.OperationResultJson("207", "fail");
                    }
                }

                catch (Exception ex)
                {
                    return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                }
            }

        }

        //add 3-15
        [WebMethod(Description = "添加设备AD采样数据时间和路径")]
        public static string addsensorad(int sensorintdeviceID, string sensorDataDate, string sensorDataPath)
        {
            MySQLDB.InitDb();
            string sensorid = "0";
            //从数据库中查找当前ID是否存在
            try
            {
                DataSet ds1 = new DataSet("tsensorad");
                string strSQL1 = "  SELECT intdeviceID FROM tsensorad where intdeviceID=" + sensorintdeviceID;
                ds1 = MySQLDB.SelectDataSet(strSQL1, null);
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        sensorid = ds1.Tables[0].Rows[0][0].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

            //************************************************************
            if (sensorid == "0")//若不存在,则添加,创建子表并新增数据
            {
                //DataSet ds = new DataSet("dssensorad");
                //string strResult = "";
                MySqlParameter[] parmss = null;
                string strSQL = "";
                //string strSQL2 = "";
                bool IsDelSuccess = false;
                //先在母表中插入ID和字表名
                string childName = "tsensoradchild" + sensorintdeviceID.ToString();
                strSQL = "insert into tsensorad (intdeviceID,ChildTable) values (?sensorintdeviceID , ?sensorChildTable);";
                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?sensorintdeviceID", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorChildTable", MySqlDbType.VarChar)
                                     };
                parmss[0].Value = sensorintdeviceID;
                parmss[1].Value = childName;

                try
                {
                    IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                    if (IsDelSuccess != false)
                    {
                        creatNewTable(childName);//创建子表
                        insertADData(childName, sensorDataDate, sensorDataPath);

                        return PublicMethod.OperationResultJson("0", "ok");
                    }
                    else
                    {
                        return PublicMethod.OperationResultJson("207", "fail");
                    }
                }
                catch (Exception ex)
                {
                    return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                }   
            }

            else//若ID存在,就查找子表,在子表中新增数据
            {
                DataSet ds = new DataSet("dssensorinfo");
                //MySqlParameter[] parmss = null;
                string strSQL1 = "";
                //bool IsDelSuccess = false;
                //查找子表
                try
                {
                    DataSet ds1 = new DataSet("tsensorad");
                    strSQL1 = "  SELECT ChildTable FROM tsensorad where intdeviceID=" + sensorintdeviceID;
                    ds1 = MySQLDB.SelectDataSet(strSQL1, null);
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        // 有数据集
                        {
                            sensorid = ds1.Tables[0].Rows[0][0].ToString();
                            //向子表插入数据
                            insertADData(sensorid, sensorDataDate, sensorDataPath);                         
                        }
                    }
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                catch (Exception ex)
                {
                    return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                }
            }
        }


        //add 3-15
        [WebMethod(Description = "前台添加设备命令数和命令参数")]
        public static string addsensorcfg(int sensorintdeviceID, int sensorCmdNumHex, int sensorCapTimeHour, int sensorCapTimeMinute, int sensorOpenTime, int sensorCloseTime)//sensorcfg可由多个参数组成,后台再解析
        {
            MySQLDB.InitDb();
            string sensorid = "0";
            //从数据库中查找当前ID是否存在
            try
            {
                DataSet ds1 = new DataSet("tsensorcfg");
                string strSQL1 = "  SELECT intdeviceID FROM tsensorcfg where intdeviceID=" + sensorintdeviceID;
                ds1 = MySQLDB.SelectDataSet(strSQL1, null);
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        sensorid = ds1.Tables[0].Rows[0][0].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

            //************************************************************
            if (sensorid == "0")//若不存在,则添加
            {
                DataSet ds = new DataSet("dssensorcfg");
                string strResult = "";
                MySqlParameter[] parmss = null;
                string strSQL = "";
                bool IsDelSuccess = false;
                strSQL = " insert into tsensorcfg (intdeviceID, CmdNumHex, CapTimeHour,CapTimeMinute,OpenTime,CloseTime) values" +
                    "(?sensorintdeviceID, ?sensorCmdNumHex, ?sensorCapTimeHour, ?sensorCapTimeMinute, ?sensorOpenTime, ?sensorCloseTime);";

                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?sensorintdeviceID", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorCmdNumHex", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorCapTimeHour", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorCapTimeMinute", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorOpenTime", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorCloseTime", MySqlDbType.Int32),
                                     };
                parmss[0].Value = sensorintdeviceID;
                parmss[1].Value = sensorCmdNumHex;
                parmss[2].Value = sensorCapTimeHour;
                parmss[3].Value = sensorCapTimeMinute;
                parmss[4].Value = sensorOpenTime;
                parmss[5].Value = sensorCloseTime;

                try
                {
                    IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                    if (IsDelSuccess != false)
                    {
                        return PublicMethod.OperationResultJson("0", "ok");
                    }
                    else
                    {
                        return PublicMethod.OperationResultJson("207", "fail");
                    }
                }

                catch (Exception ex)
                {
                    return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                }
            }

            else//若ID存在,就更新update
            {
                DataSet ds = new DataSet("dssensorcfg");
                string strResult = "";
                MySqlParameter[] parmss = null;
                string strSQL = "";
                bool IsDelSuccess = false;
                strSQL = "Update tsensorcfg SET CmdNumHex=?sensorCmdNumHex, CapTimeHour =?sensorCapTimeHour ,CapTimeMinute = ?sensorCapTimeMinute,OpenTime = ?sensorOpenTime,CloseTime = ?sensorCloseTime WHERE intdeviceID=?sensorintdeviceID";

                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?sensorintdeviceID", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorCmdNumHex", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorCapTimeHour", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorCapTimeMinute", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorOpenTime", MySqlDbType.Int32),
                                         new MySqlParameter("?sensorCloseTime", MySqlDbType.Int32),
                                     };
                parmss[0].Value = sensorintdeviceID;
                parmss[1].Value = sensorCmdNumHex;
                parmss[2].Value = sensorCapTimeHour;
                parmss[3].Value = sensorCapTimeMinute;
                parmss[4].Value = sensorOpenTime;
                parmss[5].Value = sensorCloseTime;

                try
                {
                    IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                    if (IsDelSuccess != false)
                    {
                        return PublicMethod.OperationResultJson("0", "ok");
                    }
                    else
                    {
                        return PublicMethod.OperationResultJson("207", "fail");
                    }
                }

                catch (Exception ex)
                {
                    return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                }
            }
        }

        //读取后台添加设备命令数和命令参数 add 3-17//3-18改成多列
        [WebMethod(Description = "读取后台添加设备命令数和命令参数")]
        public static int[] readsensorcfg(int sensorintdeviceID)
        {
            MySQLDB.InitDb();
            int[] sensorcfg = new int[5];
            int CmdNumHex = 0;
            int CapTimeHour = 0;
            int CapTimeMinute = 0;
            int OpenTime = 0;
            int CloseTime = 0;
            //从数据库中查找当前ID是否存在
            try
            {
                DataSet ds1 = new DataSet("tsensorcfg");
                string strSQL1 = "  SELECT CmdNumHex, CapTimeHour, CapTimeMinute, OpenTime, CloseTime FROM tsensorcfg where intdeviceID=" + sensorintdeviceID;
                ds1 = MySQLDB.SelectDataSet(strSQL1, null);
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        CmdNumHex = (int) ds1.Tables[0].Rows[0][0];
                        CapTimeHour = (int) ds1.Tables[0].Rows[0][1];
                        CapTimeMinute = (int) ds1.Tables[0].Rows[0][2];
                        OpenTime = (int) ds1.Tables[0].Rows[0][3];
                        CloseTime = (int) ds1.Tables[0].Rows[0][4];

                        sensorcfg[0] = CmdNumHex;
                        sensorcfg[1] = CapTimeHour;
                        sensorcfg[2] = CapTimeMinute;
                        sensorcfg[3] = OpenTime;
                        sensorcfg[4] = CloseTime;

                        return sensorcfg;
                    }
                    else return null;
                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
                //return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }
        }

        //创建新表 add 3-17
        [WebMethod(Description = "创建新表")]
        public static string creatNewTable(string childName)
        {
            MySQLDB.InitDb();
            MySqlParameter[] parmss = null;
            string strSQL = "";
            bool IsDelSuccess = false;
            strSQL = "CREATE TABLE dbvpipe."+ childName + "(DataID INT AUTO_INCREMENT, DataDate VARCHAR(45), DataPath VARCHAR(45), PRIMARY KEY (`DataID`));";//建立新表
            /*parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?sensorChildTable", MySqlDbType.VarChar)
                                     };
            parmss[0].Value = childName;*/
            try
            {
                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                if (IsDelSuccess != false)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("207", "fail");
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }
        }

        //插入AD数据信息 add 3-17
        [WebMethod(Description = "插入AD数据信息")]
        public static string insertADData(string tableName, string sensorDataDate, string sensorDataPath)
        {
            MySQLDB.InitDb();
            MySqlParameter[] parmss = null;
            string strSQL = "";
            bool IsDelSuccess = false;
            //添加数据
            strSQL = " insert into " +tableName+" (DataDate,DataPath) values" +
                "(?sensorDataDate,?sensorDataPath);";

            parmss = new MySqlParameter[]
                                 {
                                     //new MySqlParameter("?tableName", MySqlDbType.VarChar),
                                     new MySqlParameter("?sensorDataDate", MySqlDbType.DateTime),
                                     new MySqlParameter("?sensorDataPath", MySqlDbType.VarChar)
                                 };
            //parmss[0].Value = tableName;
            parmss[0].Value = Convert.ToDateTime(sensorDataDate);
            parmss[1].Value = sensorDataPath;
            try
            {
                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                if (IsDelSuccess != false)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("207", "fail");
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }
        }

        //立即采样（前台演示）add 3-27
        [WebMethod(Description = "立即采样（前台演示）")]
        public static string getADNow(string sensorintdeviceID)
        {
            int sensorintdeviceID_ = Int32.Parse(sensorintdeviceID);
            string i = netdevice.GetADNow(sensorintdeviceID_);
            return i;
            //string i = netdevice.GetADNow(sensorintdeviceID);
            //return i;   OK or Fail
            //OK--开始采样
            //Fail--发送立即采样命令失败
        }

        //获取上传进度（前台演示）add 3-29
        [WebMethod(Description = "获取上传数据的进度（前台演示）")]
        public static string getUploadStatus(string sensorintdeviceID)
        {
            int sensorintdeviceID_ = Int32.Parse(sensorintdeviceID);
            string i = (netdevice.GetUploadStatus(sensorintdeviceID_) / 6.0).ToString();
            return i;
        }

    }
}