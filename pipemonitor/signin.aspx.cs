using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;
public partial class signin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Session.Remove("UserName");
            Session.Remove("UserID");
            
        }
    }

    [WebMethod(Description = "用户登录")]
    public static string userLogin(string loginname, string password)
    {
        DataSet ds = new DataSet();
        MySQLDB.InitDb();

        string strSQL =
            " select u.UserID, u.*,a.AreaName,g.* " +
            " from tuser u,tarea a ,tgroup g " +
            " where u.AreaID=a.areaid " +
            " and u.GroupID=g.GroupID " +
            " and  u.UserName=?UserName ";


        MySqlParameter[] parms = new MySqlParameter[]
                                {
                                  new MySqlParameter("?UserName",MySqlDbType.VarChar)
                                };

        try
        {
            parms[0].Value = loginname.ToLower();//全部小写

            ds = MySQLDB.SelectDataSet(strSQL, parms);
            if (ds.Tables[0].Rows.Count == 0)  //用户名不存在
                return PublicMethod.getResultJson(ErrorCodeDefinition.USER_NOT_EXISTS, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.USER_NOT_EXISTS));


            else //做下一步的处理,检查密码是否正确
            {
                string pwd = "";
                pwd = PublicMethod.GetMD5Hash(password);

                string strDBpwd = ds.Tables[0].Rows[0]["Password"].ToString();
                if (strDBpwd.Equals(pwd))
                {
                    string strResult = "";

                    HttpContext.Current.Session["UserName"] = loginname;
                    HttpContext.Current.Session["UserID"] = ds.Tables[0].Rows[0]["UserID"].ToString();
                    HttpContext.Current.Session["AreaID"] = ds.Tables[0].Rows[0]["AreaID"].ToString();
                    return PublicMethod.DataTableToJson(ds.Tables[0]);

                }
                else
                {
                    return PublicMethod.getResultJson(ErrorCodeDefinition.USER_PASSWORD_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.USER_PASSWORD_ERROR));
                }
            }
        }
        catch (Exception ex)
        {
            return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
        }

    }

}