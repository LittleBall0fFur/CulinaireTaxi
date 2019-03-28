using MySql.Data.MySqlClient;

namespace CulinaireTaxi.Database
{

    public static class CulinaireTaxiDB
    {

	private static MySqlConnection m_connection;

	static CulinaireTaxiDB()
	{
	    m_connection = new MySqlConnection("server=sql7.freemysqlhosting.net;uid=sql7285675;pwd=ITnJKJJc4r;database=sql7285675");

	    Initialize();
	}

	private static void Initialize()
	{
	    //m_connection.Open();

	    //var command = new MySqlCommand("CREATE TABLE IF NOT EXISTS Account(id BIGINT NOT NULL, email NVARCHAR(256) NOT NULL, password NVARCHAR(128) NOT NULL, PRIMARY KEY(id))", m_connection);
	    //command.ExecuteNonQuery();

	    //m_connection.Close();
	}

    }

}
