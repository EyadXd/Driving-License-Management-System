using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persons_Data_Access_Layer;
using System.Data;
using System.IO;
using System.Threading;

namespace Persons_Business_Layer
{
    public class clsPerson
    {
        public enum EnMode {AddNew = 1 , Update = 2};

        public EnMode _Mode;
        public int ID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CountryID { get; set; }
        public string ImagePath { get; set; }



        private clsPerson(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, short Gender, string Address, string PhoneNumber, string Email, int CountryID, string ImagePath)
        {
            this._Mode = EnMode.Update;

            this.ID = ID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;
        }
        public clsPerson()
        {
            this._Mode = EnMode.AddNew;

            this.ID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Today;
            this.Gender = -1;
            this.Address = "";
            this.PhoneNumber = "";
            this.Email = "";
            this.CountryID = 0;
            this.ImagePath = null;
        }



        private int _AddPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, short Gender, string Address, string PhoneNumber, string Email, int CountryID, string ImagePath)
        {
            return Persons_Data_Access_Layer.clsDataAccess.AddPerson(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, PhoneNumber, Email, CountryID, ImagePath);
        }
        private bool _UpdatePerson(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, short Gender, string Address, string PhoneNumber, string Email, int CountryID, string ImagePath)
        {
            return Persons_Data_Access_Layer.clsDataAccess.UpdatePerson(ID,NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, PhoneNumber, Email, CountryID, ImagePath);
        }


        static public bool IsExisted(int ID)
        {
            return clsDataAccess.IsExisted(ID);
        }
        static public bool IsExisted(string NationalNo)
        {
            return clsDataAccess.IsExisted(NationalNo);
        }


        static public clsPerson FindPerson(int ID)
        {
            if(!IsExisted(ID))
            {
                return new clsPerson();
            }    
            string Nationalno = "", Firstname = "", Lastname = "", Secondname = "", Thirdname = "", email = "", phone = "", address = "", imagepath = "";
            short gender = -1;
            int countryid = 0;
            DateTime dateOfbirth = DateTime.Now;

            if(clsDataAccess.FindPeron(ID, ref Nationalno, ref Firstname, ref Secondname, ref Thirdname, ref Lastname, ref dateOfbirth, ref gender, ref address, ref phone, ref email, ref countryid,ref imagepath))
            {
                return new clsPerson(ID, Nationalno,Firstname,Secondname,Thirdname,Lastname,dateOfbirth,gender,address,phone,email,countryid,imagepath);
            }
            else
            {
                return new clsPerson();
            }
        }
        static public clsPerson FindPerson(string Nationalno)
        {
            if (!IsExisted(Nationalno))
            {
                return new clsPerson();
            }
            int Id = -1;string Firstname = "", Lastname = "", Secondname = "", Thirdname = "", email = "", phone = "", address = "", imagepath = "";
            short gender = -1;
            int countryid = 0;
            DateTime dateOfbirth = DateTime.Now;

            if (clsDataAccess.FindPeron(Nationalno,ref Id, ref Firstname, ref Secondname, ref Thirdname, ref Lastname, ref dateOfbirth, ref gender, ref address, ref phone, ref email, ref countryid, ref imagepath))
            {
                return new clsPerson(Id, Nationalno, Firstname, Secondname, Thirdname, Lastname, dateOfbirth, gender, address, phone, email, countryid, imagepath);
            }
            else
            {
                return new clsPerson();
            }
        }

        static public bool DeletePerson(int ID)
        {
            if (!(IsPersonConnectedWithOtherService(ID)))
            {
                if (IsExisted(ID))
                {
                    return clsDataAccess.DeletePerson(ID);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        static public bool DeletePerson(int ID,string ImagePath)
        {
            if (!IsPersonConnectedWithOtherService(ID))
            {
                if (IsExisted(ID))
                {
                    if (ImagePath != "")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (ImagePath != "")
                            {
                                try
                                {
                                    File.Delete(ImagePath);
                                    break;
                                }
                                catch (IOException)
                                {
                                    Thread.Sleep(1000);
                                }
                            }
                        }
                    }
                        return clsDataAccess.DeletePerson(ID);
                    
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        static public bool DeletePerson(string Nationalno)
        {
            if (IsExisted(Nationalno))
            {
                return clsDataAccess.DeletePerson(Nationalno);
            }
            else
            {
                return false;
            }
        }
        public bool DeletePerson()
        {
            if (IsExisted(this.NationalNo))
            {
                
                return (DeletePerson(this.NationalNo));
            }
            else
            {
                return false;
            }
        }

        static public DataTable ListPersons()
        {
            DataTable tb = clsDataAccess.ListPersons();
            return tb;
        }



        public bool Save()
        {
            switch (_Mode)
            {
                case EnMode.Update:
                    return _UpdatePerson(ID,NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gender,Address,PhoneNumber,Email,CountryID,ImagePath);

                case EnMode.AddNew:
                    this.ID=_AddPerson(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, PhoneNumber, Email, CountryID, ImagePath);
                    return this.ID != -1;

                default:
                    return false;
            }
        }


        static public string GetPersonImagePath(int PersonID)
        {
            return clsDataAccess.GetPersonImagePath(PersonID);
        }

        static public bool IsPersonConnectedWithOtherService(int PersonID)
        {
            return clsDataAccess.IsPersonConnectedWithAnyService(PersonID);
        }










    }
}
