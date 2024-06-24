using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Connection_Infos;

namespace Countries_Data_Access_Layer
{
    public class DataAccess
    {
        static public DataTable ListCountriesNames()
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select CountryName From Countries
                              order by CountryID
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

        static public string FindCountry(int ID)
        {
            SqlConnection Connection = new SqlConnection(clsConnectionInfos.ConnectionString);

            string Querey = @"Select CountryName From Countries Where CountryID = @ID";

            SqlCommand command = new SqlCommand(Querey, Connection);

            command.Parameters.AddWithValue("@ID", ID);

            string CountryName = "";
            try
            {
                Connection.Open();

                CountryName = Convert.ToString(command.ExecuteScalar());
            }
            catch
            {

            }
            finally
            {
                Connection.Close();
            }

            return CountryName;
        }
    }
}
