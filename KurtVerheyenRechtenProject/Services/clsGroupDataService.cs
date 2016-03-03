using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KurtVerheyenRechtenProject.Model;
using KurtVerheyenRechtenProject.DAL;

namespace KurtVerheyenRechtenProject.Services
{
    class clsGroupDataService : IGroupDataService
    {
        IGroupRepository Repo = new clsGroupRepository();
        public clsGroupDataService()
        {
            this.Repo = Repo;
        }



        public bool DeleteGroup(clsGroups myGroup)
        {
            return Repo.DeleteGroup(myGroup);
        }

        public ObservableCollection<clsGroups> GetAllGroups()
        {
            return Repo.GetAllGroups();
        }

        public ObservableCollection<clsToegangsRechten> GetAllRightsByGroupId(int myId)
        {
            throw new NotImplementedException();
        }

        public clsGroups GetFirstGroup()
        {
            return Repo.GetFirstGroup();
        }


        public clsGroups GetGroupById(int myID)
        {
            return Repo.GetGroupById(myID);
        }

        public bool InsertGroup(clsGroups myGroup)
        {
            return Repo.InsertGroup(myGroup);
        }

        public bool UpdateGroup(clsGroups myGroup)
        {
            return Repo.UpdateGroup(myGroup);
        }
    }
}
