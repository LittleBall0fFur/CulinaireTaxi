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

namespace CulinaireTaxi.Pages.App
{

    public class TaxiCompanyModel : PageModel
    {

        public const string POSTID_CALENDAR_BACKWARDS = "sub_date";
        public const string POSTID_CALENDAR_TODAY = "today_date";
        public const string POSTID_CALENDAR_FORWARDS = "add_date";

        public const string POSTID_CLIENTS_REMOVE = "remove_client";

        public const string POSTID_DECLINE_RESERVATION = "delete_reservation";
        public const string POSTID_CONFIRM_RESERVATION = "confirm_reservation";

        public const string POSTID_DELETE_NOTIFICATION = "delete_notification";

        public long ResID
        {
            get;
            set;
        }

        public long ResIDCancel
        {
            get;
            set;
        }

        public long NotificationID
        {
            get;
            set;
        }

        public UserAgent UserAgent
        {
            get;
            set;
        }

        [BindProperty]
        [Required]
        public string PostID
        {
            get;
            set;
        }

        [BindProperty]
        public DateTime Date
        {
            get;
            set;
        }

        [BindProperty]
        public long ClientID
        {
            get;
            set;
        }

        public long CustomerID
        {
            get;
            set;
        }

        public long CustomerIDCancel
        {
            get;
            set;
        }

        private readonly DateTime TODAY = DateTime.Today;

        List<Reservation> reservations = new List<Reservation>();
        int resID;

        public TaxiCompanyModel(UserAgent userAgent)
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

                case POSTID_CLIENTS_REMOVE:
                    POST_ClientsRemove();
                    break;

                case POSTID_DECLINE_RESERVATION:
                    POST_Decline_Reservation();
                    break;

                case POSTID_CONFIRM_RESERVATION:
                    POST_Confirm_Reservation();
                    break;
                case POSTID_DELETE_NOTIFICATION:
                    POST_Delete_Notification();
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

        private void POST_ClientsRemove()
        {
            ContractTable.DeleteContract(ClientID, UserAgent.Account.CompanyId.Value);
        }

        private void POST_Decline_Reservation()
        {
            ReservationTable.UpdateReservationStatus(long.Parse(Request.Form["ResID"]), ReservationStatus.DECLINED);
            NotificationTable.CreateNotification(UserAgent.Account.Id, long.Parse(Request.Form["CustomerIDCancel"]), long.Parse(Request.Form["ResIDCancel"]), 0);
        }

        private void POST_Confirm_Reservation()
        {
            ReservationTable.UpdateReservationStatus(long.Parse(Request.Form["ResID"]), ReservationStatus.ACCEPTED);
            NotificationTable.CreateNotification(UserAgent.Account.Id, long.Parse(Request.Form["CustomerID"]), long.Parse(Request.Form["ResID"]), 1);
        }

        private void POST_Delete_Notification()
        {
            NotificationTable.DeleteNotification(long.Parse(Request.Form["NotificationID"]));
        }

        public Reservation GetReservationByID(int id)
        {
            return reservations[id];
        }

        public void AddReservation(Reservation res)
        {
            resID = reservations.Count();
            reservations.Add(res);
        }

        public int GetResID()
        {
            return resID;
        }

    }

}