using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    public enum Status
    {
        Remise,
        Dienst,
        Onderhoud,
        Defect,
        Schoonmaak
    }

    public class Service
    {
        #region fields
        private Database database;
        #endregion
        #region properties
        public List<Maintenance> Maintenances { get; set; }
        public List<Clean> Cleans { get; set; }
        public List<Repair> Repairs { get; set; }
        public List<User> Users { get; set; }
        public Status Status { get; set; }
        #endregion
        #region constructor

        public Service()
        {
            database = new Database();
            Cleans = database.LoadClean();
            Repairs = database.LoadRepair();
            Users = database.GetUsers();
        }
        #endregion
        #region private methods

        #endregion
        #region public methods
        public Clean GetClean(int cleanId)
        {
            Clean clean = null;
            return clean;
        }

        public Tram GetTram(int tramNr)
        {
            return database.GetTramId(tramNr);
        }

        public Repair GetRepair(int repairid)
        {
            Repair repair = null;
            return repair;
        }

        public void InsertClean(Clean clean)
        {
            database.InsertClean(clean);
        }

        public void InsertRepair(Repair repair)
        {
            database.InsertRepair(repair);
        }

        public void UpdateClean(Clean clean)
        {
            database.UpdateClean(clean);
        }

        public void UpdateRepair(Repair repair)
        {
            database.UpdateRepair(repair);
        }

        public void CompleteClean(Clean clean)
        {
            database.RemoveClean(clean);
        }

        public void CompleteRepair(Repair repair)
        {
            database.RemoveRepair(repair);
        }
        #endregion
    }