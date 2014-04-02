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
			
			MySqlConnection IDbConnection = new MySqlConnection(conexion);
			IDbConnection.Open();
			
			string sql = "select * from articulo";
			
			MySqlCommand mySqlCommand = IDbConnection.CreateCommand();
			mySqlCommand.CommandText = sql;
			
			MySqlDataReader mySqlDataReader;
			mySqlDataReader = mySqlCommand.ExecuteReader();
    
    		while (mySqlDataReader.Read()) {
       		Console.WriteLine(mySqlDataReader.GetString(0) + ", " + mySqlDataReader.GetString(1));
    		}
			IDbConnection.Close();
						
		}
	}
}
