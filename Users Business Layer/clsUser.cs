using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data_Access_Layer;
using System.Data;

namespace Users_Business_Layer
{
    public class clsUser
    {
        //vars
        public enum EnMode { AddNew = 1, Update = 2 };

        public EnMode Mode;
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }
        public int PersonID { set; get; }


        //Constractors
        public clsUser(int UserID,int PersonID,string UserName,string Password , bool IsActive)
        {
            this.Mode = EnMode.Update;

            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
        }

        public clsUser()
        {
            this.Mode = EnMode.AddNew;

            UserID = -1;
            UserName = "";
            Password = "";
            IsActive = false;
            PersonID = -1;
        }


        //Methods
        private int _AddUser()
        {
            return DataAccess.AddUser(PersonID, UserName, Password, IsActive);
        }
        private bool _UpdateUser()
        {
            return DataAccess.UpdateUser(UserID, PersonID, UserName, Password, IsActive);
        }


        static public bool IsExistedByUserID(int UserID)
        {
            return DataAccess.IsExistedByUserID(UserID);
        }
        static public bool IsExistedByPersonID(int PersonID)
        {
            return DataAccess.IsExistedByPersonID(PersonID);
        }
        static public bool IsExistedByUserName(string UserName)
        {
            return DataAccess.IsExistedByUserName(UserName);
        }
        

        static public clsUser FindByUserID(int UserID)
        {
            if (!IsExistedByUserID(UserID))
            {
                return new clsUser();
            }
            else
            {
                int personid = 0;
                string username = " ", password = " ";
                bool isactive = false;

                if (DataAccess.FindUserByUserID(UserID, ref username, ref password, ref isactive, ref personid))
                { 
                    return new clsUser(UserID, personid, username, password, isactive);
                }
                else
                {
                    return new clsUser();
                }
            }
        }
        static public clsUser FindByUserName(string UserName)
        {
            if (!IsExistedByUserName(UserName))
            {
                return new clsUser();
            }
            else
            {
                int personid = 0,userid = 0;
                string password = " ";
                bool isactive = false;

                if (DataAccess.FindUserByUserName(UserName,ref personid,ref userid,ref password,ref isactive))
                {
                    return new clsUser(userid, personid, UserName, password, isactive);
                }
                else
                {
                    return new clsUser();
                }
            }
        }
        static public clsUser FindByPersonID(int PersonID)
        {
            if (!IsExistedByPersonID(PersonID))
            {
                return new clsUser();
            }
            else
            {
                int userid = 0;
                string username = " ", password = " ";
                bool isactive = false;

                if (DataAccess.FindUserByPersonID(PersonID,ref userid,ref username,ref password,ref isactive))
                {
                    return new clsUser(userid, PersonID, username, password, isactive);
                }
                else
                {
                    return new clsUser();
                }
            }
        }


       public bool Save()
        {
            switch (Mode)
            {
                case EnMode.AddNew:
                    this.UserID = _AddUser();
                    return this.UserID != -1;
                        ;

                case EnMode.Update:
                    return _UpdateUser();
                default:
                    return false;
            }
        }


        static public bool DeleteUser(int UserID)
        {
            if (!(IsUserConnectedToAnyServiceByUserID(UserID)))
            {
                if (IsExistedByUserID(UserID))
                {
                    return (DataAccess.DeleteUserByUserID(UserID));
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

        static public DataTable ListUsers()
        {
            return DataAccess.ListUsers();
        }

        static public int GetPersonID(int UserID)
        {
            return DataAccess.GetPersonID(UserID);
        }

        static public bool IsUserConnectedToAnyServiceByUserID(int UserID)
        {
            return DataAccess.IsUserConnectedToAnyServiceByUserID(UserID);
        }

    }
}
