using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using EhiContactsWeb.Models;
using EhiContactsWeb.Repository;

namespace EhiContactsWeb.Controllers
{
    public class ContactsController : Controller
    {

        private IContactsRepo _contactsRepo;

        public ContactsController(IContactsRepo contactsRepo)
        {
            _contactsRepo = contactsRepo;
        }

        // GET: Contacts
        public ActionResult Index()
        {
            IEnumerable<ContactViewModel> contacts = null;

            var result = _contactsRepo.GetContacts();
            //If success received   
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ContactViewModel>>();
                readTask.Wait();
                contacts = readTask.Result;
            }
            else
            {
                //Error response received   
                contacts = Enumerable.Empty<ContactViewModel>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }

            return View(contacts);
        }

        // GET: Contacts/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            ContactViewModel contact = null;

            var result = _contactsRepo.GetContact(id);
            //If success received   
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<ContactViewModel>();
                readTask.Wait();

                contact = readTask.Result;
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        public ActionResult Create(ContactViewModel contactViewModel)
        {
            try
            {
                var saveData = _contactsRepo.CreateContact(contactViewModel);
                if (saveData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return View("Create");
        }

        // GET: Contacts/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ContactViewModel contact = null;

            var result = _contactsRepo.GetContact(id);
            //If success received   
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<ContactViewModel>();
                readTask.Wait();

                contact = readTask.Result;
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        public ActionResult Edit(ContactViewModel contactViewModel)
        {
            try
            {
                var saveData = _contactsRepo.EditContact(contactViewModel);
                if (saveData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Contacts record not update...!";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int id)
        {
            var displayData = _contactsRepo.DeleteContact(id);
            if (displayData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}
