using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;

namespace pipemonitor
{
    public partial class PropertyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {
                    Server.Transfer("signin.aspx");
                }




            }
        }

        [WebMethod(Description = "获取传感器地理位置")]
        public static string GetSensorPos(string SensorID)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dspipe");

            string strSQL = "select w.SencondHead,p.StartLocation,p.EndLocation from tsensor w,tpipe p where w.SensorID=" + SensorID + " and w.PipeID=p.PipeID";

            try
            {
                ds = MySQLDB.SelectDataSet(strSQL, null);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        return PublicMethod.DataTableToJson(ds.Tables[0]);

                    }
                    else
                    {
                        return PublicMethod.OperationResultJson("99", "数据为空");
                    }; ;
                }
                else
                {
                    return PublicMethod.OperationResultJson("99", "数据为空");
                }

            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }

        [WebMethod(Description = "获取水表地理位置")]
        public static string GetWatermeterPos(string WatermeterID)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dspipe");

            string strSQL = "select w.Position,p.StartLocation,p.EndLocation from twatermeter w,tpipe p where w.WaterMeterID=" + WatermeterID + " and w.PipeID=p.PipeID";

            try
            {
                ds = MySQLDB.SelectDataSet(strSQL, null);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        return PublicMethod.DataTableToJson(ds.Tables[0]);

                    }
                    else
                    {
                        return PublicMethod.OperationResultJson("99", "数据为空");
                    }; ;
                }
                else
                {
                    return PublicMethod.OperationResultJson("99", "数据为空");
                }

            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }

    }
}