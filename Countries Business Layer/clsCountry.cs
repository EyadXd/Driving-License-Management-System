using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Countries_Business_Layer
{
    public class clsCountry
    {
        static public DataTable ListCountriesNames()
        {
            return Countries_Data_Access_Layer.DataAccess.ListCountriesNames();
        }
        static public string FindCountry(int ID)
        {
            return Countries_Data_Access_Layer.DataAccess.FindCountry(ID);
        }
    }
}
