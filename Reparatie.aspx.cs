using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reparatie : System.Web.UI.Page
{
    private Service service;
    private Repair selectedRepair;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Service"] != null)
        {
            service = (Service)Session["Service"];
        }
        else
        {
            service = new Service();
        }

        rptRepair.DataSource = service.Repairs;
        rptRepair.DataBind();

        if (Request.QueryString["query"] != null)
        {
            selectedRepair = service.GetRepair(Convert.ToInt32(Request.QueryString["query"]));

            tbTramNrRep.Text = Convert.ToString(selectedRepair.Tram.TramNr);
            tbDescrRep.Text = selectedRepair.Description;
            ddlUserRep.SelectedItem.Text = selectedRepair.User.ToString();
            ddlStatusRep.SelectedItem.Text = "REPAIR";
        }

        if (!IsPostBack)
        {
            foreach (User user in service.Users)
            {
                ddlUserRep.Items.Add(user.Name);
            }
        }
    }

    protected void btAdjustCln_Click(object sender, EventArgs e)
    {
        if (selectedRepair == null)
        {
            DateTime date = ClDateRep.SelectedDate;
            DateTime estimate = ClDateDoneRep.SelectedDate;
            Tram tram = service.GetTram(Convert.ToInt32(tbTramNrRep.Text));
            User user = null;
            string description = tbDescrRep.Text;
            foreach (User u in service.Users)
            {
                if (u.Name == ddlUserRep.SelectedItem.Text)
                {
                    user = u;
                    break;
                }
            }

            selectedRepair = new Repair(0, date, " - ", tram, user, estimate, description);

            service.InsertRepair(selectedRepair);
        }
        else
        {
            int repairId = selectedRepair.Id;
            DateTime date = ClDateRep.SelectedDate;
            DateTime estimate = ClDateDoneRep.SelectedDate;
            Tram tram = service.GetTram(Convert.ToInt32(tbTramNrRep.Text));
            User user = null;
            string description = tbDescrRep.Text;
            foreach (User u in service.Users)
            {
                if (u.Name == ddlUserRep.SelectedItem.Text)
                {
                    user = u;
                    break;
                }
            }

            selectedRepair = new Repair(repairId, date, " - ", tram, user, estimate, description);
            service.UpdateRepair(selectedRepair);
        }
    }
}