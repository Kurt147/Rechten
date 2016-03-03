using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KurtVerheyenRechtenProject.Services;
using System.Windows.Input;
using KurtVerheyenRechtenProject.Model;
using System.Windows;
using System.Collections.ObjectModel;

namespace KurtVerheyenRechtenProject.ViewModels
{
    public class clsGroupsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        clsGroupDataService _groupDataService;
        clsPermissionDataService _permissionDataService;
        clsToegangsRechten _Permission;

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
        private clsGroups _SelectedGroup;

        public clsGroups SelectedGroup
        {
            get
            {
                if (_SelectedGroup == null)
                {
                    _SelectedGroup = _groupDataService.GetFirstGroup();
                }
                return _SelectedGroup;
            }
           
            set
            {
                if (value != null)
                {
                    if (_SelectedGroup != null && _SelectedGroup.IsDirty == true)
                    {
                        if (MessageBox.Show("Do you want to save " + _SelectedGroup.Groupname + "?", "SAVE", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            _SelectedGroup.IsDirty = false;
                            ExecuteCMD_Save(null);
                            _SelectedGroup.MySelectedIndex = 0;
                            isSave = false;
                        }
                        else
                        {
                            LoadData();
                        }
                    }
                    _SelectedGroup = value;
                    isDelete = true;
                }
                RaisePropertyChanged("SelectedGroup");
            }
        }

        private clsToegangsRechten _SelectedPermission;
        public clsToegangsRechten SelectedPermission
        {
            get
            {
                if(_SelectedPermission == null)
                {
                    _SelectedPermission = _permissionDataService.GetPermissionByGroupIDItemName(SelectedGroup.GroupID, clsVariabelen.itemName);
                }
                return _SelectedPermission;
            }
            set
            {
                _SelectedPermission = value;
                RaisePropertyChanged("SelectedPermission");
            }
        }

        private ObservableCollection<clsGroups> _Groups;
        public ObservableCollection<clsGroups> Groups
        {
            get { return _groupDataService.GetAllGroups(); }
            set
            {
                _Groups = value;
                RaisePropertyChanged("Groups");

            }
        }
        public clsGroupsVM()
        {
            _groupDataService = new clsGroupDataService();
            cmdSave = new clsCustomCommand(ExecuteCMD_Save, CanExecuteCMD_Save);
            cmdDelete = new clsCustomCommand(ExecuteCMD_Delete, CanExecuteCMD_Delete);
            cmdNew = new clsCustomCommand(ExecuteCMD_New, CanExecuteCMD_New);
        }
        private bool CanExecuteCMD_New(object obj)
        {
            //return SelectedPermission.Nieuw;
            return true;
        }

        private void ExecuteCMD_New(object obj)
        {
            clsGroups GroupToInsert = new clsGroups()
            {
                GroupID = 0,
                Groupname = string.Empty,
                ControlField = null
            };
            SelectedGroup = GroupToInsert;

            isDelete = false;
            isSave = false;

            NewStatus = true;
        }

        private bool CanExecuteCMD_Delete(object obj)
        {
            //return SelectedPermission.Delete;
            return true;
        }

        private void ExecuteCMD_Delete(object obj)
        {
            if (MessageBox.Show("This record  " + _SelectedGroup.Groupname + " will be deleted", "Delete", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                if (SelectedGroup != null)
                {
                    if (_groupDataService.DeleteGroup(SelectedGroup))
                    {
                        LoadData();
                        SelectedGroup.MySelectedIndex = 0;
                        NewStatus = false;
                    }
                    else
                    {
                        MessageBox.Show(SelectedGroup.ErrorBoodschap);
                    }
                }
            }
        }

        private bool CanExecuteCMD_Save(object obj)
        {
            //return SelectedPermission.Save;
            return true;
        }

        private void ExecuteCMD_Save(object obj)
        {
            if (SelectedGroup != null)
            {
                if (NewStatus == true)
                {
                    if (_groupDataService.InsertGroup(SelectedGroup))
                    {
                        LoadData();
                        SelectedGroup.IsDirty = false;
                        SelectedGroup.MySelectedIndex = (int)Groups.Count - 1;
                        NewStatus = false;
                        isSave = false;
                        isNew = true;
                    }
                    else
                    {
                        MessageBox.Show(SelectedGroup.ErrorBoodschap);
                    }
                }
                else
                {
                    if (_groupDataService.UpdateGroup(SelectedGroup))
                    {
                        LoadData();
                        SelectedGroup.IsDirty = false;
                        SelectedGroup.MySelectedIndex = 0;
                        NewStatus = false;
                        isSave = false;
                        isNew = false;
                    }
                    else
                    {
                        MessageBox.Show(SelectedGroup.ErrorBoodschap);
                    }
                }
            }
        }

        private void LoadData()
        {
            Groups = _groupDataService.GetAllGroups();
        }
    }
}
