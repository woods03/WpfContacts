using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfContacts.Model
{
    public class Contact
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string Email { get; set; }
        public string HomeAddress { get; set; }
        public string OfficeAddress { get; set; }
    }
}
