using MySql.Data.MySqlClient;
using System.Data;

namespace CulinaireTaxi.Database.Entities
{

    public class Account : Accessor
    {

	public enum Type : byte
	{
	    CUSTOMER = 0,
	    COMPANY = 1
	}

	public class ContactDetails
	{

	    public string County
	    {
		get;
		set;
	    }

	    public string City
	    {
		get;
		set;
	    }

	    public string Street
	    {
		get;
		set;
	    }

	    public string PostalCode
	    {
		get;
		set;
	    }

	    public string PhoneNumber
	    {
		get;
		set;
	    }

	}

	public long Id
	{
	    get;
	    private set;
	}

	public Type AccountType
	{
	    get;
	    private set;
	}

	public string Email
	{
	    get;
	    set;
	}

	public string Password
	{
	    get;
	    set;
	}

	public ContactDetails Contact
	{
	    get;
	    set;
	}

	private Account()
	{

	}

	/// <summary>
	/// Attempts to create a new account in the database.
	/// </summary>
	/// <param name="accountType">The type of the account.</param>
	/// <param name="email">The email of the account.</param>
	/// <param name="password">The password of the account.</param>
	/// <param name="contact">OPTIONAL: The contact details of the account.</param>
	/// <returns>The newly created account on success, null otherwise (i.e. a conflicting account exists).</returns>
	public static Account CreateAccount(Type accountType, string email, string password, ContactDetails contact = null)
	{
	    using (var connection = new MySqlConnection(CONNECTION_STRING))
	    {
		connection.Open();

		using (var createAccountCMD = connection.CreateCommand())
		{
		    createAccountCMD.CommandText =
		    "INSERT IGNORE INTO Account" +
		    " (type, email, password, county, city, street, postal_code, phone_number)" +
		    " VALUES" +
		    " (@accountType, @email, @password, @county, @city, @street, @postal_code, @phone_number)";

		    var parameters = createAccountCMD.Parameters;

		    parameters.AddWithValue("@accountType", (byte)accountType);
		    parameters.AddWithValue("@email", email);
		    parameters.AddWithValue("@password", password);
		    parameters.AddWithValue("@county", contact?.County);
		    parameters.AddWithValue("@city", contact?.City);
		    parameters.AddWithValue("@street", contact?.Street);
		    parameters.AddWithValue("@postal_code", contact?.PostalCode);
		    parameters.AddWithValue("@phone_number", contact?.PhoneNumber);

		    bool success = (createAccountCMD.ExecuteNonQuery() != 0);

		    if (success)
		    {
			Account account = new Account();

			account.Id = createAccountCMD.LastInsertedId;

			account.AccountType = accountType;

			account.Email = email;
			account.Password = password;

			account.Contact = contact;

			return account;
		    }
		    else
		    {
			return null;
		    }
		}
	    }
	}

	/// <summary>
	/// Attempt to retrieve an account from the database.
	/// </summary>
	/// <param name="email">The email of the account.</param>
	/// <returns>The account if one was found, null otherwise.</returns>
	public static Account RetrieveByEmail(string email)
	{
	    using (var connection = new MySqlConnection(CONNECTION_STRING))
	    {
		connection.Open();

		using (var retrieveAccountCMD = connection.CreateCommand())
		{
		    retrieveAccountCMD.CommandText = "SELECT * FROM Account WHERE email = @email";
		    retrieveAccountCMD.Parameters.AddWithValue("@email", email);

		    using (var reader = retrieveAccountCMD.ExecuteReader(CommandBehavior.SingleRow))
		    {
			if (reader.Read())
			{
			    Account account = new Account();

			    account.Id = reader.GetInt64(0);

			    account.AccountType = (Type)reader.GetByte(1);

			    account.Email = reader.GetString(2);
			    account.Password = reader.GetString(3);

			    account.Contact.County = reader.GetString(4);
			    account.Contact.City = reader.GetString(5);
			    account.Contact.Street = reader.GetString(6);
			    account.Contact.PostalCode = reader.GetString(7);
			    account.Contact.PhoneNumber = reader.GetString(8);

			    return account;
			}
			else
			{
			    return null;
			}
		    }
		}
	    }
	}

	public void Update()
	{

	}

	public void Delete()
	{

	}

    }

}
