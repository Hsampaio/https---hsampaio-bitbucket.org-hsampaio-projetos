using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace WSFutebol.Classes
{
    public class Pessoa
    {
        private int codigo;
        private string gcm;
        private string siglaTime;

        public int Codigo
        {
            get
            {
                return codigo;
            }

            set
            {
                codigo = value;
            }
        }

        public string Gcm
        {
            get
            {
                return gcm;
            }

            set
            {
                gcm = value;
            }
        }

        public string SiglaTime
        {
            get
            {
                return siglaTime;
            }

            set
            {
                siglaTime = value;
            }
        }

        public int Inserir()
        {

            if (!JaExisteGCM())
            {
                DBConnect dbInsert = new DBConnect();
                string sComandoSql = "INSERT INTO T_PESSOA(GCMCODE, SIGLATIME) " +
                                     "              VALUES(@GCMCODE, @SIGLATIME)";

                MySqlCommand cmd = new MySqlCommand(sComandoSql);

                cmd.Parameters.AddWithValue("@GCMCODE", this.gcm);
                cmd.Parameters.AddWithValue("@SIGLATIME", this.siglaTime);

                try
                {
                    int iRetorno = dbInsert.ExecuteNonQuery(cmd); 
                    return iRetorno;
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
            else
            {
                DBConnect dbUpdate = new DBConnect();
                string sComandoSql = "UPDATE T_PESSOA SET SIGLATIME = @SIGLATIME WHERE GCMCODE = @GCMCODE";

                MySqlCommand cmd = new MySqlCommand(sComandoSql);

                cmd.Parameters.AddWithValue("@GCMCODE", this.gcm);
                cmd.Parameters.AddWithValue("@SIGLATIME", this.siglaTime);

                try
                {   
                    int iRetorno = dbUpdate.ExecuteNonQuery(cmd); ;
                    dbUpdate.Close_Connection();
                    return iRetorno;
                }
                catch (Exception e)
                {
                    return -1;
                }
            }

        }

        public bool JaExisteGCM()
        {

            DBConnect dbSelect = new DBConnect();
            string sComandoSql = @" SELECT A.CODIGO
                                    FROM T_PESSOA A                                            
                                   WHERE A.GCMCODE = @GCMCODE   ";
            MySqlCommand cmd = new MySqlCommand(sComandoSql);
            cmd.Parameters.AddWithValue("@GCMCODE", this.gcm);

            bool bRetorno = false;

            try
            {
                MySqlDataReader dataReader = dbSelect.ExecuteReader(cmd);
                while (dataReader.Read())
                {
                    int iCod = 0;
                    iCod = Int32.Parse(dataReader["CODIGO"].ToString());
                    bRetorno = true;
                    break;
                }
            }
            catch (Exception e)
            {
                bRetorno = false;
            }
            dbSelect.Close_Connection();
            return bRetorno;
        }


        public static List<Pessoa> BuscarListaPessoas()
        {

            DBConnect dbSelect = new DBConnect();
            string sComandoSql = @" SELECT CODIGO, GCMCODE
                                      FROM T_PESSOA      
                                     WHERE ENVIAGCM = 'S' ";
            MySqlCommand cmd = new MySqlCommand(sComandoSql);
            
            List<Pessoa> lista = new List<Pessoa>();

            try
            {
                MySqlDataReader dataReader = dbSelect.ExecuteReader(cmd);
                while (dataReader.Read())
                {
                    Pessoa usuario = new Pessoa();
                    usuario.Codigo = Int32.Parse(dataReader["CODIGO"].ToString());
                    usuario.Gcm = dataReader["GCMCODE"].ToString();

                    lista.Add(usuario);
                }
            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            dbSelect.Close_Connection();
            return lista;

        }
    }

}