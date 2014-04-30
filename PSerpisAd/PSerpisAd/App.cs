using System;
using System.Data;

namespace PSerpisAd
{
	public class App
	{
		private static App instance = new App();
		
		public static App Instance {
			get {return instance;}
		}

		private App () {
		}
		
		private IDbConnection dbConnection;
		public IDbConnection DbConnection {
			get {return dbConnection;}
			set {
				dbConnection = value;
				if (dbConnection.State == ConnectionState.Closed)
					dbConnection.Open ();
			}
		}	
	}
}

