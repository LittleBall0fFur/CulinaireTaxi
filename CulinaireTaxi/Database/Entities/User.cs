
namespace CulinaireTaxi.Database.Entities
{

    public abstract class User : Accessor
    {

	public Account Account
	{
	    get;
	    protected set;
	}

	public long Id
	{
	    get
	    {
		return Account.Id;
	    }
	}

    }

}
