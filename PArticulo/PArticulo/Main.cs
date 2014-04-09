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
			String horaActual = DateTime.Now.ToString();
			
			string sql = "select * from articulo";
			
			//Para el update
			MySqlCommand updateMySqlCommand = mySqlConnection.CreateCommand();
			updateMySqlCommand.CommandText = "update articulo set nombre=@nombre where id=1";
			MySqlParameter mySqlParameter = updateMySqlCommand.CreateParameter();
			mySqlParameter.ParameterName = "nombre";
			mySqlParameter.Value = horaActual;
			updateMySqlCommand.Parameters.Add(mySqlParameter);
			
			updateMySqlCommand.ExecuteNonQuery();
			
			//Para leer el select
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
