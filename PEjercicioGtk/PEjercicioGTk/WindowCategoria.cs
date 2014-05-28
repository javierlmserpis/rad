using System;
using MySql.Data.MySqlClient;

	public partial class WindowCategoria : Gtk.Window
	{
	MainWindow mainWindow = new MainWindow();
		public WindowCategoria () : 
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();

		string conexion = "Server=localhost;Database=dbrepaso;User Id=root;Password=sistemas";
		mainWindow.Destroy ();

		MySqlConnection mySqlConnection = new MySqlConnection (conexion);
		button2.Clicked += delegate {
			añadirCategoriaSql(mySqlConnection);
			};
	}

	public void añadirCategoriaSql(MySqlConnection mySqlConnection){
		if (mainWindow.verificacionPag()){
		string numeroCategoria = entry4.Text;
		string añadirSql = "insert into categoria (nombre) values('"+numeroCategoria+"')";
		mySqlConnection.Open ();

		if (numeroCategoria.Equals ("") ) {
			mySqlConnection.Close ();
			entry4.Text = "No deje en blanco nada";

		} else {

			MySqlCommand añadir = mySqlConnection.CreateCommand ();
			añadir.CommandText = añadirSql;
			añadir.ExecuteNonQuery ();

			mySqlConnection.Close ();
			}
		}
	}
}
	

