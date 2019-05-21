
namespace CulinaireTaxi.Database.Entities
{

    public class ContactDetails
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

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

	    public string County
	    {
	        get;
	        set;
	    }

	    public string City
	    {
	        get;
	        set;
	    }

	    public string Street
	    {
	        get;
	        set;
	    }

	    public string PostalCode
	    {
	        get;
	        set;
	    }

	    public string PhoneNumber
	    {
	        get;
	        set;
	    }

        public bool MissingData()
        {
            return string.IsNullOrEmpty(City) || string.IsNullOrEmpty(Street) || string.IsNullOrEmpty(PostalCode) || string.IsNullOrEmpty(PhoneNumber);
        }

    }

}
