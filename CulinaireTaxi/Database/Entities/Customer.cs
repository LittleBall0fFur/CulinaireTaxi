using MySql.Data.MySqlClient;
using System.Data;

namespace CulinaireTaxi.Database.Entities
{

    public class Customer : User
    {

	public string FirstName
	{
	    get;
	    set;
	}

	public string LastName
	{
	    get;
	    set;
	}

	private Customer()
	{

	}

	/// <summary>
	/// Attempts to create a new customer in the database.
	/// </summary>
	/// <param name="account">The account of the customer, previously acquired by running Account.CreateAccount(...).</param>
	/// <param name="firstName">The first name of the customer.</param>
	/// <param name="lastName">The last name of the customer.</param>
	/// <returns>The newly created customer on success, null otherwise (i.e. a conflicting customer exists).</returns>
	public static Customer CreateCustomer(Account account, string firstName, string lastName)
	{
	    using (var connection = new MySqlConnection(CONNECTION_STRING))
	    {
		connection.Open();

		using (var createCustomerCMD = connection.CreateCommand())
		{
		    createCustomerCMD.CommandText =
		    "INSERT IGNORE INTO Customer" +
		    " (account_id, first_name, last_name)" +
		    " VALUES" +
		    " (@accountId, @firstName, @lastName)";

		    var parameters = createCustomerCMD.Parameters;

		    parameters.AddWithValue("@accountId", account.Id);
		    parameters.AddWithValue("@firstName", firstName);
		    parameters.AddWithValue("@lastName", lastName);

		    bool success = (createCustomerCMD.ExecuteNonQuery() != 0);

		    if (success)
		    {
			Customer customer = new Customer();

			customer.Account = account;

			customer.FirstName = firstName;
			customer.LastName = lastName;

			return customer;
		    }
		    else
		    {
			return null;
		    }
		}
	    }
	}

	/// <summary>
	/// Attempt to retrieve a customer from the database.
	/// </summary>
	/// <param name="account">The account of the customer.</param>
	/// <returns>The customer if one was found, null otherwise.</returns>
	public static Customer RetrieveByAccount(Account account)
	{
	    using (var connection = new MySqlConnection(CONNECTION_STRING))
	    {
		connection.Open();

		using (var retrieveCustomerCMD = connection.CreateCommand())
		{
		    retrieveCustomerCMD.CommandText = $"SELECT first_name, last_name FROM Customer WHERE account_id = {account.Id}";

		    using (var reader = retrieveCustomerCMD.ExecuteReader(CommandBehavior.SingleRow))
		    {
			if (reader.Read())
			{
			    Customer customer = new Customer();

			    customer.Account = account;

			    customer.FirstName = reader.GetString(0);
			    customer.LastName = reader.GetString(1);

			    return customer;
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
