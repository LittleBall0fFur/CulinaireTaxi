using CulinaireTaxi.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CulinaireTaxi.Pages
{

    public class LoginModel : PageModel
    {

	public UserAgent UserAgent
	{
	    get;
	    private set;
	}

	public LoginModel(UserAgent userAgent)
	{
	    UserAgent = userAgent;
	}

    }

}