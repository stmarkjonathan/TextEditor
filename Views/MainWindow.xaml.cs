using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using TextEditor.Commands;
using TextEditor.Models;
using TextEditor.ViewModels;
using TextEditor.Views.UserControls;

namespace TextEditor
{
    public partial class MainWindow : Window
    {
        WindowViewModel ViewModel;
        Dictionary<string, Action> commandIndex;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel = new WindowViewModel();
            tabControl.SelectedIndex = 0;
        }

        private void IntializeMenuOptions(object sender, RoutedEventArgs e)
        {
            /*
             Intialize a dict of commands to have a easy reference to commands for buttons
             */
            var activeTab = MainWindow.FindVisualChild<TextBox>(tabControl);



            commandIndex = new Dictionary<string, Action>()
            {
                { "NewTab", () =>
                    {
                        var tabs = ViewModel.Instances;
                        ViewModel.AddTab();
                        tabControl.SelectedItem = tabs[tabs.Count-1];
                        
                    }
                },

                { "OpenFile", () => 
                    {
                        var tabs = ViewModel.Instances;

                        
                        ViewModel.OpenFile();

                        if(tabs?.Any() != false)
                        {
                            tabControl.SelectedItem = tabs[tabs.Count-1];
                        }
                            
                    } 
                },

                { "SaveFile", () =>
                    {
                        var tabs = ViewModel.Instances;
                        if(tabs?.Any() != false)
                        {
                            ViewModel.Save();
                        }
                    }
                },

                { "SaveFileAs", () =>
                    {
                        var tabs = ViewModel.Instances;
                        if(tabs?.Any() != false)
                        {
                             ViewModel.SaveAs();
                        }
                       
                    }
                },

                { "UndoText", () => activeTab.Undo() } ,
                { "RedoText", () => activeTab.Redo() },
                { "CutText", () => activeTab.Cut() },
                { "CopyText", () => activeTab.Copy() },
                { "PasteText", () => activeTab.Paste() },
                { "DeleteText", () =>  activeTab.SelectedText = String.Empty }
            };
        }

        public RelayCommand<String> Perform => new RelayCommand<String>(execute => PerformCommand(execute));

        private void PerformCommand(string tag)
        {

            if(tag != null)
            {
                if (commandIndex.ContainsKey(tag))
                {
                    commandIndex[tag].Invoke();
                }
            }


        }

        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int childCount = 0; childCount < VisualTreeHelper.GetChildrenCount(parent); childCount++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, childCount);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

    }
}

