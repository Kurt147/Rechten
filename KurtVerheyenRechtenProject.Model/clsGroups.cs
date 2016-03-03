using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KurtVerheyenRechtenProject.Model
{
    public class clsGroups : INotifyPropertyChanged, IDataErrorInfo
    {
        private int groupID;
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        private string groupname;
        public string Groupname
        {
            get { return groupname; }
            set { groupname = value; }
        }


        private int mySelectedIndex;
        public int MySelectedIndex
        {
            get { return mySelectedIndex; }
            set
            {
                mySelectedIndex = value;
                RaisePropertyChanged("MySelectedIndex");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string myPropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(myPropertyName));
            }
        }
        private bool isDirty = false;
        public bool IsDirty
        {
            get { return isDirty; }
            set
            {
                isDirty = value;
                RaisePropertyChanged("IsDirty");
            }
        }
        private object controlField;
        public object ControlField
        {
            get { return controlField; }
            set { controlField = value; }
        }
        public string ErrorBoodschap { get; set; }
        private List<string> ErrorList = new List<string>();

        public string Error
        {
            get
            {
                if (ErrorList.Count > 0)
                {
                    return "NOK";
                }
                else
                {
                    return null;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if(obj is clsGroups)
            {
                clsGroups group = obj as clsGroups;
                if(this.groupID == group.groupID)
                {
                    return true;
                }
            }
            return false;
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "GroupName":
                        if (string.IsNullOrWhiteSpace(groupname))
                        {
                            error = "Groupname is a required field!";
                            if (ErrorList.Contains("GroupName") == false)
                            {
                                ErrorList.Add("GroupName");
                            }
                        }
                        else if (groupname.Length > 50)
                        {
                            error = "Your text is too long!";
                            if (ErrorList.Contains("GroupName") == false)
                            {
                                ErrorList.Add("GroupName");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("GroupName"))
                            {
                                ErrorList.Remove("GroupName");
                            }
                        }
                        return error;
                    default:
                        error = null;
                        return error;
                }
            }
        }
    }
}
