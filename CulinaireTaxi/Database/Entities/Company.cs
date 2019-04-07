
namespace CulinaireTaxi.Database.Entities
{

    public enum CompanyType : byte
    {
        RESTAURANT = 0,
        TAXI = 1
    }

    public class Company
    {

        public long Id
        {
            get;
            set;
        }

        public CompanyType CompanyType
        {
            get;
            set;
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

        public double Latitude
        {
            get;
            set;
        }

        public double Longtitude
        {
            get;
            set;
        }

    }

}
