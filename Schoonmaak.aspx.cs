using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Schoonmaak : System.Web.UI.Page
    {
        private Service service;
        private Clean selectedClean;

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

            rptClean.DataSource = service.Repairs;
            rptClean.DataBind();

            if (Request.QueryString["query"] != null)
            {
                selectedClean = service.GetClean(Convert.ToInt32(Request.QueryString["query"]));

                tbTramNrClean.Text = Convert.ToString(selectedClean.Tram.TramNr);
                ddlUserClean.SelectedItem.Text = selectedClean.User.ToString();
                ddlStatusClean.SelectedItem.Text = "SCHOONMAAK";
            }

            if (!IsPostBack)
            {
                foreach (User user in service.Users)
                {
                    ddlUserClean.Items.Add(user.Name);
                }

                for(int i = 0; i < 6; i++)
                {
                    ddlDateClean.Items.Add(DateTime.Now.AddDays(i).ToString());
                }
            }
        }

        protected void btAdjustCln_Click(object sender, EventArgs e)
        {
            if (selectedClean == null)
            {
                DateTime date = Convert.ToDateTime(ddlDateClean.SelectedItem.Text);
                Tram tram = service.GetTram(Convert.ToInt32(tbTramNrClean.Text));
                User user = null;
                foreach (User u in service.Users)
                {
                    if (u.Name == ddlUserClean.SelectedItem.Text)
                    {
                        user = u;
                        break;
                    }
                }

                selectedClean = new Clean(0, date, " - ", tram, user);

                service.InsertClean(selectedClean);
            }
            else
            {
                int cleanId = selectedClean.Id;
                DateTime date = Convert.ToDateTime(ddlDateClean.SelectedItem.Text);
                Tram tram = service.GetTram(Convert.ToInt32(tbTramNrClean.Text));
                User user = null;
                foreach (User u in service.Users)
                {
                    if (u.Name == ddlUserClean.SelectedItem.Text)
                    {
                        user = u;
                        break;
                    }
                }

                selectedClean = new Clean(cleanId, date, " - ", tram, user);
                service.UpdateClean(selectedClean);
            }
        }
    }