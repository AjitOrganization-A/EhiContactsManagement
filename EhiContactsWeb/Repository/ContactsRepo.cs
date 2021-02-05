using EhiContactsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace EhiContactsWeb.Repository
{
    public class ContactsRepo : IContactsRepo
    {
        public HttpResponseMessage CreateContact(ContactViewModel contactViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                var insertRecord = client.PostAsJsonAsync<ContactViewModel>("contact", contactViewModel);
                insertRecord.Wait();
                var saveData = insertRecord.Result;
                return saveData;
            }           
        }

        public HttpResponseMessage DeleteContact(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                var deleteRecord = client.DeleteAsync("contact?contactId=" + id.ToString());
                deleteRecord.Wait();
                var displayData = deleteRecord.Result;
                return displayData;
            }
        }

        public HttpResponseMessage EditContact(ContactViewModel contactViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                var insertRecord = client.PutAsJsonAsync<ContactViewModel>("contact", contactViewModel);
                insertRecord.Wait();
                var saveData = insertRecord.Result;
                return saveData;
            }
        }

        HttpResponseMessage IContactsRepo.GetContact(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");

                //Called Member default GET All records  
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request  
                var responseTask = client.GetAsync("contact?contactId=" + id.ToString());
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                return result;

            }
        }

        HttpResponseMessage IContactsRepo.GetContacts()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");

                var responseTask = client.GetAsync("contact");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                return result;
            }
        }
    }
}