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

	public static Account CreateAccount(Type accountType, string email, string password, ContactDetails contact)
	{
	    throw new System.NotImplementedException();
	}

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
