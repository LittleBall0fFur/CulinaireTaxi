using System;

namespace CulinaireTaxi.Database.Entities
{

    public class Reservation
    {

	public enum Status : byte
	{
	    OPEN = 0,
	    DECLINED,
	    ACCEPTED
	}

	public long id;

	public long customerId;
	public long companyId;

	public DateTime fromDate;
	public DateTime tillDate;

	public int guestsAmount;

	public Status status;

    }

}
