using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for PanelControl
/// </summary>
public class PanelControl
{
        public Track Track_ {get; set;}
        public Label InfoLabel { get; set; }
        public ListBox Sectors {get; set;}
        public ListBox SectorInfo {get; set;}

        public Button GetInfo { get; set; }


        public string PanelID { get; set; }

        public Panel Panel_ {get; set;}
        public PanelControl(string panelid)
        {
            PanelID = panelid;


            Panel_ = new Panel();
            Panel_.Controls.Add(new LiteralControl("<br />"));
            InfoLabel = new Label();
            InfoLabel.Text = "Track: " + "###" + " Sectors:";
            Panel_.Controls.Add(InfoLabel);
            Panel_.Controls.Add(new LiteralControl("<br />"));

            Sectors = new ListBox();
            Sectors.Width = 110;
            Sectors.Height = 135;
            Panel_.Controls.Add(Sectors);
            Panel_.Controls.Add(new LiteralControl("<br />"));

            /*
            Label lbl2 = new Label();
            lbl2.Text = "Sector info:";
            Panel_.Controls.Add(lbl2);
            Panel_.Controls.Add(new LiteralControl("<br />"));

            //Show info button:
            
            GetInfo = new Button();
            GetInfo.Text = "Toon info";
            GetInfo.Click += GetInfo_Click;
            Panel_.Controls.Add(GetInfo);
            Panel_.Controls.Add(new LiteralControl("<br />"));
            */
            /*
            SectorInfo = new ListBox();
            SectorInfo.Width = 100;
            SectorInfo.Height = 50;
            Panel_.Controls.Add(SectorInfo);
            Panel_.Controls.Add(new LiteralControl("<br />"));
            */

            /*Label lblspaceholder = new Label();
            lblspaceholder.Text = " ";
            Panel_.Controls.Add(lblspaceholder);*/
        }

        void GetInfo_Click(object sender, EventArgs e)
        {
            
        }

        public void AddTrack(Track trck)
        {
            Track_ = trck;
            Sectors.DataSource = null;
            //Sectors.Height = (137 / 8) * Track_.Sectors.Count;
            Sectors.DataSource = Track_.Sectors;
            Sectors.DataBind();
        }
}