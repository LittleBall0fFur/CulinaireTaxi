using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CulinaireTaxi.Pages.App
{
    public class AdminModel : PageModel
    {
        public const string POSTID_CONFIRM_COMPANY = "confirm_company";
        public const string POSTID_DECLINE_COMPANY = "decline_company";


        public void OnGet()
        {

        }
    }
}