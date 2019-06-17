using CulinaireTaxi.Database.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using static CulinaireTaxi.Database.CulinaireTaxiDB;

namespace CulinaireTaxi.Database
{

    public static class CompanyTable
    {

        /// <summary>
        /// Attempts to create a new company in the database.
        /// </summary>
        /// <param name="companyType">The type of the company.</param>
        /// <param name="name">The name of the company.</param>
        /// <param name="description">The description of the company.</param>
        /// <param name="latitude">The latitude of the company's location.</param>
        /// <param name="longitude">The longitude of the company's location.</param>
        /// <returns>The newly created company on success, null otherwise (i.e. a conflicting company exists).</returns>
        public static Company CreateCompany(CompanyType companyType, string name, string description, double latitude, double longitude)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var createCompanyCMD = connection.CreateCommand())
                {
                    createCompanyCMD.CommandText =
                    "INSERT IGNORE INTO Company" +
                    " (type, name, description, latitude, longitude) " +
                    "VALUES" +
                    " (@companyType, @name, @description, @latitude, @longitude)";

                    var parameters = createCompanyCMD.Parameters;

                    parameters.AddWithValue("@companyType", (byte)companyType);
                    parameters.AddWithValue("@name", name);
                    parameters.AddWithValue("@description", description);
                    parameters.AddWithValue("@latitude", latitude);
                    parameters.AddWithValue("@longitude", longitude);

                    bool success = (createCompanyCMD.ExecuteNonQuery() != 0);

                    if (success)
                    {
                        Company company = new Company();

                        company.Id = createCompanyCMD.LastInsertedId;

                        company.CompanyType = companyType;

                        company.Name = name;
                        company.Description = description;

                        company.Latitude = latitude;
                        company.Longitude = longitude;

                        company.IsConfirmed = false;

                        return company;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Attempt to retrieve a company from the database.
        /// </summary>
        /// <param name="id">The id of the company.</param>
        /// <returns>The company if one was found, null otherwise.</returns>
        public static Company RetrieveCompany(long id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var retrieveCompanyCMD = connection.CreateCommand())
                {
                    retrieveCompanyCMD.CommandText = $"SELECT type, name, description, latitude, longitude, is_confirmed FROM Company WHERE id = {id}";

                    using (var reader = retrieveCompanyCMD.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            Company company = new Company();

                            company.Id = id;

                            company.CompanyType = (CompanyType)reader.GetByte(0);

                            company.Name = reader.GetString(1);
                            company.Description = reader.GetString(2);

                            company.Latitude = reader.GetDouble(3);
                            company.Longitude = reader.GetDouble(4);

                            company.IsConfirmed = reader.GetBoolean(5);

                            return company;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Attempt to retrieve <paramref name="N"/> companies starting from <paramref name="startId"/> from the database.
        /// </summary>
        /// <param name="startId">The company id at which to start.</param>
        /// <param name="N">The amount of companies to retrieve.</param>
        /// <returns>A List of at most <paramref name="N"/> companies.</returns>
        public static List<Company> RetrieveCompaniesInRange(int startId, int N)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var retrieveCompaniesCMD = connection.CreateCommand())
                {
                    retrieveCompaniesCMD.CommandText = $"SELECT * FROM Company WHERE id >= {startId} ORDER BY id ASC LIMIT {N}";

                    using (var reader = retrieveCompaniesCMD.ExecuteReader())
                    {
                        List<Company> companies = new List<Company>(N);

                        while (reader.Read())
                        {
                            Company company = new Company();

                            company.Id = reader.GetInt64(0);

                            company.CompanyType = (CompanyType)reader.GetByte(1);

                            company.Name = reader.GetString(2);
                            company.Description = reader.GetString(3);

                            company.Latitude = reader.GetDouble(4);
                            company.Longitude = reader.GetDouble(5);

                            company.IsConfirmed = reader.GetBoolean(6);

                            companies.Add(company);
                        }

                        return companies;
                    }
                }
            }
        }

        /// <summary>
        /// Attempt to retrieve all companies of the given type from the database.
        /// </summary>
        /// <param name="companyType">The type of the companies to retrieve.</param>
        /// <returns>A List of companies with the given type.</returns>
        public static List<Company> RetrieveCompaniesOfType(CompanyType companyType)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var retrieveCompaniesCMD = connection.CreateCommand())
                {
                    retrieveCompaniesCMD.CommandText = $"SELECT id, name, description, latitude, longitude, is_confirmed FROM Company WHERE type = {(byte)companyType}";

                    using (var reader = retrieveCompaniesCMD.ExecuteReader())
                    {
                        List<Company> companies = new List<Company>();

                        while (reader.Read())
                        {
                            Company company = new Company();

                            company.Id = reader.GetInt64(0);

                            company.CompanyType = companyType;

                            company.Name = reader.GetString(1);
                            company.Description = reader.GetString(2);

                            company.Latitude = reader.GetDouble(3);
                            company.Longitude = reader.GetDouble(4);

                            company.IsConfirmed = reader.GetBoolean(5);

                            companies.Add(company);
                        }

                        return companies;
                    }
                }
            }
        }

        /// <summary>
        /// Attempt to retrieve all companies of the given type from the database.
        /// </summary>
        /// <param name="companyType">The type of the companies to retrieve.</param>
        /// <returns>A List of companies with the given type.</returns>
        public static List<Company> RetrieveCompaniesByIsConfirmed(bool isConfirmed)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var retrieveCompaniesCMD = connection.CreateCommand())
                {
                    retrieveCompaniesCMD.CommandText = $"SELECT id, type, name, description, latitude, longitude, is_confirmed FROM Company WHERE is_confirmed = {isConfirmed}";

                    using (var reader = retrieveCompaniesCMD.ExecuteReader())
                    {
                        List<Company> companies = new List<Company>();

                        while (reader.Read())
                        {
                            Company company = new Company();

                            company.Id = reader.GetInt64(0);

                            company.CompanyType = (CompanyType)reader.GetByte(1);

                            company.Name = reader.GetString(2);
                            company.Description = reader.GetString(3);

                            company.Latitude = reader.GetDouble(4);
                            company.Longitude = reader.GetDouble(5);

                            company.IsConfirmed = reader.GetBoolean(6);

                            companies.Add(company);
                        }

                        return companies;
                    }
                }
            }
        }

        /// <summary>
        /// Updates a company's name.
        /// </summary>
        /// <param name="id">The id of the company to update.</param>
        /// <param name="new_name">The new name of the company.</param>
        /// <returns>True if the company's name was updated, false otherwise.</returns>
        public static bool UpdateCompanyName(long id, string new_name)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var updateCompanyCMD = connection.CreateCommand())
                {
                    updateCompanyCMD.CommandText = $"UPDATE Company SET name = {new_name} WHERE id = {id}";

                    bool companyUpdated = (updateCompanyCMD.ExecuteNonQuery() != 0);

                    return companyUpdated;
                }
            }
        }

        /// <summary>
        /// Updates a company's description.
        /// </summary>
        /// <param name="id">The id of the company to update.</param>
        /// <param name="new_description">The new description of the company.</param>
        /// <returns>True if the company's description was updated, false otherwise.</returns>
        public static bool UpdateCompanyDescription(long id, string new_description)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var updateCompanyCMD = connection.CreateCommand())
                {
                    updateCompanyCMD.CommandText = $"UPDATE Company SET description = {new_description} WHERE id = {id}";

                    bool companyUpdated = (updateCompanyCMD.ExecuteNonQuery() != 0);

                    return companyUpdated;
                }
            }
        }

        /// <summary>
        /// Updates a company's location.
        /// </summary>
        /// <param name="id">The id of the company to update.</param>
        /// <param name="latitude">The latitude of the company's new location.</param>
        /// <param name="longitude">The longitude of the company's new location.</param>
        /// <returns>True if the company's location was updated, false otherwise.</returns>
        public static bool UpdateCompanyLocation(long id, double latitude, double longitude)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var updateCompanyCMD = connection.CreateCommand())
                {
                    updateCompanyCMD.CommandText = $"UPDATE Company SET latitude = {latitude}, longitude = {longitude} WHERE id = {id}";

                    bool companyUpdated = (updateCompanyCMD.ExecuteNonQuery() != 0);

                    return companyUpdated;
                }
            }
        }

        public static bool UpdateCompanyConfirm(long id, bool isConfirmed)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var updateCompanyCMD = connection.CreateCommand())
                {
                    updateCompanyCMD.CommandText = $"UPDATE Company SET is_confirmed = {(isConfirmed ? '0' : '1')} WHERE id = {id}";

                    bool companyUpdated = (updateCompanyCMD.ExecuteNonQuery() != 0);

                    return companyUpdated;
                }
            }
        }

        /// <summary>
        /// Deletes an existing company.
        /// </summary>
        /// <param name="id">The id of the company to delete.</param>
        /// <returns>True if the company was deleted, false otherwise.</returns>
        public static bool DeleteCompany(long id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (var deleteCompanyCMD = connection.CreateCommand())
                {
                    deleteCompanyCMD.CommandText = $"DELETE FROM Company WHERE id = {id}";

                    bool companyDeleted = (deleteCompanyCMD.ExecuteNonQuery() != 0);

                    return companyDeleted;
                }
            }
        }

    }

}
