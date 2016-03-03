using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KurtVerheyenRechtenProject.Model;
using KurtVerheyenRechtenProject.ViewModels;
using KurtVerheyenRechtenProject.Services;

namespace KurtVerheyenRechtenProject.View
{
    /// <summary>
    /// Interaction logic for ucUser.xaml
    /// </summary>
    public partial class ucUser : UserControl
    {
        public ucUser()
        {
            InitializeComponent();
        }

        private void lstGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstGroup.SelectedValuePath != null)
            {
                clsVariabelen.groupIDcboBox = Convert.ToInt32(lstGroup.SelectedValue);
            }
            
        }
    }
}
