using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KurtVerheyenRechtenProject.Model;
using System.Collections.ObjectModel;

namespace KurtVerheyenRechtenProject.DAL
{
    public interface IUserRepository
    {
        clsUsers GetUserById(int myId);
        clsUsers GetFirstUser();
        ObservableCollection<clsUsers> GetAllUsers();
        ObservableCollection<clsUsers> GetAllUsersByGroupId(int myID);
        bool DeleteUser(clsUsers myUser);
        bool InsertUser(clsUsers myUser, int groupId);
        bool UpdateUser(clsUsers myUser);
    }
}
