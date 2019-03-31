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

	/// <summary>
	/// Attempts to create a new reservation in the database.
	/// </summary>
	/// <param name="customerId">The id of customer who placed the reservation.</param>
	/// <param name="companyId">The id of the company.</param>
	/// <param name="fromDate">The start date to reserve for.</param>
	/// <param name="tillDate">The end date to reserve for.</param>
	/// <param name="guestsAmount">The amount of guests expected.</param>
	/// <returns>The newly created reservation on success, null otherwise (i.e. a conflicting reservation exists).</returns>
	public static Reservation CreateReservation(long customerId, long companyId, DateTime fromDate, DateTime tillDate, int guestsAmount)
	{
	    using (var connection = new MySqlConnection(CONNECTION_STRING))
	    {
		connection.Open();

		using (var createReservationCMD = connection.CreateCommand())
		{
		    createReservationCMD.CommandText =
		     "INSERT IGNORE INTO Reservation" +
		     " (customer_id, company_id, from_date, till_date, guests_amount)" +
		     " VALUES" +
		    $" ({customerId}, {companyId}, '{fromDate.ToMySqlFormat()}', '{tillDate.ToMySqlFormat()}', {guestsAmount})";

		    bool success = (createReservationCMD.ExecuteNonQuery() != 0);

		    if (success)
		    {
			Reservation reservation = new Reservation();

			reservation.Id = createReservationCMD.LastInsertedId;

			reservation.CustomerId = customerId;
			reservation.CompanyId = companyId;

			reservation.FromDate = fromDate;
			reservation.TillDate = tillDate;

			reservation.GuestsAmount = guestsAmount;

			reservation.ReservationStatus = Status.OPEN;

			return reservation;
		    }
		    else
		    {
			return null;
		    }
		}
	    }
	}

	/// <summary>
	/// Attempt to retrieve reservations from the database.
	/// </summary>
	/// <param name="companyId">The id of the company associated with the reservations.</param>
	/// <returns>A list of reservations.</returns>
	public static List<Reservation> RetrieveReservationsFor(long companyId)
	{
	    return RetrieveReservations($" WHERE company_id = {companyId}");
	}

	/// <summary>
	/// Attempt to retrieve reservations from the database.
	/// </summary>
	/// <param name="companyId">The id of the company associated with the reservations.</param>
	/// <param name="status">The status of the reservations.</param>
	/// <returns>A list of reservations.</returns>
	public static List<Reservation> RetrieveReservationsFor(long companyId, Status status)
	{
	    return RetrieveReservations($" WHERE company_id = {companyId} AND status = {(byte)status}");
	}

	/// <summary>
	/// Attempt to retrieve reservations from the database.
	/// </summary>
	/// <param name="companyId">The id of the company associated with the reservations.</param>
	/// <param name="start">The minimum 'FromDate' of the reservations.</param>
	/// <param name="end">The maximum 'FromDate' of the reservations.</param>
	/// <returns>A list of reservations.</returns>
	public static List<Reservation> RetrieveReservationsFor(long companyId, DateTime start, DateTime end)
	{
	    return RetrieveReservations($" WHERE company_id = {companyId} AND (from_date BETWEEN '{start.ToMySqlFormat()}' AND '{end.ToMySqlFormat()}')");
	}

	/// <summary>
	/// Attempt to retrieve reservations from the database.
	/// </summary>
	/// <param name="companyId">The id of the company associated with the reservations.</param>
	/// <param name="start">The minimum 'FromDate' of the reservations.</param>
	/// <param name="end">The maximum 'FromDate' of the reservations.</param>
	/// <param name="status">The status of the reservations.</param>
	/// <returns>A list of reservations.</returns>
	public static List<Reservation> RetrieveReservationsFor(long companyId, DateTime start, DateTime end, Status status)
	{
	    return RetrieveReservations($" WHERE company_id = {companyId} AND (from_date BETWEEN '{start.ToMySqlFormat()}' AND '{end.ToMySqlFormat()}') AND status = {(byte)status}");
	}

	/// <summary>
	/// Attempt to retrieve reservations from the database.
	/// </summary>
	/// <param name="customerId">The id of the customer who placed the reservations.</param>
	/// <returns>A list of reservations.</returns>
	public static List<Reservation> RetrieveReservationsFrom(long customerId)
	{
	    return RetrieveReservations($" WHERE customer_id = {customerId}");
	}

	/// <summary>
	/// Attempt to retrieve reservations from the database.
	/// </summary>
	/// <param name="customerId">The id of the customer who placed the reservations.</param>
	/// <param name="status">The status of the reservations.</param>
	/// <returns>A list of reservations.</returns>
	public static List<Reservation> RetrieveReservationsFrom(long customerId, Status status)
	{
	    return RetrieveReservations($" WHERE customer_id = {customerId} AND status = {(byte)status}");
	}

	/// <summary>
	/// Attempt to retrieve reservations from the database.
	/// </summary>
	/// <param name="customerId">The id of the customer who placed the reservations.</param>
	/// <param name="start">The minimum 'FromDate' of the reservations.</param>
	/// <param name="end">The maximum 'FromDate' of the reservations.</param>
	/// <returns>A list of reservations.</returns>
	public static List<Reservation> RetrieveReservationsFrom(long customerId, DateTime start, DateTime end)
	{
	    return RetrieveReservations($" WHERE customer_id = {customerId} AND (from_date BETWEEN '{start.ToMySqlFormat()}' AND '{end.ToMySqlFormat()}')");
	}

	/// <summary>
	/// Attempt to retrieve reservations from the database.
	/// </summary>
	/// <param name="customerId">The id of the customer who placed the reservations.</param>
	/// <param name="start">The minimum 'FromDate' of the reservations.</param>
	/// <param name="end">The maximum 'FromDate' of the reservations.</param>
	/// <param name="status">The status of the reservations.</param>
	/// <returns>A list of reservations.</returns>
	public static List<Reservation> RetrieveReservationsFrom(long customerId, DateTime start, DateTime end, Status status)
	{
	    return RetrieveReservations($" WHERE customer_id = {customerId} AND (from_date BETWEEN '{start.ToMySqlFormat()}' AND '{end.ToMySqlFormat()}') AND status = {(byte)status}");
	}

	/// <summary>
	/// Attempt to retrieve reservations from the database matching the given condition.
	/// </summary>
	/// <param name="condition">The condition to which the reservations must comply</param>
	/// <returns>A list of reservations matching the given condition.</returns>
	private static List<Reservation> RetrieveReservations(string condition)
	{
	    using (var connection = new MySqlConnection(CONNECTION_STRING))
	    {
		connection.Open();

		using (var retrieveReservationsCMD = connection.CreateCommand())
		{
		    retrieveReservationsCMD.CommandText = $"SELECT * FROM Reservation" + condition + " ORDER BY from_date ASC";

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

			    reservation.GuestsAmount = reader.GetInt32(5);

			    reservation.ReservationStatus = (Status)reader.GetByte(6);

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
