using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Reservation
{
    public int Id { get; set; }
    public Track Track { get; set; }
    public Tram Tram { get; set; }


    public Reservation(int id, Track track, Tram tram)
    {
        this.Id = id;
        this.Track = track;
        this.Tram = tram;
    }

}
