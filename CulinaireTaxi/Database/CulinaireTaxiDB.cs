using MySql.Data.MySqlClient;

namespace CulinaireTaxi.Database
{

    public static class CulinaireTaxiDB
    {

        /// <summary>
        /// The connection string used to connect to the database.
        /// </summary>
        public static string ConnectionString
        {
            get;
            private set;
        }

        /// <summary>
        /// Call this method before using the database to initialize it.
        /// </summary>
        /// <param name="connectionString">The connection string to use for connecting to the database.</param>
        public static void InitializeDatabase(string connectionString)
        {
            ConnectionString = connectionString;

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var createCompanyTableCMD = connection.CreateCommand())
                using (var createContractTableCMD = connection.CreateCommand())
                using (var createAccountTableCMD = connection.CreateCommand())
                using (var createRatingTableCMD = connection.CreateCommand())
                using (var createReservationTableCMD = connection.CreateCommand())
                using (var createCleanReservationsEventCMD = connection.CreateCommand())
                {
                    #region CREATE_TABLE_COMMANDS

                    createCompanyTableCMD.CommandText =
                    "CREATE TABLE IF NOT EXISTS Company" +
                    "(id BIGINT NOT NULL AUTO_INCREMENT," +
                    " type TINYINT NOT NULL," +
                    " name TEXT NOT NULL," +
                    " description TEXT NOT NULL," +
                    " latitude DOUBLE NOT NULL," +
                    " longitude DOUBLE NOT NULL," +
                    " PRIMARY KEY (id))";

                    createContractTableCMD.CommandText =
                    "CREATE TABLE IF NOT EXISTS has_contracted" +
                    "(client_id BIGINT NOT NULL," +
                    " contractor_id BIGINT NOT NULL," +
                    " PRIMARY KEY (client_id, contractor_id)," +
                    " FOREIGN KEY (client_id)" +
                    " REFERENCES Company(id)" +
                    " ON DELETE CASCADE" +
                    " ON UPDATE CASCADE," +
                    " FOREIGN KEY (contractor_id)" +
                    " REFERENCES Company(id)" +
                    " ON DELETE CASCADE" +
                    " ON UPDATE CASCADE)";

                    createAccountTableCMD.CommandText =
                    "CREATE TABLE IF NOT EXISTS Account" +
                    "(id BIGINT NOT NULL AUTO_INCREMENT," +
                    " type TINYINT NOT NULL," +
                    " email VARCHAR(320) NOT NULL UNIQUE," +
                    " password VARCHAR(128) NOT NULL," +
                    " first_name TEXT NOT NULL," +
                    " last_name TEXT NOT NULL," +
                    " county VARCHAR(16)," +
                    " city VARCHAR(48)," +
                    " street VARCHAR(96)," +
                    " postal_code VARCHAR(32)," +
                    " phone_number VARCHAR(32)," +
                    " company_id BIGINT," +
                    " PRIMARY KEY (id)," +
                    " FOREIGN KEY (company_id)" +
                    " REFERENCES Company(id)" +
                    " ON DELETE SET NULL" +
                    " ON UPDATE CASCADE)";

                    createRatingTableCMD.CommandText =
                    "CREATE TABLE IF NOT EXISTS gives_rating" +
                    "(customer_id BIGINT NOT NULL," +
                    " company_id BIGINT NOT NULL," +
                    " rating TINYINT NOT NULL," +
                    " PRIMARY KEY (customer_id, company_id)," +
                    " FOREIGN KEY (customer_id)" +
                    " REFERENCES Account(id)" +
                    " ON DELETE CASCADE" +
                    " ON UPDATE CASCADE," +
                    " FOREIGN KEY (company_id)" +
                    " REFERENCES Company(id)" +
                    " ON DELETE CASCADE" +
                    " ON UPDATE CASCADE)";

                    createReservationTableCMD.CommandText =
                    "CREATE TABLE IF NOT EXISTS Reservation" +
                    "(id BIGINT NOT NULL AUTO_INCREMENT," +
                    " customer_id BIGINT NOT NULL," +
                    " company_id BIGINT NOT NULL," +
                    " from_date DATETIME NOT NULL," +
                    " till_date DATETIME NOT NULL," +
                    " guests_amount INT NOT NULL," +
                    " status TINYINT NOT NULL DEFAULT 0," +
                    " PRIMARY KEY (id)," +
                    " FOREIGN KEY (customer_id)" +
                    " REFERENCES Account(id)" +
                    " ON DELETE CASCADE" +
                    " ON UPDATE CASCADE," +
                    " FOREIGN KEY (company_id)" +
                    " REFERENCES Company(id)" +
                    " ON DELETE CASCADE" +
                    " ON UPDATE CASCADE)";

                    createCleanReservationsEventCMD.CommandText =
                    "CREATE EVENT IF NOT EXISTS clear_old_reservations " +
                    "ON SCHEDULE EVERY 1 WEEK DO " +
                    "DELETE FROM Reservation WHERE till_date <= (NOW() - INTERVAL 1 WEEK)";

                    #endregion

                    createCompanyTableCMD.ExecuteNonQuery();
                    createContractTableCMD.ExecuteNonQuery();
                    createAccountTableCMD.ExecuteNonQuery();
                    createRatingTableCMD.ExecuteNonQuery();
                    createReservationTableCMD.ExecuteNonQuery();
                    createCleanReservationsEventCMD.ExecuteNonQuery();
                }
            }
        }

    }

}
