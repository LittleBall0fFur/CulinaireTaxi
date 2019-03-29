using CulinaireTaxi.Database.Entities;
using CulinaireTaxi.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using User = System.String; //Placeholder type

namespace CulinaireTaxi.Authentication
{

    /// <summary>
    /// A class representing the current user.
    /// </summary>
    public sealed class UserAgent
    {

	private const string USER_KEY = "UserAgent_User";

	private ISession m_session;

	/// <summary>
	/// Indicates whether the current user is logged in.
	/// </summary>
	public bool IsAuthenticated
	{
	    get
	    {
		return (User != null);
	    }
	}

	/// <summary>
	/// The account associated with the current user, null if the user is not logged in.
	/// </summary>
	public User User
	{
	    get
	    {
		return m_session.GetObject<User>(USER_KEY);
	    }

	    private set
	    {
		m_session.SetObject(USER_KEY, value);
	    }
	}

	/// <summary>
	/// Constructs a new UserAgent using a session to identify the current user.
	/// </summary>
	/// <param name="session">The current user's session.</param>
	public UserAgent(IHttpContextAccessor httpContextAccessor)
	{
	    m_session = httpContextAccessor.HttpContext.Session;
	}

	/// <summary>
	/// Registers an account for the current user on success.
	/// </summary>
	/// <param name="email"></param>
	/// <param name="password"></param>
	public void Register(string email, string password)
	{
	    throw new NotImplementedException();
	}

	/// <summary>
	/// Associates the current user with a registered account on success.
	/// </summary>
	/// <param name="email">The email of a registered account.</param>
	/// <param name="password">The password of a registered account.</param>
	public void Login(string email, string password)
	{
	    throw new NotImplementedException();
	}

	/// <summary>
	/// Removes the association between a registered account and the current user.
	/// </summary>
	public void Logout()
	{
	    User = null;
	}

    }

}
