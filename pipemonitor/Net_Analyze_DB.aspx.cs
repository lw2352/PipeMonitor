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
    public partial class Net_Analyze_DB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //1.read data path
        //2.start analyze
        //3.store analyze result
        //4.get A[],B[],C[] and result(演示)

        public static Net_Analyze netAnalyze = new Net_Analyze();//实例化

        [WebMethod(Description = "读取设备ID对应的最后一次AD数据保存路径")]//add 3-28
        public static string readDataPath(int sensorintdeviceID)
        {
            MySQLDB.InitDb();
            string sensorid = "0";
            //从数据库中查找当前ID是否存在
            try
            {
                DataSet ds = new DataSet("tsensorad");
                string strSQL = "  SELECT intdeviceID FROM tsensorad where intdeviceID=" + sensorintdeviceID;
                ds = MySQLDB.SelectDataSet(strSQL, null);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        sensorid = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

            if (sensorid != "0")//若id存在，就找出最大dataID对应的路径
            {
                string strSQL1 = "";
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
                            //查询子表数据
                            try
                            {
                                DataSet ds2 = new DataSet("tsensorad1");
                                string strSQL2 = "  SELECT DataPath FROM "+ sensorid + " ORDER BY DataID DESC limit 1";
                                ds2 = MySQLDB.SelectDataSet(strSQL2, null);
                                if (ds2 != null)
                                {
                                    if (ds2.Tables[0].Rows.Count > 0)
                                    // 有数据集
                                    {
                                        string path = null;
                                        path = ds2.Tables[0].Rows[0][0].ToString();
                                        return path;
                                    }
                                    else return PublicMethod.OperationResultJson("207", "fail");
                                }
                                else return PublicMethod.OperationResultJson("207", "fail");
                            }
                            catch (Exception ex)
                            {
                                //return null;
                                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                            }
                        }
                        else return PublicMethod.OperationResultJson("207", "fail");
                    }
                    else return PublicMethod.OperationResultJson("207", "fail");
                    
                }
                catch (Exception ex)
                {
                    return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                }
            }
            else return PublicMethod.OperationResultJson("207", "fail");
        }

        [WebMethod(Description = "读取设备ID对应的最后一次AD数据保存时刻")]//add 3-28
        public static string readDataDate(int sensorintdeviceID)
        {
            MySQLDB.InitDb();
            string sensorid = "0";
            //从数据库中查找当前ID是否存在
            try
            {
                DataSet ds = new DataSet("tsensorad");
                string strSQL = "  SELECT intdeviceID FROM tsensorad where intdeviceID=" + sensorintdeviceID;
                ds = MySQLDB.SelectDataSet(strSQL, null);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        sensorid = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

            if (sensorid != "0")//若id存在，就找出最大dataID对应的路径
            {
                string strSQL1 = "";
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
                            //查询子表数据
                            try
                            {
                                DataSet ds2 = new DataSet("tsensorad1");
                                string strSQL2 = "  SELECT DataDate FROM " + sensorid + " ORDER BY DataID DESC limit 1";
                                ds2 = MySQLDB.SelectDataSet(strSQL2, null);
                                if (ds2 != null)
                                {
                                    if (ds2.Tables[0].Rows.Count > 0)
                                    // 有数据集
                                    {
                                        string date = null;
                                        date = ds2.Tables[0].Rows[0][0].ToString();
                                        return date;
                                    }
                                    else return PublicMethod.OperationResultJson("207", "fail");
                                }
                                else return PublicMethod.OperationResultJson("207", "fail");
                            }
                            catch (Exception ex)
                            {
                                //return null;
                                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                            }
                        }
                        else return PublicMethod.OperationResultJson("207", "fail");
                    }
                    else return PublicMethod.OperationResultJson("207", "fail");

                }
                catch (Exception ex)
                {
                    return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                }
            }
            else return PublicMethod.OperationResultJson("207", "fail");
        }

        [WebMethod(Description = "获取path对应的归一化DataA")]//add 3-28
        public static double[] readDataA(string path)
        { 
            double[] DataA = netAnalyze.getDataA(path);
            return DataA;
        }

        [WebMethod(Description = "获取path对应的归一化DataB")]//add 3-28
        public static double[] readDataB(string path)
        {
            double[] DataB = netAnalyze.getDataB(path);
            return DataB;
        }

        [WebMethod(Description = "获取经过算法分析后的DataC")]//add 3-28
        public static double[] readDataC(string path)
        {
            double[] DataC = netAnalyze.getAnalyzeDataC();
            return DataC;
        }

        [WebMethod(Description = "获取偏移值")]//add 3-28
        public static int readOffSet(string path)
        {
            int offset = netAnalyze.GetOffSet();
            return offset;
        }

        /*[WebMethod(Description = "AutoAnalyze")]//add 5-10
        public static int autoAnalyze(int idA, int idB)
        {
            //int offset = netAnalyze.AutoAnalyze(id);
            //return offset;
        }*/

    }//end of public partial class Net_Analyze_DB : System.Web.UI.Page
}//end of namespace pipemonitor