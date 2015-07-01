using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

using System.Data;

/// <summary>
/// Handles the connection and executing queries on the database
/// </summary>
public class DBManager
{
    //Connection string:
    //static private string DBConString = "User Id=" + "dbi304418" + ";Password=" + "KeLdtn9JNW" + ";Data Source=" + " //192.168.15.50:1521/fhictora" + ";";
    static private OracleConnection Con;

    static public OracleConnection Connection //Opens the connection to the database
    {
        get
        {
            if (Con == null)
            {
                Con = new OracleConnection("Data Source=//fhictora01.fhict.local:1521/fhictora;User ID=dbi298723;Password=sVX2Blr8oU");
                Con.Open();

            }
            return Con;
        }
    }
    public DBManager()
    {

    }

    //Executes a query onto the database
    static public OracleDataReader ExecuteQuery(string query)
    {
        OracleCommand cmd = new OracleCommand(query, Connection);
        OracleDataReader ODReader = cmd.ExecuteReader();
        //Connection.Close();
        return ODReader;
    }
    //Executes a stored procedure on the database
    static public OracleCommand ExecuteProcedure(string storedprocedure)
    {
        OracleCommand command = new OracleCommand(storedprocedure, Connection);

        command.CommandType = CommandType.StoredProcedure;
        //Connection.Close();
        return command;
    }

}