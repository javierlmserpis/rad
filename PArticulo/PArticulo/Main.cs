using System;
using MySql.Data.MySqlClient;
using PSerpisAd;
using System.Data;

namespace PArticulo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			App.Instance.DbConnection = new MySqlConnection("Server=localhost;Database=dbprueba;User Id=root;Password=sistemas");
			
			String horaActual = DateTime.Now.ToString();
			string sql = "select * from articulo";
			
			
			//Para el update con interfaces y metodo de parametros
			IDbCommand updateDbCommand = App.Instance.DbConnection.CreateCommand();
			updateDbCommand.CommandText = "update articulo set nombre=@nombre where id=1";
			
			DbCommandUtil.AddParameter(updateDbCommand,"nombre",horaActual);
			updateDbCommand.ExecuteNonQuery();
			
			//Para delete
			IDbCommand deleteDbCommand = App.Instance.DbConnection.CreateCommand();
			deleteDbCommand.CommandText = "delete from articulo where id=@numerodelete";
			
			DbCommandUtil.AddParameter(deleteDbCommand,"numerodelete",6);
			deleteDbCommand.ExecuteNonQuery(); 
			
			//Para insertar
		    IDbCommand insertDbCommand = App.Instance.DbConnection.CreateCommand();
			insertDbCommand.CommandText = "insert into articulo (id,nombre)" +
											 "values(6,@nombreinsert)";
			
			DbCommandUtil.AddParameter(insertDbCommand,"nombreinsert","articulo 6");
			insertDbCommand.ExecuteNonQuery(); 
			
			//Para leer el select
			IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand();
			selectDbCommand.CommandText = sql;
			IDataReader mySqlDataReader;
			mySqlDataReader = selectDbCommand.ExecuteReader();
    		
			
    	//	while (mySqlDataReader.Read()) {
      // 		Console.WriteLine(mySqlDataReader.GetString(0) + ", " + mySqlDataReader.GetString(1)+ ", "+mySqlDataReader.GetString(2));
    //		}
			
			while (mySqlDataReader.Read()){
				Console.WriteLine("id={0} nombre={1}", mySqlDataReader["id"], mySqlDataReader["nombre"]);
			}
			
			mySqlDataReader.Close();
			App.Instance.DbConnection.Close();
						
		}
	}
}
