using System.Collections.Generic;

namespace CulinaireTaxi.Database.Entities
{

    public abstract class User
    {

	public Account Account
	{
	    get;
	    set;
	}

	public ContactDetails ContactDetails
	{
	    get;
	    set;
	}

	/*
	public abstract List<Reservation> GetReservations();

	public abstract List<Reservation> GetUnconfirmedReservations();
	public abstract List<Reservation> GetConfirmedReservations();

	public abstract List<Reservation> GetCancelledReservations();

	public abstract List<Reservation> GetPaidReservations();
	*/

    }

}
