using CulinaireTaxi.Database.Entities;
using MySql.Data.MySqlClient;
using System.Data;
using static CulinaireTaxi.Database.CulinaireTaxiDB;

namespace CulinaireTaxi.Database
{

    public static class AccountTable
    {

	/// <summary>
	/// Attempts to create a new account in the database.
	/// </summary>
	/// <param name="accountType">The type of the account.</param>
	/// <param name="email">The email of the account.</param>
	/// <param name="password">The password of the account.</param>
	/// <param name="contact">The contact details of the account.</param>
	/// <param name="companyId">OPTIONAL: The company id of the account.</param>
	/// <returns>The newly created account on success, null otherwise (i.e. a conflicting account exists).</returns>
	public static Account CreateAccount(AccountType accountType, string email, string password, ContactDetails contact, long? companyId = null)
	{
	    using (var connection = new MySqlConnection(ConnectionString))
	    {
		    connection.Open();

		    using (var createAccountCMD = connection.CreateCommand())
		    {
		        createAccountCMD.CommandText =
		        "INSERT IGNORE INTO Account" +
		        " (type, email, password, first_name, last_name, county, city, street, postal_code, phone_number, company_id)" +
		        " VALUES" +
		        " (@accountType, @email, @password, @firstName, @lastName, @county, @city, @street, @postalCode, @phoneNumber, @companyId)";

		        var parameters = createAccountCMD.Parameters;

		        parameters.AddWithValue("@accountType", (byte)accountType);
		        parameters.AddWithValue("@email", email);
		        parameters.AddWithValue("@password", password);
		        parameters.AddWithValue("@firstName", contact.FirstName);
		        parameters.AddWithValue("@lastName", contact.LastName);
		        parameters.AddWithValue("@county", contact.County);
		        parameters.AddWithValue("@city", contact.City);
		        parameters.AddWithValue("@street", contact.Street);
		        parameters.AddWithValue("@postalCode", contact.PostalCode);
		        parameters.AddWithValue("@phoneNumber", contact.PhoneNumber);
		        parameters.AddWithValue("@companyId", companyId);

		        bool success = (createAccountCMD.ExecuteNonQuery() != 0);

		        if (success)
		        {
			    Account account = new Account();

			    account.Id = createAccountCMD.LastInsertedId;

			    account.AccountType = accountType;

			    account.Email = email;
			    account.Password = password;

			    account.Contact = contact;

			    account.CompanyId = companyId;

			    return account;
		        }
		        else
		        {
			    return null;
		        }
		    }
	    }
	}

    public static Account RetrieveAccountByID(long id)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            using (var retrieveAccountCMD = connection.CreateCommand())
            {
                retrieveAccountCMD.CommandText = "SELECT * FROM Account WHERE id = @id";
                retrieveAccountCMD.Parameters.AddWithValue("@id", id);

                using (var reader = retrieveAccountCMD.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        Account account = new Account();

                        account.Id = reader.GetInt64(0);

                        account.AccountType = (AccountType)reader.GetByte(1);

                        account.Email = reader.GetString(2);
                        account.Password = reader.GetString(3);

                        account.Contact.FirstName = reader.GetString(4);
                        account.Contact.LastName = reader.GetString(5);

                        account.Contact.County = !reader.IsDBNull(6) ? reader.GetString(6) : null;
                        account.Contact.City = !reader.IsDBNull(7) ? reader.GetString(7) : null;
                        account.Contact.Street = !reader.IsDBNull(8) ? reader.GetString(8) : null;
                        account.Contact.PostalCode = !reader.IsDBNull(9) ? reader.GetString(9) : null;
                        account.Contact.PhoneNumber = !reader.IsDBNull(10) ? reader.GetString(10) : null;

                        account.CompanyId = !reader.IsDBNull(11) ? (long?)reader.GetInt64(11) : null;

                        return account;
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
        /// Attempt to retrieve an account from the database.
        /// </summary>
        /// <param name="email">The email of the account.</param>
        /// <returns>The account if one was found, null otherwise.</returns>
        public static Account RetrieveAccount(string email)
	    {
	        using (var connection = new MySqlConnection(ConnectionString))
	        {
		        connection.Open();

		        using (var retrieveAccountCMD = connection.CreateCommand())
		        {
		            retrieveAccountCMD.CommandText = "SELECT * FROM Account WHERE email = @email";
		            retrieveAccountCMD.Parameters.AddWithValue("@email", email);

		            using (var reader = retrieveAccountCMD.ExecuteReader(CommandBehavior.SingleRow))
		            {
			            if (reader.Read())
			            {
			                Account account = new Account();

			                account.Id = reader.GetInt64(0);

			                account.AccountType = (AccountType)reader.GetByte(1);

			                account.Email = reader.GetString(2);
			                account.Password = reader.GetString(3);

			                account.Contact.FirstName = reader.GetString(4);
			                account.Contact.LastName = reader.GetString(5);

			                account.Contact.County = !reader.IsDBNull(6) ? reader.GetString(6) : null;
			                account.Contact.City = !reader.IsDBNull(7) ? reader.GetString(7) : null;
			                account.Contact.Street = !reader.IsDBNull(8) ? reader.GetString(8) : null;
			                account.Contact.PostalCode = !reader.IsDBNull(9) ? reader.GetString(9) : null;
			                account.Contact.PhoneNumber = !reader.IsDBNull(10) ? reader.GetString(10) : null;

			                account.CompanyId = !reader.IsDBNull(11) ? (long?)reader.GetInt64(11) : null;

			                return account;
			            }
			            else
			            {
			                return null;
			            }
		            }
		        }
	        }
	    }
    }
}
