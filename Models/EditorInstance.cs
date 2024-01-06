using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Extensions;

namespace TextEditor.Models
{
    public class EditorInstance : INotifyPropertyChanged
    {

        private StringBuilder _fileContent = new StringBuilder();
        public int CharCount
        {
            get { return _fileContent.Length ; }
        }

        public int WordCount
        {
            get { return _fileContent.ToString().WordCount(); }
        }


        public string FileContent 
        {  
            get 
            { 
                return _fileContent.ToString(); 
            } 
            set 
            {
                _fileContent.Clear();
                _fileContent.Append(value);
                OnPropertyChanged("FileContent");
                OnPropertyChanged("CharCount");
                OnPropertyChanged("WordCount");
            }
        }

        private string _filePath;

        public string FilePath
        {
            get { return _filePath;  }
            set 
            { 
                _filePath = value;
                OnPropertyChanged("FilePath");
                OnPropertyChanged("FileName");
            }
        }

        public string FileName
        {
            get 
            {
                if (_filePath != null)
                    return Path.GetFileName(_filePath);
                else
                    return "New File";             
            }
        }
        public EditorInstance(){}

        public EditorInstance(string fileData, string path)
        {

            FileContent = fileData;
            FilePath = path;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
