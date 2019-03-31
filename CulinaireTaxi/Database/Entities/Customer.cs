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

	public static Customer CreateCustomer(Account account, string firstName, string lastName)
	{
	    throw new System.NotImplementedException();
	}

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
