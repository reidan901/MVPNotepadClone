
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Document = Tema1.Model.DocumentModel;

namespace Tema1.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        private ICommand Open;
        private ICommand New;
        private ICommand Save;
        private ICommand SaveAs;
        private ICommand Replace;

        public DocumentViewModel Document;

        public MenuViewModel(DocumentViewModel document)
        {
            Document = document;
        }

        public void FileOpen(object sender)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = "C\\Users\\Andrei\\Desktop\\anu 2 part 1\\mvp";
            openFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                var fileStream = openFile.OpenFile();
                var streamReader = new StreamReader(fileStream);
                Document.Documents.Add(new Document(streamReader.ReadToEnd(), true,openFile.FileName.ToString()));
            }
        }

        public void FileNew(object sender)
        {
            Document.Documents.Add(new Document("", false));
            Document.SelectedDocument = Document.Documents[Document.Documents.Count-1];
            Document.OnPropertyChanged("SelectedDocument");
        }

        public void FileSave(object sender)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text Files | *.txt";
            saveFile.DefaultExt = "txt";
            if (Document.SelectedDocument.Filepath == "")
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                    File.WriteAllText(saveFile.FileName, Document.SelectedDocument.Content);
                Document.SelectedDocument.IntialContent = Document.SelectedDocument.Content;
                Document.SelectedDocument.Filename = saveFile.FileName;
                Document.SelectedDocument.IsSaved = true;
            }
            else
            {
                Document.SelectedDocument.IsSaved = true;
                Document.SelectedDocument.IntialContent = Document.SelectedDocument.Content;
                File.WriteAllText(Document.SelectedDocument.Filepath, Document.SelectedDocument.Content);
            }
        }

        public void FileSaveAs(object sender)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text Files | *.txt";
            saveFile.DefaultExt = "txt";
            if (saveFile.ShowDialog() == DialogResult.OK)
                File.WriteAllText(saveFile.FileName, Document.SelectedDocument.Content);
            Document.SelectedDocument.IntialContent = Document.SelectedDocument.Content;
            Document.SelectedDocument.Filename = saveFile.FileName;
            Document.SelectedDocument.IsSaved = true;
        }

        public void WordReplace(object sender)
        {
            ReplaceWindow replaceWindow = new ReplaceWindow();
            replaceWindow.Show();
            ReplaceMenuViewModel.documentRef = Document;
        }
        public ICommand OpenFile
        {
            get
            {
                if (Open == null)
                    Open = new RelayCommand(FileOpen);
                return Open;
            }
        }
        
        public ICommand NewFile
        {
            get
            {
                if (New == null)
                    New = new RelayCommand(FileNew);
                return New;
            }
        }

        public ICommand SaveFile
        {
            get
            {
                if (Save == null)
                    Save = new RelayCommand(FileSave);
                return Save;
            }
        }

        public ICommand SaveAsFile
        {
            get
            {
                if (SaveAs == null)
                    SaveAs = new RelayCommand(FileSaveAs);
                return SaveAs;
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

        public ICommand ReplaceAlleWord
        {
            get
            {
                if (Replace == null)
                    Replace = new RelayCommand(WordReplace);
                return Replace;
            }
        }
    }
}
