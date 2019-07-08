using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CulinaireTaxi.Authentication;
using CulinaireTaxi.Database;
using CulinaireTaxi.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CulinaireTaxi.Pages
{
    public class CustomerModel : PageModel
    {

        public const string POSTID_CALENDAR_BACKWARDS = "sub_date";
        public const string POSTID_CALENDAR_TODAY = "today_date";
        public const string POSTID_CALENDAR_FORWARDS = "add_date";

        public const string POSTID_CREATE_RESERVATION_UPDATE_INFO = "create_reservation_update_info";
        public const string POSTID_CREATE_RESERVATION = "create_reservation";
        public const string POSTID_DELETE_RESERVATION = "delete_reservation";
        public const string POSTID_UPDATE_INFO = "update_info";

        public const string POSTID_DELETE_NOTIFICATION = "delete_notification";


        List<Reservation> reservations = new List<Reservation>();
        int resID;

        private readonly DateTime TODAY = DateTime.Today;

        [BindProperty]
        [Required]
        public string PostID
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public UserAgent UserAgent
        {
            get;
            set;
        }

        public long ResID
        {
            get;
            set;
        }

        [BindProperty]
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

        // reseveer
        [BindProperty]
        public long restaurant
        {
            get;
            set;
        }

        [BindProperty]
        public string fromdate
        {
            get;
            set;
        }

        [BindProperty]
        public string fromtime
        {
            get;
            set;
        }

        [BindProperty]
        public string tilldate
        {
            get;
            set;
        }

        [BindProperty]
        public string tilltime
        {
            get;
            set;
        }

        [BindProperty]
        public int guestsamount
        {
            get;
            set;
        }

        public long NotificationID
        {
            get;
            set;
        }

        public CustomerModel(UserAgent userAgent)
        {
            UserAgent = userAgent;
            Date = DateTime.Today;
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            switch (PostID)
            {
                case POSTID_CALENDAR_BACKWARDS:
                    POST_CalendarBackwards();
                    break;
                case POSTID_CALENDAR_TODAY:
                    POST_CalendarToday();
                    break;
                case POSTID_CALENDAR_FORWARDS:
                    POST_CalendarForwards();
                    break;
                case POSTID_CREATE_RESERVATION:
                    POST_Create_Reservation();
                    break;
                case POSTID_DELETE_RESERVATION:
                    POST_Delete_Reservation();
                    break;
                case POSTID_UPDATE_INFO:
                    POST_Update_Info();
                    break;
                case POSTID_DELETE_NOTIFICATION:
                    POST_Delete_Notification();
                    break;
                case POSTID_CREATE_RESERVATION_UPDATE_INFO:
                    POST_Update_Info();
                    POST_Create_Reservation();
                    break;

            }
        }

        public DateTime GetDate()
        {
            return Date;
        }

        public void SetDate(DateTime _date)
        {
            Date = _date;
        }

        private void POST_CalendarBackwards()
        {
            Date = Date.AddDays(-1);
        }

        private void POST_CalendarForwards()
        {
            Date = Date.AddDays(1);
        }

        private void POST_CalendarToday()
        {
            Date = TODAY;
        }

        private void POST_Create_Reservation()
        {
            //TO-DO fix validation for input fields
            if (!UserAgent.IsAuthenticated)
            {
                return;
            }

            if (UserAgent.Account.Contact.MissingData())
            {
                return;
            }

            Reservation res = ReservationTable.CreateReservation(UserAgent.Account.Id, restaurant, DateTime.Parse(fromdate + " " + fromtime), DateTime.Parse(tilldate + " " + tilltime), guestsamount);
            NotificationTable.CreateNotification(UserAgent.Account.Id, AccountTable.RetrieveAccountByCompanyID(restaurant).Id, res.Id, 2);
        }

        private void POST_Update_Info()
        {
            ContactDetails contact = UserAgent.Account.Contact;

            contact.County = county;
            contact.City = city;
            contact.Street = streetname;
            contact.PostalCode = postalcode;
            contact.PhoneNumber = phonenumber;

            AccountTable.UpdateAccountContactDetails(UserAgent.Account.Id, contact);
        }

        private void POST_Delete_Notification()
        {
            NotificationTable.DeleteNotification(long.Parse(Request.Form["NotificationID"]));
        }

        public void AddReservation(Reservation res)
        {
            resID = reservations.Count();
            reservations.Add(res);
        }

        private void POST_Delete_Reservation()
        {
            ReservationTable.DeleteReservation(long.Parse(Request.Form["ResID"]));
        }

    }

}
