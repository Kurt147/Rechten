using KurtVerheyenRechtenProject.Model;
using KurtVerheyenRechtenProject.Services;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System;

namespace KurtVerheyenRechtenProject.ViewModels
{
    public class clsUsersVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        clsUserDataService _UserDataService;
        clsGroupDataService _GroupDataService = new clsGroupDataService();
        private bool NewStatus = false;
        public ICommand cmdSave { get; set; }
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }

        bool isDelete = false;
        bool isSave = false;
        bool isNew = true;

        private bool _IsFocused;

        public bool IsFocused
        {
            get { return _IsFocused; }
            set
            {
                _IsFocused = value;
                RaisePropertyChanged("IsFocused");
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private clsUsers _SelectedUser;

        public clsUsers SelectedUser
        {
            get
            {
                if (_SelectedUser == null)
                {
                    _SelectedUser = _UserDataService.GetFirstUser();
                }
                _SelectedGroupp = _GroupDataService.GetGroupById(_SelectedUser.GroupID);
                //SelectedUser.GroupID = SelectedGroupp.GroupID;

                return _SelectedUser;

            }
            set
            {
                if (value != null)
                {
                    if (_SelectedUser != null && _SelectedUser.IsDirty == true)
                    {
                        if (MessageBox.Show("Do you want to save " + _SelectedUser.UserName + "?", "SAVE", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            _SelectedUser.IsDirty = false;
                            ExecuteCMD_Save(null);
                            _SelectedUser.MySelectedIndex = 0;
                            isSave = false;
                        }
                        else
                        {
                            LoadData();
                        }
                    }
                    _SelectedUser = value;
                    isDelete = true;
                }
                RaisePropertyChanged("SelectedUser");
                RaisePropertyChanged("SelectedGroupp");
            }
        }

        private clsGroups _SelectedGroupp;

        public clsGroups SelectedGroupp
        {
            get
            {
                //clsGroups group = new clsGroups();
                //group = _GroupDataService.GetGroupById(SelectedUser.GroupID);
                return _SelectedGroupp;
            }
            set
            {
                _SelectedGroupp = value;
                RaisePropertyChanged("SelectedGroupp");
            }
        }
        private ObservableCollection<clsUsers> _Users;
        public ObservableCollection<clsUsers> Users
        {
            get { return _UserDataService.GetAllUsers(); }
            set
            {
                _Users = value;
                RaisePropertyChanged("Users");

            }
        }

        private ObservableCollection<clsGroups> _Groupss;
        public ObservableCollection<clsGroups> Groupss
        {
            get { return _GroupDataService.GetAllGroups(); }
            set
            {
                _Groupss = value;
                RaisePropertyChanged("Groupss");
            }
        }

        public clsUsersVM()
        {
            _UserDataService = new clsUserDataService();
            cmdSave = new clsCustomCommand(ExecuteCMD_Save, CanExecuteCMD_Save);
            cmdDelete = new clsCustomCommand(ExecuteCMD_Delete, CanExecuteCMD_Delete);
            cmdNew = new clsCustomCommand(ExecuteCMD_New, CanExecuteCMD_New);
        }

        private bool CanExecuteCMD_New(object obj)
        {
            if (NewStatus)
            {
                isNew = false;
            }

            //clsToegangsRechten _Permission;

            //clsPermissionDataService _PermissonDataService = new clsPermissionDataService();

            //_Permission = _PermissonDataService.GetPermissionByGroupIDItemName(SelectedGroupp.GroupID, "test");

            return isNew;
        }

        private void ExecuteCMD_New(object obj)
        {
            clsUsers UserToInsert = new clsUsers()
            {
                UserID = 0,
                UserName = string.Empty,
                UserPassword = string.Empty,
                Email = string.Empty,
                GroupID = 0,
                ControlField = null
            };
            SelectedUser = UserToInsert;

            isDelete = false;
            isSave = false;

            NewStatus = true;
        }

        private bool CanExecuteCMD_Delete(object obj)
        {
            if (SelectedUser != null)
            {
                isDelete = true;
                if (NewStatus)
                {
                    isDelete = false;
                }
            }
            else
            {
                isDelete = false;
            }
            return isDelete;
        }

        private void ExecuteCMD_Delete(object obj)
        {
            if (MessageBox.Show("This record  " + _SelectedUser.UserName + " will be deleted", "Delete", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                if (SelectedUser != null)
                {
                    if (_UserDataService.DeleteUser(SelectedUser))
                    {
                        LoadData();
                        SelectedUser.MySelectedIndex = 0;
                        NewStatus = false;
                    }
                    else
                    {
                        MessageBox.Show(SelectedUser.ErrorBoodschap);
                    }
                }
            }
        }

        private bool CanExecuteCMD_Save(object obj)
        {
            //if (SelectedUser != null && SelectedUser.Error == null && SelectedUser.IsDirty == true)
            //{
            //    isDelete = true;
            //    isSave = true;
            //}

            //else
            //{
            //    isDelete = false;
            //    isSave = false;
            //}
            isSave = true;
            return isSave;
        }

        private void ExecuteCMD_Save(object obj)
        {
            if (SelectedUser != null)
            {
                if (NewStatus == true)
                {
                    
                    //SelectedUser.GroupID = SelectedGroupp.GroupID;
                    
                    if (_UserDataService.InsertUser(SelectedUser, clsVariabelen.groupIDcboBox))
                    {
                        LoadData();
                        SelectedUser.IsDirty = false;
                        SelectedUser.MySelectedIndex = Users.Count - 1;
                        NewStatus = false;
                        isSave = false;
                        isNew = true;
                    }
                    else
                    {
                        MessageBox.Show(SelectedUser.ErrorBoodschap);
                    }
                }
                else
                {
                    if (_UserDataService.UpdateUser(SelectedUser))
                    {
                        LoadData();
                        SelectedUser.IsDirty = false;
                        SelectedUser.MySelectedIndex = 0;
                        NewStatus = false;
                        isSave = false;
                        isNew = false;
                    }
                    else
                    {
                        MessageBox.Show(SelectedUser.ErrorBoodschap);
                    }
                }
            }
        }

        private void LoadData()
        {
            Users = _UserDataService.GetAllUsers();
        }
    }
}
