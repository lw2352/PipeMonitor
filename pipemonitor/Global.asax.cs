using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;



//!!!还是要用哈希表来存socket，数据库来存放具体信息
namespace pipemonitor
{
    public class Global : System.Web.HttpApplication
    {
        public static Net_Device NetMethod = new Net_Device();//实例化化
        //public static Form1 form1 = new Form1();//实例化化
        void Application_Start(object sender, EventArgs e)
        {
            //配置log4net
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));

            // 在应用程序启动时运行的代码
            // MySQLDB.InitDb();
            NetMethod.OpenServer();//开启端口监听服务
            //form1.OpenServer();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码
          //  MySQLDB.InitDb();
        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }

       


    }//public class Global : System.Web.HttpApplication
}//namespace pipemonitor
