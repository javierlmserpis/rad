using System;
using MySql.Data.MySqlClient;
namespace PArticulo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string conexion =
				"Server=localhost;" +
				"Database=dbrepaso;" +
				"User Id=root;" +
				"Password =sistemas";
			
			MySqlConnection mySqlConnection = new MySqlConnection(conexion);
			mySqlConnection.Open();
			
			string sql = "select * from articulo";
			
			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
			mySqlCommand.CommandText = sql;
			
			MySqlDataReader mySqlDataReader;
			mySqlDataReader = mySqlCommand.ExecuteReader();
    
    		while (mySqlDataReader.Read()) {
       		Console.WriteLine(mySqlDataReader.GetString(0) + ", " + mySqlDataReader.GetString(1));
    		}
			mySqlConnection.Close();
						
		}
	}
}
