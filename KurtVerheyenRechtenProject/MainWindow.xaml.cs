using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using KurtVerheyenRechtenProject.Commands;
using KurtVerheyenRechtenProject.Model;
using KurtVerheyenRechtenProject.DAL;
using KurtVerheyenRechtenProject.View;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace KurtVerheyenRechtenProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool AllRightsChecked = true;
        int help = 0;
        ObservableCollection<clsToegangsRechten> resultIntersection = new ObservableCollection<clsToegangsRechten>();
        private bool AllButtonsChecked = true;
        private ObservableCollection<clsToegangsRechten> _Permissions = new ObservableCollection<clsToegangsRechten>();
        private void OpenUserControl(UserControl myUS)
        {
            Grid gr = new Grid();
            if (grdMain.Children.Count > 1)
            {
                grdMain.Children.RemoveAt(1);
            }
            Grid.SetColumn(myUS, 2);
            Grid.SetRow(myUS, 5);
            grdMain.Children.Add(myUS);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            treeMenu.Items.Clear();
            addTreeViewMenu();
        }
        public void addTreeViewMenu()
        {
            foreach (MenuItem menuItem in menu.Items)
            {
                TreeViewItem item = new TreeViewItem();
                Style style = this.FindResource("TopLevel") as Style;
                item.Style = style;
                item.Header = new CheckBox()
                {
                    Content = menuItem.Header,
                    //Name = menuItem.Name + "TreeViewItem",
                    Command = MyCommands.CheckboxChecked,
                };
                item.Name = menuItem.Name + "TreeViewItem";
                if (menuItem.HasItems)
                {
                    addTreeViewMenuSubItems(menuItem, item);
                    item.IsExpanded = true;
                    treeMenu.Items.Add(item);
                }
                else
                {
                    ucTreeViewItems uc = new ucTreeViewItems();
                    uc.chkHeader.Content = menuItem.Header;
                    uc.chkHeader.Command = MyCommands.CheckboxChecked;
                    item.Header = uc;
                    treeMenu.Items.Add(item);
                }
            }

        }
        public void addTreeViewMenu(TreeView treeview)
        {
            //ucTreeViewItems uc = new ucTreeViewItems();
            //uc.Content = (ucTreeViewItems)subItem.Header;

            //item.Header = uc;
            foreach (TreeViewItem menuItem in treeview.Items)
            {
                TreeViewItem item = new TreeViewItem();
                item.Name = menuItem.Name + "TreeViewItem";
                if (menuItem.HasItems)
                {
                    Style style = this.FindResource("MidLevel") as Style;
                    item.Style = style;
                    CheckBox check = (CheckBox)menuItem.Header;
                    menuItem.Header = null;
                    item.Header = check;
                    addTreeViewMenuSubItems(menuItem, item);
                    item.IsExpanded = true;
                    treeMenu.Items.Add(item);
                }
                else
                {
                    Style style = this.FindResource("LowLevel") as Style;
                    item.Style = style;
                    ucTreeViewItems uc = new ucTreeViewItems();
                    uc.chkHeader.Content = (ucTreeViewItems)menuItem.Header;
                    item.Header = uc;
                    treeMenu.Items.Add(item);
                }
            }

        }
        public void addTreeViewMenu(TreeView treeview, TreeView copyview)
        {
            //ucTreeViewItems uc = new ucTreeViewItems();
            //uc.Content = (ucTreeViewItems)subItem.Header;

            //item.Header = uc;
            foreach (TreeViewItem menuItem in treeview.Items)
            {

                TreeViewItem item = new TreeViewItem();
                Style style = this.FindResource("TopLevel") as Style;
                item.Style = style;
                if (menuItem.HasItems)
                {

                    CheckBox check = (CheckBox)menuItem.Header;
                    check.Command = MyCommands.CheckboxChecked;
                    menuItem.Header = null;
                    item.Header = check;
                    addTreeViewMenuSubItems(menuItem, item);
                    item.IsExpanded = true;
                    copyview.Items.Add(item);
                }
                else
                {

                    ucTreeViewItems uc = new ucTreeViewItems();
                    uc.Content = (ucTreeViewItems)menuItem.Header;
                    uc.chkHeader.Command = MyCommands.CheckboxChecked;
                    item.Header = uc;
                    copyview.Items.Add(item);
                }
            }

        }
        public void addTreeViewMenuSubItems(MenuItem menuItem, TreeViewItem currItem)
        {
            foreach (MenuItem subItem in menuItem.Items)
            {
                TreeViewItem item = new TreeViewItem();
                Style style = this.FindResource("MidLevel") as Style;
                item.Style = style;
                item.Header = new CheckBox()
                {
                    Content = subItem.Header,
                    Command = MyCommands.CheckboxChecked,
                };
                item.Name = subItem.Name + "TreeViewItem";
                if (subItem.HasItems)
                {
                    currItem.Items.Add(item);
                    addTreeViewMenuSubItems(subItem, item);
                    item.IsExpanded = true;
                }
                else
                {
                    ucTreeViewItems uc = new ucTreeViewItems();
                    Style styleLow = this.FindResource("LowLevel") as Style;
                    item.Style = styleLow;
                    uc.chkHeader.Content = subItem.Header;
                    uc.chkHeader.Command = MyCommands.CheckboxChecked;
                    item.Name = subItem.Name + "TreeViewItem";
                    item.Header = uc;
                    currItem.Items.Add(item);
                }
            }
        }
        private void btnLoadRightsOfGroup_Click(object sender, RoutedEventArgs e)
        {
            treeMenu.Items.Clear();
            addTreeViewMenu();
            if (clsVariabelen.groupIDcboBox != 0)
            {
                //clsPermissionsRepository _Permission;
                clsGroupRepository _Group = new clsGroupRepository();
                _Permissions = null;
                _Permissions = _Group.GetAllRightsByGroupId(clsVariabelen.groupIDcboBox);
                foreach (TreeViewItem item in treeMenu.Items)
                {
                    foreach (clsToegangsRechten _Permission in _Permissions)
                    {
                        if (item.Header.GetType() == typeof(CheckBox))
                        {
                            CheckBox check = (CheckBox)item.Header;

                            if (_Permission.ItemName == item.Name && _Permission.ItemHeader == check.Content.ToString())
                            {
                                check.IsChecked = true;
                                if (item.HasItems)
                                {
                                    checkByGroupId(item);
                                }
                            }
                        }
                        else if (item.Header.GetType() == typeof(ucTreeViewItems))
                        {
                            ucTreeViewItems ucTree = (ucTreeViewItems)item.Header;
                            if (_Permission.ItemName == item.Name && _Permission.ItemHeader == ucTree.chkHeader.Content.ToString())
                            {
                                ucTree.chkHeader.IsChecked = true;
                                ucTree.chkNew.IsChecked = _Permission.Nieuw;
                                ucTree.chkSave.IsChecked = _Permission.Save;
                                ucTree.chkDelete.IsChecked = _Permission.Delete;
                                ucTree.chkCancel.IsChecked = _Permission.Cancel;
                                ucTree.chkPrint.IsChecked = _Permission.Print;
                                ucTree.chkFind.IsChecked = _Permission.Find;
                                ucTree.chkHelp.IsChecked = _Permission.Help;
                                ucTree.chkClose.IsChecked = _Permission.Close;

                            }
                        }

                    }

                }
            }
        }
        private void checkByGroupId(TreeViewItem item)
        {
            foreach (TreeViewItem childItem in item.Items)
            {
                foreach (clsToegangsRechten _Permission in _Permissions)
                {
                    if (childItem.Header.GetType() == typeof(CheckBox))
                    {
                        CheckBox check = (CheckBox)childItem.Header;

                        if (_Permission.ItemName == item.Name && _Permission.ItemHeader == check.Content.ToString())
                        {
                            check.IsChecked = true;
                            if (childItem.HasItems)
                            {
                                checkByGroupId(childItem);
                            }
                        }
                    }
                    else if (childItem.Header.GetType() == typeof(ucTreeViewItems))
                    {
                        ucTreeViewItems ucTree = (ucTreeViewItems)childItem.Header;
                        if (_Permission.ItemName == item.Name && _Permission.ItemHeader == ucTree.chkHeader.Content.ToString())
                        {
                            ucTree.chkHeader.IsChecked = true;
                            ucTree.chkNew.IsChecked = _Permission.Nieuw;
                            ucTree.chkSave.IsChecked = _Permission.Save;
                            ucTree.chkDelete.IsChecked = _Permission.Delete;
                            ucTree.chkCancel.IsChecked = _Permission.Cancel;
                            ucTree.chkPrint.IsChecked = _Permission.Print;
                            ucTree.chkFind.IsChecked = _Permission.Find;
                            ucTree.chkHelp.IsChecked = _Permission.Help;
                            ucTree.chkClose.IsChecked = _Permission.Close;

                        }
                    }
                }
            }
        }

        public void addTreeViewMenuSubItems(TreeViewItem menuItem, TreeViewItem currItem)
        {
            foreach (TreeViewItem subItem in menuItem.Items)
            {
                TreeViewItem item = new TreeViewItem();
                Style style = this.FindResource("MidLevel") as Style;
                item.Style = style;
                TreeViewItem itemHeader = new TreeViewItem();
                itemHeader = subItem;

                if (itemHeader.HasItems)
                {
                    CheckBox check = (CheckBox)itemHeader.Header;
                    check.Command = MyCommands.CheckboxChecked;
                    itemHeader.Header = null;
                    //check.Content = subItem.Header;
                    item.Header = check;
                    currItem.Items.Add(item);
                    addTreeViewMenuSubItems(itemHeader, item);
                    item.IsExpanded = true;

                }
                else
                {
                    ucTreeViewItems uc = new ucTreeViewItems();
                    Style styleLow = this.FindResource("LowLevel") as Style;
                    item.Style = styleLow;
                    uc.Content = (ucTreeViewItems)itemHeader.Header;
                    uc.chkHeader.Command = MyCommands.CheckboxChecked;
                    item.Header = uc;

                    currItem.Items.Add(item);
                }
            }
        }

        //public void checkTreeView(clsGroups Group)
        //{
        //    clsPermissionsRepository _perRepo = new clsPermissionsRepository();

        //    ObservableCollection<clsToegangsRechten> _Permissions = new ObservableCollection<clsToegangsRechten>();
        //    foreach (clsToegangsRechten x in _perRepo.LoadPermissions(Group.GroupID))
        //    {
        //        _Permissions.Add(x);
        //    }

        //    foreach (TreeViewItem item in treeMenu.Items)
        //    {
        //        clsToegangsRechten _PermissionCheck = new clsToegangsRechten();
        //        _PermissionCheck.ItemName = "beheer";
        //        _PermissionCheck.ItemHeader = "beheer";
        //        _PermissionCheck.GroupID = Group.GroupID;
        //        if (_Permissions.Contains(_PermissionCheck))
        //        {
        //            MessageBox.Show("ok");
        //        }
        //        else
        //        {
        //            MessageBox.Show("nok");
        //        }
        //    }
        //}


        private void CheckedCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CheckedCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Source.GetType() == typeof(CheckBox))
            {
                CheckBox checkbox = (CheckBox)e.Source;
                TreeViewItem item = (TreeViewItem)checkbox.Parent;

                if (checkbox.IsChecked == true)
                {
                    try
                    {

                        while (item.Parent.GetType() != typeof(TreeView))
                        {
                            TreeViewItem itemParent = (TreeViewItem)item.Parent;
                            CheckBox checkParent = (CheckBox)itemParent.Header;
                            checkParent.IsChecked = true;

                            checkbox = checkParent;
                            item = itemParent;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    List<TreeViewItem> child = new List<TreeViewItem>();
                    child = GetChildren(item);
                    checkChildren(item, child);
                }
            }
            else if (e.Source.GetType() == typeof(ucTreeViewItems))
            {
                ucTreeViewItems uc = (ucTreeViewItems)e.Source;
                TreeViewItem item = (TreeViewItem)uc.Parent;

                //if (uc.chkHeader.IsChecked == true)
                //{
                try
                {

                    while (item.Parent.GetType() != typeof(TreeView))
                    {
                        TreeViewItem itemParent = (TreeViewItem)item.Parent;
                        CheckBox checkParent = (CheckBox)itemParent.Header;
                        checkParent.IsChecked = true;

                        // uc.chkHeader = checkParent;
                        item = itemParent;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                //}
                //else
                //{
                //    //List<TreeViewItem> child = new List<TreeViewItem>();
                //    //child = GetChildren(item);
                //    //checkChildren(item, child);
                //}
            }
        }
        public void checkChildren(TreeViewItem item, List<TreeViewItem> childList)
        {
            foreach (TreeViewItem childItem in childList)
            {
                if (childItem.Header.GetType() == typeof(CheckBox))
                {
                    CheckBox checkChild = (CheckBox)childItem.Header;
                    checkChild.IsChecked = false;

                    List<TreeViewItem> child = new List<TreeViewItem>();
                    child = GetChildren(item);

                    if (child.Count != 0)
                    {
                        checkChildren(childItem, child);
                    }
                }
                else if (childItem.Header.GetType() == typeof(ucTreeViewItems))
                {
                    ucTreeViewItems uc = (ucTreeViewItems)childItem.Header;

                    if (uc.Content.GetType() == typeof(ucTreeViewItems))
                    {
                        ucTreeViewItems uct = (ucTreeViewItems)uc.Content;
                        uct.chkHeader.IsChecked = false;
                        if (uct.Content.GetType() == typeof(ucTreeViewItems))
                        {
                            ucTreeViewItems uctr = (ucTreeViewItems)uct.Content;
                            uctr.chkHeader.IsChecked = false;
                        }
                    }

                    uc.chkHeader.IsChecked = false;

                }

            }
        }
        List<TreeViewItem> GetChildren(TreeViewItem parent)
        {
            List<TreeViewItem> children = new List<TreeViewItem>();

            if (parent != null)
            {
                foreach (var item in parent.Items)
                {
                    TreeViewItem child = item as TreeViewItem;

                    if (child == null)
                    {
                        child = parent.ItemContainerGenerator.ContainerFromItem(child) as TreeViewItem;
                    }

                    children.Add(child);
                }
            }

            return children;
        }

        private void btnSelectAllRights_Click(object sender, RoutedEventArgs e)
        {
            if (treeMenu.Items.Count != 1)
            {
                foreach (TreeViewItem item in treeMenu.Items)
                {
                    if (item.Header.GetType() == typeof(CheckBox))
                    {
                        CheckBox combo = (CheckBox)item.Header;
                        combo.IsChecked = AllRightsChecked;

                    }
                    else if (item.Header.GetType() == typeof(ucTreeViewItems))
                    {
                        ucTreeViewItems uc = (ucTreeViewItems)item.Header;
                        CheckBox combo = (CheckBox)uc.chkHeader;
                        combo.IsChecked = AllRightsChecked;
                    }
                    if (item.HasItems)
                    {
                        checkAllRightsFromChild(item);
                    }
                }
                if (AllRightsChecked)
                {
                    AllRightsChecked = false;
                }
                else
                {
                    AllRightsChecked = true;
                }
            }
        }
        private void checkAllRightsFromChild(TreeViewItem treeViewItem)
        {
            List<TreeViewItem> child = GetChildren(treeViewItem);
            foreach (TreeViewItem item in child)
            {
                if (item.Header.GetType() == typeof(CheckBox))
                {
                    CheckBox combo = (CheckBox)item.Header;
                    combo.IsChecked = AllRightsChecked;

                }
                else if (item.Header.GetType() == typeof(ucTreeViewItems))
                {
                    ucTreeViewItems uc = (ucTreeViewItems)item.Header;
                    CheckBox combo = (CheckBox)uc.chkHeader;
                    combo.IsChecked = AllRightsChecked;
                }
                if (item.HasItems)
                {
                    checkAllRightsFromChild(item);
                }
            }

        }
        private void btnSelectAllButtons_Click(object sender, RoutedEventArgs e)
        {
            if (treeMenu.Items.Count != 1)
            {
                foreach (TreeViewItem item in treeMenu.Items)
                {
                    if (item.Header.GetType() == typeof(ucTreeViewItems))
                    {
                        ucTreeViewItems uc = (ucTreeViewItems)item.Header;
                        uc.chkNew.IsChecked = AllButtonsChecked;
                        uc.chkSave.IsChecked = AllButtonsChecked;
                        uc.chkDelete.IsChecked = AllButtonsChecked;
                        uc.chkPrint.IsChecked = AllButtonsChecked;
                        uc.chkFind.IsChecked = AllButtonsChecked;
                        uc.chkHelp.IsChecked = AllButtonsChecked;
                        uc.chkClose.IsChecked = AllButtonsChecked;
                        uc.chkCancel.IsChecked = AllButtonsChecked;
                    }
                    else
                    {
                        checkAllButtonsFromChild(item);
                    }
                }
                if (AllButtonsChecked)
                {
                    AllButtonsChecked = false;
                }
                else
                {
                    AllButtonsChecked = true;
                }
            }
        }
        private void checkAllButtonsFromChild(TreeViewItem item)
        {
            List<TreeViewItem> children = GetChildren(item);
            foreach (TreeViewItem childItem in children)
            {
                if (childItem.Header.GetType() == typeof(ucTreeViewItems))
                {
                    ucTreeViewItems uc = (ucTreeViewItems)childItem.Header;
                    uc.chkNew.IsChecked = AllButtonsChecked;
                    uc.chkSave.IsChecked = AllButtonsChecked;
                    uc.chkDelete.IsChecked = AllButtonsChecked;
                    uc.chkPrint.IsChecked = AllButtonsChecked;
                    uc.chkFind.IsChecked = AllButtonsChecked;
                    uc.chkHelp.IsChecked = AllButtonsChecked;
                    uc.chkClose.IsChecked = AllButtonsChecked;
                    uc.chkCancel.IsChecked = AllButtonsChecked;
                }
                else
                {
                    checkAllButtonsFromChild(childItem);
                }
            }

        }

        private void btnCopyAllRights_Click(object sender, RoutedEventArgs e)
        {
            btnPasteAllRights.IsEnabled = true;
            treeMenuu.Items.Clear();
            addTreeViewMenu(treeMenu, treeMenuu);
            treeMenu.Items.Clear();
            treeMenu.Items.Add("Rights are copied");
            btnCopyAllRights.IsEnabled = false;
        }
        private void btnPasteAllRights_Click(object sender, RoutedEventArgs e)
        {
            btnCopyAllRights.IsEnabled = true;
            treeMenu.Items.Clear();
            addTreeViewMenu(treeMenuu, treeMenu);
            btnPasteAllRights.IsEnabled = true;
            btnPasteAllRights.IsEnabled = false;
        }

        private void btnOpenUsers_Click(object sender, RoutedEventArgs e)
        {
            //clsGroups gr = new clsGroups();
            //gr.GroupID = 1;
            //gr.Groupname = "boeie";
            //checkTreeView(gr);
            //ucUser _ucUser = new ucUser();
            //OpenUserControl(_ucUser);
            ucUserView.Visibility = Visibility.Visible;
            ucGroupView.Visibility = Visibility.Hidden;
        }
        private void btnOpenGroups_Click(object sender, RoutedEventArgs e)
        {
            //clsVariabelen.itemName = "ZorgTreeViewItem";
            //ucGroup _ucGroup = new ucGroup();
            //OpenUserControl(_ucGroup);
            ucUserView.Visibility = Visibility.Hidden;
            ucGroupView.Visibility = Visibility.Visible;
        }

        private void ucGroupView_MouseMove(object sender, MouseEventArgs e)
        {
            if (ucGroupView.lstKeuze.SelectedValue != null)
                clsVariabelen.groupIDcboBox = Convert.ToInt32(ucGroupView.lstKeuze.SelectedValue);
        }

        private void btnSaveRightsOfGroup_Click(object sender, RoutedEventArgs e)
        {
            _Permissions.Clear();
            clsPermissionsRepository _perDelete = new clsPermissionsRepository();
            clsToegangsRechten _deleteHelp = new clsToegangsRechten();

            clsGroupRepository _perGroup = new clsGroupRepository();
            ObservableCollection<clsToegangsRechten> _OldPermission = _perGroup.GetAllRightsByGroupId(clsVariabelen.groupIDcboBox);
            //ObservableCollection<clsToegangsRechten> _OldPermissionn;

            _deleteHelp.GroupID = clsVariabelen.groupIDcboBox;
            _perDelete.DeletePermission(_deleteHelp);
            foreach (TreeViewItem item in treeMenu.Items)
            {
                clsToegangsRechten _Permission = new clsToegangsRechten();

                if (item.Header is CheckBox)
                {
                    CheckBox check = (CheckBox)item.Header;
                    if (check.IsChecked == true)
                    {
                        _Permission.ItemName = item.Name;
                        _Permission.ItemHeader = check.Content.ToString();
                        _Permission.GroupID = clsVariabelen.groupIDcboBox;

                        clsPermissionsRepository _perSave = new clsPermissionsRepository();
                        _perSave.InsertPermission(_Permission);
                        _Permissions.Add(_Permission);
                        if (item.HasItems)
                        {
                            addChildItems(item);
                        }
                    }
                }
                else if (item.Header is ucTreeViewItems)
                {
                    ucTreeViewItems ucTree = (ucTreeViewItems)item.Header;
                    CheckBox check = ucTree.chkHeader;
                    if (check.IsChecked == true)
                    {
                        _Permission.ItemName = item.Name;
                        _Permission.ItemHeader = check.Content.ToString();
                        _Permission.GroupID = clsVariabelen.groupIDcboBox;
                        CheckBox Nieuw = ucTree.chkNew;
                        CheckBox Save = ucTree.chkSave;
                        CheckBox Delete = ucTree.chkDelete;
                        CheckBox Print = ucTree.chkPrint;
                        CheckBox Find = ucTree.chkFind;
                        CheckBox Help = ucTree.chkHelp;
                        CheckBox Close = ucTree.chkClose;
                        CheckBox Cancel = ucTree.chkCancel;

                        _Permission.Nieuw = (bool)Nieuw.IsChecked;
                        _Permission.Save = (bool)Save.IsChecked;
                        _Permission.Delete = (bool)Delete.IsChecked;
                        _Permission.Print = (bool)Print.IsChecked;
                        _Permission.Find = (bool)Find.IsChecked;
                        _Permission.Help = (bool)Help.IsChecked;
                        _Permission.Close = (bool)Close.IsChecked;
                        _Permission.Cancel = (bool)Cancel.IsChecked;

                        clsPermissionsRepository _perSave = new clsPermissionsRepository();
                        _perSave.InsertPermission(_Permission);
                        _Permissions.Add(_Permission);
                    }
                }


            }
            //int aantal = _Permissions.Count;
            clsUserRepository _userRepo = new clsUserRepository();
            ObservableCollection<clsUsers> _Users = _userRepo.GetAllUsersByGroupId(clsVariabelen.groupIDcboBox);
            SmtpClient smtp = new SmtpClient("smtp.live.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Properties.Resources.Email, Properties.Resources.Wachtwoord);
            MailMessage msg = new MailMessage();

            //var _OldPermissionn = _Permissions.ToList().Intersect(_OldPermission.ToList());
            help = 0;

            //_OldPermissionn = ListIntersection(_Permissions, _OldPermission);

            //for(int i = 0; i < _Permissions.Count; i++)
            //{
            //    clsToegangsRechten _Perm = _Permissions[i];
            //    if(_OldPermission.Where(c => c.ItemHeader == _Perm.ItemHeader && c.ItemName == _Perm.ItemName).Any() == true)
            //    {
            //        _OldPermission.Add(_Perm);   
            //    }
            //}

            List<clsToegangsRechten> a = _OldPermission.Where(x => !_Permissions.Contains(x)).ToList<clsToegangsRechten>();
            List<clsToegangsRechten> b = _Permissions.Where(x => !_OldPermission.Contains(x)).ToList<clsToegangsRechten>();
            a.AddRange(b);



            //for (int i = _Permissions.Count; i >= 0; i--)
            //{
            //    clsToegangsRechten _Perm = _Permissions[i];

            //    if (_Permissions.Where(_Permissions => _Permissions.ItemHeader == _Perm.ItemHeader && _Permissions.ItemName == _Perm.ItemName).Any() == true)
            //    {
            //        for (int j = _OldPermission.Count; j >= 0; j--)
            //        {

            //            _OldPermission.Remove(_Perm);
            //        }
            //        //_OldPermission.Remove(_Perm);
            //    }

            //}



            //foreach(clsToegangsRechten _Perm in _OldPermissionn)
            //{
            //    if (_Permissions.Where(_Permissions => _Permissions.ItemHeader == _Perm.ItemHeader && _Permissions.ItemName == _Perm.ItemName).Any() == true)
            //    {
            //        _OldPermission.Remove(_Perm);
            //    }
            //    //else
            //    //{
            //    //    _OldPermission.Add(_Perm);
            //    //}
            //}
            StringBuilder sb = new StringBuilder();

            foreach (clsToegangsRechten _Perm in a)
            {
                sb.Append(_Perm.ItemHeader + " rechten zijn gewijzigd" + Environment.NewLine);
            }
            //sb.Remove(0, 1);
            foreach (clsUsers _User in _Users)
            {
                if (_User.Email != string.Empty)
                {
                    msg.From = new MailAddress(Properties.Resources.Email);
                    msg.Subject = "Rechten wijziging";
                    msg.Body = "Beste " + _User.UserName + Environment.NewLine + Environment.NewLine + sb + Environment.NewLine + Environment.NewLine + "MVG" + Environment.NewLine + "De netwerkbeheerder :)";
                    msg.To.Add(_User.Email);
                    smtp.Send(msg);
                }
            }
            MessageBox.Show("Mails sent");
            //_OldPermissionn.Clear();
            _OldPermission.Clear();
            a.Clear();
            _Permissions.Clear();
        }
        private void addChildItems(TreeViewItem item)
        {
            foreach (TreeViewItem childItem in item.Items)
            {
                clsToegangsRechten _Permission = new clsToegangsRechten();

                if (childItem.Header.GetType() == typeof(CheckBox))
                {
                    CheckBox check = (CheckBox)childItem.Header;
                    if (check.IsChecked == true)
                    {
                        _Permission.ItemName = item.Name;
                        _Permission.ItemHeader = check.Content.ToString();
                        _Permission.GroupID = clsVariabelen.groupIDcboBox;

                        clsPermissionsRepository _perSave = new clsPermissionsRepository();
                        _perSave.InsertPermission(_Permission);
                        _Permissions.Add(_Permission);
                    }
                }
                else if (childItem.Header.GetType() == typeof(ucTreeViewItems))
                {
                    ucTreeViewItems ucTree = (ucTreeViewItems)childItem.Header;
                    CheckBox check = ucTree.chkHeader;
                    if (check.IsChecked == true)
                    {
                        _Permission.ItemName = item.Name;
                        _Permission.ItemHeader = check.Content.ToString();
                        _Permission.GroupID = clsVariabelen.groupIDcboBox;
                        CheckBox Nieuw = ucTree.chkNew;
                        CheckBox Save = ucTree.chkSave;
                        CheckBox Delete = ucTree.chkDelete;
                        CheckBox Print = ucTree.chkPrint;
                        CheckBox Find = ucTree.chkFind;
                        CheckBox Help = ucTree.chkHelp;
                        CheckBox Close = ucTree.chkClose;
                        CheckBox Cancel = ucTree.chkCancel;

                        _Permission.Nieuw = (bool)Nieuw.IsChecked;
                        _Permission.Save = (bool)Save.IsChecked;
                        _Permission.Delete = (bool)Delete.IsChecked;
                        _Permission.Print = (bool)Print.IsChecked;
                        _Permission.Find = (bool)Find.IsChecked;
                        _Permission.Help = (bool)Help.IsChecked;
                        _Permission.Close = (bool)Close.IsChecked;
                        _Permission.Cancel = (bool)Cancel.IsChecked;

                        clsPermissionsRepository _perSave = new clsPermissionsRepository();
                        _perSave.InsertPermission(_Permission);
                        _Permissions.Add(_Permission);
                    }
                }

                if (childItem.HasItems)
                {
                    addChildItems(childItem);
                }
            }
        }
        //private ObservableCollection<clsToegangsRechten> ListIntersection(ObservableCollection<clsToegangsRechten> A, ObservableCollection<clsToegangsRechten> B)
        //{
        //    help++;
        //    if (help == 1)
        //        resultIntersection.Clear();

        //    ObservableCollection<clsToegangsRechten> d = new ObservableCollection<clsToegangsRechten>();

        //    if (B.Count < A.Count)
        //    {
        //        for (int i = 0; i < A.Count; ++i)
        //            d.Add(A[i]);

        //        for (int i = 0; i < B.Count; ++i)
        //            if (d.Contains(B[i]) == false)
        //                resultIntersection.Add(B[i]);
        //        if (help == 1)
        //            ListIntersection(B, A);

        //    }
        //    else
        //    {
        //        for (int i = 0; i < B.Count; ++i)
        //            d.Add(B[i]);

        //        for (int i = 0; i < A.Count; ++i)
        //            if (d.Contains(A[i]) == false)
        //                resultIntersection.Add(A[i]);
        //        if (help == 1)
        //            ListIntersection(B, A);
        //    }
        //    return resultIntersection;
        //}
    }

}
