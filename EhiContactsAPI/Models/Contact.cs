using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EhiContactsAPI.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
    }
}