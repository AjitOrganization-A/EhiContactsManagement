using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EhiContactsAPI.Models;

namespace EhiContactsAPI.Controllers
{
    public class ContactController : ApiController
    {
        ContactsDBEntities contactsDBEntities = new ContactsDBEntities();

        // GET api/<controller>
        /////* @"https://localhost:44334/api/contact"*/
        [HttpGet]
        public IHttpActionResult GetContacts()
        {
            var results = contactsDBEntities.tblContacts.ToList();
            return Ok(results);
        }

        [HttpPost]
        public IHttpActionResult ContactsInsert(tblContact contact)
        {
            contactsDBEntities.tblContacts.Add(contact);
            contactsDBEntities.SaveChanges();
            return Ok();
        }


        [HttpGet]
        public IHttpActionResult GetContact(int contactId)
        {
            Contact contact = null;
            contact = contactsDBEntities.tblContacts.Where(x => x.ContactId == contactId).Select(x => new Contact()
            {
                ContactId = x.ContactId,
                FirstName = x.FirstName,
                Lastname = x.Lastname,
                EmailId = x.EmailId,
                PhoneNumber = x.PhoneNumber,
                Status = x.Status
            }).FirstOrDefault<Contact>();
            if(contact==null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        public IHttpActionResult Put(Contact contact)
        {

            var updateContact = contactsDBEntities.tblContacts.Where(x => x.ContactId == contact.ContactId).FirstOrDefault<tblContact>();
            if(updateContact!=null)
            {
                updateContact.ContactId = contact.ContactId;
                updateContact.FirstName = contact.FirstName;
                updateContact.Lastname = contact.Lastname;
                updateContact.EmailId = contact.EmailId;
                updateContact.PhoneNumber = contact.PhoneNumber;
                updateContact.Status = contact.Status;
                contactsDBEntities.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        public IHttpActionResult Delete(int contactId)
        {

            var contactDelete = contactsDBEntities.tblContacts.Where(x => x.ContactId == contactId).FirstOrDefault();
            contactsDBEntities.Entry(contactDelete).State = System.Data.Entity.EntityState.Deleted;
            contactsDBEntities.SaveChanges();
            return Ok();
        }


    }
}