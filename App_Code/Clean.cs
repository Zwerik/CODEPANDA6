using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class Clean : Maintenance
    {
        #region properties
        #endregion
        #region constructor


        public Clean(int id, DateTime date, string type, Tram tram, User user)
            : base(id, date, type, tram, user)
        {

        }
        #endregion
        #region private methods
        #endregion
        #region public methods
        #endregion
    }