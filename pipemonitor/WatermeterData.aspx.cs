using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pipemonitor
{
    public partial class WatermeterData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              this.watermeterIdValue.Value = Request.QueryString["watermeter"].ToString();
             
            }
        }
    }
}