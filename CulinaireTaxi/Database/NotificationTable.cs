using CulinaireTaxi.Database.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CulinaireTaxi.Database.CulinaireTaxiDB;

namespace CulinaireTaxi.Database
{
    public class NotificationTable
    {
        public static Notification CreateNotification(long companyID, long customerID, long resID, int messageType)
        {
            string message = "Message of notification";
            string title = "Title of notification";

            switch (messageType)
            {
                case 0:
                    title = "Reservation canceled";
                    message = "Your reservation with " + AccountTable.RetrieveAccountByID(companyID) + " at " + ReservationTable.RetrieveReservation(resID).FromDate.ToString("D") + " " + ReservationTable.RetrieveReservation(resID).FromDate.ToString("HH:mm") + " has been canceled.";
                    break;
                case 1:
                    title = "Reservation confirmd";
                    message = "Your reservation with " + AccountTable.RetrieveAccountByID(companyID) + " at " + ReservationTable.RetrieveReservation(resID).FromDate.ToString("D") + " " + ReservationTable.RetrieveReservation(resID).FromDate.ToString("HH:mm") + " has been confirmd.";
                    break;
            }
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var createNotificationCMD = connection.CreateCommand())
                {
                    createNotificationCMD.CommandText =
                    "INSERT IGNORE INTO Notification" +
                    " (title, message, company_id, customer_id, reservation_id) " +
                    "VALUES" +
                    " (@title, @message, @companyID, @customerID, @resID)";

                    var parameters = createNotificationCMD.Parameters;

                    parameters.AddWithValue("@title", title);
                    parameters.AddWithValue("@message", message);
                    parameters.AddWithValue("@companyID", companyID);
                    parameters.AddWithValue("@customerID", customerID);
                    parameters.AddWithValue("@resID", resID);

                    bool success = (createNotificationCMD.ExecuteNonQuery() != 0);

                    if (success)
                    {
                        Notification notification = new Notification();

                        notification.ID = createNotificationCMD.LastInsertedId;

                        notification.Title = title;

                        notification.Message = message;
                        notification.CompanyID = companyID;

                        notification.CustomerID = customerID;
                        notification.ReservationID = resID;

                        return notification;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public static List<Notification> RetrieveReservationsFrom(long companyID)
        {
            return RetrieveNotifications($" WHERE customer_id = {companyID}");
        }

        public static List<Notification> RetrieveReservationsFor(long customerID)
        {
            return RetrieveNotifications($" WHERE customer_id = {customerID}");
        }

        private static List<Notification> RetrieveNotifications(string condition)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var retrieveNotificationsCMD = connection.CreateCommand())
                {
                    retrieveNotificationsCMD.CommandText = $"SELECT * FROM Notification" + condition;

                    using (var reader = retrieveNotificationsCMD.ExecuteReader())
                    {
                        List<Notification> notifications = new List<Notification>();

                        while (reader.Read())
                        {
                            Notification notification = new Notification();

                            notification.ID = reader.GetInt64(0);

                            notification.Title = reader.GetString(1);
                            notification.Message = reader.GetString(2);
                            
                            notification.CompanyID = reader.GetInt64(3);
                            notification.CustomerID = reader.GetInt64(4);


                            notification.ReservationID = reader.GetInt32(5);

                            notifications.Add(notification);
                        }

                        return notifications;
                    }
                }
            }
        }
    }
}
