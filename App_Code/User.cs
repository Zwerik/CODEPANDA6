using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class User
    {
        #region properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Functie Functie_ { get; set; }
        #endregion
        #region constructor

       
        public User(int id, string name, string username, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Username = username;
            this.Password = password;
        }
        #endregion
        #region private methods
        #endregion
        #region public methods
        #endregion
    }