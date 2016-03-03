using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KurtVerheyenRechtenProject.Model;
using System.Collections.ObjectModel;

namespace KurtVerheyenRechtenProject.DAL
{
    public interface IPermissionsRepository
    {
        bool DeletePermission(clsToegangsRechten myPermission);
        bool InsertPermission(clsToegangsRechten myPermission);
        bool UpdatePermission(clsToegangsRechten myPermission);
        clsToegangsRechten GetPermissionByGroupIDItemName(int GroupId, string ItemName);
    }
}
