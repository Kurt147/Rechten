   M S B A M L   `   `   `  ����U  QKurtVerheyenRechtenProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:KurtVerheyenRechtenProject.ViewKurtVerheyenRechtenProject.View  5      ]3clr-namespace:KurtVerheyenRechtenProject.ViewModels%KurtVerheyenRechtenProject.ViewModels  5	      ,    &KurtVerheyenRechtenProject.View.ucUser   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:KurtVerheyenRechtenProject.View     5      BVM3clr-namespace:KurtVerheyenRechtenProject.ViewModels     5	        � DataContext  5
      ��    
_VMLocator �� Source# [   �� Path$ UsersViewModel� � 	Resources 5      6   0KurtVerheyenRechtenProject.ViewModels.clsUsersVM  
clsUsersVM��%   &	         5   
   6-   5      .��5      � -   5        $��grdMain��5       � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $
��auto �6   6.   �� 5      $
��auto �6   6.   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      $��30 �6   6*   �� 5      $��30 �6   6*   �� 5      $��30 �6   6*   �� 5      $��30 �6   6*   �� 5      $��50 �6   6*   .+�5   
   �� -   5        $��lstKeuze��5      $��2��6#   $��1��67   $	��150��6G    �� SelectedValuePath$ UserID��5"       �� DisplayMemberPath$ UserName��5#      ��5      �� $ Users� �� Mode$ TwoWay=�	 �� SelectedItem	 5       �� *SelectedUser+$ TwoWay=�
 �� SelectedIndex
 5!      �� *SelectedUser.MySelectedIndex+$ TwoWay=�5#   1   �� 5$   
   $��1��6   $��1��6    .��6-   	UserName:68   �� 5%   
   $��2��6   $��1��6"   $	��150��6/   ��5&      �� *SelectedUser.UserName+6;   �� 5'   
   $��1��6   $��2��6    .��6-   	Password:68   �� 5(   
   $��2��6   $��2��6"   $	��150��6/   ��5)      �� *SelectedUser.UserPassword+6?   �� 5*   
   $��1��6   $��3��6    .��6-   Email:65   �� 5+   
   $��2��6   $��3��6"   $	��150��6/   ��5,      �� *SelectedUser.Email+68   �� 5-   
   $��1��6   $��4��6    .��6-   Group:65   �� 5.   
   -   5        $��lstGroup��5.      $��2��6#   $��4��63   $	��150��6@   $ �� IsSynchronizedWithCurrentItem$
 TrueY�51      $ GroupID��52      $ 	Groupname��53      ��5/      �� $ Groupss�$ TwoWay=�	 50      �� *SelectedGroupp+$ TwoWay=�53   _   /   )KurtVerheyenRechtenProject.View.ucButtons  54   
   -   5          Name$ CrudButtons��54      $��1��6/   $��2��6?   $��5��6S   6b   55      56      