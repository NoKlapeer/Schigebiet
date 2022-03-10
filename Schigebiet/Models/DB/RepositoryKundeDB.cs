
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace Schigebiet.Models.DB
{
    public class RepositoryKundeDB : IRepositoryKundeDB
    {
        private string _connectionString = "Server=localhost;database=schidb;user=root;password=";
        private DbConnection _conn;


        public void Connect()
        {
            if (this._conn == null)
            {
                this._conn = new MySqlConnection(this._connectionString);
            }
            if (this._conn.State != ConnectionState.Open)
            {
                this._conn.Open();
            }
        }
        public void Disconnect()
        {
            if ((this._conn != null) && (this._conn.State == ConnectionState.Open))
            {
                this._conn.Close();
            }
        }


        public bool ChangeUserData(int userId, Kunde newKundenData)
        {
            if (this._conn?.State == System.Data.ConnectionState.Open)
            {
                DbCommand cmd = this._conn.CreateCommand();
                cmd.CommandText = "update users set username = @username, password = sha2(@password, 512), " +
                    "email = @email, birthdate = @birthdate, gender = @gender where user_id = @user_id";
                DbParameter paramUN = cmd.CreateParameter();
                paramUN.ParameterName = "username";
                paramUN.DbType = System.Data.DbType.String;
                paramUN.Value = newKundenData.Name;

                DbParameter paramPW = cmd.CreateParameter();
                paramPW.ParameterName = "password";
                paramPW.DbType = System.Data.DbType.String;
                paramPW.Value = newKundenData.Password;

                DbParameter paramEmail = cmd.CreateParameter();
                paramEmail.ParameterName = "email";
                paramEmail.DbType = System.Data.DbType.String;
                paramEmail.Value = newKundenData.EMail;

                DbParameter paramBD = cmd.CreateParameter();
                paramBD.ParameterName = "birthdate";
                paramBD.DbType = System.Data.DbType.Date;
                paramBD.Value = newKundenData.Birthdate;

                DbParameter paramGender = cmd.CreateParameter();
                paramGender.ParameterName = "gender";
                paramGender.DbType = System.Data.DbType.Int32;
                paramGender.Value = newKundenData.Geschlecht;

                DbParameter paramID = cmd.CreateParameter();
                paramGender.ParameterName = "user_id";
                paramGender.DbType = System.Data.DbType.Int32;
                paramGender.Value = newKundenData.KundenId;


                cmd.Parameters.Add(paramUN);
                cmd.Parameters.Add(paramPW);
                cmd.Parameters.Add(paramEmail);
                cmd.Parameters.Add(paramBD);
                cmd.Parameters.Add(paramGender);
                cmd.Parameters.Add(paramID);

                return cmd.ExecuteNonQuery() == 1;
            }
            return false;
        }
        public bool Delete(int userId)
        {

            if (this._conn?.State == ConnectionState.Open)
            {

                DbCommand cmdDelete = this._conn.CreateCommand();

                cmdDelete.CommandText = "delete from users where user_id = @userId";


                DbParameter paramUI = cmdDelete.CreateParameter();

                paramUI.ParameterName = "userId";
                paramUI.DbType = DbType.String;
                paramUI.Value = userId;

                cmdDelete.Parameters.Add(paramUI);

                return cmdDelete.ExecuteNonQuery() == 1;
            }

            return false;
        }
        public List<Kunde> GetAllKunden()
        {

            List<Kunde> kunden = new List<Kunde>();

            if (this._conn?.State == ConnectionState.Open)
            {

                DbCommand cmdAllUsers = this._conn.CreateCommand();
                cmdAllUsers.CommandText = "select * from kunden";

                using (DbDataReader reader = cmdAllUsers.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        kunden.Add(new Kunde()
                        {
                            KundenId = Convert.ToInt32(reader["k_id"]),
                            Name = Convert.ToString(reader["name"]),
                            Password = Convert.ToString(reader["password"]),
                            EMail = Convert.ToString(reader["email"]),
                            Birthdate = Convert.ToDateTime(reader["birthdate"]),
                            Geschlecht = (Geschlecht)Convert.ToInt32(reader["geschlecht"])
                        });
                    }

                }   
                    

            }

            return kunden;
        }
        public Kunde GetKunde(int userId)
        {
            if (this._conn?.State == System.Data.ConnectionState.Open)
            {
                DbCommand cmd = this._conn.CreateCommand();
                cmd.CommandText = "select * from kunden where _id = @user_id";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Kunde()
                        {
                            KundenId = userId,
                            Name = Convert.ToString(reader["username"]),
                            Password = Convert.ToString(reader["password"]),
                            Birthdate = Convert.ToDateTime(reader["birthdate"]),
                            EMail = Convert.ToString(reader["email"]),
                            Geschlecht = (Geschlecht)Convert.ToInt32(reader["gender"])
                        };
                    }
                }
            }
            return new Kunde();

        }

        public bool Insert(Kunde kunde)
        {

            if (this._conn?.State == ConnectionState.Open)
            {
                DbCommand cmdInsert = this._conn.CreateCommand();
                cmdInsert.CommandText = "insert into kunden values(null, @name, sha2(@password, 512), @mail, @bDate, @gender)";

                DbParameter paramUN = cmdInsert.CreateParameter();
                paramUN.ParameterName = "name";
                paramUN.DbType = DbType.String;
                paramUN.Value = kunde.Name;

                DbParameter paramPWD = cmdInsert.CreateParameter();
                paramPWD.ParameterName = "password";
                paramPWD.DbType = DbType.String;
                paramPWD.Value = kunde.Password;

                DbParameter paramEMail = cmdInsert.CreateParameter();
                paramEMail.ParameterName = "mail";
                paramEMail.DbType = DbType.String;
                paramEMail.Value = kunde.EMail;

                DbParameter paramBDate = cmdInsert.CreateParameter();
                paramBDate.ParameterName = "bDate";
                paramBDate.DbType = DbType.Date;
                paramBDate.Value = kunde.Birthdate;

                DbParameter paramGender = cmdInsert.CreateParameter();
                paramGender.ParameterName = "gender";
                paramGender.DbType = DbType.Int32;
                paramGender.Value = kunde.Geschlecht;

                cmdInsert.Parameters.Add(paramUN);
                cmdInsert.Parameters.Add(paramPWD);
                cmdInsert.Parameters.Add(paramEMail);
                cmdInsert.Parameters.Add(paramBDate);
                cmdInsert.Parameters.Add(paramGender);

                return cmdInsert.ExecuteNonQuery() == 1;
            }

            return false;
        }
        public bool Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public bool ChangeUserData(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
