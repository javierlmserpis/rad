using System;
using MySql.Data.MySqlClient;
using System.Data;

	public partial class WindowArticulo : Gtk.Window
	{
		MainWindow mainWindow = new MainWindow();
		
		public WindowArticulo () : 
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();

		string conexion = "Server=localhost;Database=dbrepaso;User Id=root;Password=sistemas";

		mainWindow.Destroy ();
		MySqlConnection mySqlConnection = new MySqlConnection (conexion);
		button1.Clicked += delegate {
			añadirArticuloSql(mySqlConnection);
			};
		}

	public void añadirArticuloSql(MySqlConnection mySqlConnection){
		string nombre = entry1.Text;
		string categoria = entry2.Text;
		string precio = entry3.Text;
		string añadirSql = "insert into articulo (nombre,categoria,precio) values('"+nombre+"','"+categoria +"','"+ precio+"')";
		mySqlConnection.Open ();
	
		if (nombre.Equals ("") || categoria.Equals("") || precio.Equals("")) {
			mySqlConnection.Close ();
			entry1.Text = "No deje en blanco nada";
			entry2.Text = "Reinicie la ventana";
			entry3.Text = "Conexion cerrada por seguridad";

			} else {
				MySqlCommand añadir = mySqlConnection.CreateCommand ();
				añadir.CommandText = añadirSql;
				añadir.ExecuteNonQuery ();
				entry1.Text = "Se ha añadido exitosamente";
				entry2.Text = "";
				entry3.Text = "";
				mySqlConnection.Close ();
			}
		}
	}

	 


