using CulinaireTaxi.Database.Entities;
using MySql.Data.MySqlClient;

namespace CulinaireTaxi.Database
{

    public static class CulinaireTaxiDB
    {

	private static MySqlConnection m_connection;

	static CulinaireTaxiDB()
	{
	    m_connection = new MySqlConnection(/*"server=localhost;uid=root;pwd=rootpass;database=culinairetaxi;"*/"server=sql7.freemysqlhosting.net;uid=sql7285675;pwd=ITnJKJJc4r;database=sql7285675");

	    Initialize();
	}

	private static void Initialize()
	{
	    m_connection.Open();

	    #region CREATE_TABLE_COMMANDS

	    var CREATE_ACCOUNT_TABLE = new MySqlCommand(
		"CREATE TABLE IF NOT EXISTS Account" +
		"(id BIGINT NOT NULL AUTO_INCREMENT," +
		" type TINYINT NOT NULL," +
		" email VARCHAR(320) NOT NULL UNIQUE," +
		" password VARCHAR(128) NOT NULL," +
		" county VARCHAR(16)," +
		" city VARCHAR(48)," +
		" street VARCHAR(96)," +
		" postal_code VARCHAR(32)," +
		" phone_number VARCHAR(32)," +
		" PRIMARY KEY (id))", m_connection);

	    var CREATE_CUSTOMER_TABLE = new MySqlCommand(
		"CREATE TABLE IF NOT EXISTS Customer" +
		"(account_id BIGINT NOT NULL," +
		" first_name TEXT NOT NULL," +
		" last_name TEXT NOT NULL," +
		" PRIMARY KEY (account_id)," +
		" FOREIGN KEY (account_id)" +
		" REFERENCES Account(id)" +
		" ON DELETE CASCADE" +
		" ON UPDATE CASCADE)", m_connection);

	    var CREATE_COMPANY_TABLE = new MySqlCommand(
		"CREATE TABLE IF NOT EXISTS Company" +
		"(account_id BIGINT NOT NULL," +
		" type TINYINT NOT NULL," +
		" name TEXT NOT NULL," +
		" description TEXT NOT NULL," +
		" PRIMARY KEY (account_id)," +
		" FOREIGN KEY (account_id)" +
		" REFERENCES Account(id)" +
		" ON DELETE CASCADE" +
		" ON UPDATE CASCADE)", m_connection);

	    var CREATE_RATING_TABLE = new MySqlCommand(
		"CREATE TABLE IF NOT EXISTS gives_rating" +
		"(customer_id BIGINT NOT NULL," +
		" company_id BIGINT NOT NULL," +
		" rating TINYINT NOT NULL," +
		" PRIMARY KEY (customer_id, company_id)," +
		" FOREIGN KEY (customer_id)" +
		" REFERENCES Customer(account_id)" +
		" ON DELETE CASCADE" +
		" ON UPDATE CASCADE," +
		" FOREIGN KEY (company_id)" +
		" REFERENCES Company(account_id)" +
		" ON DELETE CASCADE" +
		" ON UPDATE CASCADE)", m_connection);

	    var CREATE_RESERVATION_TABLE = new MySqlCommand(
		"CREATE TABLE IF NOT EXISTS Reservation" +
		"(id BIGINT NOT NULL AUTO_INCREMENT," +
		" customer_id BIGINT NOT NULL," +
		" company_id BIGINT NOT NULL," +
		" from_date DATETIME NOT NULL," +
		" till_date DATETIME NOT NULL," +
		" guests_amount INT NOT NULL," +
		" status TINYINT NOT NULL," +
		" PRIMARY KEY (id)," +
		" FOREIGN KEY (customer_id)" +
		" REFERENCES Customer(account_id)" +
		" ON DELETE CASCADE" +
		" ON UPDATE CASCADE," +
		" FOREIGN KEY (company_id)" +
		" REFERENCES Company(account_id)" +
		" ON DELETE CASCADE" +
		" ON UPDATE CASCADE)", m_connection);

	    #endregion

	    CREATE_ACCOUNT_TABLE.ExecuteNonQuery();
	    CREATE_CUSTOMER_TABLE.ExecuteNonQuery();
	    CREATE_COMPANY_TABLE.ExecuteNonQuery();
	    CREATE_RATING_TABLE.ExecuteNonQuery();
	    CREATE_RESERVATION_TABLE.ExecuteNonQuery();

	    m_connection.Close();
	}

	public static Account RetrieveAccount()
	{
	    throw new System.NotImplementedException();
	}

	public static Customer RetrieveCustomer()
	{
	    throw new System.NotImplementedException();
	}

	public static Company RetrieveCompany()
	{
	    throw new System.NotImplementedException();
	}

    }

}
