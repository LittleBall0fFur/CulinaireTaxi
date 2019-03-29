
namespace CulinaireTaxi.Database.Entities
{

    public struct Company
    {

	public enum Type : byte
	{
	    RESTAURANT = 0,
	    TAXI
	}

	public long accountId;

	public Type type;

	public string name;
	public string description;

    }

}
