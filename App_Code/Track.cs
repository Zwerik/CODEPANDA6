using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Track
{
    public int Id { get; set; }
    public int TrackNr { get; set; }
    public List<Sector> Sectors { get; set; }
    public List<Reservation> Reservations { get; set; }

    public Track(int id, int trackNr)
    {
        this.Id = id;
        this.TrackNr = trackNr;
    }

    public void LinkSectors(List<Sector> sectors)
    {
        Sectors = sectors;
    }

    public override string ToString()
    {
        return TrackNr.ToString();
    }
}