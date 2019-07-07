using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CulinaireTaxi.Pages.App{
    public class AdminModel : PageModel{
        public const string POSTID_CONFIRM_COMPANY = "confirm_company";
        public const string POSTID_DECLINE_COMPANY = "decline_company";
        
        [BindProperty]
        [Required]
        public string postID { get; set; }

        [BindProperty]
        [Required]
        public string companyID { get; set; }

        [BindProperty]
        [Required]
        public string confirmType { get; set; }

        public void OnPost(){
            if (!ModelState.IsValid){
                return;
            }

            if (postID == "CHECK_COMPANY") {
                Database.CompanyTable.UpdateCompanyConfirm(long.Parse(companyID), confirmType == POSTID_CONFIRM_COMPANY);
            }
        }
    }
}