using KurtVerheyenRechtenProject.Model;
using KurtVerheyenRechtenProject.DAL;
using System.Collections.ObjectModel;
using System;

namespace KurtVerheyenRechtenProject.Services
{
    public class clsUserDataService : IUserDataService
    {
        IUserRepository Repo = new clsUserRepository();
        public clsUserDataService()
        {
            this.Repo = Repo;
        }

        public bool DeleteUser(clsUsers myUser)
        {
            return Repo.DeleteUser(myUser);
        }

        public ObservableCollection<clsUsers> GetAllUsers()
        {
            return Repo.GetAllUsers();
        }

        public clsUsers GetFirstUser()
        {
            return Repo.GetFirstUser();
        }

        public clsUsers GetUserById(int myId)
        {
            return Repo.GetUserById(myId);
        }

        public bool InsertUser(clsUsers myUser, int groupId)
        {
            return Repo.InsertUser(myUser, groupId);
        }

        public bool UpdateUser(clsUsers myUser)
        {
            return Repo.UpdateUser(myUser);
        }
    }
}
