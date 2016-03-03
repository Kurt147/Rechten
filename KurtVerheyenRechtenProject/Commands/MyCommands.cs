using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KurtVerheyenRechtenProject.Commands
{
    public static class MyCommands
    {
        public static readonly RoutedUICommand CheckboxChecked = new RoutedUICommand
                (
                        "CheckboxChecked",
                        "CheckboxChecked",
                        typeof(MyCommands)
                //new InputGestureCollection()
                //{
                //            new KeyGesture(Key.F4, ModifierKeys.Alt)
                //}
                );

        //Define more commands here, just like the one above
    }
}
