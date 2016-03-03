using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KurtVerheyenRechtenProject.Model;
using System.Collections.ObjectModel;

namespace KurtVerheyenRechtenProject.DAL
{
    public interface IGroupRepository
    {
        clsGroups GetGroupById(int myID);
        ObservableCollection<clsGroups> GetAllGroups();
        clsGroups GetFirstGroup();
        ObservableCollection<clsToegangsRechten> GetAllRightsByGroupId(int myId);
        bool DeleteGroup(clsGroups myGroup);
        bool InsertGroup(clsGroups myGroup);
        bool UpdateGroup(clsGroups myGroup);
    }
}
