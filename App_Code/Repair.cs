using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    public class Repair : Maintenance
    {
        #region properties

        public DateTime Estimate { get; set; }
        public string Description { get; set; }

        #endregion

        #region constructor

        public Repair(int id, DateTime date, string type, Tram tram, User user, DateTime estimate, string description)
            : base(id, date, type, tram, user)
        {
            this.Estimate = estimate;
            this.Description = description;
        }
        #endregion

        #region private methods

        #endregion

        #region public methods

        #endregion
    }