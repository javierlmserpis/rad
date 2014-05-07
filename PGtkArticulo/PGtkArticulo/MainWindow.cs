using System;
using Gtk;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Serpis.Ad;
using System.Data;

public partial class MainWindow: Gtk.Window
{	
	
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand();
		
		selectDbCommand.CommandText = "select * from articulo";
		
		IDataReader dataReader = selectDbCommand.ExecuteReader();
		
		addColumns(dataReader);
		
		ListStore listStore = newListStore(dataReader.FieldCount);
		treeView.Model = listStore;
		fillListStore(listStore,dataReader);
	}
	
	private void addColumns(IDataReader dataReader) {
		for (int i = 0; i < dataReader.FieldCount; i++)
				treeView.AppendColumn(dataReader.GetName (i),new CellRendererText(),"text",i);
	}
	
	private ListStore newListStore(int count) {
		Type[] types = new Type[count];
		for (int i = 0; i < count; i++)
			types[i] = typeof(string);
		return new ListStore(types);
	}
	
	private void fillListStore(ListStore listStore, IDataReader dataReader) {
		while (dataReader.Read()){
			listStore.AppendValues(dataReader["id"].ToString(),dataReader["nombre"].ToString(), 
			                       dataReader["categoria"].ToString(), dataReader["precio"].ToString());
		}
		dataReader.Close();
	}
	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
