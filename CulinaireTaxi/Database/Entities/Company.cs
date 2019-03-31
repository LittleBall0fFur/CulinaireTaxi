using MySql.Data.MySqlClient;
using System.Data;

namespace CulinaireTaxi.Database.Entities
{

    public class Company : User
    {

	public enum Type : byte
	{
	    RESTAURANT = 0,
	    TAXI = 1
	}

	public Type CompanyType
	{
	    get;
	    private set;
	}

	public string Name
	{
	    get;
	    set;
	}

	public string Description
	{
	    get;
	    set;
	}

	private Company()
	{

	}

	/// <summary>
	/// Attempts to create a new company in the database.
	/// </summary>
	/// <param name="account">The account of the company, previously acquired by running Account.CreateAccount(...).</param>
	/// <param name="companyType">The type of the company.</param>
	/// <param name="name">The name of the company.</param>
	/// <param name="description">The description of the company.</param>
	/// <returns>The newly created company on success, null otherwise (i.e. a conflicting company exists).</returns>
	public static Company CreateCompany(Account account, Type companyType, string name, string description)
	{
	    using (var connection = new MySqlConnection(CONNECTION_STRING))
	    {
		connection.Open();

		using (var createCompanyCMD = connection.CreateCommand())
		{
		    createCompanyCMD.CommandText =
		    "INSERT IGNORE INTO Company" +
		    " (account_id, type, name, description)" +
		    " VALUES" +
		    " (@accountId, @companyType, @name, @description)";

		    var parameters = createCompanyCMD.Parameters;

		    parameters.AddWithValue("@accountId", account.Id);
		    parameters.AddWithValue("@companyType", (byte)companyType);
		    parameters.AddWithValue("@name", name);
		    parameters.AddWithValue("@description", description);

		    bool success = (createCompanyCMD.ExecuteNonQuery() != 0);

		    if (success)
		    {
			Company company = new Company();

			company.Account = account;

			company.CompanyType = companyType;

			company.Name = name;
			company.Description = description;

			return company;
		    }
		    else
		    {
			return null;
		    }
		}
	    }
	}

	/// <summary>
	/// Attempt to retrieve a company from the database.
	/// </summary>
	/// <param name="account">The account of the company.</param>
	/// <returns>The company if one was found, null otherwise.</returns>
	public static Company RetrieveByAccount(Account account)
	{
	    using (var connection = new MySqlConnection(CONNECTION_STRING))
	    {
		connection.Open();

		using (var retrieveCompanyCMD = connection.CreateCommand())
		{
		    retrieveCompanyCMD.CommandText = $"SELECT type, name, description FROM Company WHERE account_id = {account.Id}";

		    using (var reader = retrieveCompanyCMD.ExecuteReader(CommandBehavior.SingleRow))
		    {
			if (reader.Read())
			{
			    Company company = new Company();

			    company.Account = account;

			    company.CompanyType = (Type)reader.GetByte(0);

			    company.Name = reader.GetString(1);
			    company.Description = reader.GetString(2);

			    return company;
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
