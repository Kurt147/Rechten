using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KurtVerheyenRechtenProject.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KurtVerheyenRechtenProject.Services
{
    public interface IUserDataService
    {
        clsUsers GetUserById(int myId);
        clsUsers GetFirstUser();
        ObservableCollection<clsUsers> GetAllUsers();
        bool DeleteUser(clsUsers myUser);
        bool InsertUser(clsUsers myUser, int groupId);
        bool UpdateUser(clsUsers myUser);
    }
}
