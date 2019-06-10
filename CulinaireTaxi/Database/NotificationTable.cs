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
        public static Notification CreateNotification(long sender, long recipient, long resID, int messageType)
        {
            string message = "Message of notification";
            string title = "Title of notification";

            switch (messageType)
            {
                case 0:
                    title = "Reservation canceled";
                    message = "Your reservation with " + AccountTable.RetrieveAccountByID(sender) + " at " + ReservationTable.RetrieveReservation(resID).FromDate.ToString("D") + " " + ReservationTable.RetrieveReservation(resID).FromDate.ToString("HH:mm") + " has been canceled.";
                    break;
                case 1:
                    title = "Reservation confirmd";
                    message = "Your reservation with " + AccountTable.RetrieveAccountByID(sender) + " at " + ReservationTable.RetrieveReservation(resID).FromDate.ToString("D") + " " + ReservationTable.RetrieveReservation(resID).FromDate.ToString("HH:mm") + " has been confirmd.";
                    break;
                case 2:
                    title = "A reservation has been made";
                    message = AccountTable.RetrieveAccountByID(sender).Contact.FullName + "has made a reservation on" + ReservationTable.RetrieveReservation(resID).FromDate.ToString("D") + " " + ReservationTable.RetrieveReservation(resID).FromDate.ToString("HH:mm") + ", please check your calander to accept or decline.";
                    break;
            }
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var createNotificationCMD = connection.CreateCommand())
                {
                    createNotificationCMD.CommandText =
                    "INSERT IGNORE INTO Notification" +
                    " (title, message, sender_id, recipient_id, reservation_id) " +
                    "VALUES" +
                    " (@title, @message, @sender, @recipient, @resID)";

                    var parameters = createNotificationCMD.Parameters;

                    parameters.AddWithValue("@title", title);
                    parameters.AddWithValue("@message", message);
                    parameters.AddWithValue("@sender", sender);
                    parameters.AddWithValue("@recipient", recipient);
                    parameters.AddWithValue("@resID", resID);

                    bool success = (createNotificationCMD.ExecuteNonQuery() != 0);

                    if (success)
                    {
                        Notification notification = new Notification();

                        notification.ID = createNotificationCMD.LastInsertedId;

                        notification.Title = title;
                        notification.Message = message;
                        notification.Recipient = recipient;
                        notification.Sender = sender;
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

        public static List<Notification> GetNotificationsFor(long id)
        {
            return RetrieveNotifications($" WHERE recipient_id = {id}");
        }

        public static List<Notification> GetNotificationsFrom(long id)
        {
            return RetrieveNotifications($" WHERE sender_id = {id}");
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
                            notification.Sender = reader.GetInt64(3);
                            notification.Recipient = reader.GetInt64(4);
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
