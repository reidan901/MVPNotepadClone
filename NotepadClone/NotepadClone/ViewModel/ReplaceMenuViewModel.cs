using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Document = Tema1.Model.DocumentModel;

namespace Tema1.ViewModel
{
    class ReplaceMenuViewModel:BaseViewModel
    {
        public string Replacement { get; set; }

        public string ToReplace { get; set; }
        public bool AllFiles { get; set; } = true;

        static public DocumentViewModel documentRef { get; set; }

        private ICommand Replace;
        private ICommand ReplaceAll;

        public void WordReplace(object sender)
        {
            string contentToReplace;
            if (AllFiles == false)
            {
                if (ToReplace.Length == 1)
                {
                    var regex = new Regex(Regex.Escape(ToReplace));
                    contentToReplace = regex.Replace(documentRef.SelectedDocument.Content, Replacement, 1);
                }
                else
                {
                    contentToReplace = documentRef.SelectedDocument.Content.Replace(ToReplace, Replacement);
                }
                documentRef.SelectedDocument.Content = contentToReplace;
                documentRef.OnPropertyChanged("SelectedDocument");
            }
            else
            {
                foreach(Document doc in documentRef.Documents)
                {
                    if (ToReplace.Length == 1 && doc.Content.IndexOf(ToReplace[0])!=-1)
                    {
                        var regex = new Regex(Regex.Escape(ToReplace));
                        contentToReplace = regex.Replace(doc.Content, Replacement, 1);
                    }
                    else
                    {
                        contentToReplace = documentRef.SelectedDocument.Content.Replace(ToReplace, Replacement);
                    }
                    if(contentToReplace!=doc.Content)
                    {
                        doc.Content = contentToReplace;
                        return;
                    }
                }
            }
        }

        public void WordReplaceAll(object sender)
        {
            string contentToReplace;
            if (AllFiles == false)
            {
                contentToReplace = documentRef.SelectedDocument.Content.Replace(ToReplace, Replacement);
                documentRef.SelectedDocument.Content = contentToReplace;
                documentRef.OnPropertyChanged("SelectedDocument");
            }
            else
            {
                foreach (Document doc in documentRef.Documents)
                {
                    doc.Content = doc.Content.Replace(ToReplace, Replacement);
                }
            }   
        }
        public ICommand ReplaceWord
        {
            get
            {
                if (Replace == null)
                    Replace = new RelayCommand(WordReplace);
                return Replace;
            }
        }

        public ICommand ReplaceAllWord
        {
            get
            {
                if (ReplaceAll == null)
                    ReplaceAll = new RelayCommand(WordReplaceAll);
                return ReplaceAll;
            }
        }
    }
}
