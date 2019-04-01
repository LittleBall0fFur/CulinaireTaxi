using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CulinaireTaxi.Extensions;

namespace CulinaireTaxi.Pages
{
    public class CompanyPageModel : PageModel
    {
        [BindProperty] public DateTime date { get; set; }
        private readonly DateTime today = DateTime.Today;

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
            date = date;
        }

        public DateTime GetDate()
        {
            return date;
        }
    }
}