
namespace CulinaireTaxi.Database.Entities
{

    public abstract class User : Accessor
    {

	public Account Account
	{
	    get;
	    protected set;
	}

    }

}
