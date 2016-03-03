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
    public class clsGroupRepository : IGroupRepository
    {
        private static ObservableCollection<clsGroups> groups;
        //public object DAC { get; set; }
        public clsGroupRepository()
        {

        }
        public clsGroups GetFirstGroup()
        {
            if (groups == null)
            {
                LoadGroups();
            }
            return groups.FirstOrDefault();
        }
        public bool DeleteGroup(clsGroups myGroup)
        {
            int nr = 0;
            DataTable DTTemp = null;
            DTTemp = clsDAC.ExecuteDataTable(Properties.Resources.D_Groups.ToString(), ref nr,
                clsDAC.Parameter("@GroupID", myGroup.GroupID),
                clsDAC.Parameter("@ControlField", myGroup.ControlField),
                clsDAC.Parameter("@ReturnValue", 0));

            return ErrorBoodschappen(nr, myGroup);
        }

        public ObservableCollection<clsGroups> GetAllGroups()
        {
            LoadGroups();
            return groups;
        }

        private void LoadGroups()
        {
            groups = new ObservableCollection<clsGroups>();
            DataTable DT = new DataTable();
            DT = clsDAC.ExecuteDataTable(Properties.Resources.S_Groups.ToString());
            foreach (DataRow DR in DT.Rows)
            {
                var x = new clsGroups()
                {
                    GroupID = (int)DR[0],
                    Groupname = DR[1].ToString(),
                    ControlField = DR[2]
                };
                groups.Add(x);
            }
        }

        public ObservableCollection<clsToegangsRechten> GetAllRightsByGroupId(int myId)
        {
            int nr = 0;
            DataTable DTTemp = null;
            DTTemp = clsDAC.ExecuteDataTable(Properties.Resources.S_PermissionsByGroupID.ToString(), ref nr,
              clsDAC.Parameter("@GroupID", myId));
            ObservableCollection<clsToegangsRechten> lijst = new ObservableCollection<clsToegangsRechten>();
            foreach (DataRow DR in DTTemp.Rows)
            {
                clsToegangsRechten re = new clsToegangsRechten();
                re.GroupID = (int)DR[0];
                re.ItemName = DR[1].ToString();
                re.ItemHeader = DR[2].ToString();
                re.Nieuw = (bool)DR[3];
                re.Save = (bool)DR[4];
                re.Delete = (bool)DR[5];
                re.Cancel = (bool)DR[6];
                re.Print = (bool)DR[7];
                re.Find = (bool)DR[8];
                re.Help = (bool)DR[9];
                re.Close = (bool)DR[10];

                lijst.Add(re);
            }

            return lijst;
        }

        public clsGroups GetGroupById(int myID)
        {
            if (groups == null)
            {
                LoadGroups();
            }
            return groups.Where(c => c.GroupID == myID).FirstOrDefault();
        }

        public bool InsertGroup(clsGroups myGroup)
        {
            int nr = 0;
            DataTable DTTemp = null;
            DTTemp = clsDAC.ExecuteDataTable(Properties.Resources.I_Groups.ToString(), ref nr,
              clsDAC.Parameter("@GroupName", myGroup.Groupname));
            return ErrorBoodschappen(nr, myGroup);
        }

        public bool UpdateGroup(clsGroups myGroup)
        {
            int nr = 0;
            DataTable DTTemp = null;
            DTTemp = clsDAC.ExecuteDataTable(Properties.Resources.U_Groups.ToString(), ref nr,
                clsDAC.Parameter("@GroupID", myGroup.GroupID),
                clsDAC.Parameter("@GroupName", myGroup.Groupname),
                clsDAC.Parameter("@ControlField", myGroup.ControlField),
                clsDAC.Parameter("@ReturnValue", 0));

            return ErrorBoodschappen(nr, myGroup);
        }

        public string ErrorBoodschop { get; set; }
        private bool ErrorBoodschappen(int myNR, clsGroups myGroup)
        {
            if (myNR == 1) //Het is gelukt
            {
                return true;
            }
            else if (myNR == 2) //Er was een concurrency probleem
            {
                myGroup.ErrorBoodschap = "Concurrency issue";
                return false;
            }
            else //er was een onbekend probleem
            {
                myGroup.ErrorBoodschap = "A oeps issue";
                return false;
            }

        }

    }
}
