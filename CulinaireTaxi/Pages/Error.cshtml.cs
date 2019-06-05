using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CulinaireTaxi.Pages
{

    public class ErrorModel : PageModel
    {

        public const int NO_STATUS_CODE = -1;

        public bool HasCode
        {
            get;
            private set;
        }

        public int Code
        {
            get;
            private set;
        }

        public void OnGet(int? code)
        {
            HasCode = (code != null);
            Code = (code ?? NO_STATUS_CODE);
        }

    }

}
