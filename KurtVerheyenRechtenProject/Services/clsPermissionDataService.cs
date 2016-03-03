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
    public class clsPermissionDataService : IPermissionDataService
    {
        IPermissionsRepository Repo = new clsPermissionsRepository();
        public clsPermissionDataService()
        {
            this.Repo = Repo;
        }

        public bool DeletePermission(clsToegangsRechten myPermission)
        {
            return Repo.DeletePermission(myPermission);
        }

        public clsToegangsRechten GetPermissionByGroupIDItemName(int GroupId, string ItemName)
        {
            return Repo.GetPermissionByGroupIDItemName(GroupId, ItemName);
        }

        public bool InsertPermission(clsToegangsRechten myPermission)
        {
            return Repo.InsertPermission(myPermission);
        }

        public bool UpdatePermission(clsToegangsRechten myPermission)
        {
            return Repo.UpdatePermission(myPermission);
        }
    }
}
