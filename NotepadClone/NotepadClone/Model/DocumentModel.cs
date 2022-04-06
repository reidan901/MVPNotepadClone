using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1.Model
{
    public class DocumentModel : INotifyPropertyChanged
    {
        public static int NewlyOpenedFiles = 1;

        private string _intialContent;
        public string IntialContent
        {
            set => _intialContent = value;
        }
        private string _content;
        public string Content { get => _content;
            set
            {
                if (value == _content)
                {
                    return;
                }
                _content = value;
                if (value == _intialContent)
                    IsSaved = true;
                else IsSaved = false;
                OnPropertyChanged(nameof(Content));
            }
        }

        private bool _isSaved;
        public bool IsSaved { get => _isSaved;
            set
            {
                if (value == IsSaved)
                    return;
                _isSaved = value;
                OnPropertyChanged(nameof(IsSaved));
            }
        }

        private string _filepath;

        public string Filepath
        {
            get => _filepath;
        }

        private string _filename;

        public string Filename { get => _filename;
            set
            {
                if (value == _filepath)
                    return;
                _filename = Path.GetFileNameWithoutExtension(value);
                _filepath = value;
                OnPropertyChanged(nameof(Filename));
            }
        }


        public DocumentModel(string content, bool isOpened, string filepath="") 
        {
            _isSaved = true;
            _intialContent = content;
            _content = content;
            if (!isOpened)
            {
                _filename = "File " + NewlyOpenedFiles;
                _filepath = "";
                NewlyOpenedFiles++;
            }
            else
            {
                _filename = Path.GetFileNameWithoutExtension(filepath);
                _filepath = filepath;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
