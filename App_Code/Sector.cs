using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Sector
{
    public int Id { get; set; }
    public int SectorNr { get; set; }
    public Track Track { get; set; }
    public bool Blocked { get; set; }
    public Tram tram;


    public Sector(int id, int sectorNr, Track track)
    {
        this.Id = id;
        this.SectorNr = sectorNr;
        this.Track = track;
        this.Blocked = false;
    }
    public override string ToString()
    {
        string str = SectorNr.ToString();

        if (Blocked)
            str += "- [:#:] -";
        else
            str += "- [:=:] -";
        if(tram != null)
        {
            str += " " + tram.TramNr; 
        }
        else
        {
            str += " ...";
        }
        /*
        string tStr;
        if(this.tram != null)
            tStr = "ID: " + Id.ToString() + "\nNummer: " + SectorNr.ToString() + "\nTrack: " + Track.ToString() + "\nBlocked: " + Blocked.ToString() + "\nTram:" + this.tram.ToString();
        else
            tStr = "ID: " + Id.ToString() + "\nNummer: " + SectorNr.ToString() + "\nTrack: " + Track.ToString() + "\nBlocked: " + Blocked.ToString() + "\nTram: N/A";
        return tStr;
        //return SectorNr.ToString();
         */
        return str;
    }

    public bool AddTram(Tram tram)
    {
        if (this.tram != null)
            return false;
        this.tram = tram;
        this.Blocked = true;
        return true;
    }

    public bool RemoveTram()
    {
        if (tram == null)
            return false;
        this.tram = null;
        this.Blocked = false;
        return true;
    }
}
