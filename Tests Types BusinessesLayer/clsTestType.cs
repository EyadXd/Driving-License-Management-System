using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Tests_Types_DataLayer;

namespace Tests_Types_BusinessesLayer
{
    public class clsTestType
    {
        public enum EnMode { AddNew = 1, Update = 2 };

        public EnMode Mode;
        public int TestTypeID { set; get; }
        public string TestTypeName { set; get; }
        public string TestTypeDescription { set; get; }
        public double TestTypeFees { set; get; }

        static public DataTable ListtestsTypes()
        {
            return DataAccess.ListTests();
        }

        public clsTestType()
        {
            Mode = EnMode.AddNew;

            TestTypeID = -1;
            TestTypeName = "";
            TestTypeDescription = "";
            TestTypeFees = -1.5;
        }

        private clsTestType(int TesttypeId, string TestTypename, string TestDescription, double TesttypeFees)
        {
            Mode = EnMode.Update;

            TestTypeID = TesttypeId;
            TestTypeName = TestTypename;
            TestTypeDescription = TestDescription;
            TestTypeFees = TesttypeFees;
        }

        private bool _UpdateTestType()
        {
            return DataAccess.UpdateTestsTyps(TestTypeID,TestTypeName,TestTypeDescription,TestTypeFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case EnMode.AddNew:
                    return false;

                case EnMode.Update:
                    return _UpdateTestType();
                default:
                    return false;
            }
        }

        static public clsTestType Find(int ID)
        {
            double fees = 0;
            string name = "";
            string Description = "";

            if (DataAccess.Find(ID, ref name,ref Description, ref fees))
            {
                return new clsTestType(ID, name, Description, fees);
            }
            else
            {
                return new clsTestType();
            }
        }

    }
}

