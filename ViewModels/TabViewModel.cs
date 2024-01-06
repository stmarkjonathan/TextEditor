using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Models;

namespace TextEditor.ViewModels
{
    public class TabViewModel
    {

        private EditorInstance _tab; 
        public EditorInstance Tab
        {
            get
            {
                return _tab;
            }
            set
            {
                _tab = value;
            }

        }

        public TabViewModel() => _tab = new EditorInstance();

        public TabViewModel(string fileContent, string filePath) => _tab = new EditorInstance(fileContent, filePath);
        
    }
}
