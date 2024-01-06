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

namespace TextEditor.Views.UserControls
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl
    {
        public event RoutedEventHandler UC_MenuBarLoaded;

        public MenuBar()
        {
            InitializeComponent();
        }

        private void UC_MenuBar_Loaded(object sender, RoutedEventArgs e)
        {
            UC_MenuBarLoaded?.Invoke(sender, e);
        }
    }
}
