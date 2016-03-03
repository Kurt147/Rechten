using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KurtVerheyenRechtenProject.Model
{
    public class clsToegangsRechten : INotifyPropertyChanged
    {
        private int groupID;
        public int GroupID
        {
            get { return groupID; }
            set
            {
                groupID = value;
                RaisePropertyChanged("GroupID");
            }
        }
        private string itemName;
        public string ItemName
        {
            get { return itemName; }
            set
            {
                itemName = value;
                RaisePropertyChanged("ItemName");
            }
        }
        private string itemHeader;
        public string ItemHeader
        {
            get { return itemHeader; }
            set
            {
                itemHeader = value;
                RaisePropertyChanged("ItemHeader");
            }
        }
        private bool nieuw;
        public bool Nieuw
        {
            get { return nieuw; }
            set
            {
                nieuw = value;
                RaisePropertyChanged("Nieuw");
            }
        }
        private bool save;
        public bool Save
        {
            get { return save; }
            set {
                save = value;
                RaisePropertyChanged("Save");
            }
        }
        private bool delete;
        public bool Delete
        {
            get { return delete; }
            set {
                delete = value;
                RaisePropertyChanged("Delete");
            }
        }
        private bool print;
        public bool Print
        {
            get { return print; }
            set {
                print = value;
                RaisePropertyChanged("Print");
            }
        }
        private bool find;
        public bool Find
        {
            get { return find; }
            set {
                find = value;
                RaisePropertyChanged("Find");
            }
        }
        private bool help;
        public bool Help
        {
            get { return help; }
            set {
                help = value;
                RaisePropertyChanged("Help");
            }
        }
        private bool close;
        public bool Close
        {
            get { return close; }
            set {
                close = value;
                RaisePropertyChanged("Close");
            }
        }
        private bool cancel;
        public bool Cancel
        {
            get { return cancel; }
            set {
                cancel = value;
                RaisePropertyChanged("Cancel");
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
            set
            {
                controlField = value;
                RaisePropertyChanged("ControlField");
            }
        }
        public string ErrorBoodschap { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is clsToegangsRechten)
            {
                clsToegangsRechten _perm = obj as clsToegangsRechten;
                if (this.groupID == _perm.groupID && this.itemHeader == _perm.itemHeader && this.itemName == _perm.itemName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
