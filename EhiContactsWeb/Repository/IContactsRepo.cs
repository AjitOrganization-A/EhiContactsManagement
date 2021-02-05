using EhiContactsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EhiContactsWeb.Repository
{
    public interface IContactsRepo
    {
        HttpResponseMessage GetContacts();
        HttpResponseMessage GetContact(int id);

        HttpResponseMessage CreateContact(ContactViewModel contactViewModel);

        HttpResponseMessage EditContact(ContactViewModel contactViewModel);

        HttpResponseMessage DeleteContact(int id);
    }
}
