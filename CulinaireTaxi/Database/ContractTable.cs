using CulinaireTaxi.Database.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using static CulinaireTaxi.Database.CulinaireTaxiDB;

namespace CulinaireTaxi.Database
{

    public static class ContractTable
    {

        /// <summary>
        /// Creates a new contract between two companies, a client (e.g. the Restaurant) and a contractor (e.g. the TaxiCompany).
        /// </summary>
        /// <param name="clientId">The id of the client company.</param>
        /// <param name="contractorId">The id of the contractor company.</param>
        /// <returns>True if the contract was created, false otherwise.</returns>
        public static bool CreateContract(long clientId, long contractorId)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var createContractCMD = connection.CreateCommand())
                {
                    createContractCMD.CommandText = $"INSERT IGNORE INTO has_contracted (client_id, contractor_id) VALUES ({clientId}, {contractorId})";

                    bool contractCreated = (createContractCMD.ExecuteNonQuery() != 0);

                    return contractCreated;
                }
            }
        }

        /// <summary>
        /// Retrieves a list of all companies contracted by the given company.
        /// </summary>
        /// <param name="clientId">The id of the client company (e.g. the Restaurant).</param>
        /// <returns>A list of companies contracted by the given company.</returns>
        public static List<Company> RetrieveContractors(long clientId)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var retrieveContractorsCMD = connection.CreateCommand())
                {
                    retrieveContractorsCMD.CommandText =
                    $"SELECT Company.id, Company.type, Company.name, Company.description FROM Company, has_contracted WHERE has_contracted.contractor_id = Company.id AND has_contracted.client_id = {clientId}";

                    using (var reader = retrieveContractorsCMD.ExecuteReader())
                    {
                        List<Company> contractors = new List<Company>();

                        while (reader.Read())
                        {
                            Company contractor = new Company();

                            contractor.Id = reader.GetInt64(0);

                            contractor.CompanyType = (CompanyType)reader.GetByte(1);

                            contractor.Name = reader.GetString(2);
                            contractor.Description = reader.GetString(3);

                            contractors.Add(contractor);
                        }

                        return contractors;
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a list of companies that have contracted the given company.
        /// </summary>
        /// <param name="contractorId">The id of the contractor company (e.g. the TaxiCompany).</param>
        /// <returns>A list of companies that have contracted given company</returns>
        public static List<Company> RetrieveClients(long contractorId)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var retrieveContractorsCMD = connection.CreateCommand())
                {
                    retrieveContractorsCMD.CommandText =
                    $"SELECT Company.id, Company.type, Company.name, Company.description FROM Company, has_contracted WHERE has_contracted.client_id = Company.id AND has_contracted.contractor_id = {contractorId}";

                    using (var reader = retrieveContractorsCMD.ExecuteReader())
                    {
                        List<Company> clients = new List<Company>();

                        while (reader.Read())
                        {
                            Company client = new Company();

                            client.Id = reader.GetInt64(0);

                            client.CompanyType = (CompanyType)reader.GetByte(1);

                            client.Name = reader.GetString(2);
                            client.Description = reader.GetString(3);

                            clients.Add(client);
                        }

                        return clients;
                    }
                }
            }
        }

        /// <summary>
        /// Deletes an existing contract between two companies, a client company (e.g. the Restaurant) and a contractor company (e.g. the TaxiCompany).
        /// </summary>
        /// <param name="clientId">the id of the client company.</param>
        /// <param name="contractorId">the id of the contractor company.</param>
        /// <returns>True if the contract was deleted, false otherwise.</returns>
        public static bool DeleteContract(long clientId, long contractorId)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var deleteContractCMD = connection.CreateCommand())
                {
                    deleteContractCMD.CommandText = $"DELETE FROM Contract WHERE client_id = {clientId} AND contractor_id = {contractorId}";

                    bool contractDeleted = (deleteContractCMD.ExecuteNonQuery() != 0);

                    return contractDeleted;
                }
            }
        }

    }

}
