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
		
		//TreeViewFiller.Fill(treeView, App.Instance.DbConnection, "select id, nombre, categoria, precio from articulo");
		
		IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand();
		
		selectDbCommand.CommandText = "select * from articulo";
		
		IDataReader dataReader = selectDbCommand.ExecuteReader();
		
		addColumns(dataReader);
		
		ListStore listStore = newListStore(dataReader.FieldCount);
		treeView.Model = listStore;
		fillListStore(listStore,dataReader);
		dataReader.Close();
		
		//DELETE
		deleteAction.Sensitive = false;
		
		deleteAction.Activated += delegate {
			if (treeView.Selection.CountSelectedRows() == 0)
				return;
			
			TreeIter treeIter;
			treeView.Selection.GetSelected(out treeIter);
			object id = listStore.GetValue (treeIter,0);
			
			MessageDialog messageDialog = new MessageDialog(this,
			  DialogFlags.DestroyWithParent, MessageType.Question,
			  ButtonsType.YesNo,"¿Quieres eliminar el elemento seleccionado?");
			  messageDialog.Title = "Eliminar elemento";
			
			ResponseType response = (ResponseType)messageDialog.Run(); 
			messageDialog.Destroy();
			if (response == ResponseType.Yes) {
			IDbCommand deleteDbCommand = App.Instance.DbConnection.CreateCommand();
			deleteDbCommand.CommandText = "delete from articulo where id=" +id;
				deleteDbCommand.ExecuteNonQuery();
			}
		};
			treeView.Selection.Changed += delegate {
			bool hasSelectedRows = treeView.Selection.CountSelectedRows() > 0;
			editAction.Sensitive = hasSelectedRows;
			deleteAction.Sensitive = hasSelectedRows;
			};
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
		listStore.Clear();
		
		while (dataReader.Read()){
			listStore.AppendValues(dataReader["id"].ToString(),dataReader["nombre"].ToString(), 
			                       dataReader["categoria"].ToString(), dataReader["precio"].ToString());
		}
	}
	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
