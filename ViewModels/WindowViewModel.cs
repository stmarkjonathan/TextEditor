using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TextEditor.Commands;

namespace TextEditor.ViewModels
{
    public class WindowViewModel
    {
        private ObservableCollection<TabViewModel> _instances = new ObservableCollection<TabViewModel>();
        public ObservableCollection<TabViewModel> Instances
        {
            get { return _instances; }
            set { _instances = value; }
        }

        private int _activeTabID;

        public int ActiveTabID
        {
            get { return _activeTabID; }
            set { _activeTabID = value; }
        }

        public RelayCommand SaveCommand => new RelayCommand(execute => Save());
        public RelayCommand SaveAsCommand => new RelayCommand(execute => SaveAs());

        public RelayCommand AddCommand => new RelayCommand(execute => AddTab());

        public RelayCommand<TabViewModel> DeleteCommand => new RelayCommand<TabViewModel>(execute => DeleteTab(execute));

        public WindowViewModel()
        {
            _instances.Add(new TabViewModel());
        }

        public void AddTab() => _instances.Add(new TabViewModel()); 

        private void DeleteTab(TabViewModel targetTab) => Instances.Remove(targetTab); 

        public void Save()
        {
            var targetTab = Instances[ActiveTabID].Tab;

            //if the active tab's path is null, that means this is a new, unsaved file
            if (targetTab.FilePath == null)
            {
                SaveAs();
                return;
            }

            File.WriteAllText(targetTab.FilePath, targetTab.FileContent);
        }

        public void SaveAs()
        {
            var targetTab = Instances[ActiveTabID].Tab;

            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveDialog.FilterIndex = 1;
            saveDialog.RestoreDirectory = true;

            if (saveDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveDialog.FileName, targetTab.FileContent);
                targetTab.FilePath = saveDialog.FileName;
            }
        }

        public void OpenFile()
        {
            var filePath = string.Empty;

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openDialog.FilterIndex = 2;
            openDialog .RestoreDirectory = true;

            if(openDialog.ShowDialog() == true)
            {
                filePath = openDialog.FileName;

                var fileStream = openDialog.OpenFile();

                using(StreamReader stream = new StreamReader(fileStream))
                {
                    _instances.Add(new TabViewModel(stream.ReadToEnd(), openDialog.FileName));
                }
            }
        }
    }
}
