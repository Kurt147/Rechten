   M S B A M L   `   `   `  ����U  QKurtVerheyenRechtenProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:KurtVerheyenRechtenProject.ViewKurtVerheyenRechtenProject.View  5      ]3clr-namespace:KurtVerheyenRechtenProject.ViewModels%KurtVerheyenRechtenProject.ViewModels  5	      -    'KurtVerheyenRechtenProject.View.ucGroup   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:KurtVerheyenRechtenProject.View     5      BVM3clr-namespace:KurtVerheyenRechtenProject.ViewModels     5	        � DataContext  5
      ��    
_VMLocator �� Source# [   �� Path$ GroupsViewModel� � 	Resources 5      7   1KurtVerheyenRechtenProject.ViewModels.clsGroupsVM  clsGroupsVM��%   &	         5   
   6/   5      .��5      � -   5        $��grdMain��5       � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $
��auto �6   6.   �� 5      $
��auto �6   6.   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5      $��50 �6   6*   �� 5      $��50 �6   6*   �� 5      $��30 �6   6*   �� 5      $��30 �6   6*   �� 5      $��50 �6   6*   .+�5   
   �� -   5        $��lstKeuze��5      $��2��6#   $	��150��63    �� SelectedValuePath$ GroupID��5"       �� DisplayMemberPath$ 	Groupname��5#      $��5q�60   $
��Left=�6;   ��5      �� $ Groups� �� Mode$ TwoWay=�	 �� SelectedItem	 5       �� *SelectedGroup+$ TwoWay=�
 �� SelectedIndex
 5!      �� *SelectedGroup.MySelectedIndex+$ TwoWay=�5#   X   �� 5&   
   $��Right=�6   $��1��6,   $��1��6<   $��5q�6I   .��6T   
GroupName:6`   �� 5'   
   $��2��6   $��1��6"   $	��150��6/   $
��Left=�6;   $��5q�6V   ��5(      �� *SelectedGroup.Groupname+6=   /   )KurtVerheyenRechtenProject.View.ucButtons  5*   
   -   5          Name$ CrudButtons��5*      $��1��6/   $��2��6?   $��2��6S   6b   5.      5/      