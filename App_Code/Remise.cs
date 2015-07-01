using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Web.UI;
using System.Web.UI.WebControls;

public class Remise
{
    public List<Tram> Trams { get; set; }
    public List<Reservation> Reservations { get; set; }
    public List<Track> Tracks { get; set; }
    public List<PanelControl> PanelControls { get; set; }


    public Remise()
    {
        Trams = new List<Tram>();
        Tracks = new List<Track>();
    }

    public void InitialisePanelControls(List<PanelControl> panels)
    {
        PanelControls = new List<PanelControl>();
        PanelControls = panels;
        
        foreach(Track trck in Tracks) //Set info labels for all tracks
        {
            string trcknr = "p" + trck.TrackNr;
            foreach(PanelControl pc in PanelControls)
            {
                if(pc.PanelID == trcknr)
                {
                    //pc.Track_ = trck;
                    pc.AddTrack(trck);
                    pc.InfoLabel.Text = "Track: " + trck.TrackNr + " Sectors:";
                    
                    for(int i=0; i<pc.Sectors.Items.Count; i++)
                    {
                        ListItem li = pc.Sectors.Items[i];

                        if(pc.Track_.Sectors[i].Blocked)
                        {
                            li.Attributes.Add("Style", "color: red;");
                        }
                        else
                        {
                            li.Attributes.Add("Style", "color: black;");
                        }
                        if(pc.Track_.Sectors[i].tram != null)
                        {
                            li.Attributes.Add("Style", "color: navy;");
                        }
                    }
                }
            }
        }
    }

    public void FetchTrams()
    {
        Trams.Clear();
        string query = "select * from tram";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return;
        }
        else while (dbr.Read())
            {
                Trams.Add(new Tram(Convert.ToInt32(dbr["ID"].ToString()), Convert.ToInt32(dbr["Nummer"].ToString())));
            }
    }

    public void FetchTracks()
    {
        Tracks.Clear();
        string query = "select * from Spoor";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return;
        }
        else while (dbr.Read())
            {
                Track track = new Track(Convert.ToInt32(dbr["ID"].ToString()), Convert.ToInt32(dbr["Nummer"].ToString()));
                track.LinkSectors(FetchSectors(track));
                Tracks.Add(track);
            }
    }

    public List<Sector> FetchSectors(Track track)
    {
        List<Sector> sectors = new List<Sector>();
        string query = "select * from sector where \"Spoor_ID\" = " + track.Id;
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return sectors;
        }
        else while (dbr.Read())
        {
            Sector sector;
            sector = new Sector(Convert.ToInt32(dbr["ID"].ToString()), Convert.ToInt32(dbr["Nummer"].ToString()), track);
            string tramid = dbr["Tram_Id"].ToString();
            if (tramid != null && tramid != "") 
            {
                int sid = Convert.ToInt32(dbr["Tram_Id"].ToString());
                int idx = Trams.FindIndex(p => p.Id == sid);
                sector.AddTram(Trams[idx]);
            }
            int block = Convert.ToInt32(dbr["Blokkade"].ToString());
            if(block == 1)
            {
                sector.Blocked = true;
            }
            else
                sector.Blocked = false;
            sectors.Add(sector);
        }
        return sectors;
    }

    public Sector FindPlacedTram(Tram tram)
    {
        int tramid = tram.Id;
        Sector seccout = null;

        for(int i=1; i<Tracks.Count; i++)
        {
            seccout= Tracks[i].Sectors.Find(p => p.tram != null && p.tram.Id == tramid);
            if (seccout != null)
                break;
        }
        return seccout;
    }

    public bool CanBeExited(Sector sec) //Check if a spot has access to the exit of a track.
    {
        Track strck = null;
        int idx=-1;
        foreach(Track trck in Tracks)
        {
            idx = trck.Sectors.FindIndex(p => p.Id == sec.Id);
            if(idx != -1)
            {
                strck = trck;
                break;
            }
        }
        if (strck == null || idx == -1) //Invalid sector (track could not be found)
            return false;
        if (idx == strck.Sectors.Count)
            return true;

        for(int i=idx+1; i<strck.Sectors.Count; i++) //Check if any of the above laying sectors are occupied
        {
            if (strck.Sectors[i].Blocked)
                return false;
        }
        return true;
        
    }
    public Sector GetTramSector(Tram tram)
    {
        Sector sec;
        int tramid = tram.Id;
        int found = -1;
        int idx = -1;
        for(int i=0; i<Tracks.Count; i++)
        {
            found = Tracks[i].Sectors.FindIndex(p => p.tram.Id == tramid);
            if(found != -1)
            {
                idx = i;
                break;
            }
        }
        if (found == null)
            return null;
        else
            return Tracks[idx].Sectors[found];
    }
    public void GenerateSectors()
    {
        int idx = 1;
        //Emptying table
        string querydel = "delete from Sector";
        OracleDataReader dbr = DBManager.ExecuteQuery(querydel);
        foreach (Track track in this.Tracks) //For each track in the list, add amount*sector
        {
            int t = track.TrackNr;
            bool a;
            if(t == 57)
                a = true;

            int amount = GetGenAmount(t);

                for (int i = 1; i < amount + 1; i++)
                {
                    string inssec = "insert into sector(\"ID\",\"Spoor_ID\",\"Tram_ID\",\"Nummer\",\"Beschikbaar\",\"Blokkade\") values (" + idx + ", " + track.Id + "," + "null" + "," + i + ",0 ,0)";
                    OracleDataReader dbr2 = DBManager.ExecuteQuery(inssec);
                    idx++;
                }
        }
    }

    private int GetGenAmount(int t)
    {
        int amount = 1;
        if (t >= 32 && t <= 38)
            amount = 4;
        else if (t >= 30 && t <= 31)
            amount = 3;
        else if (t == 40)
            amount = 7;
        else if (t >= 41 && t <= 44)
            amount = 4;
        else if (t == 45)
            amount = 8;
        else if (t == 58)
            amount = 5;
        else if (t == 57)
            amount = 8;
        else if (t >= 54 && t <= 56)
            amount = 7;
        else if (t >= 52 && t <= 53)
            amount = 6;
        else if (t == 51)
            amount = 5;
        else if (t >= 63 && t <= 64)
            amount = 4;
        else if (t >= 61 && t <= 62)
            amount = 3;
        else if (t >= 74 && t <= 77)
            amount = 3;
        else if (t >= 12 && t <= 21)
            amount = 1;
        return amount;
    }
    static public User User_Authenticate(string email, string password)
    {
        User user = null;

        string query = "Select * from Medewerker where \"Gebruikersnaam\" = '" + email + "' and \"Wachtwoord\" = '" + password + "'";
        //Console.WriteLine(query);
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return null;
        }
        else while (dbr.Read())
        {
            int functie_id = Convert.ToInt32(dbr["FUNCTIE_ID"].ToString());
            user = new User(Convert.ToInt32(dbr["ID"].ToString()), dbr["NAAM"].ToString(), dbr["GEBRUIKERSNAAM"].ToString(), dbr["WACHTWOORD"].ToString());
            user.Functie_ = (Functie)functie_id;
        }
        return user;
    }

    public void UpdateSector(Sector sector)
    {
        string query;
        if(sector.tram != null)
        {
            if(sector.Blocked)
                query = "update Sector set \"Tram_ID\"= " + sector.tram.Id + ", \"Beschikbaar\"= " + 1 + ", \"Blokkade\"=" + 1 + " where \"ID\"= " + sector.Id;
            else
                query = "update Sector set \"Tram_ID\"= " + sector.tram.Id + ", \"Beschikbaar\"= " + 1 + ", \"Blokkade\"=" + 0 + " where \"ID\"= " + sector.Id;
        }
        else
        {
            if(sector.Blocked)
                query = "update Sector set \"Beschikbaar\"= " + 1 + ", \"Blokkade\"=" + 1 + ", \"Tram_ID\"= null  where \"ID\"= " + sector.Id;
            else
                query = "update Sector set \"Beschikbaar\"= " + 1 + ", \"Blokkade\"=" + 0 + ", \"Tram_ID\"= null  where \"ID\"= " + sector.Id;
        }
        if(sector.Blocked)
        {
            BlockUnreachable(sector);
        }
        else
        {
            UnblockReachable(sector);
        }
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
    }

    private void BlockUnreachable(Sector sec)
    {
        Track strck = null;
        int idx = -1;
        foreach (Track trck in Tracks)
        {
            idx = trck.Sectors.FindIndex(p => p.Id == sec.Id);
            if (idx != -1)
            {
                strck = trck;
                break;
            }
        }
        if (strck == null || idx == -1) //Invalid sector (track could not be found)
            return;
        if (idx == 0)
            return;

        for (int i = idx - 1; i > -1; i--) //Block all sectors
        {
            if (strck.Sectors[i].Blocked)
                return;
            else
            {
                strck.Sectors[i].Blocked = true;
                UpdateSector(strck.Sectors[i]);
            }
        }
    }
    private void UnblockReachable(Sector sec)
    {
        Track strck = null;
        int idx = -1;
        foreach (Track trck in Tracks)
        {
            idx = trck.Sectors.FindIndex(p => p.Id == sec.Id);
            if (idx != -1)
            {
                strck = trck;
                break;
            }
        }
        if (strck == null || idx == -1) //Invalid sector (track could not be found)
            return;

        for(int i=idx-1; i>-1; i--) //Block all sectors
        {
            if (strck.Sectors[i].tram == null)
            {
                strck.Sectors[i].Blocked = false;
                UpdateSector(strck.Sectors[i]);
            }
            else
                return;
        }
    }
}