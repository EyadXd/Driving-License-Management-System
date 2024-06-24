using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Connection_Infos;

namespace ApplicationsTyps_DataAccess_Layer
{
    static public class DataAccess
    {
        static public DataTable ListApplications()
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select * From ApplicationTypes
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
        static public bool UpdateApplicationTyps(int ApplicationID,string ApplicationName,double ApplicationFees)
        {
            SqlConnection connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"
                               Update ApplicationTypes
                               Set	ApplicationTypeTitle = @Title,
                               	ApplicationFees = @Fees
                               	where ApplicationTypeID = @ID;";

            SqlCommand command = new SqlCommand(Querey, connection);

            command.Parameters.AddWithValue("@Title", ApplicationName);
            command.Parameters.AddWithValue("@Fees", ApplicationFees);
            command.Parameters.AddWithValue("@ID", ApplicationID);




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
        static public bool Find(int Id ,ref string name ,ref double Fees)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select * From ApplicationTypes 
                              Where ApplicationTypeID=@ID";

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

                    name = Reader["ApplicationTypeTitle"].ToString();
                    Fees = Convert.ToDouble(Reader["ApplicationFees"]);
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
