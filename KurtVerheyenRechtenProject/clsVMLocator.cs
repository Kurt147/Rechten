using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KurtVerheyenRechtenProject.ViewModels;

namespace KurtVerheyenRechtenProject
{
    public class clsVMLocator
    {
        #region My ViewModels
        private static clsUsersVM _clsUsersVM;
        #endregion

        public static clsUsersVM UsersViewModel
        {
            get
            {
                _clsUsersVM = new clsUsersVM();
                return _clsUsersVM;
            }
        }

        private static clsGroupsVM _clsGroupsVM;

        public static clsGroupsVM GroupsViewModel
        {
            get
            {
                _clsGroupsVM = new clsGroupsVM();
                return _clsGroupsVM;
            }
        }

    }
}
