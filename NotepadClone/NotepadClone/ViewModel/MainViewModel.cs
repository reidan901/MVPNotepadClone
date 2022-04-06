using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1.ViewModel
{
    public class MainViewModel
    {
        public DocumentViewModel Document { get; set; }
        public MenuViewModel Menu { get; set; }
        public MainViewModel()
        {
            Document = new DocumentViewModel();
            Menu = new MenuViewModel(Document);
        }
    }
}
