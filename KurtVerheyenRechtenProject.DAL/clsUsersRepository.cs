using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KurtVerheyenRechtenProject.Model;
using System.Data;

namespace KurtVerheyenRechtenProject.DAL
{
    public class clsUserRepository : IUserRepository
    {
        private static ObservableCollection<clsUsers> _Users;
        //public object DAC { get; set; }
        public clsUserRepository()
        {

        }
        public clsUsers GetFirstUser()
        {
            if(_Users == null)
            {
                LoadUsers();
            }
            return _Users.FirstOrDefault();
        }

        public bool DeleteUser(clsUsers myUser)
        {
            int nr = 0;
            DataTable DTTemp = null;
            DTTemp = clsDAC.ExecuteDataTable(Properties.Resources.D_Users.ToString(), ref nr,
                clsDAC.Parameter("@userId", myUser.UserID),
                clsDAC.Parameter("@ControlField", myUser.ControlField),
                clsDAC.Parameter("@ReturnValue", 0));

            return ErrorBoodschappen(nr, myUser);
        }

        public ObservableCollection<clsUsers> GetAllUsers()
        {
            LoadUsers();
            return _Users;
        }

        private void LoadUsers()
        {
            _Users = new ObservableCollection<clsUsers>();
            DataTable DT = new DataTable();
            DT = clsDAC.ExecuteDataTable(Properties.Resources.S_Users.ToString());
            foreach (DataRow DR in DT.Rows)
            {
                clsUsers x = new clsUsers()
                {
                    UserID = (int)DR[0],
                    UserName = DR[1].ToString(),
                    UserPassword = DR[2].ToString(),
                    Email = DR[3].ToString(),
                    GroupID = (int)DR[4],
                    ControlField = DR[5]
                };
                _Users.Add(x);
            }
        }

        public clsUsers GetUserById(int myId)
        {
            if(_Users == null)
            {
                LoadUsers();
            }
            return _Users.Where(c => c.UserID == myId).FirstOrDefault();
        }

        public bool InsertUser(clsUsers myUser, int groupId)
        {
            int nr = 0;
            DataTable DTTemp = null;
            DTTemp = clsDAC.ExecuteDataTable(Properties.Resources.I_Users.ToString(), ref nr,
              clsDAC.Parameter("@Name", myUser.UserName),
              clsDAC.Parameter("@Password", myUser.UserPassword),
              clsDAC.Parameter("@Email", myUser.Email),
              clsDAC.Parameter("@GroupID", groupId));
              //clsDAC.Parameter("@ReturnValue", 0));
            return ErrorBoodschappen(nr, myUser);
        }

        public bool UpdateUser(clsUsers myUser)
        {
            int nr = 0;
            DataTable DTTemp = null;
            DTTemp = clsDAC.ExecuteDataTable(Properties.Resources.U_Users.ToString(), ref nr,
                clsDAC.Parameter("@userId", myUser.UserID),
                clsDAC.Parameter("@userName", myUser.UserName),
                clsDAC.Parameter("@userPassword", myUser.UserPassword),
                clsDAC.Parameter("@Email", myUser.Email),
                clsDAC.Parameter("@GroupID", myUser.GroupID),
                clsDAC.Parameter("@ControlField", myUser.ControlField),
                clsDAC.Parameter("@ReturnValue", 0));

            return ErrorBoodschappen(nr, myUser);
        }

        public string ErrorBoodschop { get; set; }
        private bool ErrorBoodschappen(int myNR, clsUsers myUser)
        {
            if(myNR == 1) //Het is gelukt
            {
                return true;
            }
            else if(myNR == 2) //Er was een concurrency probleem
            {
                myUser.ErrorBoodschap = "Concurrency issue";
                return false;
            }
            else //er was een onbekend probleem
            {
                myUser.ErrorBoodschap = "A oeps issue";
                return false;
            }

        }

        public ObservableCollection<clsUsers> GetAllUsersByGroupId(int myID)
        {
            if (_Users == null)
            {
                LoadUsers();
            }
            ObservableCollection<clsUsers> lijst = new ObservableCollection<clsUsers>();
            foreach(clsUsers user in _Users)
            {
                if(user.GroupID == myID)
                {
                    lijst.Add(user);
                }
            }
            return lijst;
        }
    }
}
