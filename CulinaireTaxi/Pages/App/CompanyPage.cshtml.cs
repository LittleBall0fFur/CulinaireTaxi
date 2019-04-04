using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CulinaireTaxi.Extensions;
using CulinaireTaxi.Database.Entities;

namespace CulinaireTaxi.Pages
{
    public class CompanyPageModel : PageModel
    {
        [BindProperty] public DateTime date { get; set; }
        private readonly DateTime today = DateTime.Today;

        List<Reservation> reservations = new List<Reservation>();
        int resID;

        public void OnGet()
        {
            date = DateTime.Today;
        }

        public void SetDate(DateTime _date)
        {
            date = _date;
        }

        public void OnPostSub()
        {
            date = date.AddDays(-1);
        }

        public void OnPostAdd()
        {
            date = date.AddDays(1);
        }

        public void OnPostToday()
        {
            date = today;
        }

        public DateTime GetDate()
        {
            return date;
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