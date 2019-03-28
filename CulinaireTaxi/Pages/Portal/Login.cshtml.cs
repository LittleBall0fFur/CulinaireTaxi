using CulinaireTaxi.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CulinaireTaxi.Pages
{

    public class LoginModel : PageModel
    {

	public UserAgent UserAgent
	{
	    get;
	    private set;
	}

	[BindProperty, Required]
	[EmailAddress]
	public string Email
	{
	    get;
	    set;
	}

	[BindProperty, Required]
	public string Password
	{
	    get;
	    set;
	}

	public LoginModel(UserAgent userAgent)
	{
	    UserAgent = userAgent;
	}

	public void OnPost()
	{
	    if (ModelState.IsValid)
	    {

	    }
	}

    }

}
