using Microsoft.AspNetCore.Http;
using System;
using Account = System.Int32; //Account class placeholder

namespace CulinaireTaxi.Authentication
{

    /// <summary>
    /// Adds additional methods to ISession for easy authentication of a session.
    /// </summary>
    public static class SessionExtension
    {

	/// <summary>
	/// Key indicating where the account id should be stored in the session.
	/// </summary>
	private const string ACCOUNT_ID_KEY = "accountID";

	/// <summary>
	/// Associates this session with a registered account on success.
	/// </summary>
	/// <param name="email">The email of the registered account.</param>
	/// <param name="password">The password of the registered account.</param>
	public static void LogIn(this ISession session, string email, string password)
	{
	    //Query database for an account id where account with the given email and password

	    //If an account was found
		//Store account id in session
		
	    //Return.

	    throw new NotImplementedException();
	}

	/// <summary>
	/// Check whether an account is currently associated with this session
	/// </summary>
	/// <returns>True if an account is currently associated with this session, false otherwise.</returns>
	public static bool IsAuthenticated(this ISession session)
	{
	    return (session.Get(ACCOUNT_ID_KEY) == null); 
	}

	/// <summary>
	/// Get the account currently associated with this session.
	/// </summary>
	/// <returns>The account if one is currently associated with this session, null otherwise.</returns>
	public static Account GetCurrentAccount(this ISession session)
	{
	    //Retrieve account id from session
	    
	    //If account id was found
		//Create new Account object with the account id
		//Return the Account object.
	    //Else
		//Return null.

	    throw new NotImplementedException();
	}

	/// <summary>
	/// Log out the account associated with this session.
	/// </summary>
	public static void LogOut(this ISession session)
	{
	    session.Remove(ACCOUNT_ID_KEY);
	}
	
    }

}
