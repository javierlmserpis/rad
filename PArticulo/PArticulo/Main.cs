using System;
using MySql.Data.MySqlClient;
// using System.Data para interfaces

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
			MySqlCommand updateMySqlCommand = mySqlConnection.CreateCommand();
			
			//TODO
			updateMySqlCommand.CommandText = "update articulo set nombre=nombre where id=1"; //acabar ejerc parametro
			
			//TODO parametro
			updateMySqlCommand.ExecuteNonQuery();
			//
			
			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
			mySqlCommand.CommandText = sql;
			
			MySqlDataReader mySqlDataReader;
			mySqlDataReader = mySqlCommand.ExecuteReader();
    
    	//	while (mySqlDataReader.Read()) {
      // 		Console.WriteLine(mySqlDataReader.GetString(0) + ", " + mySqlDataReader.GetString(1)+ ", "+mySqlDataReader.GetString(2));
    //		}
			
			while (mySqlDataReader.Read()){
				Console.WriteLine("id={0} nombre={1}", mySqlDataReader["id"], mySqlDataReader["nombre"]);
			}
			mySqlDataReader.Close();
			mySqlConnection.Close();
						
		}
	}
}
