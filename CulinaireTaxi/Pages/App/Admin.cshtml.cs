using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CulinaireTaxi.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CulinaireTaxi.Pages.App
{

    public class AdminModel : PageModel
    {

        public const string POSTID_CHECK_COMPANY = "CHECK_COMPANY";

        public const string CONFIRMTYPE_CONFIRM_COMPANY = "confirm_company";
        public const string CONFIRMTYPE_DECLINE_COMPANY = "decline_company";

        [BindProperty, Required]
        public string PostID
        {
            get;
            set;
        }

        [BindProperty, Required]
        public long CompanyID
        {
            get;
            set;
        }

        [BindProperty, Required]
        public string ConfirmType
        {
            get;
            set;
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            switch(PostID)
            {
                case POSTID_CHECK_COMPANY:
                    POST_CheckCompany();
                    break;

            }
        }

        private void POST_CheckCompany()
        {
            if (ConfirmType == CONFIRMTYPE_DECLINE_COMPANY)
            {
                var companyAccount = AccountTable.RetrieveAccountByCompanyID(CompanyID);

                AccountTable.DeleteAccount(companyAccount.Id);
                CompanyTable.DeleteCompany(CompanyID);
            }
            else if (ConfirmType == CONFIRMTYPE_CONFIRM_COMPANY)
            {
                CompanyTable.UpdateCompanyConfirm(CompanyID, true);
            }
        }

    }

}