using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Connection_Infos;
namespace Persons_Data_Access_Layer
{
    static public class clsDataAccess
    {
        static public int AddPerson(string NationalNo,string FirstName,string SecondName,string ThirdName,string LastName,DateTime DateOfBirth,short Gender,string Address,string PhoneNumber,string Email,int CountryID,string ImagePath)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Query = @"
        INSERT INTO [dbo].[People]
        ([NationalNo], [FirstName], [SecondName], [ThirdName], [LastName], [DateOfBirth], [Gendor], [Address], [Phone], [Email], [NationalityCountryID], [ImagePath])
        VALUES
        (@nationalnumber, @firstname, @secondname, @thirdname, @lastname, @dateofbirth, @gender, @address, @phone, @email, @countryid, @imagepath);
        SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@nationalnumber", NationalNo);
            command.Parameters.AddWithValue("@firstname", FirstName);
            command.Parameters.AddWithValue("@secondname", SecondName);
            command.Parameters.AddWithValue("@thirdname", ThirdName);
            command.Parameters.AddWithValue("@lastname", LastName);
            command.Parameters.AddWithValue("@dateofbirth", DateOfBirth);
            command.Parameters.AddWithValue("@gender", Gender);
            command.Parameters.AddWithValue("@address", Address);
            command.Parameters.AddWithValue("@phone", PhoneNumber);
            command.Parameters.AddWithValue("@countryid", CountryID);


            if(string.IsNullOrEmpty(Email))
            {
                command.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@email", Email);
            }


            if (string.IsNullOrEmpty(ImagePath))
            {
                command.Parameters.AddWithValue("@imagepath", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@imagepath", ImagePath);
            }

            int Id = -1;

            try
            {
                Connection.Open();

                object result = command.ExecuteScalar();

                if(result!=null&&int.TryParse(Convert.ToString(result),out int res))
                {
                    Id = res;
                }

            }
            catch(Exception s)
            {
                
            }
            finally
            {
                Connection.Close();
            }
            return Id;
        }
       
        
        static public bool DeletePerson(int ID)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"
          DELETE FROM [dbo].[People]
          WHERE People.PersonID=@ID;
                             ";

            SqlCommand command = new SqlCommand(Querey, Connection);

            command.Parameters.AddWithValue("@ID", ID);



            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
        static public bool DeletePerson(string NationalNo)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"
                            DELETE FROM [dbo].[People]
                                  WHERE People.NationalNo=@nationalnumber;
                             ";

            SqlCommand command = new SqlCommand(Querey, Connection);

            command.Parameters.AddWithValue("@nationalnumber", NationalNo);



            try
            {
                Connection.Open();
                command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
        
        
        static public bool FindPeron(int ID,ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gender, ref string Address, ref string PhoneNumber, ref string Email, ref int CountryID, ref string ImagePath)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select * From People 
                              Where PersonID=@ID";

            SqlCommand Command = new SqlCommand(Querey, Connection);

            Command.Parameters.AddWithValue("@ID", ID);


            bool IsFound = false;
            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if(Reader.Read())
                {
                    IsFound = true;

                    NationalNo = Convert.ToString(Reader["NationalNo"]);
                    FirstName = Convert.ToString(Reader["FirstName"]);
                    SecondName = Convert.ToString(Reader["SecondName"]);
                    ThirdName = Convert.ToString(Reader["ThirdName"]);
                    LastName = Convert.ToString(Reader["LastName"]);
                    DateOfBirth = DateTime.Parse(Convert.ToString(Reader["DateOfBirth"]));
                    Gender = (short)((Byte)(Reader["Gendor"]));
                    Address = Convert.ToString(Reader["Address"]);
                    PhoneNumber = Convert.ToString(Reader["Phone"]); 
                    Email = Convert.ToString(Reader["Email"]);  
                    CountryID = Convert.ToInt32(Reader["NationalityCountryID"]);
                    ImagePath = Reader["ImagePath"] != DBNull.Value ? Convert.ToString(Reader["ImagePath"]) : null;


                }
                else
                {
                    IsFound = false;
                }


            }
            catch(Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }
        static public bool FindPeron(string NationalNo ,ref int ID,  ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref short Gender, ref string Address, ref string PhoneNumber, ref string Email, ref int CountryID, ref string ImagePath)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select * From People 
                             Where NationalNo = @NationalNumber";

            SqlCommand Command = new SqlCommand(Querey, Connection);

            Command.Parameters.AddWithValue("@NationalNumber", NationalNo);


            bool IsFound = false;
            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    ID = Convert.ToInt32(Reader["PersonID"]);
                    FirstName = Convert.ToString(Reader["FirstName"]);
                    SecondName = Convert.ToString(Reader["SecondName"]);
                    ThirdName = Convert.ToString(Reader["ThirdName"]);
                    LastName = Convert.ToString(Reader["LastName"]);
                    DateOfBirth = DateTime.Parse(Convert.ToString(Reader["DateOfBirth"]));
                    Gender = Convert.ToByte(Reader["Gendor"]);
                    Address = Convert.ToString(Reader["Address"]);
                    PhoneNumber = Convert.ToString(Reader["Phone"]);
                    Email = Reader["Email"] != null ? Convert.ToString(Reader["Email"]) : null;
                    CountryID = Convert.ToInt32(Reader["NationalityCountryID"]);
                    ImagePath = Reader["ImagePath"] != DBNull.Value ? Convert.ToString(Reader["ImagePath"]) : null;


                }
                else
                {
                    IsFound = false;
                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }

        static public bool UpdatePerson(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, short Gender, string Address, string PhoneNumber, string Email, int CountryID, string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"UPDATE [dbo].[People]
                                SET [NationalNo] = @NationalNo
                                   ,[FirstName] = @FirstName
                                   ,[SecondName] =@SecondName
                                   ,[ThirdName] = @ThirdName
                                   ,[LastName] = @LastName
                                   ,[DateOfBirth] = @DateOfBirth
                                   ,[Gendor] = @Gendor
                                   ,[Address] = @Address
                                   ,[Phone] = @Phone
                                   ,[Email] = @Email
                                   ,[NationalityCountryID] = @CountryID
                                   ,[ImagePath] = @ImagePath
                              WHERE People.PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Querey, connection);

            command.Parameters.AddWithValue("@PersonID", ID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", PhoneNumber);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            if (String.IsNullOrEmpty(ImagePath))
            {
                command.Parameters.AddWithValue("@ImagePath",DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath",ImagePath);
            }

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

        static public bool IsExisted(int ID)
        {
            SqlConnection connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select 1 From People
                              where PersonID = @ID;";

            SqlCommand command = new SqlCommand(Querey, connection);

            command.Parameters.AddWithValue("@ID", ID);

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
        static public bool IsExisted(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select 1 From People
                              where NationalNo = @NationalNo;";

            SqlCommand command = new SqlCommand(Querey, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            bool IsFound = false;

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                IsFound = Result != null;

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


        static public DataTable ListPersons()
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,
                                Case 
                                When Gendor = 0 then 'Male'
                                When Gendor = 1 then 'Female'
                                End As Gender,
                                		
                                Address,Phone,Email,Countries.CountryName As Nationality
                               						
                                from People

								inner join Countries On People.NationalityCountryID = Countries.CountryID
                                
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

        static public string GetPersonImagePath(int ID)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select ImagePath From People Where PersonID = @ID";

            SqlCommand Command = new SqlCommand(Querey, Connection);

            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();

                string ImagePath = Convert.ToString(Command.ExecuteScalar());

                return ImagePath;
            }
            catch
            {
                return "";
            }
            finally
            {
                Connection.Close();
            }

            return ""; 
        }

        static public bool IsPersonConnectedWithAnyService(int ID)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"IF EXISTS (SELECT 1 FROM Users WHERE PersonID = @ID)
                OR EXISTS (SELECT 1 FROM Applications WHERE ApplicantPersonID = @ID)
                OR EXISTS (SELECT 1 FROM Drivers WHERE PersonID = @ID)
            BEGIN
                SELECT 1;
            END
            ELSE
            BEGIN
                SELECT 0;
            END"; 

            SqlCommand Command = new SqlCommand(Querey, Connection);

            Command.Parameters.AddWithValue("@ID", ID);

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
