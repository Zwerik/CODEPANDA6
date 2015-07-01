using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

using System.Data;

    public class Database
    {
        private OracleConnection conn;


        public Database()
        {
            conn = new OracleConnection("Data Source=//fhictora01.fhict.local:1521/fhictora;User ID=dbi298723;Password=sVX2Blr8oU");
        }

        //<summary>
        // Name: NonQueryBase
        // This method is used for executing nonquery based SQL statements. 
        // This drastically lowers the amount of copied code and makes the update 
        // and insert statements more readable
        //</summary>
        private void NonQueryBase(OracleCommand command)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                command.ExecuteNonQuery();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

                /// <summary>
        /// Name: GetUser
        /// Retrieves the User from the database with the same Id as the input.
        /// Returns a null user if the user was not found
        /// </summary>
        public User GetUser(int userId)
        {
            string sql = "SELECT * FROM \"Medewerker\" WHERE \"Id\" = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("id", userId));

            User user = null;

            string name = "";
            string username = "";
            string password = "";

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    name = Convert.ToString(reader["Naam"]);
                    username = Convert.ToString(reader["Gebruikersnaam"]);
                    password = Convert.ToString(reader["Wachtwoord"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            user = new User(userId, name, username, password);
            return user;
        }

        /// <summary>
        /// Name: GetUser
        /// Retrieves the User from the database with the same username as the input.
        /// Returns a null user if the user was not found
        /// </summary>
        public List<User> GetUsers()
        {
            string sql = "SELECT * FROM MEDEWERKER";
            OracleCommand command = new OracleCommand(sql, conn);

            List<User> users = new List<User>();
            User user = null;

            int id = -1;
            string name = "";
            string username = "";
            string password = "";

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["Id"]);
                    name = Convert.ToString(reader["Naam"]);
                    username = Convert.ToString(reader["Gebruikersnaam"]);
                    password = Convert.ToString(reader["Wachtwoord"]);

                    user = new User(id, name, username, password);
                    users.Add(user);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return users;
        }

        /// <summary>
        /// Name: GetUser
        /// Retrieves the User from the database with the same username as the input.
        /// Returns a null user if the user was not found
        /// </summary>
        /*public User GetUser(string username)
        {
            string sql = "SELECT * FROM \"Medewerker\" WHERE \"Gebruikersnaam\" = :username";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("username", username));

            User user = null;

            int id = -1;
            string name = "";
            string password = "";

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["Id"]);
                    name = Convert.ToString(reader["Naam"]);
                    password = Convert.ToString(reader["Wachtwoord"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            user = new User(id, name, username, password);
            return user;
        }*/

        /// <summary>
        /// Name: GetTram
        /// Retrieves the Tram with the same Id as the input from the database.
        /// Returns a null Tram if the tram was not found
        /// </summary>
        public Tram GetTram(int tramId)
        {
            string sql = "SELECT \"Nummer\" FROM TRAM WHERE \"ID\" = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("id", tramId));

            Tram tram = null;

            int tramNr = -1;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tramNr = Convert.ToInt32(reader["Nummer"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            if (tramNr != -1)
            {
                tram = new Tram(tramId, tramNr);
            }

            return tram;
        }

        /// <summary>
        /// Name: GetTramId
        /// Retrieves the Tram with the same Id as the input from the database.
        /// Returns a null Tram if the tram was not found
        /// </summary>
        public Tram GetTramId(int tramNr)
        {
            string sql = "SELECT \"ID\" FROM TRAM WHERE \"Nummer\" = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("id", tramNr));

            Tram tram = null;

            int tramId = -1;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tramId = Convert.ToInt32(reader["ID"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            if (tramNr != -1)
            {
                tram = new Tram(tramId, tramNr);
            }

            return tram;
        }

        public List<Clean> LoadClean()
        {
            string sql = "SELECT * FROM TRAM_ONDERHOUD WHERE \"TypeOnderhoud\" = 'SCHOONMAAK'";
            OracleCommand command = new OracleCommand(sql, conn);

            List<Clean> cleans = new List<Clean>();
            Clean clean = null;

            int id = -1;
            DateTime date;
            string type;
            Tram tram;
            int tramId;
            int userId;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["ID"]);
                    userId = Convert.ToInt32(reader["Medewerker_ID"]);
                    date = Convert.ToDateTime(reader["DatumTijdstip"]);
                    type = Convert.ToString(reader["TypeOnderhoud"]);
                    tramId = Convert.ToInt32(reader["Tram_ID"]);

                    clean = new Clean(id, date, type, new Tram(tramId, tramId), new User(userId, "", "", ""));
                    cleans.Add(clean);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            foreach (Clean c in cleans)
            {
                c.Tram = GetTram(c.Tram.Id);
            }

            foreach (Clean c in cleans)
            {
                c.User = GetUser(c.User.Id);
            }

            return cleans;
        }

        public List<Repair> LoadRepair()
        {
            string sql = "SELECT * FROM TRAM_ONDERHOUD WHERE \"TypeOnderhoud\" = 'REPARATIE'";
            OracleCommand command = new OracleCommand(sql, conn);

            List<Repair> repairs = new List<Repair>();
            Repair repair = null;

            int id = -1;
            DateTime date;
            DateTime estimate;
            string type;
            Tram tram;
            int tramId;
            int userId;
            string description;


            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["ID"]);
                    date = Convert.ToDateTime(reader["DatumTijdstip"]);
                    estimate = Convert.ToDateTime(reader["BeschikbaarDatum"]);
                    type = Convert.ToString(reader["TypeOnderhoud"]);
                    tramId = Convert.ToInt32(reader["Tram_ID"]);
                    userId = Convert.ToInt32(reader["Medewerker_ID"]);
                    description = Convert.ToString(reader["Beschrijving"]);

                    repair = new Repair(id, date, type, new Tram(tramId, tramId), new User(userId, "", "", ""), estimate, description);
                    repairs.Add(repair);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            foreach (Repair r in repairs)
            {
                r.Tram = GetTram(r.Tram.Id);
            }

            foreach (Repair r in repairs)
            {
                r.User = GetUser(r.User.Id);
            }

            return repairs;
        }

        public Repair LoadRepair(int repairId)
        {
            string sql = "SELECT * FROM TRAM_ONDERHOUD WHERE \"TypeOnderhoud\" = 'REPARATIE' AND \"Id\" = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add("id", repairId);

            Repair repair = null;

            int id = -1;
            DateTime date;
            DateTime estimate;
            string type;
            Tram tram;
            int tramId;
            int userId;
            string description;


            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["ID"]);
                    date = Convert.ToDateTime(reader["DatumTijdstip"]);
                    estimate = Convert.ToDateTime(reader["BeschikbaarDatum"]);
                    type = Convert.ToString(reader["TypeOnderhoud"]);
                    tramId = Convert.ToInt32(reader["Tram_ID"]);
                    userId = Convert.ToInt32(reader["Medewerker_ID"]);
                    description = Convert.ToString(reader["Beschrijving"]);

                    repair = new Repair(id, date, type, new Tram(tramId, tramId), new User(userId, "", "", ""), estimate, description);
                    break;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            repair.Tram = GetTram(repair.Tram.Id);
            repair.User = GetUser(repair.User.Id);

            return repair;
        }

        public void InsertClean(Clean clean)
        {
            string sql = "INSERT INTO TRAM_ONDERHOUD(\"Medewerker_Id\", \"Tram_Id\", \"DatumTijdstip\", \"TypeOnderhoud\") VALUES (:userid, :tramid, :datetime, 'SCHOONMAAK')";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add("userid", clean.User.Id);
            command.Parameters.Add("tramid", clean.Tram.Id);
            command.Parameters.Add("datetime", clean.Date);

            NonQueryBase(command);
        }

        public void InsertRepair(Repair repair)
        {
            string sql = "INSERT INTO TRAM_ONDERHOUD(\"Medewerker_ID\", \"Tram_ID\", \"DatumTijdstip\", \"BeschikbaarDatum\", \"TypeOnderhoud\", \"Beschrijving\") VALUES (:userid, :tramid, :datetime, :estimate, 'REPARATIE', :description)";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add("userid", repair.User.Id);
            command.Parameters.Add("tramid", repair.Tram.Id);
            command.Parameters.Add("datetime", repair.Date);
            command.Parameters.Add("estimate", repair.Estimate);
            command.Parameters.Add("description", repair.Description);

            NonQueryBase(command);
        }

        public void RemoveClean(Clean clean)
        {
            string sql = "DELETE FROM TRAM_ONDERHOUD WHERE \"ID\" = :id)";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add("id", clean.Id);

            NonQueryBase(command);
        }

        public void RemoveRepair(Repair repair)
        {
            string sql = "DELETE FROM TRAM_ONDERHOUD WHERE \"ID\" = :id)";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add("id", repair.Id);

            NonQueryBase(command);
        }

        public void UpdateClean(Clean clean)
        {
            string sql = "UPDATE \"TRAM_ONDERHOUD\" SET \"Medewerker_ID\" = :userid, \"Tram_Id\" = :tramid, \"DatumTijdstip\" = :date WHERE \"ID\" = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add("id", clean.Id);
            command.Parameters.Add("userid", clean.User.Id);
            command.Parameters.Add("tramid", clean.Tram.Id);
            command.Parameters.Add("date", clean.Date);

            NonQueryBase(command);
        }

        public void UpdateRepair(Repair repair)
        {
            string sql = "UPDATE TRAM_ONDERHOUD SET \"Medewerker_ID\" = :userid, \"Tram_Id\" = :tramid, \"DatumTijdstip\" = :date, \"BeschikbaarDatum\" = :estimate, \"Beschrijving\" = :description WHERE \"ID\" = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add("id", repair.Id);
            command.Parameters.Add("userid", repair.User.Id);
            command.Parameters.Add("tramid", repair.Tram.Id);
            command.Parameters.Add("date", repair.Date);
            command.Parameters.Add("estimate", repair.Estimate);
            command.Parameters.Add("description", repair.Description);

            NonQueryBase(command);
        }
    }