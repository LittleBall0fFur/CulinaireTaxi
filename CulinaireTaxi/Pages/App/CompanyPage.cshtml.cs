using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CulinaireTaxi.Pages
{
    public class CompanyPageModel : PageModel
    {
        private DateTime date = DateTime.Today;
        private readonly DateTime today = DateTime.Today;

        public void OnPostSub()
        {
            date.AddDays(-1);
        }

        public void OnPostAdd()
        {
            date.AddDays(1);
        }

        public void OnPostToday()
        {
            date = today;
        }

        public DateTime GetDate()
        {
            return date;
        }
    }
}