using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Tram
{
    public int Id { get; set; }
    public int TramNr { get; set; }
    public User Driver { get; set; }
    public Reservation Reservation { get; set; }
    public Status Status { get; set; }

    public Tram(int id, int tramNr)
    {
        this.Id = id;
        this.TramNr = tramNr;
    }

    public void AddDriver(User driver)
    {
        this.Driver = driver;
    }

    public void AddReservation(Reservation reservation)
    {
        this.Reservation = reservation;
    }

    public void ChangeStatus(Status status)
    {
        this.Status = status;
    }

    public override string ToString()
    {
        return Id.ToString();
    }
}
