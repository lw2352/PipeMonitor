using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pipemonitor
{
    public partial class currentsensordata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {
                    Server.Transfer("signin.aspx");
                }


                this.SensorIdValue.Value = Request.QueryString["sensor"].ToString();

            }
        }
    }
}