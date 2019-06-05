using CulinaireTaxi.Database.Entities;
using CulinaireTaxi.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using static CulinaireTaxi.Database.CulinaireTaxiDB;

namespace CulinaireTaxi.Database
{

    public static class ReservationTable
    {

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
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var createReservationCMD = connection.CreateCommand())
                {
                    createReservationCMD.CommandText =
                     "INSERT IGNORE INTO Reservation" +
                     " (customer_id, company_id, from_date, till_date, guests_amount) " +
                     "VALUES" +
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

                        reservation.Status = ReservationStatus.OPEN;

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
        public static List<Reservation> RetrieveReservationsFor(long companyId, ReservationStatus status)
        {
            return RetrieveReservations($" WHERE company_id = {companyId} AND status = {(byte)status}");
        }

        /// <summary>
        /// Attempt to retrieve reservations from the database.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static List<Reservation> RetrieveReservationsFor(long companyId, DateTime day)
        {
            return RetrieveReservationsFor(companyId, day.Date, day.Date.AddDays(1));
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
        /// <param name="companyId"></param>
        /// <param name="day"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static List<Reservation> RetrieveReservationsFor(long companyId, DateTime day, ReservationStatus status)
        {
            return RetrieveReservationsFor(companyId, day.Date, day.Date.AddDays(1), status);
        }

        /// <summary>
        /// Attempt to retrieve reservations from the database.
        /// </summary>
        /// <param name="companyId">The id of the company associated with the reservations.</param>
        /// <param name="start">The minimum 'FromDate' of the reservations.</param>
        /// <param name="end">The maximum 'FromDate' of the reservations.</param>
        /// <param name="status">The status of the reservations.</param>
        /// <returns>A list of reservations.</returns>
        public static List<Reservation> RetrieveReservationsFor(long companyId, DateTime start, DateTime end, ReservationStatus status)
        {
            return RetrieveReservations($" WHERE company_id = {companyId} AND (from_date BETWEEN '{start.ToMySqlFormat()}' AND '{end.ToMySqlFormat()}') AND status = {(byte)status}");
        }

        public static List<Reservation> RetrieveReservationsFrom(long customerId, DateTime day)
        {
            return RetrieveReservationsFrom(customerId, day.Date, day.Date.AddDays(1));
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
        public static List<Reservation> RetrieveReservationsFrom(long customerId, ReservationStatus status)
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
        public static List<Reservation> RetrieveReservationsFrom(long customerId, DateTime start, DateTime end, ReservationStatus status)
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
            using (var connection = new MySqlConnection(ConnectionString))
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

                            reservation.Status = (ReservationStatus)reader.GetByte(6);

                            reservations.Add(reservation);
                        }

                        return reservations;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the status of a reservation to the given status.
        /// </summary>
        /// <param name="id">The id of the reservation to update.</param>
        /// <param name="new_status">The new status of the reservation.</param>
        /// <returns>True if the reservation was updated, false otherwise.</returns>
        public static bool UpdateReservationStatus(long id, ReservationStatus new_status)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var updateReservationCMD = connection.CreateCommand())
                {
                    updateReservationCMD.CommandText = $"UPDATE Reservation SET status = {(byte)new_status} WHERE id = {id}";

                    bool reservationUpdated = (updateReservationCMD.ExecuteNonQuery() != 0);

                    return reservationUpdated;
                }
            }
        }

        /// <summary>
        /// Deletes an existing reservation.
        /// </summary>
        /// <param name="id">The id of the reservation to delete.</param>
        /// <returns>True if the reservation was deleted, false otherwise.</returns>
        public static bool DeleteReservation(long id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var deleteReservationCMD = connection.CreateCommand())
                {
                    deleteReservationCMD.CommandText = $"DELETE FROM Reservation WHERE id = {id}";

                    bool reservationDeleted = (deleteReservationCMD.ExecuteNonQuery() != 0);

                    return reservationDeleted;
                }
            }
        }

    }

}
