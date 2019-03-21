using CulinaireTaxi.Database;
using System;
using System.Collections.Generic;

namespace CulinaireTaxi.Authentication
{

    [Obsolete("This class is now deprecated, use SessionExtension instead.")]
    public class Session {

	private bool _logedin;

	public int id;
	public string firstname;
	public string lastname;
	public string email;

	public Session() {
	    _logedin = false;
	}

	public bool login(DatabaseReader _database, string _email, string _password) {
	    bool acceptAccountDetails = _database.containsData("users", string.Format("email = '{0}' AND password = '{1}'", _email, _password));

	    if (acceptAccountDetails) {
		id = int.Parse(_database.getData("users", "id", string.Format("email = '{0}' AND password = '{1}'", _email, _password)));
		firstname = _database.getData("users", "firstname", string.Format("email = '{0}' AND password = '{1}'", _email, _password));
		lastname = _database.getData("users", "lastname", string.Format("email = '{0}' AND password = '{1}'", _email, _password));
		email = _email;
		_logedin = true;
	    }

	    return acceptAccountDetails;
	}

	public bool registAccount(DatabaseReader _database, string _fistname, string _lastname, string _email, string _password) {
	    SortedList<string, string> newUser = new SortedList<string, string>();
	    newUser.Add("firstname", _fistname);
	    newUser.Add("lastname", _lastname);
	    newUser.Add("email", _email);
	    newUser.Add("password", _password);

	    if (_database.containsData("users", string.Format("email = '{0}'", _email))) {
		return false;
	    }

	    _database.addData("users", newUser);
	    this.login(_database, _email, _password);
	    return true;
	}

	public void logout() {
	    id = -1;
	    firstname = "";
	    lastname = "";
	    email = "";
	    _logedin = false;
	}

	public bool isLogedin() {
	    return _logedin;
	}

    }

}
