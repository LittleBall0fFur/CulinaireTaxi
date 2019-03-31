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

	public static Company CreateCompany(Account account, Type companyType, string name, string description)
	{
	    throw new System.NotImplementedException();
	}

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
