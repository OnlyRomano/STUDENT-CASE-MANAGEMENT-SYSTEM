//add first this library
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System;


namespace CS311C_DATABASE2024
{
    class Class1
    {
        private string sqlConString;
        public int rowAffected = 0;
        public Class1(string server_address, string database, string username, string password)
        {
            //Server = server name(xampp) Uid = username Pwd = password
            sqlConString = "Server = " + server_address + "; Database = " + database + "; User Id = "
             + username + "; Password = " + password + ";";
        }
        //select
        public DataTable GetData(string sql)
        {
            //connection
            SqlConnection Sqlcon = new SqlConnection(sqlConString);
            //checking if the connection is close
            if (Sqlcon.State == ConnectionState.Closed) Sqlcon.Open();
            //creating a command using the connection and the sql query
            SqlCommand SQLcom = new SqlCommand(sql, Sqlcon);
            //creating the adapter using the created sql command
            SqlDataAdapter SQLadap = new SqlDataAdapter(SQLcom);
            DataSet ds = new DataSet();
            //fill the dataset using the adapter
            SQLadap.Fill(ds);
            return ds.Tables[0];
        }
        //insert, update, delete
        public void executeSQL(string sql)
        {
            //connection
            SqlConnection Sqlcon = new SqlConnection(sqlConString);
            //open the connection
            if (Sqlcon.State == ConnectionState.Closed) Sqlcon.Open();
            //build the sql command using the sql statement and the connection
            SqlCommand SQLcom = new SqlCommand(sql, Sqlcon);
            //execute the sql command
            rowAffected = SQLcom.ExecuteNonQuery();
        }

        internal DataTable GetData(string query, object value)
        {
            throw new NotImplementedException();
        }

        public string SqlConString { get; set; }

    }
}