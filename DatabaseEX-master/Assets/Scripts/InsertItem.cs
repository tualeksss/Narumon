using UnityEngine;
using System.Collections;
using Mono.Data.SqliteClient;
using System.Data;
using System;
using UnityEngine.UI;

public class InsertItem : MonoBehaviour {

	public InputField nameInput;
	public InputField passwordInput;
	public InputField ageInput;
	public Text scrollview;

	public void Insert(){
		string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(connectionString);

		dbconn.Open(); //Open connection to the database.

		IDbCommand dbcmd = dbconn.CreateCommand();

		string sqlQuery = "insert into Account (UserName,Password,Age) values ('"+ nameInput.text +"','"+ passwordInput.text +"',"+ ageInput.text +")";
		dbcmd.CommandText = sqlQuery;
		dbcmd.ExecuteNonQuery ();
		dbcmd.Dispose();dbcmd = null;
		dbconn.Close();dbconn = null;
	}

	public void Select(){
		string connectionString = "URI=file:" + Application.dataPath + "/myDatabase.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(connectionString);
		dbconn.Open(); //Open connection to the database.

		scrollview.text = " ";
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = "SELECT * FROM Account";
		dbcmd.CommandText = sqlQuery;

		IDataReader reader = dbcmd.ExecuteReader();
		scrollview.text=" ";
		while(reader.Read()) {
			string user = reader.GetString (1);
			string pass = reader.GetString (2);
			int age = reader.GetInt32 (3);

			scrollview.text += user + " - " + pass + " - " + age + "\n";

		}
		reader.Close();reader = null;
		dbcmd.Dispose();dbcmd = null;
		dbconn.Close();dbconn = null;
	}
}