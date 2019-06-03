using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CulinaireTaxi.Extensions;
using CulinaireTaxi.Database.Entities;
using System.ComponentModel.DataAnnotations;
using CulinaireTaxi.Database;
using CulinaireTaxi.Authentication;

namespace CulinaireTaxi.Pages
{

    public class CompanyPageModel : PageModel
    {

        public const string POSTID_CALENDAR_BACKWARDS = "sub_date";
        public const string POSTID_CALENDAR_TODAY = "today_date";
        public const string POSTID_CALENDAR_FORWARDS = "add_date";

        public const string POSTID_CONTRACTORS_ADD = "add_contractor";
        public const string POSTID_CONTRACTORS_REMOVE = "remove_contractor";

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
        public long ContractorID
        {
            get;
            set;
        }

        private readonly DateTime TODAY = DateTime.Today;

        List<Reservation> reservations = new List<Reservation>();
        int resID;

        public CompanyPageModel(UserAgent userAgent)
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

                case POSTID_CONTRACTORS_ADD:
                    POST_ContractorsAdd();
                    break;

                case POSTID_CONTRACTORS_REMOVE:
                    POST_ContractorsRemove();
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

        private void POST_ContractorsAdd()
        {
            ContractTable.CreateContract(UserAgent.Account.CompanyId.Value, ContractorID);
        }

        private void POST_ContractorsRemove()
        {
            ContractTable.DeleteContract(UserAgent.Account.CompanyId.Value, ContractorID);
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