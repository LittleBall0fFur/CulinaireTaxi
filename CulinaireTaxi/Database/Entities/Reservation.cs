using CulinaireTaxi.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CulinaireTaxi.Database.Entities
{

    public class Reservation : Accessor
    {

	public enum Status : byte
	{
	    OPEN = 0,
	    DECLINED = 1,
	    ACCEPTED = 2,
	    PAID = 4
	}

	public long Id
	{
	    get;
	    private set;
	}

	public long CustomerId
	{
	    get;
	    private set;
	}

	public long CompanyId
	{
	    get;
	    private set;
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

	public Status ReservationStatus
	{
	    get;
	    set;
	}

	private Reservation()
	{

	}

	public static Reservation CreateReservation(long customerId, long companyId, DateTime from, DateTime till, int guestsAmount)
	{
	    throw new NotImplementedException();
	}

	public List<Reservation> RetrieveReservationsFor(long companyId)
	{
	    return RetrieveReservations($" WHERE company_id = {companyId}");
	}

	public List<Reservation> RetrieveReservationsFor(long companyId, Status status)
	{
	    return RetrieveReservations($" WHERE company_id = {companyId} AND status = {(byte)status}");
	}

	public List<Reservation> RetrieveReservationsFor(long companyId, DateTime start, DateTime end)
	{
	    return RetrieveReservations($" WHERE company_id = {companyId} AND (fromDate BETWEEN '{start.ToMySqlFormat()}' AND '{end.ToMySqlFormat()}')");
	}

	public List<Reservation> RetrieveReservationsFor(long companyId, DateTime start, DateTime end, Status status)
	{
	    return RetrieveReservations($" WHERE company_id = {companyId} AND (fromDate BETWEEN '{start.ToMySqlFormat()}' AND '{end.ToMySqlFormat()}') AND status = {(byte)status}");
	}

	public List<Reservation> RetrieveReservationsFrom(long customerId)
	{
	    return RetrieveReservations($" WHERE customer_id = {customerId}");
	}

	public List<Reservation> RetrieveReservationsFrom(long customerId, Status status)
	{
	    return RetrieveReservations($" WHERE customer_id = {customerId} AND status = {(byte)status}");
	}

	public List<Reservation> RetrieveReservationsFrom(long customerId, DateTime start, DateTime end)
	{
	    return RetrieveReservations($" WHERE customer_id = {customerId} AND (fromDate BETWEEN '{start.ToMySqlFormat()}' AND '{end.ToMySqlFormat()}')");
	}

	public List<Reservation> RetrieveReservationsFrom(long customerId, DateTime start, DateTime end, Status status)
	{
	    return RetrieveReservations($" WHERE customer_id = {customerId} AND (fromDate BETWEEN '{start.ToMySqlFormat()}' AND '{end.ToMySqlFormat()}') AND status = {(byte)status}");
	}

	private List<Reservation> RetrieveReservations(string condition)
	{
	    using (var connection = new MySqlConnection(CONNECTION_STRING))
	    {
		connection.Open();

		using (var retrieveReservationsCMD = connection.CreateCommand())
		{
		    retrieveReservationsCMD.CommandText = $"SELECT * FROM Reservation" + condition + " ORDER BY fromDate ASC";

		    using (var reader = retrieveReservationsCMD.ExecuteReader())
		    {
			List<Reservation> reservations = new List<Reservation>();

			while (reader.Read())
			{
			    Reservation reservation = new Reservation();

			    reservation.Id = reader.GetInt64(0);

			    reservation.CustomerId = reader.GetInt64(1);
			    reservation.CompanyId = reader.GetInt64(2);

			    reservation.FromDate = reader.GetDateTime(3);
			    reservation.TillDate = reader.GetDateTime(4);

			    reservation.ReservationStatus = (Status)reader.GetByte(5);

			    reservations.Add(reservation);
			}

			return reservations;
		    }
		}
	    }
	}

	public void Update()
	{

	}

	public void Delete()
	{

	}

    }

}
