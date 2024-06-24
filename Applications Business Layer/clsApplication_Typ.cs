using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ApplicationsTyps_DataAccess_Layer;

namespace Application_Typs_Business_Layer
{
    public class clsApplication_Type
    {
        public enum EnMode { AddNew = 1, Update = 2 };

        public EnMode Mode;
        public int ApplicationTypeID { set; get; }
        public string ApplicationTypeName { set; get; }
        public double ApplicationTypeFees { set; get; }

        static public DataTable ListApplicationsTypes()
        {
            return ApplicationsTyps_DataAccess_Layer.DataAccess.ListApplications();
        }

        public clsApplication_Type()
        {
            Mode = EnMode.AddNew;

            ApplicationTypeID = -1;
            ApplicationTypeName = "";
            ApplicationTypeFees = -1.5;
        }

        private clsApplication_Type(int ApplicationtypeId,string ApplicationTypename,double ApplicationtypeFees)
        {
            Mode = EnMode.Update;

            ApplicationTypeID = ApplicationtypeId;
            ApplicationTypeName = ApplicationTypename;
            ApplicationTypeFees = ApplicationtypeFees;
        }

        private bool _UpdateApplicationType()
        {
            return DataAccess.UpdateApplicationTyps(ApplicationTypeID,ApplicationTypeName,ApplicationTypeFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case EnMode.AddNew:
                    return false;

                case EnMode.Update:
                    return _UpdateApplicationType();
                default:
                    return false;
            }
        }

        static public clsApplication_Type Find(int ID)
        {
            double fees = 0;
            string name = "";

            if(DataAccess.Find(ID,ref name,ref fees))
            {
                return new clsApplication_Type(ID, name, fees);
            }
            else
            {
                return new clsApplication_Type();
            }
        }

    }
}
