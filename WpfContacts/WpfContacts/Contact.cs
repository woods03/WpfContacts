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
        public string img { get; set; } // path
        public string Name { get; set; }
        public string Number { get; set; }

        public Contact() { }
        
        public Contact(string _Name, string _Number,string _img)
        {
            this.Name = _Name;
            this.img = _img;

            if(_Number.Contains("+"))
            {
                this.Number = _Number;
            }
            else
            {
                this.Number = "+" + _Number;
            }
        }
    }
}
