using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfContacts.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IEnumerable<Model.Contact> contacts;
        public IEnumerable<Model.Contact> Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }
    }
}
