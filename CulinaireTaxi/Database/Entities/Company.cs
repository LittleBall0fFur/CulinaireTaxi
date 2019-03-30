
namespace CulinaireTaxi.Database.Entities
{

    public class Company : User
    {

	public enum Type : byte
	{
	    RESTAURANT = 0,
	    TAXI
	}

	public Type type;

	public string name;
	public string description;

    }

}
