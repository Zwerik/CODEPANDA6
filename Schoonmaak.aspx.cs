using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Schoonmaak : System.Web.UI.Page
    {
        private Service service;

        protected void Page_Load(object sender, EventArgs e)
        {
            service = new Service();
            
            rptClean.DataSource = service.Cleans;
            rptClean.DataBind();
        }

        protected void btAdjustCln_Click(object sender, EventArgs e)
        {

        }
    }