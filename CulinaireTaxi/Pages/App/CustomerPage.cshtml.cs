using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CulinaireTaxi.Authentication;
using CulinaireTaxi.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CulinaireTaxi.Pages
{
    public class CustomerPageModel : PageModel
    {
        private UserAgent _user;
        public CustomerPageModel(UserAgent user) {
            _user = user;
        }

        [BindProperty, Required]
        public string form_id
        {
            get;
            set;
        }

        // user detail form
        [BindProperty]
        public string county
        {
            get;
            set;
        }
        [BindProperty]
        public string city
        {
            get;
            set;
        }
        [BindProperty]
        public string streetname
        {
            get;
            set;
        }
        [BindProperty]
        public string postalcode
        {
            get;
            set;
        }
        [BindProperty]
        public string phonenumber
        {
            get;
            set;
        }

        public void OnPost()
        {
            if (!_user.IsAuthenticated) {
                return;
            }

            if (form_id == "0") {
                Database.Entities.ContactDetails contact = _user.Account.Contact;
                contact.County = county;
                contact.City = city;
                contact.Street = streetname;
                contact.PostalCode = postalcode;
                contact.PhoneNumber = phonenumber;
                AccountTable.UpdateAccountContactDetails(_user.Account.Id, contact);
            } else if (form_id == "1") {

            }
        }
    }
}