using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Connection_Infos;
using System.Data;

namespace Users_Data_Access_Layer
{
    public class DataAccess
    {
        static public int AddUser(int PersonId,string UserName,string Password , bool IsActive)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Query = @" 
                             INSERT INTO [dbo].[Users]
                                        ([PersonID]
                                        ,[UserName]
                                        ,[Password]
                                        ,[IsActive])
                                  VALUES
                                        (@PersonID,
                             		    @UserName,
                             			@Password,
                             			@isactive)
                             
        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@PersonID", PersonId);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isactive", IsActive);
            

            int Id = -1;

            try
            {
                Connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(Convert.ToString(result), out int res))
                {
                    Id = res;
                }

            }
            catch (Exception s)
            {

            }
            finally
            {
                Connection.Close();
            }
            return Id;
        }

        static public bool DeleteUserByUserID(int UserID)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"
          DELETE FROM [dbo].[Users]
          WHERE Users.UserID=@ID;
                             ";

            SqlCommand command = new SqlCommand(Querey, Connection);

            command.Parameters.AddWithValue("@ID", UserID);

            int res = 0;
            try
            {
                Connection.Open();
                res = command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }
            return res > 0;
        }
        static public bool DeleteUserByUserName(string UserName)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"
          DELETE FROM [dbo].[Users]
          WHERE Users.UserName=@name;
                             ";

            SqlCommand command = new SqlCommand(Querey, Connection);

            command.Parameters.AddWithValue("@name", UserName);

            int res = 0;

            try
            {
                Connection.Open();
                res = Convert.ToInt32(command.ExecuteNonQuery()); 
            }
            catch
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }
            return res > 0;
        }
        static public bool DeleteUserByPersonID(int PersonID)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"
          DELETE FROM [dbo].[Users]
          WHERE Users.PersonID=@ID;
                             ";

            SqlCommand command = new SqlCommand(Querey, Connection);

            command.Parameters.AddWithValue("@ID", PersonID);

            int res = 0;

            try
            {
                Connection.Open();
                res = Convert.ToInt32(command.ExecuteNonQuery());
            }
            catch
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }
            return res > 0;
        }



        static public bool FindUserByUserID(int UserID,ref string UserName , ref string Password , ref bool IsActive , ref int PersonID)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select * From Users 
                              Where UserID=@ID";

            SqlCommand Command = new SqlCommand(Querey, Connection);

            Command.Parameters.AddWithValue("@ID", UserID);


            bool IsFound = false;
            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    UserName = Convert.ToString(Reader["UserName"]);
                    Password = Convert.ToString(Reader["Password"]);
                    IsActive = Convert.ToBoolean(Reader["IsActive"]);
                    PersonID = Convert.ToInt32(Reader["PersonID"]);
                }
                else
                {
                    IsFound = false;
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }
        static public bool FindUserByPersonID(int PersonID ,ref int UserID, ref string UserName, ref string Password, ref bool IsActive)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select * From Users 
                              Where PersonID=@ID";

            SqlCommand Command = new SqlCommand(Querey, Connection);

            Command.Parameters.AddWithValue("@ID", PersonID);


            bool IsFound = false;
            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    UserName = Convert.ToString(Reader["UserName"]);
                    Password = Convert.ToString(Reader["Password"]);
                    IsActive = Convert.ToString(Reader["IsActive"]) == "1" ? true : false;
                    UserID = Convert.ToInt32(Reader["UserID"]);
                }
                else
                {
                    IsFound = false;
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }
        static public bool FindUserByUserName(string UserName, ref int PersonID, ref int UserID, ref string Password, ref bool IsActive)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select * From Users 
                              Where UserName=@Username";

            SqlCommand Command = new SqlCommand(Querey, Connection);

            Command.Parameters.AddWithValue("@Username", UserName);


            bool IsFound = false;
            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    Password = Convert.ToString(Reader["Password"]);
                    IsActive = Convert.ToBoolean(Reader["IsActive"]);
                    UserID = Convert.ToInt32(Reader["UserID"]);
                    PersonID = Convert.ToInt32(Reader["PersonID"]);

                }
                else
                {
                    IsFound = false;
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }


        static public bool UpdateUser(int UserID,int PersonId, string UserName, string Password, bool IsActive)
        {
            SqlConnection connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"UPDATE [dbo].[Users]
                                SET [PersonID] = @personID
                                   ,[UserName] = @username
                                   ,[Password] =@password
                                   ,[IsActive] = @isactive
                              WHERE Users.UserID = @UserID";

            SqlCommand command = new SqlCommand(Querey, connection);

            command.Parameters.AddWithValue("@personID", PersonId);
            command.Parameters.AddWithValue("@username", UserName);
            command.Parameters.AddWithValue("@password", Password);
            command.Parameters.AddWithValue("@isactive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);
          


            int AffectedRows = 0;

            try
            {
                connection.Open();

                AffectedRows = command.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return AffectedRows > 0;
        }


        static public bool IsExistedByUserID(int UserID)
        {
            SqlConnection connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select 1 From Users
                              where UserID = @ID;";

            SqlCommand command = new SqlCommand(Querey, connection);

            command.Parameters.AddWithValue("@ID", UserID);

            bool IsFound = false;

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                IsFound = (Result != null);

            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }
        static public bool IsExistedByUserName(string UserName)
        {
            SqlConnection connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select 1 From Users
                              where UserName = @UserName;";

            SqlCommand command = new SqlCommand(Querey, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            bool IsFound = false;

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                IsFound = (Result != null);

            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }
        static public bool IsExistedByPersonID(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select 1 From Users
                              where PersonID = @ID;";

            SqlCommand command = new SqlCommand(Querey, connection);

            command.Parameters.AddWithValue("@ID", PersonID);

            bool IsFound = false;

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                IsFound = (Result != null);

            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }


        static public DataTable ListUsers()
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select UserID,Users.PersonID,UserName,FulName = (People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName) , IsActive From Users 
                              inner join People on Users.PersonID = People.PersonID;
                               ";

            SqlCommand Command = new SqlCommand(Querey, Connection);

            DataTable datatable = new DataTable();

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    datatable.Load(Reader);
                }

            }
            catch
            {

            }
            finally
            {
                Connection.Close();
            }

            return datatable;
        }

        static public int GetPersonID(int UserID)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select PersonID From Users
                              Where UserID = @userid
                             ";

            SqlCommand Command = new SqlCommand(Querey, Connection);
            Command.Parameters.AddWithValue("@userid", UserID);

            int PersonID = -1;

            try
            {
                Connection.Open();

                PersonID = Convert.ToInt32(Command.ExecuteScalar());
            }
            catch
            {

            }
            finally
            {
                Connection.Close();
            }

            return PersonID;
        }

        static public bool IsUserConnectedToAnyServiceByUserID(int UserID)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"IF EXISTS (SELECT 1 FROM DetainedLicenses WHERE ReleasedByUserID = @ID Or CreatedByUserID = @ID)
                OR EXISTS (SELECT 1 FROM Applications WHERE CreatedByUserID = @ID)
                OR EXISTS (SELECT 1 FROM Drivers WHERE CreatedByUserID = @ID)
                OR EXISTS (SELECT 1 FROM InternationalLicenses WHERE CreatedByUserID = @ID)
                OR EXISTS (SELECT 1 FROM TestAppointments WHERE CreatedByUserID = @ID)
                OR EXISTS (SELECT 1 FROM Tests WHERE CreatedByUserID = @ID)
                OR EXISTS (SELECT 1 FROM Licenses WHERE CreatedByUserID = @ID)

            BEGIN
                SELECT 1;
            END
            ELSE
            BEGIN
                SELECT 0;
            END";

            SqlCommand Command = new SqlCommand(Querey, Connection);

            Command.Parameters.AddWithValue("@ID", UserID);

            bool IsConnected = false;

            try
            {
                Connection.Open();

                IsConnected = (Convert.ToString(Command.ExecuteScalar()) == "1");
            }
            catch
            {

            }
            finally
            {
                Connection.Close();
            }
            return IsConnected;
        }
       




    }
}
