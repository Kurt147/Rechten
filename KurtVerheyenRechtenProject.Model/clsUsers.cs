using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KurtVerheyenRechtenProject.Model
{
    public class clsUsers : INotifyPropertyChanged, IDataErrorInfo
    {
        private int userId;
        public int UserID
        {
            get { return userId; }
            set
            {
                if (userId != value)
                {
                    //IsDirty = true;
                    userId = value;
                    RaisePropertyChanged("UserID");
                }

            }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    if (userName != null)
                    {
                        IsDirty = true;
                    }
                    userName = value;
                    RaisePropertyChanged("UserName");
                }
            }
        }
        private string userPassword;
        public string UserPassword
        {
            get { return userPassword; }
            set
            {
                if (userPassword != value)
                {
                    if (userPassword != null)
                    {
                        IsDirty = true;
                    }
                    userPassword = value;
                    RaisePropertyChanged("UserPassword");
                }
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    if (email != null)
                    {
                        IsDirty = true;
                    }
                    email = value;
                    RaisePropertyChanged("Email");
                }
            }
        }
        private int groupID;
        public int GroupID
        {
            get { return groupID; }
            set
            {
                if (groupID != value)
                {
                    //IsDirty = true;
                    groupID = value;
                    RaisePropertyChanged("GroupID");
                }
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

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "UserName":
                        if (string.IsNullOrWhiteSpace(userName))
                        {
                            error = "UserName is a required field!";
                            if (ErrorList.Contains("UserName") == false)
                            {
                                ErrorList.Add("UserName");
                            }
                        }
                        else if (userName.Length > 50)
                        {
                            error = "Your text is too long!";
                            if (ErrorList.Contains("UserName") == false)
                            {
                                ErrorList.Add("UserName");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("UserName"))
                            {
                                ErrorList.Remove("UserName");
                            }
                        }
                        return error;
                    case "UserPassword":
                        if (string.IsNullOrWhiteSpace(userPassword))
                        {
                            error = "UserPassword is a required field!";
                            if (ErrorList.Contains("UserPassword") == false)
                            {
                                ErrorList.Add("UserPassword");
                            }
                        }
                        else if (userPassword.Length > 50)
                        {
                            error = "Your text is too long! (min 6 max 50)";
                            if (ErrorList.Contains("UserPassword") == false)
                            {
                                ErrorList.Add("UserPassword");
                            }
                        }
                        else if (userPassword.Length < 6)
                        {
                            error = "Your text is too short! (min 6 max 50)";
                            if (ErrorList.Contains("UserPassword") == false)
                            {
                                ErrorList.Add("UserPassword");
                            }
                        }
                        else
                        {
                            if (ErrorList.Contains("UserPassword"))
                            {
                                ErrorList.Remove("UserPassword");
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
