using CulinaireTaxi.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CulinaireTaxi.Pages
{

    public class RegisterModel : PageModel
    {
        
	public UserAgent UserAgent
	{
	    get;
	    private set;
	}

	public RegisterModel(UserAgent userAgent)
	{
	    UserAgent = userAgent;
	}

    }

}