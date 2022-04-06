using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using Document = Tema1.Model.DocumentModel;

namespace Tema1.ViewModel
{
    public class DocumentViewModel : BaseViewModel 
    {
        public ObservableCollection<Document> Documents { get; set; }
        public Document SelectedDocument { get; set; }

        private ICommand Close;

        public DocumentViewModel()
        {
           Documents= new ObservableCollection<Document>();
           Documents.Add(new Document("",false));
        }
        public void TabClose(object sender)
        {
            var document = sender as Document;
            if(document.IsSaved==false)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show("Do you want to save?", "Save", buttons);
                if(result==DialogResult.Yes)
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.Filter = "Text Files | *.txt";
                    saveFile.DefaultExt = "txt";
                    if (document.Filepath == "")
                    {
                        if (saveFile.ShowDialog() == DialogResult.OK)
                            File.WriteAllText(saveFile.FileName, document.Content);
                        document.IntialContent = document.Content;
                        document.Filename = saveFile.FileName;
                        document.IsSaved = true;
                    }
                    else
                    {
                        document.IsSaved = true;
                        document.IntialContent = document.Content;
                        File.WriteAllText(document.Filepath, document.Content);
                    }
                }
            }
            if (Documents.Count > 1)
                Documents.Remove(document);
            OnPropertyChanged("Document");
        }
        public ICommand CloseTab
        {
            get
            {
                if (Close == null)
                    Close = new RelayCommand(TabClose);
                return Close;
            }
        }
    }
}
