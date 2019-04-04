using CulinaireTaxi.Database;
using CulinaireTaxi.Database.Entities;
using CulinaireTaxi.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace CulinaireTaxi.Authentication
{

    /// <summary>
    /// A class representing the current user.
    /// </summary>
    public sealed class UserAgent
    {

	private const string ACCOUNT_KEY = "UserAgent_Account";

	private ISession m_session;

	private Account m_account;

	/// <summary>
	/// Indicates whether the current user is logged in.
	/// </summary>
	public bool IsAuthenticated
	{
	    get
	    {
		return (Account != null);
	    }
	}

	/// <summary>
	/// The account associated with the current user, null if the user is not logged in.
	/// </summary>
	public Account Account
	{
	    get
	    {
		return m_account;
	    }

	    private set
	    {
		m_account = value;
		m_session.SetObject(ACCOUNT_KEY, m_account);
	    }
	}

	/// <summary>
	/// Constructs a new UserAgent using a session to identify the current user.
	/// </summary>
	/// <param name="httpContextAccessor">An HttpContextAccessor from which the session is retrieved.</param>
	public UserAgent(IHttpContextAccessor httpContextAccessor)
	{
	    m_session = httpContextAccessor.HttpContext.Session;

	    Account = m_session.GetObject<Account>(ACCOUNT_KEY);
	}

	/// <summary>
	/// Registers an account for the current user on success.
	/// </summary>
	/// <param name="accountType">The account type of the account to register.</param>
	/// <param name="email">The email address of the account to register.</param>
	/// <param name="password">The password of the account to register.</param>
	/// <param name="contact">The contact details of the account to register.</param>
	public void Register(AccountType accountType, string email, string password, ContactDetails contact)
	{
	    Account = AccountTable.CreateAccount(accountType, email, password, contact);
	}

	/// <summary>
	/// Associates the current user with a registered account on success.
	/// </summary>
	/// <param name="email">The email of a registered account.</param>
	/// <param name="password">The password of a registered account.</param>
	public void Login(string email, string password)
	{
	    Account account = AccountTable.RetrieveAccount(email);

	    if (account?.Password == password)
	    {
		Account = account;
	    }
	}

	/// <summary>
	/// Removes the association between a registered account and the current user.
	/// </summary>
	public void Logout()
	{
	    Account = null;
	}

    }

}
