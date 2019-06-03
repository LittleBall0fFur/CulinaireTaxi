using System;

namespace CulinaireTaxi.Database.Entities
{

    public enum ReservationStatus : byte
    {
        OPEN = 0,
        DECLINED = 1,
        ACCEPTED = 2,
        PAID = 4
    }

    public class Reservation
    {

        public long Id
        {
            get;
            set;
        }

        public long CustomerId
        {
            get;
            set;
        }

        public long CompanyId
        {
            get;
            set;
        }

        public DateTime FromDate
        {
            get;
            set;
        }

        public DateTime TillDate
        {
            get;
            set;
        }

        public int GuestsAmount
        {
            get;
            set;
        }

        public ReservationStatus Status
        {
            get;
            set;
        }

    }

}
