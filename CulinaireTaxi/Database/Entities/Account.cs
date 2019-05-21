
namespace CulinaireTaxi.Database.Entities
{

    public enum AccountType : byte
    {
        CUSTOMER = 0,
        RESTAURANT = 1,
        TAXI = 2,
        ADMIN = 3
    }

    public class Account
    {

        public long Id
        {
            get;
            set;
        }

        public AccountType AccountType
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public ContactDetails Contact
        {
            get;
            set;
        }

        public long? CompanyId
        {
            get;
            set;
        }

        public Account()
        {
            Contact = new ContactDetails();
        }

    }

}
