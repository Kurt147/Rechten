using KurtVerheyenRechtenProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KurtVerheyenRechtenProject.Services
{
    public interface IGroupDataService
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
