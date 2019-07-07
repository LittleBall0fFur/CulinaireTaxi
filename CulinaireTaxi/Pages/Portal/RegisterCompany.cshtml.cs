using CulinaireTaxi.Attributes;
using CulinaireTaxi.Authentication;
using CulinaireTaxi.Database;
using CulinaireTaxi.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace CulinaireTaxi.Pages
{

    public class RegisterCompanyModel : PageModel
    {

        //QUICKFIX: Replace with location picker.
        private static Random random = new Random();

        public UserAgent UserAgent
        {
            get;
            private set;
        }

        [BindProperty, Required]
        public string FirstName
        {
            get;
            set;
        }

        [BindProperty, Required]
        public string LastName
        {
            get;
            set;
        }

        [BindProperty, Required]
        public CompanyType CompanyType
        {
            get;
            set;
        }

        [BindProperty, Required]
        public string CompanyName
        {
            get;
            set;
        }

        [BindProperty, Required]
        public string CompanyDescription
        {
            get;
            set;
        }

        //[BindProperty, Required]
        public double Latitude
        {
            get;
            set;
        }

        //[BindProperty, Required]
        public double Longitude
        {
            get;
            set;
        }

        [BindProperty, EmailAddress, Required]
        public string Email
        {
            get;
            set;
        }

        [BindProperty, EmailAddress, Required]
        public string ConfirmEmail
        {
            get;
            set;
        }

        [BindProperty, Password, Required]
        public string Password
        {
            get;
            set;
        }

        [BindProperty, Password, Required]
        public string ConfirmPassword
        {
            get;
            set;
        }

        public RegisterCompanyModel(UserAgent userAgent)
        {
            UserAgent = userAgent;

            //QUICKFIX: Replace with location picker.
            Latitude = random.Next(-90, 90);
            Longitude = random.Next(-180, 180);
        }

        public void OnPost()
        {
            ValidateEquality(ConfirmEmail, Email, nameof(ConfirmEmail), "The email addresses do not match!");
            ValidateEquality(ConfirmPassword, Password, nameof(ConfirmPassword), "The passwords do not match!");

            if (ModelState.IsValid)
            {
                var company = CompanyTable.CreateCompany(CompanyType, CompanyName, CompanyDescription, Latitude, Longitude);

                var accountType = (CompanyType == CompanyType.RESTAURANT) ? AccountType.RESTAURANT : AccountType.TAXI;
                UserAgent.Register(accountType, Email, Password, new ContactDetails { FirstName = FirstName, LastName = LastName }, company.Id);
            }
        }

        /// <summary>
        /// Validates whether the values of two properties are equal to eachother. This method was added since <see cref="System.ComponentModel.DataAnnotations.CompareAttribute"/> doesn't work as expected.
        /// Should the validation fail, the given error message will be added to the validation summary and <c>ModelState.IsValid</c> shall return false.
        /// </summary>
        /// <param name="property">The value of the property being compared.</param>
        /// <param name="otherProperty">The value of the property to which the property being compared should be equal.</param>
        /// <param name="propertyName">The name of the property being compared.</param>
        /// <param name="ErrorMessage">The error message to add to the validation summary when the given values are not equal.</param>
        public void ValidateEquality(IComparable property, IComparable otherProperty, string propertyName, string ErrorMessage)
        {
            if (property.CompareTo(otherProperty) != 0)
            {
                ModelState.AddModelError(propertyName, ErrorMessage);
            }
        }

    }

}
