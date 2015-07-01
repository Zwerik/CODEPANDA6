using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Beheer : System.Web.UI.Page
{
    private Remise rmanager;
    private List<PanelControl> pnlcontrols;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["Logged_In"] as User == null)
                Response.Redirect("~/Login.aspx");
            else
            {
                User user = Session["Logged_In"] as User;
                lblCurrentlyLoggedIn.Text = "Ingelogd: " + user.Name + " Functie: " + user.Functie_.ToString();
            }
        }
        List<Control> Panels = new List<Control>();

        foreach(Control ctrl in EnumerateControlsRecursive(Page))
        {
            if(ctrl is Panel)
            {
                Panels.Add(ctrl);
            }
        }
        pnlcontrols = new List<PanelControl>();
        int i=0;
        foreach(Panel c in Panels)
        {
            PanelControl pnlctrl = new PanelControl(c.ID);
            c.Controls.Add(pnlctrl.Panel_);
            pnlcontrols.Add(pnlctrl);
            i++;
        }

        rmanager = new Remise();
        rmanager.FetchTrams();
        rmanager.FetchTracks();
        //rmanager.GenerateSectors();
        rmanager.InitialisePanelControls(pnlcontrols);

        RebindResources();

        if (Session["ALERT"] != null)
        {
            string alert = Session["ALERT"] as string;
            Response.Write(alert);
            Session["ALERT"] = null;
        }
        
    }

    private void RebindResources()
    {
        //Drop down lists:
        int idx = ddlTrams.SelectedIndex;
        ddlTrams.DataValueField = "Id";
        ddlTrams.DataTextField = "TramNr";
        ddlTrams.DataSource = rmanager.Trams;
        ddlTrams.DataBind();
        if (idx != -1)
            ddlTrams.SelectedIndex = idx;

        idx = ddlTracks.SelectedIndex;
        ddlTracks.DataValueField = "Id";
        ddlTracks.DataTextField = "TrackNr";
        ddlTracks.DataSource = rmanager.Tracks;
        ddlTracks.DataBind();
        if (idx != -1)
            ddlTracks.SelectedIndex = idx;

        /*
        PanelControl tr38 = new PanelControl();
        pnl38.Controls.Add(tr38.Panel_);
         */
    }
    IEnumerable<Control> EnumerateControlsRecursive(Control parent)
    {
        foreach (Control child in parent.Controls)
        {
            yield return child;
            foreach (Control descendant in EnumerateControlsRecursive(child))
                yield return descendant;
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) //Plaats/Verwijder
    {
        if(ddlPlaats.SelectedIndex != -1)
        {
            if(ddlPlaats.SelectedIndex == 0)
            {
                ddlTracks.Enabled = true;
                tbSector.Enabled = true;
                btnPlaatsTram.Text = "(Ver) Plaats";
                ddlTrams.Enabled = true;
            }
            if(ddlPlaats.SelectedIndex == 1)
            {
                ddlTracks.Enabled = false;
                tbSector.Enabled = false;
                btnPlaatsTram.Text = "Verwijder";
            }
            if(ddlPlaats.SelectedIndex == 2)
            {
                ddlTrams.Enabled = false;
                ddlTracks.Enabled = true;
                tbSector.Enabled = true;
                btnPlaatsTram.Text = "(De)Blokkeer";
            }
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e) //Trams
    {

    }
    protected void ddlTracks_SelectedIndexChanged(object sender, EventArgs e) //Tracks
    {

    }
    protected void btnPlaatsTram_Click(object sender, EventArgs e)
    {
        if(ddlPlaats.SelectedIndex == 0) //(ver)plaats
        {
            int secnr = -1;

            try
            {
                secnr = Convert.ToInt32(tbSector.Text);
            }
            catch
            {
                Response.Write("<script>alert('Voer alleen numerieke waarden in bij sector!')</script>");
                return;
            }

            int tramid = Convert.ToInt32(ddlTrams.SelectedValue);
            int trackid = Convert.ToInt32(ddlTracks.SelectedValue);

            Track trck = rmanager.Tracks.Find(p => p.Id == trackid);
            Tram trm = rmanager.Trams.Find(p => p.Id == tramid);
            if(trck == null || trm == null)
            {
                Response.Write("<script>alert('Kan track of tram niet vinden!')</script>");
                return;
            }
            Sector sec = trck.Sectors.Find(p => p.SectorNr == secnr);
            if(sec == null)
            {
                Response.Write("<script>alert('Invalide sector nummer!')</script>");
                return;
            }
            if(sec.Blocked)
            {
                Response.Write("<script>alert('Sector is geblokkeerd! Hef eerst de blokkade op.')</script>");
                return;
            }
            if(sec.tram != null)
            {
                Response.Write("<script>alert('Plek is al bezet!')</script>");
                return;
            }
            if (!rmanager.CanBeExited(sec))
            {
                Response.Write("<script>alert('Invalide plek voor tram! Controleer of de inrit vrij is!')</script>");
                return;
            }
            Sector secwithtram = rmanager.FindPlacedTram(trm);
            if(secwithtram == null)
            {
                string alert = "<script>alert('Tram toegevoegd! Tram: " + trm.TramNr + " T:" + trck.TrackNr + " S:" + sec.SectorNr + "')</script>";
                Session["ALERT"] = alert;
                sec.AddTram(trm);
                rmanager.UpdateSector(sec);
                //Response.Write(alert);
                Response.Redirect("~/Beheer.aspx");
                //Server.Transfer("~/Beheer.aspx");
            }
            else
            {
                if(sec.Id == secwithtram.Id)
                {
                    Response.Write("<script>alert('Tram staat al op deze plaats!')</script>");
                    return;
                }
                if (!rmanager.CanBeExited(secwithtram))
                {
                    Response.Write("<script>alert('Invalide plek om naar te verplaatsen! controleer of de uitrit vrij is!')</script>");
                    return;
                }
                secwithtram.RemoveTram();
                rmanager.UpdateSector(secwithtram);
                sec.AddTram(trm);
                rmanager.UpdateSector(sec);
                string alert = "<script>alert('Tram: "+trm.TramNr+" Verplaatst van: T:"+secwithtram.Track.TrackNr+" S:"+secwithtram.SectorNr+" Naar T:" + trck.TrackNr + " S:"+ sec.SectorNr+"')</script>";
                Session["ALERT"] = alert;
                //Response.Write(alert);
                Response.Redirect("~/Beheer.aspx");
                //Server.Transfer("~/Beheer.aspx");
            }


        }
        else if(ddlPlaats.SelectedIndex == 1) //Verwijder
        {
            int tramid = Convert.ToInt32(ddlTrams.SelectedValue);
            Tram trm = rmanager.Trams.Find(p => p.Id == tramid);
            if(trm == null)
            {
                Response.Write("<script>alert('Tram kan niet gevonden worden!')</script>");
                return;
            }
            Sector sec = rmanager.FindPlacedTram(trm);
            if(sec == null)
            {
                Response.Write("<script>alert('Tram is nog nergens geplaatst!')</script>");
                return;
            }
            if(!rmanager.CanBeExited(sec))
            {
                Response.Write("<script>alert('Uitrit niet vrij voor deze tram!')</script>");
                return;
            }
            else
            {
                sec.RemoveTram();
                rmanager.UpdateSector(sec);
                string alert = "<script>alert('Tram: " + trm.TramNr + " Verwijderd van: T:" + sec.Track.TrackNr + " S:" + sec.SectorNr + "')</script>";
                Session["ALERT"] = alert;
                Response.Redirect("~/Beheer.aspx");
            }
        }
        else if(ddlPlaats.SelectedIndex == 2) //(De) Blokkeer
        {
            int secnr = -1;
            try
            {
                secnr = Convert.ToInt32(tbSector.Text);
            }
            catch
            {
                Response.Write("<script>alert('Voer alleen numerieke waarden in bij sector!')</script>");
                return;
            }
            int trackid = Convert.ToInt32(ddlTracks.SelectedValue);

            Track trck = rmanager.Tracks.Find(p => p.Id == trackid);
            if (trck == null)
            {
                Response.Write("<script>alert('Kan track of tram niet vinden!')</script>");
                return;
            }
            Sector sec = trck.Sectors.Find(p => p.SectorNr == secnr);
            if (sec == null)
            {
                Response.Write("<script>alert('Invalide sector nummer!')</script>");
                return;
            }
            if(sec.tram != null)
            {
                Response.Write("<script>alert('Kan niet (de) blokkeren! Plek wordt bezet door een tram!')</script>");
                return;
            }
            if(!rmanager.CanBeExited(sec) && sec.Blocked)
            {
                Response.Write("<script>alert('Kan niet deblokkeren! De uitrit voor deze plek is niet vrij!')</script>");
                return;
            }
            if(sec.Blocked)
            {
                sec.Blocked = false;
                rmanager.UpdateSector(sec);
            }
            else
            {
                sec.Blocked = true;
                rmanager.UpdateSector(sec);
            }
            string alert = "<script>alert('Spoor ge(de)blokkeerd op: Track: " + trck.TrackNr + " S:" + sec.SectorNr + "')</script>";
            Session["ALERT"] = alert;
            Response.Redirect("~/Beheer.aspx");
        }
    }
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session["Logged_In"] = null;
        Response.Redirect("~/Login.aspx");
    }
    protected void btnToRepa_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reparatie.aspx");
    }
    protected void btnToService_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Schoonmaak.aspx");
    }
}