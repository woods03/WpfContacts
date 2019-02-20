using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfContacts
{
    public class Contact
    {
        public Image img { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public Contact() { }
        
        public Contact(string _Name, string _Number,Image _img)
        {
            this.Name = _Name;
            this.Number = _Number;
            this.img = _img;
        }

        public void Edit(string _Name,string _Number, Image _img)
        {
            this.Name = _Name;
            this.Number = _Number;
            this.img = _img;
        }
    }
}
