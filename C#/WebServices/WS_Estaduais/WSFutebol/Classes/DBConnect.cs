using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace WSFutebol.Classes
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {

            server = ConfigurationManager.AppSettings["server"];
            database = ConfigurationManager.AppSettings["database"];
            uid = ConfigurationManager.AppSettings["uid"];
            password = ConfigurationManager.AppSettings["uid"];



            server = "inaltum.mysql.dbaas.com.br";
            database = "inaltum";
            uid = "inaltum";
            password = "androidapk1";


            string connectionString;
            connectionString = "SERVER=" + server + ";" +
                               "DATABASE=" + database + ";" +
                               "UID=" + uid + ";" +
                               "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }



        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {

                switch (ex.Number)
                {
                    case 0:
                        // EnviarEmail("helton.jhon@hotmail.com", "erro", "Cannot connect to server.  Contact administrator");
                        // MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        ///EnviarEmail("helton.jhon@hotmail.com", "erro", "Invalid username/password, please try again");
                        //  MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }


        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }


        public int ExecuteNonQuery(MySqlCommand comandoMySQL)
        {
            int iRetorno = 0;
            try
            {
                if (this.OpenConnection() == true)
                {
                    comandoMySQL.Connection = connection;

                    if (comandoMySQL.ExecuteNonQuery() > 0)
                        iRetorno = (int)comandoMySQL.LastInsertedId;
                    else
                        iRetorno = -1;

                    this.CloseConnection();
                }
            }
            catch (Exception e)
            {
                string sErro = e.Message;

                iRetorno = -1;
            }
            return iRetorno;
        }


        public MySqlDataReader ExecuteReader(MySqlCommand comandoMySQL)
        {
            MySqlDataReader dataReader = null;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = comandoMySQL;
                try
                {

                    cmd.Connection = connection;
                    dataReader = cmd.ExecuteReader();
                }
                catch (Exception e)
                {

                    string sEx = e.Message;
                }
            }


            return dataReader;
        }




        public bool Close_Connection()
        {
            try
            {
                this.connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }
    }
}
