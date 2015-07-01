using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    abstract public class Maintenance
    {
        #region properties
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public User User { get; set; }
        public Tram Tram { get; set; }
        #endregion
        #region constructor

        public Maintenance(int id, DateTime date, string type, Tram tram, User user)
        {
            this.Id = id;
            this.Date = date;
            this.Type = type;
            this.Tram = tram;
            this.User = user;
        }
        #endregion
        #region private methods
        #endregion
        #region public methods

        #endregion
    }