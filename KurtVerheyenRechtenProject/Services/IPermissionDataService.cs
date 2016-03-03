using KurtVerheyenRechtenProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KurtVerheyenRechtenProject.Services
{
    public interface IPermissionDataService
    {
        bool DeletePermission(clsToegangsRechten myPermission);
        bool InsertPermission(clsToegangsRechten myPermission);
        bool UpdatePermission(clsToegangsRechten myPermission);
        clsToegangsRechten GetPermissionByGroupIDItemName(int GroupId, string ItemName);
    }
}
