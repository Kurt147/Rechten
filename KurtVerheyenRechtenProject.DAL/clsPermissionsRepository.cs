using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KurtVerheyenRechtenProject.Model;
using System.Data;

namespace KurtVerheyenRechtenProject.DAL
{
    public class clsPermissionsRepository : IPermissionsRepository
    {
        private static ObservableCollection<clsToegangsRechten> _Permissions;

        public clsPermissionsRepository()
        {
           
        }
        public ObservableCollection<clsToegangsRechten> LoadPermissions(int myId)
        {
            int nr = 0;
            _Permissions = new ObservableCollection<clsToegangsRechten>();
            DataTable DT = new DataTable();
            DT = clsDAC.ExecuteDataTable(Properties.Resources.S_PermissionsByGroupID.ToString(), ref nr,
              clsDAC.Parameter("@GroupID", myId));
            foreach (DataRow DR in DT.Rows)
            {
                var x = new clsToegangsRechten()
                {
                    GroupID = (int)DR[0],
                    ItemName = DR[1].ToString(),
                    ItemHeader = DR[2].ToString(),
                    Nieuw = (bool)DR[3],
                    Save = (bool)DR[4],
                    Delete = (bool)DR[5],
                    Cancel = (bool)DR[6],
                    Print = (bool)DR[7],
                    Find = (bool)DR[8],
                    Help = (bool)DR[9],
                    Close = (bool)DR[10],
                    ControlField = DR[11]
                };
                _Permissions.Add(x);
            }
            return _Permissions;
        }
        public bool DeletePermission(clsToegangsRechten myPermission)
        {
            int nr = 0;
            DataTable DTTemp = null;
            DTTemp = clsDAC.ExecuteDataTable(Properties.Resources.D_Permission.ToString(), ref nr,
              clsDAC.Parameter("@GroupID", myPermission.GroupID),
              clsDAC.Parameter("@ReturnValue", 0));

            return ErrorBoodschappen(nr, myPermission);
        }

        public bool InsertPermission(clsToegangsRechten myPermission)
        {
            int nr = 0;
            DataTable DTTemp = null;
            DTTemp = clsDAC.ExecuteDataTable(Properties.Resources.I_Permissions.ToString(), ref nr,
              clsDAC.Parameter("@GroupID", myPermission.GroupID),
              clsDAC.Parameter("@ItemName", myPermission.ItemName),
              clsDAC.Parameter("@ItemHeader", myPermission.ItemHeader),
              clsDAC.Parameter("@ItemNew", myPermission.Nieuw),
              clsDAC.Parameter("@ItemSave", myPermission.Save),
              clsDAC.Parameter("@ItemDelete", myPermission.Delete),
              clsDAC.Parameter("@ItemCancel", myPermission.Cancel),
              clsDAC.Parameter("@ItemPrint", myPermission.Print),
              clsDAC.Parameter("@ItemFind", myPermission.Find),
              clsDAC.Parameter("@ItemHelp", myPermission.Help),
              clsDAC.Parameter("@ItemClose", myPermission.Close));
            return ErrorBoodschappen(nr, myPermission);
        }

        public bool UpdatePermission(clsToegangsRechten myPermission)
        {
            int nr = 0;
            DataTable DTTemp = null;
            DTTemp = clsDAC.ExecuteDataTable(Properties.Resources.U_Permission.ToString(), ref nr,
              clsDAC.Parameter("@GroupID", myPermission.GroupID),
              clsDAC.Parameter("@ItemName", myPermission.ItemName),
              clsDAC.Parameter("@ItemHeader", myPermission.ItemHeader),
              clsDAC.Parameter("@ItemNew", myPermission.Nieuw),
              clsDAC.Parameter("@ItemSave", myPermission.Save),
              clsDAC.Parameter("@ItemDelete", myPermission.Delete),
              clsDAC.Parameter("@ItemCancel", myPermission.Cancel),
              clsDAC.Parameter("@ItemPrint", myPermission.Print),
              clsDAC.Parameter("@ItemFind", myPermission.Find),
              clsDAC.Parameter("@ItemHelp", myPermission.Help),
              clsDAC.Parameter("@ItemClose", myPermission.Close),
              clsDAC.Parameter("@ControlField", myPermission.ControlField),
              clsDAC.Parameter("@ReturnValue", 0));

            return ErrorBoodschappen(nr, myPermission);
        }

        public string ErrorBoodschop { get; set; }
        private bool ErrorBoodschappen(int myNR, clsToegangsRechten myPermission)
        {
            if (myNR == 1) //Het is gelukt
            {
                return true;
            }
            else if (myNR == 2) //Er was een concurrency probleem
            {
                myPermission.ErrorBoodschap = "Concurrency issue";
                return false;
            }
            else //er was een onbekend probleem
            {
                myPermission.ErrorBoodschap = "A oeps issue";
                return false;
            }

        }

        public clsToegangsRechten GetPermissionByGroupIDItemName(int GroupId, string ItemName)
        {
            if (_Permissions == null)
            {
                LoadPermissions();
            }
            return _Permissions.Where(c => c.GroupID == GroupId && c.ItemName == ItemName).FirstOrDefault();
        }

        private void LoadPermissions()
        {
            _Permissions = new ObservableCollection<clsToegangsRechten>();
            DataTable DT = new DataTable();
            DT = clsDAC.ExecuteDataTable(Properties.Resources.S_GetAllPermissions.ToString());
            foreach (DataRow DR in DT.Rows)
            {
                clsToegangsRechten x = new clsToegangsRechten()
                {
                    GroupID = (int)DR[0],
                    ItemName = DR[1].ToString(),
                    ItemHeader = DR[2].ToString(),
                    Nieuw = (bool)DR[3],
                    Save = (bool)DR[4],
                    Delete = (bool)DR[5],
                    Cancel = (bool)DR[6],
                    Print = (bool)DR[7],
                    Find = (bool)DR[8],
                    Help = (bool)DR[9],
                    Close = (bool)DR[10],
                    ControlField = DR[11]
                };
                _Permissions.Add(x);
            }
        }
    }
}
