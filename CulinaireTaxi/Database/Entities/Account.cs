
namespace CulinaireTaxi.Database.Entities
{

    public class Account
    {

	public enum Type : byte
	{
	    CUSTOMER = 0,
	    COMPANY
	}

	public long id;

	public Type type;

	public string email;
	public string password;

	public string county;
	public string city;
	public string street;
	public string postalCode;
	public string phoneNumber;

    }

}
