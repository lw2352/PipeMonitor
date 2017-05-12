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
    public partial class WebMethods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod(Description = "修改用户的密码，返回是否修改成功的代码")]
        public static string modifyUserPassword(string oldpassword, string newpassword)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet();
            string userid = HttpContext.Current.Session["UserID"].ToString();
            //if (CheckPhoneUser(userid) == false) //如果检查用户不合法，则返回
            //    return;

            string strSQL = "select * from tuser Where UserID=?UserID";


            MySqlParameter[] parms = new MySqlParameter[]
                            {
                            new MySqlParameter("?UserID",MySqlDbType.Int32)
                            };



            try
            {
                parms[0].Value = Convert.ToInt32(userid);
                ds = MySQLDB.SelectDataSet(strSQL, parms);
                if (ds.Tables[0].Rows.Count == 0)
                    return PublicMethod.getResultJson(ErrorCodeDefinition.USER_NOT_EXISTS, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.USER_NOT_EXISTS));//数据库异常

                else //做下一步的处理,检查密码是否正确
                {
                    string pwd = "";

                    pwd = PublicMethod.GetMD5Hash(oldpassword);

                    string strDBpwd = ds.Tables[0].Rows[0]["Password"].ToString();
                    if (strDBpwd.Equals(pwd))  //在密码正确的情况下
                    {

                        string strResult = "";

                        MySqlParameter[] parmsupdate = new MySqlParameter[]
                                                               {
                                                                   new MySqlParameter("?inUserID", MySqlDbType.Int32)
                                                                   ,
                                                                   new MySqlParameter("?inNewPassword",
                                                                                      MySqlDbType.VarChar)
                                                               };


                        parmsupdate[0].Value = Convert.ToInt32(userid);
                        parmsupdate[1].Value = PublicMethod.GetMD5Hash(newpassword);
                        ;

                        //输出参数
                        MySqlParameter outPara = new MySqlParameter("?outUpdateUserPassword", MySqlDbType.Int32);
                        outPara.Direction = ParameterDirection.Output;


                        strResult = MySQLDB.ExecuteStoredProc("pupdateUserPassword", parmsupdate, outPara);
                        if (strResult.Equals("-1"))
                        {
                            return PublicMethod.getResultJson(ErrorCodeDefinition.USER_MODIFYPASSWORDFAIL, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.USER_MODIFYPASSWORDFAIL));//数据库异常
                        }
                        else
                        {
                            return PublicMethod.OperationResultJson("0", "ok");

                        }

                    }

                    else //输入密码有误
                    {
                        return PublicMethod.getResultJson(ErrorCodeDefinition.USER_OLDPASSWORDISNOTCORRECT, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.USER_OLDPASSWORDISNOTCORRECT));//数据库异常
                    }
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }

        [WebMethod(Description = "用户登录")]
        public static string userLogin(string loginname, string password)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();

            string strSQL =
                " select u.*,a.AreaName,g.* " +
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

        #region 区域相关的方法
        [WebMethod(Description = "获取区域列表")]
        public static string getAllArea()
        {
            try
            {
                MySQLDB.InitDb();
                DataSet ds = new DataSet("tCity");
                string strSQL = " Select * From tarea";



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

        [WebMethod(Description = "通过区域ID，获取区域详细信息")]
        public static string getAreaByAreaID(string areaid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipe");
                //string strSQL = " Select p.* ,pl.* "+
                //                " From tPipe p,tpipelocation pl "+
                //                   " where p.PipeID=pl.PipeID "+
                //                    " and p.AreaID=?areaid ";
                string strSQL = " Select a.*  " +
                                " From tArea a  " +
                                " where a.AreaID=?areaid ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid", MySqlDbType.Int32)
                    };

                parms[0].Value = areaid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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


        [WebMethod(Description = "添加区域")]
        public static string addArea(string areaname, string parentareaid, string location)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dsarea");
            string strResult = "";


            DataSet dsarea = new DataSet("tarea");
            string strAreaSQL = "  select PreStr from tarea where areaid=?areaid  ";

            MySqlParameter[] parmsarea = null;
            parmsarea = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid", MySqlDbType.Int32)
                    };

            parmsarea[0].Value = parentareaid;

            dsarea = MySQLDB.SelectDataSet(strAreaSQL, parmsarea);

            string PreStr = "";
            if (dsarea != null)
            {
                if (dsarea.Tables[0].Rows.Count > 0)
                // 有数据集
                {
                    PreStr = dsarea.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    PreStr = "";
                }
            }



            MySqlParameter[] parms = null;
            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inAreaName", MySqlDbType.Text),
                        new MySqlParameter("?inParentAreaID", MySqlDbType.VarChar),
                        new MySqlParameter("?inLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inPreStr", MySqlDbType.VarChar)
                    };
            parms[0].Value = areaname;
            parms[1].Value = Convert.ToInt32(parentareaid);
            parms[2].Value = location;
            parms[3].Value = PreStr;

            MySqlParameter outPara = new MySqlParameter("?outAreaID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddAreaNew", parms, outPara);
                //Context.Response.Write(strResult);
                if (Convert.ToInt32(strResult) > 0)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("207", "新增区域失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }

        [WebMethod(Description = "修改区域")]
        public static string updateAreaByAreaID(string areaid, string areaname, string location)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();
            string strResult = "";
            bool IsDelSuccess = false;
            string strSQL = "";
            MySqlParameter[] parmss = null;

            try
            {

                strSQL = " Update tarea set AreaName =?AreaName,Location=?Location  WHERE AreaID=?AreaID;";


                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?AreaName", MySqlDbType.VarChar),
                                         new MySqlParameter("?Location", MySqlDbType.VarChar),
                                         new MySqlParameter("?AreaID", MySqlDbType.Int32)
                                     };
                parmss[0].Value = areaname;
                parmss[1].Value = location;
                parmss[2].Value = Convert.ToInt32(areaid);

                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);
                return PublicMethod.OperationResultJson("0", "ok");

            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }


        }


        //删除区域
        [WebMethod(Description = "删除区域")]
        public static string deleteAreaByAreaID(string areaid)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();

            string strResult = "";
            MySqlParameter[] parms = new MySqlParameter[]
                            {
                            new MySqlParameter("?inareid",MySqlDbType.VarChar)
                            };

            parms[0].Value = areaid;
            //输出参数
            MySqlParameter outPara = new MySqlParameter("?outpdeleteAreaByAreaID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;

            try
            {
                strResult = MySQLDB.ExecuteStoredProc("pdeleteAreaByAreaID", parms, outPara);
                //Context.Response.Write(strResult);
                if (strResult.Equals("1"))
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    //删除区域失败
                    return PublicMethod.OperationResultJson("206", "删除区域失败");
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }

        #endregion

        #region 管材相关的方法
        [WebMethod(Description = "获取管材列表")]
        public static string getAllPipeMaterial()
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipematerial");
                string strSQL = " Select * From tpipematerial where deleteflag=0; ";



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


        [WebMethod(Description = "添加管材")]
        public static string addPipeMaterial(string PipeMaterialName, string PipeMaterialParameter)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dsarea");
            string strResult = "";

            MySqlParameter[] parms = null;


            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inPipeMaterialName", MySqlDbType.VarChar),
                        new MySqlParameter("?inPipeMaterialParameter", MySqlDbType.VarChar)
                    };
            parms[0].Value = PipeMaterialName;
            parms[1].Value = Convert.ToDouble(PipeMaterialParameter);

            MySqlParameter outPara = new MySqlParameter("?outPipeMaterialID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddPipeMaterial", parms, outPara);
                if (Convert.ToInt32(strResult) > 0)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("208", "新增管线失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }



        //删除管材
        [WebMethod(Description = "删除管材")]
        public static string GetAllGroups()
        {
            string strSQL = " select * from tgroup";
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tgroup");
             
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
                    }; 
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


        //删除管材
        [WebMethod(Description = "删除管材")]
        public static string deletePipeMaterialByPipeMaterialID(string pipematerialid)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();

            bool IsDelSuccess = false;
            string strSQL = "";
            string strResult = "";
            MySqlParameter[] parmss = null;

            strSQL = " Update tpipematerial set deleteflag =1  WHERE pipematerialid=?pipematerialid;";

            try
            {
                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?pipematerialid", MySqlDbType.Int32),
                                     };
                parmss[0].Value = Convert.ToInt32(pipematerialid);

                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);
                if (IsDelSuccess)
                    return PublicMethod.OperationResultJson("0", "ok");
                else
                {

                    return PublicMethod.OperationResultJson("213", "删除管材失败");
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }

        //修改管材
        [WebMethod(Description = "修改管材")]
        public static string updatePipeMaterialByPipeMaterialID(string pipematerialid,string pipematerialname,string pipematerialparameter)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();

            bool IsDelSuccess = false;
            string strSQL = "";
            string strResult = "";
            MySqlParameter[] parmss = null;

            strSQL = " Update tpipematerial set pipematerialname =?pipematerialname,pipematerialparameter =?pipematerialparameter  " +
                      " WHERE pipematerialid=?pipematerialid;";

            try
            {
                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?pipematerialname", MySqlDbType.VarChar),
                                         new MySqlParameter("?pipematerialparameter", MySqlDbType.VarChar),
                                         new MySqlParameter("?pipematerialid", MySqlDbType.Int32),
                                     };
                parmss[0].Value = pipematerialname;
                parmss[1].Value = pipematerialparameter;
                parmss[2].Value = Convert.ToInt32(pipematerialid);

                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                if (IsDelSuccess)
                    return PublicMethod.OperationResultJson("0", "ok");
                else
                {

                    return PublicMethod.OperationResultJson("211", "更新管材失败");
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }
        #endregion

        #region 管线相关的方法

        [WebMethod(Description = "添加管线")]
        public static string addPipe( string AreaID,  string StartLocation, string EndLocation,  string PrePipeID)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dspipe");
            string strResult = "";

        //    strResult = MySQLDB.ExecuteStoredProc("paddPipe", parms, outPara);

          MySqlParameter[] parms = null;

            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inPipeName", MySqlDbType.VarChar),
                        new MySqlParameter("?inPipeNo", MySqlDbType.VarChar),
                        new MySqlParameter("?inAreaID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeMaterialID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeSize", MySqlDbType.Float),
                        new MySqlParameter("?inPipeDepth", MySqlDbType.Float),
                        new MySqlParameter("?inPipeLength", MySqlDbType.Float),
                        new MySqlParameter("?inStartLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inEndLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdjoinPipeIDs", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdjoinPipeNames", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdminiUserID", MySqlDbType.Int32),
                        new MySqlParameter("?inRemark", MySqlDbType.VarChar),
                        new MySqlParameter("?inPrePipeID", MySqlDbType.Int32)
                    };
            parms[0].Value = " ";
            parms[1].Value = " ";
            parms[2].Value = Convert.ToInt32(AreaID);
            parms[3].Value = -1; ;
            parms[4].Value = 0; ;
            parms[5].Value = 0; ;
            parms[6].Value = 0;
            parms[7].Value = StartLocation;
            parms[8].Value = EndLocation;
            parms[9].Value = " ";
            parms[10].Value = " ";
            parms[11].Value = -1;
            parms[12].Value = " ";
            parms[13].Value = Convert.ToInt32(PrePipeID);

            MySqlParameter outPara = new MySqlParameter("?outPipeID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddPipe", parms, outPara);
                if (Convert.ToInt32(strResult) > 0)
                {
                    //return PublicMethod.OperationResultJson("0", "ok");
                    string strSQL = " update tpipe set PipeName='" + "管道" + outPara.Value.ToString() + "' where PipeID=" + outPara.Value.ToString();
                    MySQLDB.ExecuteNonQry(strSQL, null);
                    return PublicMethod.DataTableJsonOnlyOneColumn("NewPipeID", strResult);
                    //return strResult;
                }
                else
                {
                    return PublicMethod.OperationResultJson("209", "新增管线失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }



        [WebMethod(Description = "添加管线")]
        public static string addNextPipe(string AreaID, string StartNode, string EndLocation)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dspipe");
            string strResult = "";

            //    strResult = MySQLDB.ExecuteStoredProc("paddPipe", parms, outPara);



            MySqlParameter[] parms = null;




            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inPipeName", MySqlDbType.VarChar),
                        new MySqlParameter("?inPipeNo", MySqlDbType.VarChar),
                        new MySqlParameter("?inAreaID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeMaterialID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeSize", MySqlDbType.Float),
                        new MySqlParameter("?inPipeDepth", MySqlDbType.Float),
                        new MySqlParameter("?inPipeLength", MySqlDbType.Float),
                        new MySqlParameter("?inStartNode", MySqlDbType.Int32),
                        new MySqlParameter("?inEndLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdjoinPipeIDs", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdjoinPipeNames", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdminiUserID", MySqlDbType.Int32),
                        new MySqlParameter("?inRemark", MySqlDbType.VarChar),
                        new MySqlParameter("?inPrePipeID", MySqlDbType.Int32)
                    };
            parms[0].Value = " ";
            parms[1].Value = " ";
            parms[2].Value = Convert.ToInt32(AreaID);
            parms[3].Value = -1; ;
            parms[4].Value = 0; ;
            parms[5].Value = 0; ;
            parms[6].Value = 0;
            parms[7].Value = StartNode;
            parms[8].Value = EndLocation;
            parms[9].Value = " ";
            parms[10].Value = " ";
            parms[11].Value = -1;
            parms[12].Value = " ";
            parms[13].Value = 0;

            MySqlParameter outPara = new MySqlParameter("?outPipeID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddNextPipe", parms, outPara);
                if (Convert.ToInt32(strResult) > 0)
                {
                    //return PublicMethod.OperationResultJson("0", "ok");
                    string strSQL = " update tpipe set PipeName='" + "管道" + outPara.Value.ToString() + "' where PipeID=" + outPara.Value.ToString();
                    MySQLDB.ExecuteNonQry(strSQL, null);
                    return PublicMethod.DataTableJsonOnlyOneColumn("NewPipeID", strResult);
                    //return strResult;
                }
                else
                {
                    return PublicMethod.OperationResultJson("209", "新增管线失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }
        
        
        /*
        [WebMethod(Description = "添加管线")]
        public static string addPipe(string PipeName, string PipeNo,
            string AreaID ,string PipeMaterialID ,string PipeSize ,string PipeDepth , string PipeLength ,
            string StartLocation ,string EndLocation ,string AdjoinPipeIDs , string AdjoinPipeNames ,string AdminiUserID ,string Remark,string PrePipeID )
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dspipe");
            string strResult = "";

            MySqlParameter[] parms = null;

            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inPipeName", MySqlDbType.VarChar),
                        new MySqlParameter("?inPipeNo", MySqlDbType.VarChar),
                        new MySqlParameter("?inAreaID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeMaterialID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeSize", MySqlDbType.Float),
                        new MySqlParameter("?inPipeDepth", MySqlDbType.Float),
                        new MySqlParameter("?inPipeLength", MySqlDbType.Float),
                        new MySqlParameter("?inStartLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inEndLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdjoinPipeIDs", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdjoinPipeNames", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdminiUserID", MySqlDbType.Int32),
                        new MySqlParameter("?inRemark", MySqlDbType.VarChar),
                        new MySqlParameter("?inPrePipeID", MySqlDbType.Int32)
                    };
            parms[0].Value = PipeName;
            parms[1].Value = PipeNo;
            parms[2].Value = Convert.ToInt32(AreaID);
            parms[3].Value = Convert.ToInt32(PipeMaterialID); ;
            parms[4].Value = Convert.ToDouble(PipeSize); ;
            parms[5].Value = Convert.ToDouble(PipeDepth); ;
            parms[6].Value = Convert.ToDouble(PipeLength);
            parms[7].Value = StartLocation;
            parms[8].Value = EndLocation;
            parms[9].Value = AdjoinPipeIDs;
            parms[10].Value = AdjoinPipeNames;
            parms[11].Value = AdminiUserID;
            parms[12].Value = Remark;
            parms[13].Value = Convert.ToInt32(PrePipeID);

            MySqlParameter outPara = new MySqlParameter("?outPipeID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddPipe", parms, outPara);
                if (Convert.ToInt32(strResult) > 0)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("209", "新增管线失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }
      
        */

        [WebMethod(Description = "通过管线ID，获取管线信息")]
        public static string getPipeByPipeID(string pipeid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipe");
                string strSQL = " Select * From tPipe where PipeID=?PipeID; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?PipeID", MySqlDbType.VarChar)
                    };

                parms[0].Value = pipeid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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


        [WebMethod(Description = "通过管材ID，获取管材信息")]
        public static string getPipeMaterialByPipeMaterialID(string pipeMaterialid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipematerial");
                string strSQL = "SELECT PipeMaterialName FROM tpipematerial WHERE tpipematerial.PipeMaterialID = ?PipeMaterialID AND DeleteFlag = 0; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?PipeMaterialID", MySqlDbType.VarChar)
                    };

                parms[0].Value = pipeMaterialid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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

        [WebMethod(Description = "通过区域ID，获取区域信息")]
        public static string getAreaNameByAreaID(string areaid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("areaid");
                string strSQL = "SELECT AreaName FROM tarea WHERE AreaID = ?AreaID; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?AreaID", MySqlDbType.VarChar)
                    };

                parms[0].Value = areaid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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

        [WebMethod(Description = "通过区域ID，获取管线信息,-1表示所有区域")]
        public static string getPipeByAreaID(string areaid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipe");
                //string strSQL = " Select p.* ,pl.* "+
                //                " From tPipe p,tpipelocation pl "+
                //                   " where p.PipeID=pl.PipeID "+
                //                    " and p.AreaID=?areaid ";
                string strSQL = " Select p.* ,u.UserName,m.PipeMaterialName " +
                                 " From tPipe p  " +
                                 "left join tuser u on p.AdminUserID=u.UserID left join tpipematerial m on p.PipeMaterialID=m.PipeMaterialID where p.AreaID=?areaid and p.deleteflag=0  ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid", MySqlDbType.Int32)
                    };

                parms[0].Value = areaid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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

        [WebMethod(Description = "通过传感器ID，获取管线ID及上根管线ID")]
        public static string getPipeBySensorID(string sensorid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipe");
                string strSQL =  "SELECT tsensor.PipeID,tpipe.PrePipeID FROM tsensor INNER JOIN tpipe ON"+
                            " tsensor.PipeID = tpipe.PipeID WHERE tsensor.SensorID = ?sensorid AND tsensor.DeleteFlag = 0";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?sensorid", MySqlDbType.Int32)
                    };

                parms[0].Value = sensorid;

                string json = "";

                ds = MySQLDB.SelectDataSet(strSQL, parms);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        json = PublicMethod.TableToJson(ds.Tables[0]);

                        return json;
                    }
                    else
                    {
                        return PublicMethod.OperationResultJson("99", "数据为空");
                    };
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

        [WebMethod(Description = "通过区域ID，(包括所有下级菜单)获取管线信息,-1表示所有区域")]
        public static string getPipeByAreaIDIncludeSubAreas(string areaid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet dsarea = new DataSet("tarea");
                DataSet ds = new DataSet("twatermeter");
                //string strAreaSQL = "  select GROUP_CONCAT(AreaID) AS AreaIDs  from tarea  where parentareaid=?parentareaid  ";
                string strAreaSQL = "  select PreStr from tarea where areaid=?areaid  ";

                MySqlParameter[] parmsarea = null;
                parmsarea = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid", MySqlDbType.Int32)
                    };

                parmsarea[0].Value = areaid;

                dsarea = MySQLDB.SelectDataSet(strAreaSQL, parmsarea);

                string PreStr = "";
                if (dsarea != null)
                {
                    if (dsarea.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        PreStr = dsarea.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        PreStr = "";
                    }
                }


                string strSQL = "select sub.*,u.UserName  from (Select p.* ,a.AreaName  " +
                    " From tpipe  p  ,tarea a " +
                   "  where p.AreaID=a.AreaID " +
                    " and a.PreStr like '"    + PreStr + "%'" + " and p.deleteflag=0) sub left join tuser u on sub.AdminUserID=u.UserID ";
             
                               

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
                    };
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


        [WebMethod(Description = "修改管线")]
        public static string updatePipeByPipeID(string PipeID, string PipeName, string PipeNo,
            string AreaID, string PipeMaterialID, string PipeSize, string PipeDepth, string PipeLength,
            string StartLocation, string EndLocation, string AdjoinPipeIDs, string AdjoinPipeNames, string AdminUserID, string Remark, string PrePipeID)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();
            string strResult = "";
            bool IsDelSuccess = false;
            string strSQL = "";
            MySqlParameter[] parms = null;

            try
            {

                strSQL = " Update tPipe set PipeName =?PipeName,PipeNo=?PipeNo,AreaID=?AreaID, PipeMaterialID=?PipeMaterialID, " +
                            " PipeSize=?PipeSize,PipeDepth=?PipeDepth,PipeLength=?PipeLength ," +
                             " AdjoinPipeIDs=?AdjoinPipeIDs, " +
                             " AdjoinPipeNames=?AdjoinPipeNames,AdminUserID=?AdminUserID,Remark=?Remark,PrePipeID=?PrePipeID  " +
                           " WHERE PipeID=?PipeID;";


                parms = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?PipeName", MySqlDbType.VarChar),
                                         new MySqlParameter("?PipeNo", MySqlDbType.VarChar),
                                         new MySqlParameter("?AreaID", MySqlDbType.Int32),
                                         new MySqlParameter("?PipeMaterialID", MySqlDbType.Int32),
                                         new MySqlParameter("?PipeSize", MySqlDbType.Float),
                                         new MySqlParameter("?PipeDepth", MySqlDbType.Float),
                                         new MySqlParameter("?PipeLength", MySqlDbType.Float),
                                         //new MySqlParameter("?StartLocation", MySqlDbType.VarChar),
                                         //new MySqlParameter("?EndLocation", MySqlDbType.VarChar),
                                         new MySqlParameter("?AdjoinPipeIDs", MySqlDbType.VarChar),
                                         new MySqlParameter("?AdjoinPipeNames", MySqlDbType.VarChar),
                                         new MySqlParameter("?AdminUserID", MySqlDbType.Int32),
                                         new MySqlParameter("?Remark", MySqlDbType.VarChar),
                                         new MySqlParameter("?PrePipeID", MySqlDbType.Int32),
                                         new MySqlParameter("?PipeID", MySqlDbType.Int32),

                                     };
                parms[0].Value = PipeName;
                parms[1].Value = PipeNo;
                parms[2].Value = Convert.ToInt32(AreaID);
                parms[3].Value = Convert.ToInt32(PipeMaterialID); ;
                parms[4].Value = Convert.ToDouble(PipeSize); ;
                parms[5].Value = Convert.ToDouble(PipeDepth); ;
                parms[6].Value = Convert.ToDouble(PipeLength);
                //parms[7].Value = StartLocation;
                //parms[8].Value = EndLocation;
                parms[7].Value = AdjoinPipeIDs;
                parms[8].Value = AdjoinPipeNames;
                parms[9].Value = AdminUserID;
                parms[10].Value = Remark;
                parms[11].Value = Convert.ToInt32(PrePipeID);
                parms[12].Value = Convert.ToInt32(PipeID);

                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parms);
                if (IsDelSuccess)
                    return PublicMethod.OperationResultJson("0", "ok");
                else
                {

                    return PublicMethod.OperationResultJson("210", "更新管线失败");
                }

            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }


        }

        [WebMethod(Description = "修改管线的位置")]
        public static string updatePipeByPipeIDForPosition(string PipeID,  string StartLocation, string EndLocation)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();
            string strResult = "";
            bool IsDelSuccess = false;
            string strSQL = "";
            MySqlParameter[] parms = null;

            try
            {
                strSQL = " Update tPipe set  StartLocation=?StartLocation,EndLocation=?EndLocation  " +
                           " WHERE PipeID=?PipeID;";


                parms = new MySqlParameter[]
                                     {                                        
                                         new MySqlParameter("?StartLocation", MySqlDbType.VarChar),
                                         new MySqlParameter("?EndLocation", MySqlDbType.VarChar),
                                         new MySqlParameter("?PipeID", MySqlDbType.Int32)

                                     };

                parms[0].Value = StartLocation;
                parms[1].Value = EndLocation;
                parms[2].Value = Convert.ToInt32(PipeID);

                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parms);
                if (IsDelSuccess)
                    return PublicMethod.OperationResultJson("0", "ok");
                else
                {

                    return PublicMethod.OperationResultJson("210", "更新管线失败");
                }

            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }


        }



       
        
        
        //删除管线
        [WebMethod(Description = "删除管线")]
        public static string deletePipeByPipeID(string pipeid)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();

            bool IsDelSuccess = false;
            string strSQL = "";
            string strResult = "";
            MySqlParameter[] parmss = null;

              strSQL = " Update tpipe set deleteflag =1  WHERE pipeid=?pipeid";
            string   strSQL2 = "Update tpipe set PrePipeID=0 where PrepipeID=?pipeid";
            try
            {


                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?pipeid", MySqlDbType.Int32),
                                     };
             
                parmss[0].Value = Convert.ToInt32(pipeid);
           

                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                if (IsDelSuccess)
                // return PublicMethod.OperationResultJson("0", "ok");
                {

                    IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL2, parmss);
                    if (IsDelSuccess)
                    {
                        return PublicMethod.OperationResultJson("0", "ok");
                    }
                    else
                    {
                        return PublicMethod.OperationResultJson("212", "删除管线失败");
                    }
                }
                else
                {

                    return PublicMethod.OperationResultJson("212", "删除管线失败");
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }

        #endregion 管线相关的方法



        [WebMethod(Description = "添加附属设备1 阀 2消防栓 3井")]
        public static string addDevice(string areaid, string deviceType,string location)
        {
            MySQLDB.InitDb();

            DataSet ds = new DataSet("dsdevice");
            string strResult = "";

            MySqlParameter[] parms = null;

            parms = new MySqlParameter[]
                    {
                   
                        new MySqlParameter("?inAreaID", MySqlDbType.Int32),
                        new MySqlParameter("?inLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inTypeFlag", MySqlDbType.Int32)
                    
                    };
            parms[0].Value = Convert.ToInt32(areaid);
            parms[1].Value = location;
            parms[2].Value = Convert.ToInt32(deviceType); 
          

            MySqlParameter outPara = new MySqlParameter("?outDeviceID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddDevice", parms, outPara);
                if (Convert.ToInt32(strResult) > 0)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("214", "新增设备失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }


        }





        [WebMethod(Description = "获取附属设备")]
        public static string getDevicesIncludeSubAreas(string areaid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet dsarea = new DataSet("tarea");
                DataSet ds = new DataSet("tdevice");
                //string strAreaSQL = "  select GROUP_CONCAT(AreaID) AS AreaIDs  from tarea  where parentareaid=?parentareaid  ";
                string strAreaSQL = "  select PreStr from tarea where areaid=?areaid  ";

                MySqlParameter[] parmsarea = null;
                parmsarea = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid", MySqlDbType.Int32)
                    };

                parmsarea[0].Value = areaid;

                dsarea = MySQLDB.SelectDataSet(strAreaSQL, parmsarea);

                string PreStr = "";
                if (dsarea != null)
                {
                    if (dsarea.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        PreStr = dsarea.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        PreStr = "";
                    }
                }


                string strSQL = "Select p.* ,a.AreaName  " +
                    " From tdevice  p  ,tarea a " +
                   "  where p.AreaID=a.AreaID " +
                    " and a.PreStr like '";
                strSQL = strSQL + PreStr + "%'" + " and p.DeleteFlag=0";

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

        [WebMethod(Description = "删除设备")]
        public static string deleteDevice(string deviceid)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();

            bool IsDelSuccess = false;
            string strSQL = "";
            string strResult = "";
            MySqlParameter[] parmss = null;

            strSQL = " update tdevice set DeleteFlag =1  WHERE DeviceID=?deviceid;";

            try
            {
                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?deviceid", MySqlDbType.Int32),
                                     };
                parmss[0].Value = Convert.ToInt32(deviceid);
                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                if (IsDelSuccess)
                    return PublicMethod.OperationResultJson("0", "ok");
                else
                {

                    return PublicMethod.OperationResultJson("216", "删除设备失败");
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }



        [WebMethod(Description = "修改设备信息")]
        public static string updateDevice(string deviceid, string devicename, string areaid,  string remark)
        
        {
            DataSet ds = new DataSet();

            MySQLDB.InitDb();
            
            bool IsDelSuccess = false;
            string strSQL = "";
            MySqlParameter[] parms = null;

            try
            {

                strSQL = " UPDATE tdevice " +
                          " SET DeviceName = ?DeviceName, " +
                            " Remark = ?Remark, " +
                            " AreaID = ?AreaID " +
                       
                            " WHERE DeviceID =?DeviceID;";



                parms = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?DeviceName", MySqlDbType.VarChar),
                                         new MySqlParameter("?Remark", MySqlDbType.VarChar),
                                         new MySqlParameter("?AreaID", MySqlDbType.Int32),
                                    
                                         new MySqlParameter("?DeviceID", MySqlDbType.Int32),

                                     };
                parms[0].Value = devicename;
                parms[1].Value = remark;
                parms[2].Value = Convert.ToInt32(areaid);
                parms[3].Value = Convert.ToInt32(deviceid); 
            
                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parms);
                if (IsDelSuccess)
                    return PublicMethod.OperationResultJson("0", "ok");
                else
                {

                    return PublicMethod.OperationResultJson("216", "更新设备失败");
                }

            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }


        }


        [WebMethod(Description = "获取设备信息")]
        public static string getDeviceInfo(string deviceid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tdevice");
                string strSQL = " Select * From tdevice where DeviceID=?deviceid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?deviceid", MySqlDbType.Int32)
                    };

                parms[0].Value = deviceid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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



        #region 水表相关的方法
           [WebMethod(Description = "获取区域内所有水表")]
        public static string GetWatermeters(string areaid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipe");
                //string strSQL = " Select p.* ,pl.* "+
                //                " From tPipe p,tpipelocation pl "+
                //                   " where p.PipeID=pl.PipeID "+
                //                    " and p.AreaID=?areaid ";
                string strSQL = " Select p.* ,u.UserName " +
                                 " From twatermeter p  " +
                                 " left join tuser u on p.AdminUserID=u.UserID where p.AreaID=?areaid and p.deleteflag=0  ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid", MySqlDbType.Int32)
                    };

                parms[0].Value = areaid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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


           [WebMethod(Description = "获取区域内所有水表,包括了下级的")]
           public static string GetWatermetersIncludeSubAreas(string areaid)
           {
               MySQLDB.InitDb();
               try
               {
                   DataSet dsarea = new DataSet("tarea");
                   DataSet ds = new DataSet("twatermeter");
                   //string strAreaSQL = "  select GROUP_CONCAT(AreaID) AS AreaIDs  from tarea  where parentareaid=?parentareaid  ";
                   string strAreaSQL = "  select PreStr from tarea where areaid=?areaid  ";

                   MySqlParameter[] parmsarea = null;
                   parmsarea = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid", MySqlDbType.Int32)
                    };

                   parmsarea[0].Value = areaid;

                   dsarea = MySQLDB.SelectDataSet(strAreaSQL, parmsarea);

                   string PreStr = "";
                   if (dsarea != null)
                   {
                       if (dsarea.Tables[0].Rows.Count > 0)
                       // 有数据集
                       {
                           PreStr = dsarea.Tables[0].Rows[0][0].ToString();
                       }
                       else
                       {
                           PreStr = "";
                       }
                   }
                  

                   string strSQL = "select sub.*,u.UserName  from (Select p.* ,a.AreaName  " +
                       " From twatermeter  p  ,tarea a " +
                      "  where p.AreaID=a.AreaID " +
                       " and a.PreStr like '";
                   strSQL = strSQL + PreStr + "%'" + " and p.deleteflag=0) sub " +
                                   " left join tuser u on sub.AdminUserID=u.UserID ";

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


        [WebMethod(Description = "添加水表")]
        public static string addWatermeter(string watermetername ,
                    string watermeterno,string address,
                    string watermetertype,
                    string watermeterlocation,
                    string fatherwatermeterid,
                    string areaid,string adminuserid,string pipeid,
                    string remark,string level,string position)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dspipe");
            string strResult = "";

            MySqlParameter[] parms = null;

            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inWaterMeterName", MySqlDbType.VarChar),
                        new MySqlParameter("?inWaterMeterNo", MySqlDbType.VarChar),
                        new MySqlParameter("?inAddress", MySqlDbType.VarChar),
                        new MySqlParameter("?inWaterMeterType", MySqlDbType.VarChar),
                        new MySqlParameter("?inWaterMeterLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inFatherWaterMeterID", MySqlDbType.Int32),
                        new MySqlParameter("?inAreaID", MySqlDbType.Int32),
                        new MySqlParameter("?inAdminUserID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeID", MySqlDbType.Int32),
                        new MySqlParameter("?inRemark", MySqlDbType.VarChar),
                        new MySqlParameter("?inLevel", MySqlDbType.Int32),
                         new MySqlParameter("?inPosition", MySqlDbType.Double)
                    };
            parms[0].Value = watermetername;
            parms[1].Value = watermeterno;
            parms[2].Value = address;
            parms[3].Value = watermetertype; ;
            parms[4].Value = watermeterlocation; ;
            parms[5].Value = Convert.ToInt32(fatherwatermeterid); ;
            parms[6].Value = Convert.ToInt32(areaid);
            parms[7].Value = Convert.ToInt32(adminuserid); 
            parms[8].Value = Convert.ToInt32(pipeid); 
            parms[9].Value = remark;
            parms[10].Value = Convert.ToInt32(level);
            parms[11].Value = Convert.ToDouble(position);

            MySqlParameter outPara = new MySqlParameter("?outWatermeterID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddWatermeter", parms, outPara);
                if (Convert.ToInt32(strResult) > 0)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("214", "新增水表失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }







        [WebMethod(Description = "添加水表")]
        public static string addWatermeterSimple(
                    string areaid,  string pipeid,
                   string position)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dspipe");
            string strResult = "";

            MySqlParameter[] parms = null;

            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inWaterMeterName", MySqlDbType.VarChar),
                        new MySqlParameter("?inWaterMeterNo", MySqlDbType.VarChar),
                        new MySqlParameter("?inAddress", MySqlDbType.VarChar),
                        new MySqlParameter("?inWaterMeterType", MySqlDbType.VarChar),
                        new MySqlParameter("?inWaterMeterLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inFatherWaterMeterID", MySqlDbType.Int32),
                        new MySqlParameter("?inAreaID", MySqlDbType.Int32),
                        new MySqlParameter("?inAdminUserID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeID", MySqlDbType.Int32),
                        new MySqlParameter("?inRemark", MySqlDbType.VarChar),
                        new MySqlParameter("?inLevel", MySqlDbType.Int32),
                         new MySqlParameter("?inPosition", MySqlDbType.Double)
                    };
            parms[0].Value = "";
            parms[1].Value = "";
            parms[2].Value = "";
            parms[3].Value = ""; ;
            parms[4].Value = ""; ;
            parms[5].Value = 0;
            parms[6].Value = Convert.ToInt32(areaid);
            parms[7].Value = 0;
            parms[8].Value = Convert.ToInt32(pipeid);
            parms[9].Value = "";
            parms[10].Value = 0;
            parms[11].Value = Convert.ToDouble(position);

            MySqlParameter outPara = new MySqlParameter("?outWatermeterID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddWatermeter", parms, outPara);
                if (Convert.ToInt32(strResult) > 0)
                {

                    string strSQL = " update twatermeter set WaterMeterName='" + "水表" + outPara.Value.ToString() + "' where WaterMeterID=" + outPara.Value.ToString();
                    MySQLDB.ExecuteNonQry(strSQL, null);

                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("214", "新增水表失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }



        [WebMethod(Description = "通过水表ID，获取水表信息")]
        public static string getWatermeterByWatermeterID(string watermeterid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipe");
                string strSQL = " Select * From twatermeter where watermeterid=?watermeterid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?watermeterid", MySqlDbType.VarChar)
                    };

                parms[0].Value = watermeterid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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

        [WebMethod(Description = "修改水表")]
        public static string updateWatermeterByWatermeterID(string watermeterid, string watermetername, string watermeterno,
          string watermetertype, string areaid,string adminuserid, string remark,string FatherWater,string Level)
            
        {
            DataSet ds = new DataSet();

            MySQLDB.InitDb();
            string strResult = "";
            bool IsDelSuccess = false;
            string strSQL = "";
            MySqlParameter[] parms = null;

            try
            {

                strSQL = " UPDATE twatermeter " +
                          " SET WaterMeterName = ?WaterMeterName, " +
                            " WaterMeterNo = ?WaterMeterNo, " +
                            " Address = ?Address, " +
                            " WaterMeterType = ?WaterMeterType, " +
                            " WaterMeterLocation = ?WaterMeterLocation, " +
                            " FatherWaterMeterID = ?FatherWaterMeterID, " +
                            " AreaID = ?AreaID, " +
                            " AdminUserID = ?AdminUserID, " +
                       
                            " Remark = ?Remark, " +
                            " Level = ?Level " +
                            " WHERE WaterMeterID =?WaterMeterID;";



                parms = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?WaterMeterName", MySqlDbType.VarChar),
                                         new MySqlParameter("?WaterMeterNo", MySqlDbType.VarChar),
                                         new MySqlParameter("?Address", MySqlDbType.VarChar),
                                         new MySqlParameter("?WaterMeterType",MySqlDbType.VarChar),
                                         new MySqlParameter("?WaterMeterLocation", MySqlDbType.VarChar),
                                         new MySqlParameter("?FatherWaterMeterID", MySqlDbType.Int32),
                                         new MySqlParameter("?AreaID", MySqlDbType.Int32),
                                         new MySqlParameter("?AdminUserID", MySqlDbType.Int32),
                                       
                                         new MySqlParameter("?Remark", MySqlDbType.VarChar),
                                         new MySqlParameter("?Level", MySqlDbType.Int32),
                                         new MySqlParameter("?WaterMeterID", MySqlDbType.Int32),

                                     };
                parms[0].Value = watermetername;
                parms[1].Value = watermeterno;
                parms[2].Value = "";
                parms[3].Value = watermetertype; ;
                parms[4].Value = ""; ;
                parms[5].Value = Convert.ToInt32(FatherWater); 
                parms[6].Value = Convert.ToInt32(areaid);
                parms[7].Value = Convert.ToInt32(adminuserid);
               
                parms[8].Value = remark;
                parms[9].Value = Convert.ToInt32(Level);
                parms[10].Value = Convert.ToInt32(watermeterid);


                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parms);
                if (IsDelSuccess)
                    return PublicMethod.OperationResultJson("0", "ok");
                else
                {

                    return PublicMethod.OperationResultJson("216", "更新水表失败");
                }

            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }


        }
        //删除水表
        [WebMethod(Description = "删除水表")]
        public static string deleteWatermeterByWatermeterID(string watermeterid)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();

            bool IsDelSuccess = false;
            string strSQL = "";
            string strResult = "";
            MySqlParameter[] parmss = null;

            strSQL = " Update twatermeter set deleteflag =1  WHERE watermeterid=?watermeterid;";

            try
            {
                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?watermeterid", MySqlDbType.Int32),
                                     };
                parmss[0].Value = Convert.ToInt32(watermeterid);
                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                if (IsDelSuccess)
                    return PublicMethod.OperationResultJson("0", "ok");
                else
                {

                    return PublicMethod.OperationResultJson("216", "删除水表失败");
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }

        #endregion 水表相关的方法


        #region 水表度数、查询相关的方法
        [WebMethod(Description = "通过水表ID，以及时间范围，查询水表度数信息")]
        public static string getWatermeterReadingByWatermeterIDandCAPTime(string watermeterid,string begincaptime,string endcaptime )
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipe");
                string strSQL = "select wh.*,w.WaterMeterName,w.WaterMeterNo " +
                " from tWaterMeterHistory wh,tWaterMeter w " +
                " where wh.WaterMeterID=w.WaterMeterID " +
                " and wh.WaterMeterID=?WaterMeterID" +
                " and CAPTime BETWEEN ?Stime AND ?Etime";  

                //string strSQL = " Select * From twatermeter where watermeterid=?watermeterid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?WaterMeterID", MySqlDbType.VarChar),
                        new MySqlParameter("?Stime", MySqlDbType.DateTime),
                        new MySqlParameter("?Etime", MySqlDbType.DateTime)
                    };

                parms[0].Value = watermeterid;
                parms[1].Value =  DateTime.Parse(begincaptime);
                parms[2].Value =  DateTime.Parse(endcaptime);

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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



        [WebMethod(Description = "按日来统计水表增量，以图表显示出来,用于水平衡报表,areaid区域的ID,level=1，表示一级水表，2则是二级水表")]
        public static string getWatermeterBalanceReportForDayChart(string areaid, string begincaptime, string endcaptime)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                string strSQL = "        Select   DATE_FORMAT(T1.calday,'%Y-%m-%d') as calday, sum(T1.总数量 - T2.总数量) calcount,T1.Level " +
                     " From (Select CAPTime calday, wm.WaterMeterID ,MAX(WaterMeterReading) 总数量,wm.Level " +
                       " From tWaterMeterHistory wmh,tWaterMeter wm  " +
                         " WHERE wmh.WaterMeterID=wm.WaterMeterID  " +
                          " and wm.AreaID=?areaid1 " +
                      " and CAPTime BETWEEN @Stime1 AND @Etime1  " +
                  " Group By CAPTime, wm.WaterMeterID) T1,  " +
                  " (Select CAPTime calday, wm.WaterMeterID ,MAX(WaterMeterReading) 总数量 ,wm.Level " +
                  " From tWaterMeterHistory wmh,tWaterMeter wm  " +
                  " WHERE wmh.WaterMeterID=wm.WaterMeterID " +
                 " and wm.AreaID=?areaid2 " +
                   " and CAPTime BETWEEN @Stime2 AND @Etime2  " +
                  " Group By CAPTime, wm.WaterMeterID) T2  " +
                  " Where T1.calday = date_add(T2.calday,interval 1 day) " +
                  " and T1.WaterMeterID=T2.WaterMeterID " +
                  " group by T1.calday ,T1.Level " +
                  " order by T1.calday;";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid1", MySqlDbType.Int32),
                        new MySqlParameter("?Stime1", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime1", MySqlDbType.Datetime),
                        new MySqlParameter("?areaid2", MySqlDbType.Int32),
                        new MySqlParameter("?Stime2", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime2", MySqlDbType.Datetime),
                    };

                parms[0].Value = areaid;
                parms[1].Value = Convert.ToDateTime(begincaptime);
                parms[2].Value = Convert.ToDateTime(endcaptime);
                parms[3].Value = areaid;
                parms[4].Value = Convert.ToDateTime(begincaptime);
                parms[5].Value = Convert.ToDateTime(endcaptime); ;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        DataTable tblDatas = new DataTable("Datas");
                        DataColumn dc = null;

                        dc = tblDatas.Columns.Add("calday", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level1calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level2calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level3calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level4calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level5calcount", Type.GetType("System.String"));

                        string strCurrentDay = "";
                        DataRow newRow = null;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {


                            if (strCurrentDay != ds.Tables[0].Rows[i]["calday"].ToString())
                            {
                                if (i != 0)
                                {
                                    tblDatas.Rows.Add(newRow);
                                }
                                strCurrentDay = ds.Tables[0].Rows[i]["calday"].ToString();
                                newRow = tblDatas.NewRow();
                                newRow["calday"] = ds.Tables[0].Rows[i]["calday"].ToString();
                                newRow["level1calcount"] = "0";
                                newRow["level2calcount"] = "0";
                                newRow["level3calcount"] = "0";
                                newRow["level4calcount"] = "0";
                                newRow["level5calcount"] = "0";

                            }

                            if (ds.Tables[0].Rows[i]["Level"].ToString() == "1")
                            {
                                newRow["level1calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "2")
                            {
                                newRow["level2calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "3")
                            {
                                newRow["level3calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "4")
                            {
                                newRow["level4calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "5")
                            {
                                newRow["level5calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }

                            if (i == ds.Tables[0].Rows.Count - 1)
                            {
                                tblDatas.Rows.Add(newRow);
                            }

                        }



                        return PublicMethod.DataTableToJson(tblDatas);
                        //return PublicMethod.DataTableToJson(ds.Tables[0]);

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
        
        [WebMethod(Description = "[]按日来统计水表增量，用于水表历史记录查询里面的用水量图表之按日统计")]
        public static string getWatermeterReadingDayChartData(string watermeterid, string begincaptime, string endcaptime)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                string strSQL = "   Select   DATE_FORMAT(T1.calday,'%Y-%m-%d') as calday, sum(T1.总数量 - T2.总数量) calcount " +
                                " From (Select CAPTime calday, MAX(WaterMeterReading) 总数量  " +
                                  " From tWaterMeterHistory wmh  " +
                                    " WHERE WaterMeterID=@w1  " +
                                 " and CAPTime BETWEEN @Stime1 AND @Etime1  " +
                             " Group By CAPTime ) T1,  " +
                             " (Select CAPTime calday,MAX(WaterMeterReading) 总数量  " +
                             " From tWaterMeterHistory wmh  " +
                               " WHERE WaterMeterID=@w2  " +
                              " and CAPTime BETWEEN @Stime2 AND @Etime2 " +
                             " Group By CAPTime) T2  " +
                             " Where T1.calday = date_add(T2.calday,interval 1 day) " +
                             " group by T1.calday " +
                             " order by T1.calday;";

                //string strSQL = " Select * From twatermeter where watermeterid=?watermeterid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?w1", MySqlDbType.Int32),
                        new MySqlParameter("?Stime1", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime1", MySqlDbType.Datetime),
                        new MySqlParameter("?w2", MySqlDbType.Int32),
                        new MySqlParameter("?Stime2", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime2", MySqlDbType.Datetime),
                    };

                parms[0].Value = watermeterid;
                parms[1].Value = Convert.ToDateTime(begincaptime);
                parms[2].Value = Convert.ToDateTime(endcaptime);
                parms[3].Value = watermeterid;
                parms[4].Value = Convert.ToDateTime(begincaptime);
                parms[5].Value = Convert.ToDateTime(endcaptime);

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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

       

        [WebMethod(Description = "按月来统计水表增量，用于水表历史记录查询里面的用水量图表之按月统计")]
        public static string getWatermeterReadingMonthChartData(string watermeterid, string begincaptime, string endcaptime)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                string strSQL = "    SELECT T3.calmonth ,sum(T3.calcount) calcount   FROM " +
                                     " (SELECT T1.WaterMeterID,T1.月  calmonth,T1.总数量 - IFNULL(T2.总数量,0)  calcount " +
                                     " FROM " +
                                     " (Select wmh.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME) AS  月, " +
                                     " MAX(WaterMeterReading) AS 总数量  From tWaterMeterHistory wmh " +
                                     " WHERE WaterMeterID=@w1 " +
                                      " and CAPTime BETWEEN @Stime1 AND @Etime1  " +
                                     " Group By wmh.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME)) as T1  " +
                                     " left join (Select wmh.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME)  月, MAX(WaterMeterReading) 总数量  " +
                                     " From tWaterMeterHistory wmh  WHERE WaterMeterID=@w1 " +
                                      " and CAPTime BETWEEN @Stime2 AND @Etime2  " +
                                     " Group By wmh.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME)) T2  " +
                                     " on T2.月+1=T1.月 " +
                                     " and T1.WaterMeterID=T2.WaterMeterID) AS T3 " +
                                     " Group By T3.calmonth " +
                                     " order by calmonth ";

                //string strSQL = " Select * From twatermeter where watermeterid=?watermeterid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?w1", MySqlDbType.Int32),
                        new MySqlParameter("?Stime1", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime1", MySqlDbType.Datetime),
                        new MySqlParameter("?w2", MySqlDbType.Int32),
                        new MySqlParameter("?Stime2", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime2", MySqlDbType.Datetime),
                    };

                parms[0].Value = watermeterid;
                parms[1].Value = Convert.ToDateTime(begincaptime);
                parms[2].Value = Convert.ToDateTime(endcaptime);
                parms[3].Value = watermeterid;
                parms[4].Value = Convert.ToDateTime(begincaptime);
                parms[5].Value = Convert.ToDateTime(endcaptime);

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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


        [WebMethod(Description = "按年来统计水表增量，用于水表历史记录查询里面的用水量图表之按年统计")]
        public static string getWatermeterReadingYearChartData(string watermeterid, string begincaptime, string endcaptime)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                string strSQL = "    SELECT T3.calmonth calyear ,sum(T3.calcount) calcount   FROM " +
                                " (SELECT T1.WaterMeterID,T1.年  calmonth,T1.总数量 - IFNULL(T2.总数量,0)  calcount " +
                                " FROM " +
                                " (Select wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME) AS  年, " +
                                " MAX(WaterMeterReading) AS 总数量  From tWaterMeterHistory wm " +
                                " WHERE WaterMeterID=@w1 " +
                                 " and CAPTime BETWEEN @Stime1 AND @Etime1  " +
                                " Group By wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME)) as T1  " +
                                " left join (Select wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME)  年, MAX(WaterMeterReading) 总数量  " +
                                " From tWaterMeterHistory wm  WHERE WaterMeterID=@w2 " +
                                 " and CAPTime BETWEEN @Stime2 AND @Etime2  " +
                                " Group By wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME)) T2  " +
                                " on T2.年+1=T1.年 " +
                                " and T1.WaterMeterID=T2.WaterMeterID) AS T3 " +
                                " Group By T3.calmonth " +
                                " order by calmonth ";

                //string strSQL = " Select * From twatermeter where watermeterid=?watermeterid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?w1", MySqlDbType.Int32),
                        new MySqlParameter("?Stime1", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime1", MySqlDbType.Datetime),
                        new MySqlParameter("?w2", MySqlDbType.Int32),
                        new MySqlParameter("?Stime2", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime2", MySqlDbType.Datetime),
                    };

                parms[0].Value = watermeterid;
                parms[1].Value = Convert.ToDateTime(begincaptime);
                parms[2].Value = Convert.ToDateTime(endcaptime);
                parms[3].Value = watermeterid;
                parms[4].Value = Convert.ToDateTime(begincaptime);
                parms[5].Value = Convert.ToDateTime(endcaptime);

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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


        [WebMethod(Description = "按日来统计水表增量，以图表显示出来,用于水平衡报表")]
        public static string getWatermeterReadingDayChartDataByAreaIDandCAPTime(string areaid,string begincaptime, string endcaptime)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                string strSQL = "        Select   DATE_FORMAT(T1.calday,'%Y-%m-%d') as calday, sum(T1.总数量 - T2.总数量) calcount,T1.Level " +
                     " From (Select CAPTime calday, wm.WaterMeterID ,MAX(WaterMeterReading) 总数量,wm.Level " +
                       " From tWaterMeterHistory wmh,tWaterMeter wm  " +
                         " WHERE wmh.WaterMeterID=wm.WaterMeterID  " +
                          " and wm.AreaID=?areaid1 " +
                      " and CAPTime BETWEEN @Stime1 AND @Etime1  " +
                  " Group By CAPTime, wm.WaterMeterID) T1,  " +
                  " (Select CAPTime calday, wm.WaterMeterID ,MAX(WaterMeterReading) 总数量 ,wm.Level " +
                  " From tWaterMeterHistory wmh,tWaterMeter wm  " +
                  " WHERE wmh.WaterMeterID=wm.WaterMeterID " +
                 " and wm.AreaID=?areaid2 " +
                   " and CAPTime BETWEEN @Stime2 AND @Etime2  " +
                  " Group By CAPTime, wm.WaterMeterID) T2  " +
                  " Where T1.calday = date_add(T2.calday,interval 1 day) " +
                  " and T1.WaterMeterID=T2.WaterMeterID " +
                  " group by T1.calday ,T1.Level " +
                  " order by T1.calday;";

                //string strSQL = " Select * From twatermeter where watermeterid=?watermeterid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid1", MySqlDbType.Int32),
                        new MySqlParameter("?Stime1", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime1", MySqlDbType.Datetime),
                        new MySqlParameter("?areaid2", MySqlDbType.Int32),
                        new MySqlParameter("?Stime2", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime2", MySqlDbType.Datetime),
                    };

                parms[0].Value = areaid;
                parms[1].Value = Convert.ToDateTime(begincaptime);
                parms[2].Value = Convert.ToDateTime(endcaptime);
                parms[3].Value = areaid;
                parms[4].Value = Convert.ToDateTime(begincaptime);
                parms[5].Value = Convert.ToDateTime(endcaptime); ;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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

        /*
        [WebMethod(Description = "按月来统计水表增量，以图表显示出来,用于水平衡报表")]
        public static string getWatermeterReadingMonthChartDataByAreaIDandCAPTime(string areaid, string begincaptime, string endcaptime)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                string strSQL = "    SELECT T3.calmonth ,sum(T3.calcount) calcount ,T3.Level   FROM " +
                                            " (SELECT T1.WaterMeterID,T1.月  calmonth,T1.总数量 - IFNULL(T2.总数量,0)  calcount ,T1.Level " +
                                            " FROM " +
                                            " (Select wm.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME) AS  月, " +
                                            " MAX(WaterMeterReading) AS 总数量,wm.Level  From tWaterMeterHistory wmh,tWaterMeter wm  " +
                                            " WHERE wmh.WaterMeterID=wm.WaterMeterID " +
                                             " and wm.AreaID=?areaid1 " +
                                             " and CAPTime BETWEEN @Stime1 AND @Etime1  " +
                                            " Group By wm.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME)) as T1  " +
                                            " left join (Select wm.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME)  月, MAX(WaterMeterReading) 总数量,wm.Level  " +
                                            " From tWaterMeterHistory wmh,tWaterMeter wm  WHERE wmh.WaterMeterID=wm.WaterMeterID " +
                                              " and wm.AreaID=?areaid2 " +
                                             " and CAPTime BETWEEN @Stime2 AND @Etime2  " +
                                            " Group By wm.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME)) T2  " +
                                            " on T2.月+1=T1.月 " +
                                            " and T1.WaterMeterID=T2.WaterMeterID) AS T3 " +
                                             " Group By T3.calmonth ,T3.Level" +
                                            " order by calmonth,Level ";

                //string strSQL = " Select * From twatermeter where watermeterid=?watermeterid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid1", MySqlDbType.Int32),
                        new MySqlParameter("?Stime1", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime1", MySqlDbType.Datetime),
                        new MySqlParameter("?areaid2", MySqlDbType.Int32),
                        new MySqlParameter("?Stime2", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime2", MySqlDbType.Datetime),
                    };

                parms[0].Value = areaid;
                parms[1].Value = Convert.ToDateTime(begincaptime);
                parms[2].Value = Convert.ToDateTime(endcaptime);
                parms[3].Value = areaid;
                parms[4].Value = Convert.ToDateTime(begincaptime);
                parms[5].Value = Convert.ToDateTime(endcaptime); 

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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
        */

        [WebMethod(Description = "按月来统计水表增量，以图表显示出来,用于水平衡报表")]
        public static string getWatermeterBalanceReportForMonthChart(string areaid, string begincaptime, string endcaptime)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                string strSQL = "    SELECT T3.calmonth ,sum(T3.calcount) calcount ,T3.Level   FROM " +
                                            " (SELECT T1.WaterMeterID,T1.月  calmonth,T1.总数量 - IFNULL(T2.总数量,0)  calcount ,T1.Level " +
                                            " FROM " +
                                            " (Select wm.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME) AS  月, " +
                                            " MAX(WaterMeterReading) AS 总数量,wm.Level  From tWaterMeterHistory wmh,tWaterMeter wm  " +
                                            " WHERE wmh.WaterMeterID=wm.WaterMeterID " +
                                             " and wm.AreaID=?areaid1 " +
                                             " and CAPTime BETWEEN @Stime1 AND @Etime1  " +
                                            " Group By wm.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME)) as T1  " +
                                            " left join (Select wm.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME)  月, MAX(WaterMeterReading) 总数量,wm.Level  " +
                                            " From tWaterMeterHistory wmh,tWaterMeter wm  WHERE wmh.WaterMeterID=wm.WaterMeterID " +
                                              " and wm.AreaID=?areaid2 " +
                                             " and CAPTime BETWEEN @Stime2 AND @Etime2  " +
                                            " Group By wm.WaterMeterID,EXTRACT(YEAR_MONTH FROM CAPTIME)) T2  " +
                                            " on T2.月+1=T1.月 " +
                                            " and T1.WaterMeterID=T2.WaterMeterID) AS T3 " +
                                             " Group By T3.calmonth ,T3.Level" +
                                            " order by calmonth,Level ";

                //string strSQL = " Select * From twatermeter where watermeterid=?watermeterid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid1", MySqlDbType.Int32),
                        new MySqlParameter("?Stime1", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime1", MySqlDbType.Datetime),
                        new MySqlParameter("?areaid2", MySqlDbType.Int32),
                        new MySqlParameter("?Stime2", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime2", MySqlDbType.Datetime),
                    };

                parms[0].Value = areaid;
                parms[1].Value = Convert.ToDateTime(begincaptime);
                parms[2].Value = Convert.ToDateTime(endcaptime);
                parms[3].Value = areaid;
                parms[4].Value = Convert.ToDateTime(begincaptime);
                parms[5].Value = Convert.ToDateTime(endcaptime);

                ds = MySQLDB.SelectDataSet(strSQL, parms);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        DataTable tblDatas = new DataTable("Datas");
                        DataColumn dc = null;

                        dc = tblDatas.Columns.Add("calmonth", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level1calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level2calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level3calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level4calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level5calcount", Type.GetType("System.String"));

                        string strCurrentDay = "";
                        DataRow newRow = null;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {


                            if (strCurrentDay != ds.Tables[0].Rows[i]["calmonth"].ToString())
                            {
                                if (i != 0)
                                {
                                    tblDatas.Rows.Add(newRow);
                                }
                                strCurrentDay = ds.Tables[0].Rows[i]["calmonth"].ToString();
                                newRow = tblDatas.NewRow();
                                newRow["calmonth"] = ds.Tables[0].Rows[i]["calmonth"].ToString();
                                newRow["level1calcount"] = "0";
                                newRow["level2calcount"] = "0";
                                newRow["level3calcount"] = "0";
                                newRow["level4calcount"] = "0";
                                newRow["level5calcount"] = "0";

                            }

                            if (ds.Tables[0].Rows[i]["Level"].ToString() == "1")
                            {
                                newRow["level1calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "2")
                            {
                                newRow["level2calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "3")
                            {
                                newRow["level3calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "4")
                            {
                                newRow["level4calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "5")
                            {
                                newRow["level5calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }

                            if (i == ds.Tables[0].Rows.Count - 1)
                            {
                                tblDatas.Rows.Add(newRow);
                            }

                        }

                        return PublicMethod.DataTableToJson(tblDatas);
                        //return PublicMethod.DataTableToJson(ds.Tables[0]);

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





        [WebMethod(Description = "按年来统计水表增量，以图表显示出来,用于水平衡报表")]
        public static string getWatermeterBalanceReportForYearChart(string areaid, string begincaptime, string endcaptime)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                string strSQL = "    SELECT T3.calmonth calyear ,sum(T3.calcount) calcount ,T3.Level  FROM " +
                              " (SELECT T1.WaterMeterID,T1.年  calmonth,T1.总数量 - IFNULL(T2.总数量,0)  calcount,T1.Level " +
                              " FROM " +
                              " (Select wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME) AS  年, " +
                              " MAX(WaterMeterReading) AS 总数量  ,wm.Level From tWaterMeterHistory wmh,tWaterMeter wm  " +
                              " WHERE wmh.WaterMeterID=wm.WaterMeterID " +
                               " and wm.AreaID=?areaid1 " +
                               " and CAPTime BETWEEN @Stime1 AND @Etime1  " +
                              " Group By wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME)) as T1  " +
                              " left join (Select wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME)  年, MAX(WaterMeterReading) 总数量 ,wm.Level  " +
                              " From tWaterMeterHistory wmh,tWaterMeter wm  WHERE wmh.WaterMeterID=wm.WaterMeterID " +
                                " and wm.AreaID=?areaid2 " +
                               " and CAPTime BETWEEN @Stime2 AND @Etime2  " +
                              " Group By wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME)) T2  " +
                              " on T2.年+1=T1.年 " +
                              " and T1.WaterMeterID=T2.WaterMeterID) AS T3 " +
                              " Group By T3.calmonth,T3.Level " +
                              " order by calmonth ";

                //string strSQL = " Select * From twatermeter where watermeterid=?watermeterid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid1", MySqlDbType.Int32),
                        new MySqlParameter("?Stime1", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime1", MySqlDbType.Datetime),
                        new MySqlParameter("?areaid2", MySqlDbType.Int32),
                        new MySqlParameter("?Stime2", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime2", MySqlDbType.Datetime),
                    };

                parms[0].Value = areaid;
                parms[1].Value = Convert.ToDateTime(begincaptime);
                parms[2].Value = Convert.ToDateTime(endcaptime);
                parms[3].Value = areaid;
                parms[4].Value = Convert.ToDateTime(begincaptime);
                parms[5].Value = Convert.ToDateTime(endcaptime); ;

                ds = MySQLDB.SelectDataSet(strSQL, parms);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        DataTable tblDatas = new DataTable("Datas");
                        DataColumn dc = null;

                        dc = tblDatas.Columns.Add("calyear", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level1calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level2calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level3calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level4calcount", Type.GetType("System.String"));
                        dc = tblDatas.Columns.Add("level5calcount", Type.GetType("System.String"));

                        string strCurrentDay = "";
                        DataRow newRow = null;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {


                            if (strCurrentDay != ds.Tables[0].Rows[i]["calyear"].ToString())
                            {
                                if (i != 0)
                                {
                                    tblDatas.Rows.Add(newRow);
                                }
                                strCurrentDay = ds.Tables[0].Rows[i]["calyear"].ToString();
                                newRow = tblDatas.NewRow();
                                newRow["calyear"] = ds.Tables[0].Rows[i]["calyear"].ToString();
                                newRow["level1calcount"] = "0";
                                newRow["level2calcount"] = "0";
                                newRow["level3calcount"] = "0";
                                newRow["level4calcount"] = "0";
                                newRow["level5calcount"] = "0";

                            }

                            if (ds.Tables[0].Rows[i]["Level"].ToString() == "1")
                            {
                                newRow["level1calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "2")
                            {
                                newRow["level2calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "3")
                            {
                                newRow["level3calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "4")
                            {
                                newRow["level4calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["Level"].ToString() == "5")
                            {
                                newRow["level5calcount"] = ds.Tables[0].Rows[i]["calcount"].ToString();
                            }

                            if (i == ds.Tables[0].Rows.Count - 1)
                            {
                                tblDatas.Rows.Add(newRow);
                            }

                        }

                        return PublicMethod.DataTableToJson(tblDatas);
                        //return PublicMethod.DataTableToJson(ds.Tables[0]);

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


        /*
        [WebMethod(Description = "按年来统计水表增量，以图表显示出来,用于水平衡报表")]
        public static string getWatermeterReadingYearChartDataByAreaIDandCAPTime(string areaid, string begincaptime, string endcaptime)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                string strSQL = "    SELECT T3.calmonth calyear ,sum(T3.calcount) calcount ,T3.Level  FROM " +
                              " (SELECT T1.WaterMeterID,T1.年  calmonth,T1.总数量 - IFNULL(T2.总数量,0)  calcount,T1.Level " +
                              " FROM " +
                              " (Select wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME) AS  年, " +
                              " MAX(WaterMeterReading) AS 总数量  ,wm.Level From tWaterMeterHistory wmh,tWaterMeter wm  " +
                              " WHERE wmh.WaterMeterID=wm.WaterMeterID " +
                               " and wm.AreaID=?areaid1 " +
                               " and CAPTime BETWEEN @Stime1 AND @Etime1  " +
                              " Group By wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME)) as T1  " +
                              " left join (Select wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME)  年, MAX(WaterMeterReading) 总数量 ,wm.Level  " +
                              " From tWaterMeterHistory wmh,tWaterMeter wm  WHERE wmh.WaterMeterID=wm.WaterMeterID " +
                                " and wm.AreaID=?areaid2 " +
                               " and CAPTime BETWEEN @Stime2 AND @Etime2  " +
                              " Group By wm.WaterMeterID,EXTRACT(YEAR FROM CAPTIME)) T2  " +
                              " on T2.年+1=T1.年 " +
                              " and T1.WaterMeterID=T2.WaterMeterID) AS T3 " +
                              " Group By T3.calmonth,T3.Level " +
                              " order by calmonth ";

                //string strSQL = " Select * From twatermeter where watermeterid=?watermeterid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid1", MySqlDbType.Int32),
                        new MySqlParameter("?Stime1", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime1", MySqlDbType.Datetime),
                        new MySqlParameter("?areaid2", MySqlDbType.Int32),
                        new MySqlParameter("?Stime2", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime2", MySqlDbType.Datetime),
                    };

                parms[0].Value = areaid;
                parms[1].Value = Convert.ToDateTime(begincaptime);
                parms[2].Value = Convert.ToDateTime(endcaptime);
                parms[3].Value = areaid;
                parms[4].Value = Convert.ToDateTime(begincaptime);
                parms[5].Value = Convert.ToDateTime(endcaptime); ;

                ds = MySQLDB.SelectDataSet(strSQL, parms);

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

        */
        #endregion  水表度数、查询相关的方法
        
        
        #region 人员相关的方法

        [WebMethod(Description = "新增用户")]
        public static string AddUser(string UserName, string RealName, string AreaID, string GroupID, string PhoneNum, string Sex)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet();
            string strResult = "";

            MySqlParameter[] parms = null;
            string strSQL = "insert into tuser (UserName,PassWord,RealName,GroupID,Gender,AreaID,PhoneNumber) values (?UserName,'e10adc3949ba59abbe56e057f20f883e',?RealName,?GroupID,?Gender,?AreaID,?PhoneNumber)";
            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?UserName", MySqlDbType.VarChar),
                        new MySqlParameter("?RealName", MySqlDbType.VarChar),
                        new MySqlParameter("?GroupID", MySqlDbType.Int32),
                        new MySqlParameter("?Gender", MySqlDbType.VarChar),
                        new MySqlParameter("?AreaID", MySqlDbType.Int32),
                        new MySqlParameter("?PhoneNumber", MySqlDbType.VarChar)
                    
                    };
            parms[0].Value = UserName;
            parms[1].Value = RealName;
            parms[2].Value = int.Parse( GroupID);
            parms[3].Value = Sex;
            parms[4].Value = int.Parse(AreaID);
            parms[5].Value = PhoneNum;
     

          
            try
            {

            
                bool IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parms);
                if (IsDelSuccess)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {

                    return PublicMethod.OperationResultJson("219", "新增用户失败");
                }
               
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }


        }




        [WebMethod(Description = "更新用户")]
        public static string UpdateUser(string RealName, string AreaID, string GroupID, string PhoneNum, string Sex,string UserID)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet();
            string strResult = "";

            MySqlParameter[] parms = null;
            string strSQL = "update tuser set RealName=?RealName,GroupID=?GroupID,Gender=?Gender,AreaID=?AreaID,PhoneNumber=?PhoneNumber where UserID=?UserID";
            parms = new MySqlParameter[]
                    {
                      
                        new MySqlParameter("?RealName", MySqlDbType.VarChar),
                        new MySqlParameter("?GroupID", MySqlDbType.Int32),
                        new MySqlParameter("?Gender", MySqlDbType.VarChar),
                        new MySqlParameter("?AreaID", MySqlDbType.Int32),
                        new MySqlParameter("?PhoneNumber", MySqlDbType.VarChar),
                         new MySqlParameter("?UserID", MySqlDbType.Int32)
                    
                    };
         
            parms[0].Value = RealName;
            parms[1].Value = int.Parse(GroupID);
            parms[2].Value = Sex;
            parms[3].Value = int.Parse(AreaID);
            parms[4].Value = PhoneNum;
            parms[5].Value = UserID;



            try
            {


                bool IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parms);
                if (IsDelSuccess)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {

                    return PublicMethod.OperationResultJson("219", "新增用户失败");
                }

            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }


        }


        [WebMethod(Description = "删除用户")]
        public static string DeleteUser( string UserID)
        {
            MySQLDB.InitDb();
         
            MySqlParameter[] parms = null;
            string strSQL = "delete from tuser  where UserID=?UserID";
            parms = new MySqlParameter[]
                    {
                      
                     
                         new MySqlParameter("?UserID", MySqlDbType.Int32)
                    
                    };

         
            parms[0].Value = UserID;



            try
            {


                bool IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parms);
                if (IsDelSuccess)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {

                    return PublicMethod.OperationResultJson("219", "删除用户失败");
                }

            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }


        }
        
        
        
        
        [WebMethod(Description = "获取人员列表")]
        public static string getAllUser()
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tuser");
              //  string strSQL = " Select A.UserID,A.UserName,A.RealName,B.GroupName,C.AreaName,A.PhoneNumber,A.Gender From tuser A ,tgroup B,tarea C where A.AreaID=C.AreaID and A.GroupID=B.GroupID; ";

                string strSQL = " Select * from tuser A left join tgroup B on A.GroupID=B.GroupID left join tarea C on A.AreaID=C.AreaID";

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

        #endregion 人员相关的方法

        #region 权限相关的方法
        [WebMethod(Description = "添加权限分组")]
        public static string addGroup(string groupname,
                    string syspermission)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dsSensor");
            string strResult = "";

            MySqlParameter[] parms = null;

            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inGroupName", MySqlDbType.VarChar),
                        new MySqlParameter("?inSysPermission", MySqlDbType.VarChar),
                    };
            parms[0].Value = groupname;
            parms[1].Value = syspermission;


            MySqlParameter outPara = new MySqlParameter("?outGroupID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddGroup", parms, outPara);
                if (Convert.ToInt32(strResult) > 0)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("222", "新增权限分组失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }



        [WebMethod(Description = "获取当前用户权限")]
        public static string getCurrentPermission()
        {
            if (HttpContext.Current.Session["UserID"]==null || HttpContext.Current.Session["UserName"]==null)
            {
                return "";
            }
           string userid= HttpContext.Current.Session["UserID"].ToString();

           string username=   HttpContext.Current.Session["UserName"].ToString();
            if(username=="admin")
                return PublicMethod.DataTableJsonOnlyOneColumn("Result", "admin");
         
            MySQLDB.InitDb();
           int intResult = 0;
           try
           {
               DataSet ds = new DataSet("tsensor");
               //string strSQL = " Select * From tsensor where sensorid=?sensorid; ";

               string strSQL = "Select  p.* " +
                               " from tuser u,tgroup p " +
                               " where u.GroupID=p.GroupID " +
                                " and u.userid=?userid ";

               MySqlParameter[] parms = null;


               parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?userid", MySqlDbType.Int32)
                    };

               parms[0].Value = userid;


               string  strPermission = "";
               ds = MySQLDB.SelectDataSet(strSQL, parms);
               //if (ds != null)
               //{
               if (ds.Tables[0].Rows.Count > 0)
               // 有数据集
               {
                    strPermission = ds.Tables[0].Rows[0]["SysPermission"].ToString();
                  

                   //return PublicMethod.DataTableJsonOnlyOneColumn("Result", intResult.ToString());
                   //return PublicMethod.DataTableToJson(ds.Tables[0]);

               }
              
               //}
               //else
               //{
               //    return PublicMethod.OperationResultJson("99", "数据为空");
               //}

               return PublicMethod.DataTableJsonOnlyOneColumn("Result", strPermission);
           }
           catch (Exception ex)
           {
               return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
           }


        }









     
        [WebMethod(Description = "获取用户是否有权限，输入参数是模块名称，中文，清单如下,返回值1表示有权限，0表示没有权限；")]
        /*
         * 新增水表，修改水表，删除水表；
         * 新增传感器，修改传感器，删除传感器
         * 新增管线，修改管线，删除管线
         */
        public static string getPermissionByUserIDandPermissionName(string userid,string permissionname)
        {
            MySQLDB.InitDb();
            int intResult = 0;
            try
            {
                DataSet ds = new DataSet("tsensor");
                //string strSQL = " Select * From tsensor where sensorid=?sensorid; ";

                string strSQL = "Select  p.* " +
                                " from tuser u,tgroup p " +
                                " where u.GroupID=p.GroupID " +
                                 " and u.userid=?userid ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?userid", MySqlDbType.Int32)
                    };

                parms[0].Value = userid;


                ds = MySQLDB.SelectDataSet(strSQL, parms);
                //if (ds != null)
                //{
                    if (ds.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        string strPermission=ds.Tables[0].Rows[0]["SysPermission"].ToString();
                        string[] arrPermission = strPermission.Split('|');

                        foreach (var item in arrPermission)
                        {
                            if (permissionname==item.ToString())
                            {
                                intResult = 1;
                                break;
                            }
                        }

                        //return PublicMethod.DataTableJsonOnlyOneColumn("Result", intResult.ToString());
                        //return PublicMethod.DataTableToJson(ds.Tables[0]);

                    }

                //}
                //else
                //{
                //    return PublicMethod.OperationResultJson("99", "数据为空");
                //}

                return PublicMethod.DataTableJsonOnlyOneColumn("Result", intResult.ToString());
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }


        [WebMethod(Description = "修改分组权限")]
        public static string updateGroupByGroupID(string groupid, string syspermission)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();

            bool IsDelSuccess = false;
            string strSQL = "";
            string strResult = "";
            MySqlParameter[] parmss = null;

            strSQL = " Update tgroup set syspermission =?syspermission   " +
                      " WHERE groupid=?groupid;";

            try
            {
                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?syspermission", MySqlDbType.VarChar),
                          
                                         new MySqlParameter("?groupid", MySqlDbType.Int32)
                                     };
                parmss[0].Value = syspermission;
             
                parmss[1].Value = Convert.ToInt32(groupid);

                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                if (IsDelSuccess)
                    return PublicMethod.OperationResultJson("0", "ok");
                else
                {

                    return PublicMethod.OperationResultJson("311", "修改分组权限失败");
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }

        [WebMethod(Description = "通过群组ID，获取权限信息;用于修改群组的权限页面")]
        public static string getGroupPermissionByGroupID(string groupid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipe");
                string strSQL = " Select p.*  " +
                                " From tGroup p  " +
                                " where p.groupid=?groupid ; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?groupid", MySqlDbType.Int32)
                    };

                parms[0].Value = groupid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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

        #endregion 权限相关的方法

        #region 传感器相关的方法
        [WebMethod(Description = "添加传感器")]
        public static string addSensor(string sensorname,
                    string sensorno, 
                    string sensortype,
                    string sensorlocation,
                    string areaid, string adminuserid, string pipeid,
                    string remark,string sencondhead)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dsSensor");
            string strResult = "";

            MySqlParameter[] parms = null;



            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inSensorName", MySqlDbType.VarChar),
                        new MySqlParameter("?inSensorNo", MySqlDbType.VarChar),
                        new MySqlParameter("?inSensorType", MySqlDbType.VarChar),
                        new MySqlParameter("?inSensorLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inAreaID", MySqlDbType.Int32),
                        new MySqlParameter("?inAdminUserID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeID", MySqlDbType.Int32),
                        new MySqlParameter("?inRemark", MySqlDbType.VarChar),
                        new MySqlParameter("?inSencondHead", MySqlDbType.Int32)
                    };
            parms[0].Value = sensorname;
            parms[1].Value = sensorno;
            parms[2].Value = sensortype; ;
            parms[3].Value = sensorlocation; ;
            parms[4].Value = Convert.ToInt32(areaid);
            parms[5].Value = Convert.ToInt32(adminuserid);
            parms[6].Value = Convert.ToInt32(pipeid);
            parms[7].Value = remark;

            MySqlParameter outPara = new MySqlParameter("?outSensorID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddSensor", parms, outPara);
                if (Convert.ToInt32(strResult) > 0)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("218", "新增传感器失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }


        [WebMethod(Description = "添加传感器")]
        public static string addSensorSimple( string areaid,  string pipeid, string sencondhead)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dsSensor");
            DataSet ds1;
            DataSet ds2;
            string strResult = "";
            MySqlParameter[] parms = null;


            if (sencondhead == "1")
            {
                string sql1 = "select * from tpipe where PrePipeID=" + pipeid;
                ds1 = MySQLDB.SelectDataSet(sql1, null);
                ds2 = null;

                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    ds2 = MySQLDB.SelectDataSet("select * from tsensor where PipeID=" + r["PipeID"].ToString() + " and SencondHead=0 and DeleteFlag=0", null);

                    if (ds2.Tables[0].Rows.Count > 0)

                        return "1";

                }

                ds2 = MySQLDB.SelectDataSet("select * from tsensor where PipeID=" + pipeid + " and SencondHead=1 and DeleteFlag=0", null);
                if (ds2.Tables[0].Rows.Count > 0)

                    return "1";
            }
            else
            {

                ds1 = MySQLDB.SelectDataSet("select * from tpipe where PipeID="+pipeid+" and DeleteFlag=0", null);
                if (ds1.Tables[0].Rows.Count == 0)
                    return "1";

                int prepipeid = Convert.ToInt32(ds1.Tables[0].Rows[0]["PrePipeID"]);
           
                if (prepipeid > 0)
                {
                    ds1 = MySQLDB.SelectDataSet("select * from tpipe where PrePipeID=" + prepipeid, null);
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        ds2 = MySQLDB.SelectDataSet("select * from tsensor where PipeID=" + r["PipeID"].ToString() + " and SencondHead=0 and DeleteFlag=0", null);

                        if (ds2.Tables[0].Rows.Count > 0)

                            return "1";

                    }

                    ds2 = MySQLDB.SelectDataSet("select * from tsensor where PipeID=" + prepipeid + " and SencondHead=1 and DeleteFlag=0", null);
                    if (ds2.Tables[0].Rows.Count > 0)

                        return "1";
                }
                else
                {

                    ds2 = MySQLDB.SelectDataSet("select * from tsensor where PipeID=" + pipeid + " and SencondHead=0 and DeleteFlag=0", null);

                    if (ds2.Tables[0].Rows.Count > 0)

                        return "1";

                }

            }
            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inSensorName", MySqlDbType.VarChar),
                        new MySqlParameter("?inSensorNo", MySqlDbType.VarChar),
                        new MySqlParameter("?inSensorType", MySqlDbType.VarChar),
                        new MySqlParameter("?inSensorLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inAreaID", MySqlDbType.Int32),
                        new MySqlParameter("?inAdminUserID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeID", MySqlDbType.Int32),
                        new MySqlParameter("?inRemark", MySqlDbType.VarChar),
                        new MySqlParameter("?inSencondHead", MySqlDbType.Int32)
                    };
            parms[0].Value = "";
            parms[1].Value = "";
            parms[2].Value = "";
            parms[3].Value = "";
            parms[4].Value = Convert.ToInt32(areaid);
            parms[5].Value = -1;
            parms[6].Value = Convert.ToInt32(pipeid);
            parms[7].Value = "";
            parms[8].Value =int.Parse( sencondhead);

            MySqlParameter outPara = new MySqlParameter("?outSensorID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddSensor", parms, outPara);
                if (Convert.ToInt32(strResult) > 0)
                {

                    string strSQL = " update tsensor set SensorName='" + "传感器" + outPara.Value.ToString() + "' where SensorID=" + outPara.Value.ToString();
                    MySQLDB.ExecuteNonQry(strSQL, null);

                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("218", "新增传感器失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }


        [WebMethod(Description = "通过传感器ID，获取传感器信息")]
        public static string getSensorBySensorID(string sensorid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tsensor");
                string strSQL = " Select * From tsensor where sensorid=?sensorid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?sensorid", MySqlDbType.VarChar)
                    };

                parms[0].Value = sensorid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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

        [WebMethod(Description = "修改传感器")]
        public static string updateSensorBySensorID(string sensorid, string sensorname, string sensorno,
            string sensortype, string sensorlocation,  string areaid,
            string adminuserid,   string remark)
        {
            DataSet ds = new DataSet();

            MySQLDB.InitDb();
            string strResult = "";
            bool IsDelSuccess = false;
            string strSQL = "";
            MySqlParameter[] parms = null;

            try
            {

                strSQL = " UPDATE tsensor " +
                          " SET SensorName = ?SensorName, " +
                            " SensorNo = ?SensorNo, " +
                            " SensorType = ?SensorType, " +
                            " SensorLocation = ?SensorLocation, " +
                            " AreaID = ?AreaID, " +
                            " AdminUserID = ?AdminUserID, " +
                       
                            " Remark = ?Remark  " +
                            " WHERE SensorID =?SensorID;";



                parms = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?SensorName", MySqlDbType.VarChar),
                                         new MySqlParameter("?SensorNo", MySqlDbType.VarChar),
                                         new MySqlParameter("?SensorType", MySqlDbType.VarChar),
                                         new MySqlParameter("?SensorLocation", MySqlDbType.VarChar),
                                         new MySqlParameter("?AreaID", MySqlDbType.Int32),
                                         new MySqlParameter("?AdminUserID", MySqlDbType.Int32),
                                  
                                         new MySqlParameter("?Remark", MySqlDbType.VarChar),
                                         new MySqlParameter("?SensorID", MySqlDbType.Int32),

                                     };
                parms[0].Value = sensorname;
                parms[1].Value = sensorno;
                parms[2].Value = sensortype; ;
                parms[3].Value = sensorlocation; ;
                parms[4].Value = Convert.ToInt32(areaid);
                parms[5].Value = Convert.ToInt32(adminuserid);
           
                parms[6].Value = remark;
                parms[7].Value = Convert.ToInt32(sensorid);


                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parms);
                if (IsDelSuccess)
                    return PublicMethod.OperationResultJson("0", "ok");
                else
                {

                    return PublicMethod.OperationResultJson("219", "更新传感器失败");
                }

            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }


        }
        //删除水表
        [WebMethod(Description = "删除传感器")]
        public static string deleteSensorBySensorID(string sensorid)
        {
            DataSet ds = new DataSet();
            MySQLDB.InitDb();

            bool IsDelSuccess = false;
            string strSQL = "";
            string strResult = "";
            MySqlParameter[] parmss = null;

            strSQL = " Update tsensor set deleteflag =1  WHERE sensorid=?sensorid;";

            try
            {
                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?sensorid", MySqlDbType.Int32),
                                     };
                parmss[0].Value = Convert.ToInt32(sensorid);
                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

                if (IsDelSuccess)
                    return PublicMethod.OperationResultJson("0", "ok");
                else
                {

                    return PublicMethod.OperationResultJson("220", "删除传感器失败");
                }
            }
            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }


  

        #endregion 传感器相关的方法

        #region 传感器配对相关的方法
        [WebMethod(Description = "添加传感器配对")]
        public static string addSensorMatch(string sensoraid,
                    string sensorbid)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dsSensor");
            string strResult = "";

            MySqlParameter[] parms = null;

            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inSensorAID", MySqlDbType.Int32),
                        new MySqlParameter("?inSensorBID", MySqlDbType.Int32),
                    };
            parms[0].Value = Convert.ToInt32(sensoraid);
            parms[1].Value = Convert.ToInt32(sensorbid);


            MySqlParameter outPara = new MySqlParameter("?outSensorMatchID", MySqlDbType.Int32);
            outPara.Direction = ParameterDirection.Output;
            try
            {

                strResult = MySQLDB.ExecuteStoredProc("paddSensorMatch", parms, outPara);
                if (Convert.ToInt32(strResult) > 0)
                {
                    return PublicMethod.OperationResultJson("0", "ok");
                }
                else
                {
                    return PublicMethod.OperationResultJson("221", "新增传感器配对失败");
                }
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

        }

        [WebMethod(Description = "通过传感器ID，获取传感器配对信息")]
        public static string getSensorMatchBySensorID(string sensorid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tsensor");
                //string strSQL = " Select * From tsensor where sensorid=?sensorid; ";

                string strSQL = "select SENSORBID SensorID,s.SensorName " +
                   " from tSensormatch sm,tSensor s " +
                     " where sm.SensorBID=S.SensorID " +
                     " and sm.sensorAID=?SensorID1" +
                     " UNION " +
                     " select SENSORAID SensorID,s.SensorName " +
                     " from tSensormatch sm,tSensor s " +
                     " where sm.SensorAID=S.SensorID " +
                     "and sm.sensorBID=?SensorID2";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?SensorID1", MySqlDbType.Int32),
                        new MySqlParameter("?SensorID2", MySqlDbType.Int32)
                    };

                parms[0].Value = sensorid;
                parms[1].Value = sensorid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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


        [WebMethod(Description = "获取某区域所有的传感器")]

        public static string getSensorsByAreaID(string AreaID)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tsensor");
                //string strSQL = " Select p.* ,pl.* "+
                //                " From tPipe p,tpipelocation pl "+
                //                   " where p.PipeID=pl.PipeID "+
                //                    " and p.AreaID=?areaid ";
                string strSQL = " select A.*,B.UserName from tsensor A left join tuser B  on A.AdminUserID=B.UserID where  A.AreaID=" + AreaID + " and A.DeleteFlag=0  ";
              

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
                    };
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


        [WebMethod(Description = "获取某区域所有的消防栓 ,包括了下级所有的")]
        public static string getFireplugsByAreaIDIncludeSubAreas(string areaid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet dsarea = new DataSet("tarea");
                DataSet ds = new DataSet("twatermeter");
                //string strAreaSQL = "  select GROUP_CONCAT(AreaID) AS AreaIDs  from tarea  where parentareaid=?parentareaid  ";
                string strAreaSQL = "  select PreStr from tarea where areaid=?areaid  ";

                MySqlParameter[] parmsarea = null;
                parmsarea = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid", MySqlDbType.Int32)
                    };

                parmsarea[0].Value = areaid;

                dsarea = MySQLDB.SelectDataSet(strAreaSQL, parmsarea);

                string PreStr = "";
                if (dsarea != null)
                {
                    if (dsarea.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        PreStr = dsarea.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        PreStr = "";
                    }
                }


                string strSQL = "Select p.* ,a.AreaName  " +
                    " From tdevice  p  ,tarea a " +
                   "  where p.AreaID=a.AreaID " +
                    " and a.PreStr like '";
                strSQL = strSQL + PreStr + "%'" + " and p.deleteflag=0 and TypeFlag=2";

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


        [WebMethod(Description = "获取某区域所有的消防栓 ,包括了下级所有的")]
        public static string getValvesByAreaIDIncludeSubAreas(string areaid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet dsarea = new DataSet("tarea");
                DataSet ds = new DataSet("twatermeter");
                //string strAreaSQL = "  select GROUP_CONCAT(AreaID) AS AreaIDs  from tarea  where parentareaid=?parentareaid  ";
                string strAreaSQL = "  select PreStr from tarea where areaid=?areaid  ";

                MySqlParameter[] parmsarea = null;
                parmsarea = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid", MySqlDbType.Int32)
                    };

                parmsarea[0].Value = areaid;

                dsarea = MySQLDB.SelectDataSet(strAreaSQL, parmsarea);

                string PreStr = "";
                if (dsarea != null)
                {
                    if (dsarea.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        PreStr = dsarea.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        PreStr = "";
                    }
                }


                string strSQL = "Select p.* ,a.AreaName  " +
                    " From tdevice  p  ,tarea a " +
                   "  where p.AreaID=a.AreaID " +
                    " and a.PreStr like '";
                strSQL = strSQL + PreStr + "%'" + " and p.deleteflag=0 and TypeFlag=1";

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


        [WebMethod(Description = "获取某区域所有的传感器,包括了下级所有的")]
        public static string getSensorsByAreaIDIncludeSubAreas(string areaid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet dsarea = new DataSet("tarea");
                DataSet ds = new DataSet("twatermeter");
                //string strAreaSQL = "  select GROUP_CONCAT(AreaID) AS AreaIDs  from tarea  where parentareaid=?parentareaid  ";
                string strAreaSQL = "  select PreStr from tarea where areaid=?areaid  ";

                MySqlParameter[] parmsarea = null;
                parmsarea = new MySqlParameter[]
                    {
                        new MySqlParameter("?areaid", MySqlDbType.Int32)
                    };

                parmsarea[0].Value = areaid;

                dsarea = MySQLDB.SelectDataSet(strAreaSQL, parmsarea);

                string PreStr = "";
                if (dsarea != null)
                {
                    if (dsarea.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        PreStr = dsarea.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        PreStr = "";
                    }
                }


                string strSQL = "select sub.*,u.UserName  from (Select p.* ,a.AreaName  " +
                    " From tsensor  p  ,tarea a " +
                   "  where p.AreaID=a.AreaID " +
                    " and a.PreStr like '";
                strSQL = strSQL + PreStr + "%'" + " and p.deleteflag=0) sub " +
                                " left join tuser u on sub.AdminUserID=u.UserID ";

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

        #endregion 传感器配对相关的方法

        #region 传感器历史数据分析的方法
        [WebMethod(Description = "获取传感器历史数据分析查询列表")]
        public static string getSensorListByDate(string sensoraid, string sensorbid, string begincaptime, string endcaptime)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                //string strSQL = "   SELECT SensorAnalyzeID,SensorAID,SensorBID,AnalyzeDate " +
                //                " FROM tsensoranalyze " +
                //                " where (SensorAID=@SensorAID and SensorBID=@SensorBID) " +
                //                " OR  (SensorAID=@SensorBID and SensorBID=@SensorAID) " +
                //                " AND AnalyzeDate BETWEEN @Stime AND @Etime;";

                string strSQL = "SELECT SensorAnalyzeID,SensorAID,s.SensorName SensorAName,AnalyzeDate,s2.SensorName SensorBName,LeakMark,case LeakMark " +
                            " WHEN 0 THEN '正常' " +
                            " else  '异常' " +
                            " end  as LeakMarkDesc,sa.ProcessResult,sa.ProcessDescription " +
                            " FROM tsensoranalyze sa,tsensor s,tsensor s2 " +
                            " where s.SensorID=sa.SensorAID " +
                            " and  s2.SensorID=sa.SensorBID " +
                            " and SensorAID=@SensorAID and SensorBID=@SensorBID " +
                            " AND AnalyzeDate BETWEEN @Stime AND @Etime " +
                            " UNION " +
                            " SELECT SensorAnalyzeID,SensorAID,s.SensorName SensorAName,AnalyzeDate,s2.SensorName SensorBName,LeakMark,case LeakMark  " +
                            " WHEN 0 THEN '正常' " +
                            " else  '异常' " +
                              " end  as LeakMarkDesc,sa.ProcessResult,sa.ProcessDescription " +
                            " FROM tsensoranalyze sa,tsensor s,tsensor s2 " +
                            " where s.SensorID=sa.SensorAID " +
                            " and  s2.SensorID=sa.SensorBID " +
                            " and SensorAID=@SensorBID and SensorBID=@SensorAID" +
                            " AND AnalyzeDate BETWEEN @Stime AND @Etime ;";

                //string strSQL = " Select * From twatermeter where watermeterid=?watermeterid; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?SensorAID", MySqlDbType.Int32),
                        new MySqlParameter("?SensorBID", MySqlDbType.Int32),
                        new MySqlParameter("?Stime", MySqlDbType.Datetime),
                        new MySqlParameter("?Etime", MySqlDbType.Datetime)
                    };

                parms[0].Value = Convert.ToInt32(sensoraid);
                parms[1].Value = Convert.ToInt32(sensorbid);
                parms[2].Value = Convert.ToDateTime(begincaptime);
                parms[3].Value = Convert.ToDateTime(endcaptime);



                ds = MySQLDB.SelectDataSet(strSQL, parms);
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


        [WebMethod(Description = "获取传感器分析结果的详情")]
     
        public static string getCurrentSensorAnalyzeDetailBySensorID(string sensoraid,string sensorbid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                string strSQL = "   SELECT SensorAnalyzeID,SensorAID,SensorBID,SensorAData,SensorBData,AnalyzeDate,LeakPosition,LeakMark,AnalyzeResult,ProcessResult,ProcessDescription " +
                                " FROM tsensoranalyze " +
                                " where SensorAID=?SensorAID and SensorBID=?SensorBID";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?SensorAID", MySqlDbType.Int32),
                       new MySqlParameter("?SensorBID", MySqlDbType.Int32)
                    };

                parms[0].Value = Convert.ToInt32(sensoraid);
                parms[1].Value = Convert.ToInt32(sensorbid);

                string json = "";

                ds = MySQLDB.SelectDataSet(strSQL, parms);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        json= PublicMethod.TableToJson(ds.Tables[0]);

                        return json;
                    }
                    else
                    {
                        return PublicMethod.OperationResultJson("99", "数据为空");
                    };
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



        [WebMethod(Description = "获取传感器分析结果的详情")]
        public static string getSensorAnalyzeDetailBySensorAnalyzeID(string sensoranalyzeid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("twatermeterhistory");
                string strSQL = "   SELECT SensorAnalyzeID,SensorAID,SensorBID,SensorAData,SensorBData,AnalyzeDate,LeakPosition,LeakMark,AnalyzeResult,ProcessResult,ProcessDescription " +
                                " FROM tsensoranalyze " +
                                " where SensorAnalyzeID=@SensorAnalyzeID;";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?SensorAnalyzeID", MySqlDbType.Int32)
                    };

                parms[0].Value = Convert.ToInt32(sensoranalyzeid);



                ds = MySQLDB.SelectDataSet(strSQL, parms);
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
                    }; 
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

        #endregion 传感器历史数据分析的方法
        #region 管线位置相关的方法
        [WebMethod(Description = "添加管线位置,每组以@“隔开")]
        public static string addPipeLocation(string location,string areaid)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dspipe");
            string strResultPipeID = "";
            string strResult = "";


            //*****************
            MySqlParameter[] parmspipe = null;


            parmspipe = new MySqlParameter[]
                    {
                        new MySqlParameter("?inPipeName", MySqlDbType.VarChar),
                        new MySqlParameter("?inPipeNo", MySqlDbType.VarChar),
                        new MySqlParameter("?inAreaID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeMaterialID", MySqlDbType.Int32),
                        new MySqlParameter("?inPipeSize", MySqlDbType.Float),
                        new MySqlParameter("?inPipeDepth", MySqlDbType.Float),
                        new MySqlParameter("?inPipeLength", MySqlDbType.Float),
                        new MySqlParameter("?inStartLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inEndLocation", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdjoinPipeIDs", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdjoinPipeNames", MySqlDbType.VarChar),
                        new MySqlParameter("?inAdminiUserID", MySqlDbType.Int32),
                        new MySqlParameter("?inRemark", MySqlDbType.VarChar),
                    };
            parmspipe[0].Value = "";
            parmspipe[1].Value = "";
            parmspipe[2].Value = Convert.ToInt32(areaid);
            parmspipe[3].Value = -1; ;
            parmspipe[4].Value = 0; ;
            parmspipe[5].Value = 0; ;
            parmspipe[6].Value = 0;
            parmspipe[7].Value = "";
            parmspipe[8].Value = "";
            parmspipe[9].Value = "";
            parmspipe[10].Value = "";
            parmspipe[11].Value = -1;
            parmspipe[12].Value = "";

            MySqlParameter outParaPipeID = new MySqlParameter("?outPipeID", MySqlDbType.Int32);
            outParaPipeID.Direction = ParameterDirection.Output;
            try
            {

                strResultPipeID = MySQLDB.ExecuteStoredProc("paddPipe", parmspipe, outParaPipeID);
                //if (Convert.ToInt32(strResultPipeID) > 0)
                //{
                //    ;
                //}
                //else
                //{
                //    strResultPipeID = "-1";
                //}
            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

            //*****************
            MySqlParameter[] parms = null;

            //string strtest = "114.45,23.46@114.56,23.47@115.32,34.57";
            string[] arrlocation = location.Split('@');


            for (int i = 0; i < arrlocation.Length; i++)
            {
                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inPipeID", MySqlDbType.Int32),
                        new MySqlParameter("?inOrderNumber", MySqlDbType.Int32),
                        new MySqlParameter("?inLocation", MySqlDbType.VarChar)
                    };
                parms[0].Value = Convert.ToInt32(strResultPipeID);
                parms[1].Value = Convert.ToInt32(i);
                parms[2].Value = arrlocation[i];

                MySqlParameter outPara = new MySqlParameter("?outPipeLocationID", MySqlDbType.Int32);
                outPara.Direction = ParameterDirection.Output;
                try
                {

                    strResult = MySQLDB.ExecuteStoredProc("paddPipeLocation", parms, outPara);
                    //if (Convert.ToInt32(strResult) > 0)
                    //{
                    //    return PublicMethod.OperationResultJson("0", "ok");
                    //}
                    //else
                    //{
                    //    return PublicMethod.OperationResultJson("217", "新增管线位置失败");
                    //}
                }

                catch (Exception ex)
                {
                    return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                }
            }

            return PublicMethod.DataTableJsonOnlyOneColumn("PipeID", strResultPipeID);
            //return PublicMethod.OperationResultJson("0", "ok");
        }

        [WebMethod(Description = "修改管线位置,每组以@“隔开")]
        public static string updatePipeLocation(string pipeid,string location)
        {
            MySQLDB.InitDb();
            DataSet ds = new DataSet("dspipe");
            string strResultPipeID = "";
            string strResult = "";
            string strSQL = "";
            MySqlParameter[] parmss = null;
            bool IsDelSuccess = false;

            //*****************

            try
            {

                strSQL = " delete from  tpipelocation  WHERE pipeid=?pipeid;";


                parmss = new MySqlParameter[]
                                     {
                                         new MySqlParameter("?pipeid", MySqlDbType.Int32)
                                     };
                parmss[0].Value = Convert.ToInt32(pipeid);

                IsDelSuccess = MySQLDB.ExecuteNonQry(strSQL, parmss);

            }

            catch (Exception ex)
            {
                return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
            }

            //*****************
            MySqlParameter[] parms = null;

            //string strtest = "114.45,23.46@114.56,23.47@115.32,34.57";
            string[] arrlocation = location.Split('@');


            for (int i = 0; i < arrlocation.Length; i++)
            {
                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?inPipeID", MySqlDbType.Int32),
                        new MySqlParameter("?inOrderNumber", MySqlDbType.Int32),
                        new MySqlParameter("?inLocation", MySqlDbType.VarChar)
                    };
                parms[0].Value = Convert.ToInt32(pipeid);
                parms[1].Value = Convert.ToInt32(i);
                parms[2].Value = arrlocation[i];

                MySqlParameter outPara = new MySqlParameter("?outPipeLocationID", MySqlDbType.Int32);
                outPara.Direction = ParameterDirection.Output;
                try
                {

                    strResult = MySQLDB.ExecuteStoredProc("paddPipeLocation", parms, outPara);
                    //if (Convert.ToInt32(strResult) > 0)
                    //{
                    //    return PublicMethod.OperationResultJson("0", "ok");
                    //}
                    //else
                    //{
                    //    return PublicMethod.OperationResultJson("217", "新增管线位置失败");
                    //}
                }

                catch (Exception ex)
                {
                    return PublicMethod.getResultJson(ErrorCodeDefinition.DB_ERROR, ErrorCodeDefinition.getErrorMessageByErrorCode(ErrorCodeDefinition.DB_ERROR));//数据库异常
                }
            }


            return PublicMethod.OperationResultJson("0", "ok");
        }



      




        [WebMethod(Description = "获取某传感器配对的传感器列表")]
        public static string getMatchedSensors(string sensorid)
        {
            MySQLDB.InitDb();

            string strSQL = " Select A.*,B.PrePipeID from tsensor A ,tpipe B  where A.SensorID=?SensorID and A.PipeID=B.PipeID and B.DeleteFlag=0";

            MySqlParameter[] parms = null;


            parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?SensorID", MySqlDbType.Int32)
                    };

            parms[0].Value = Convert.ToInt32(sensorid);
            DataSet  ds = MySQLDB.SelectDataSet(strSQL, parms);
           
                if (ds.Tables[0].Rows.Count > 0)
                // 有数据集
                {
                    string pipeid = ds.Tables[0].Rows[0]["PipeID"].ToString();
                    string isSecondNode = ds.Tables[0].Rows[0]["SencondHead"].ToString();
                    string prepipe = ds.Tables[0].Rows[0]["PrePipeID"].ToString();
                    MySqlParameter[] parms2 = null;


                    parms2 = new MySqlParameter[]
                    {
                        new MySqlParameter("?prepipeid", MySqlDbType.Int32),
                         new MySqlParameter("?pipeid2", MySqlDbType.Int32)
                    };

                    parms2[0].Value = Convert.ToInt32(pipeid);
                    parms2[1].Value = Convert.ToInt32(prepipe);

                    if (isSecondNode == "1")
                    {
                        DataSet ds2 = MySQLDB.SelectDataSet("select * from tsensor where ( PipeID in (select PipeID from tpipe where PrePipeID=?prepipeid ) or PipeID=?pipeid2 ) and SencondHead=1", parms2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            return PublicMethod.DataTableToJson(ds2.Tables[0]);

                        }
                        else
                        {
                            return PublicMethod.OperationResultJson("99", "数据为空");
                        }; 
                    }
                    else
                    {
                        return PublicMethod.OperationResultJson("99", "数据为空");
                    }
                      
                   
                }
                else
                {
                    return PublicMethod.OperationResultJson("99", "数据为空");
                };
          
        }





        [WebMethod(Description = "通过管线ID，获取管线位置信息")]
        public static string getPipeLocationByPipeID(string pipeid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipe");
                //string strSQL = " Select p.* ,pl.* "+
                //                " From tPipe p,tpipelocation pl "+
                //                   " where p.PipeID=pl.PipeID "+
                //                    " and p.AreaID=?areaid ";
                string strSQL = " Select p.*  " +
                                " From tPipeLocation p  " +
                                " where p.PipeID=?PipeID order by ordernumber; ";

                MySqlParameter[] parms = null;


                parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?PipeID", MySqlDbType.Int32)
                    };

                parms[0].Value = pipeid;

                ds = MySQLDB.SelectDataSet(strSQL, parms);
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
                    }; 
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


         #endregion 管线位置相关的方法

        [WebMethod(Description = "通过区域ID，获取管道历史异常统计报表,-1是所有区域的")]
        public static string getPipeLeakReportByAreaID(string areaid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipe");
                string strArea = "";
                if (areaid != "-1")
                {
                    strArea = " and p.AreaID=" + areaid;
                }
                string strSQL = " select  plr.*,p.PipeName " +
                                " from  tpipeleakreport plr,tpipe p " +
                                          " where plr.PipeID=p.PipeID " + strArea + " order by plr.PipeID ";
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


        [WebMethod(Description = "通过管道ID，获取管道上提示信息")]
        public static string getPipeTips(string pipeid)
        {
            MySQLDB.InitDb();
            try
            {
                DataSet ds = new DataSet("tpipe");
                DataSet ds2 = new DataSet("tpipe2");
                DataSet dslast = new DataSet("tpipelast");
                string strArea = "";

                string strSQL = "  SELECT PrePipeID FROM dbvpipe.tpipe where PipeID= " + pipeid;
                string strPrePipeID = "";
                string PipeIDs = "";
                ds = MySQLDB.SelectDataSet(strSQL, null);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    // 有数据集
                    {
                        strPrePipeID = ds.Tables[0].Rows[0][0].ToString();
                        PipeIDs = "(" + pipeid + "," + strPrePipeID + ")"; //找到了两个管道ID

                        string strSQLpipe = " SELECT sensorid FROM dbvpipe.tsensor where PipeID in " + PipeIDs;
                        ds2 = MySQLDB.SelectDataSet(strSQLpipe, null);

                        string strSensorAID = "-1";
                        string strSensorBID = "-1";
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            strSensorAID = ds2.Tables[0].Rows[0][0].ToString();
                            strSensorBID = ds2.Tables[0].Rows[1][0].ToString();//找到了两个传感器ID
                        }


                        string strSQLLast = "SELECT SensorAnalyzeID,SensorAID,s.SensorName SensorAName,AnalyzeDate,s2.SensorName SensorBName,LeakMark,sa.ProcessResult,sa.ProcessDescription " +
                         " FROM tsensoranalyze sa,tsensor s,tsensor s2 " +
                         " where s.SensorID=sa.SensorAID " +
                         " and  s2.SensorID=sa.SensorBID " +
                         " and SensorAID=@SensorAID and SensorBID=@SensorBID " +
                         " UNION " +
                         " SELECT SensorAnalyzeID,SensorAID,s.SensorName SensorAName,AnalyzeDate,s2.SensorName SensorBName,LeakMark,sa.ProcessResult,sa.ProcessDescription " +
                         " FROM tsensoranalyze sa,tsensor s,tsensor s2 " +
                         " where s.SensorID=sa.SensorAID " +
                         " and  s2.SensorID=sa.SensorBID " +
                         " and SensorAID=@SensorBID and SensorBID=@SensorAID  order by SensorAnalyzeID desc limit 1";

                        MySqlParameter[] parms = null;


                        parms = new MySqlParameter[]
                    {
                        new MySqlParameter("?SensorAID", MySqlDbType.Int32),
                        new MySqlParameter("?SensorBID", MySqlDbType.Int32),
                    };

                        parms[0].Value = Convert.ToInt32(strSensorAID);
                        parms[1].Value = Convert.ToInt32(strSensorBID);

                        dslast = MySQLDB.SelectDataSet(strSQLLast, parms);

                        if (dslast != null)
                        {
                            if (dslast.Tables[0].Rows.Count > 0)
                            // 有数据集
                            {
                                return PublicMethod.DataTableToJson(dslast.Tables[0]);

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