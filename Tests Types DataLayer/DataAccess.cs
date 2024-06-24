using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Connection_Infos;

namespace Tests_Types_DataLayer
{
    public class DataAccess
    {
        static public DataTable ListTests()
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select * From TestTypes
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
        static public bool UpdateTestsTyps(int TestTypeID,string TestName, string Description,double TestFees)
        {
            SqlConnection connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"
                               Update TestTypes
                               set 
                               TestTypeTitle =@Name,
                               TestTypeDescription =@Descrtption,
                               TestTypeFees = @Fees
                               where TestTypeID = @ID;";

            SqlCommand command = new SqlCommand(Querey, connection);

            command.Parameters.AddWithValue("@Name", TestName);
            command.Parameters.AddWithValue("@Descrtption", Description);
            command.Parameters.AddWithValue("@Fees", TestFees);
            command.Parameters.AddWithValue("@ID", TestTypeID);

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
        static public bool Find(int Id, ref string name,ref string Description, ref double Fees)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select * From TestTypes 
                              Where TestTypeID=@ID";

            SqlCommand Command = new SqlCommand(Querey, Connection);

            Command.Parameters.AddWithValue("@ID", Id);


            bool IsFound = false;
            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    name = Reader["TestTypeTitle"].ToString();
                    Description = Reader["TestTypeDescription"].ToString();
                    Fees = Convert.ToDouble(Reader["TestTypeFees"]);
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


    }
}

