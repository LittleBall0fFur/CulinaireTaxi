using MySql.Data.MySqlClient;
using System.Data;
using static CulinaireTaxi.Database.CulinaireTaxiDB;

namespace CulinaireTaxi.Database
{

    public static class RatingTable
    {

	/// <summary>
	/// Creates a new rating for a company.
	/// </summary>
	/// <param name="customerId">The id of the customer who gave the rating.</param>
	/// <param name="companyId">The id of the company that was rated.</param>
	/// <param name="rating">The rating that was given.</param>
	/// <returns>True if the new rating was created, false otherwise.</returns>
	public static bool CreateRating(long customerId, long companyId, int rating)
	{
	    using (var connection = new MySqlConnection(ConnectionString))
	    {
		connection.Open();

		using (var createRatingCMD = connection.CreateCommand())
		{
		    createRatingCMD.CommandText = $"INSERT IGNORE INTO gives_rating (customer_id, company_id, rating) VALUES ({customerId}, {companyId}, {rating})";

		    bool ratingCreated = (createRatingCMD.ExecuteNonQuery() != 0);

		    return ratingCreated;
		}
	    }
	}

	/// <summary>
	/// Retrieves a rating that was given by a customer to a company.
	/// </summary>
	/// <param name="customerId">The id of the customer who gave the rating.</param>
	/// <param name="companyId">The id of the company that was rated.</param>
	/// <returns>The rating given by the customer or 0 if the customer has not yet rated the company.</returns>
	public static int RetrieveRating(long customerId, long companyId)
	{
	    using (var connection = new MySqlConnection(ConnectionString))
	    {
		connection.Open();

		using (var retrieveRatingCMD = connection.CreateCommand())
		{
		    retrieveRatingCMD.CommandText = $"SELECT rating FROM gives_rating WHERE customer_id = {customerId} AND company_id = {companyId}";

		    using (var reader = retrieveRatingCMD.ExecuteReader(CommandBehavior.SingleRow))
		    {
			if (reader.Read())
			{
			    return reader.GetInt32(0);
			}
			else
			{
			    return 0;
			}
		    }
		}
	    }
	}

	/// <summary>
	/// Retrieves the average rating of a company.
	/// </summary>
	/// <param name="companyId">The id of the company.</param>
	/// <returns>The average rating of a company or 0 if the company has not been rated yet.</returns>
	public static int RetrieveAverageRating(long companyId)
	{
	    using (var connection = new MySqlConnection(ConnectionString))
	    {
		connection.Open();

		using (var retrieveAverageRatingCMD = connection.CreateCommand())
		{
		    retrieveAverageRatingCMD.CommandText = $"SELECT AVG(rating) FROM gives_rating WHERE company_id = {companyId} GROUP BY company_id";

		    using (var reader = retrieveAverageRatingCMD.ExecuteReader(CommandBehavior.SingleRow))
		    {
			if (reader.Read())
			{
			    return reader.GetInt32(0);
			}
			else
			{
			    return 0;
			}
		    }
		}
	    }
	}

	/// <summary>
	/// Updates a rating given by a customer.
	/// </summary>
	/// <param name="customerId">The id of the customer who gave the rating.</param>
	/// <param name="companyId">The id of the company that was rated.</param>
	/// <param name="new_rating">The new rating.</param>
	/// <returns>True if the rating was updated, false otherwise.</returns>
	public static bool UpdateRating(long customerId, long companyId, int new_rating)
	{
	    using (var connection = new MySqlConnection(ConnectionString))
	    {
		connection.Open();

		using (var updateRatingCMD = connection.CreateCommand())
		{
		    updateRatingCMD.CommandText = $"UPDATE gives_rating SET rating = {new_rating} WHERE customer_id = {customerId} AND company_id = {companyId}";

		    bool ratingUpdated = (updateRatingCMD.ExecuteNonQuery() != 0);

		    return ratingUpdated;
		}
	    }
	}

	/// <summary>
	/// Deletes a rating given by a customer.
	/// </summary>
	/// <param name="customerId">The id of a customer who gave the rating.</param>
	/// <param name="companyId">The id of the company that was rated</param>
	/// <returns>True if the rating was deleted, false otherwise.</returns>
	public static bool DeleteRating(long customerId, long companyId)
	{
	    using (var connection = new MySqlConnection(ConnectionString))
	    {
		connection.Open();

		using (var deleteRatingCMD = connection.CreateCommand())
		{
		    deleteRatingCMD.CommandText = $"DELETE FROM gives_rating WHERE customer_id = {customerId} AND company_id = {companyId}";

		    bool ratingDeleted = (deleteRatingCMD.ExecuteNonQuery() != 0);

		    return ratingDeleted;
		}
	    }
	}

    }

}
