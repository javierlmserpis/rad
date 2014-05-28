using System;
using Gtk;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Serpis.Ad;
using System.Data;

/* Recupera datos de una base de datos (incluye un NoteBook) que dependiendo de la pestaña en la que estemos muestra una tabla u otra
Tiene cuatro botones, un boton añadir que inserta datos en la tabla que tengamos seleccionada mediante la pestaña
Un boton editar que solamente indica que hemos seleccionado
Un boton eliminar que dependiendo que fila y pestaña tengamos seleccionada elimina una fila de la tabla
Un boton refrescar para refrescar los datos de la tabla
 */

public partial class MainWindow: Gtk.Window
{	
	public ListStore listStore;
	public ListStore listStore2;
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		//Verificamos la pestaña en la que el usuario esta 
		if(notebook1.GetTabLabelText(notebook1.GetNthPage(0)).Equals("Articulos")){

			IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand();
			selectDbCommand.CommandText = "select * from articulo";
			IDataReader dataReader = selectDbCommand.ExecuteReader();

			for (int i = 0; i < dataReader.FieldCount; i++)
				treeView.AppendColumn(dataReader.GetName (i),new CellRendererText(),"text",i);

			listStore = newListStore(dataReader.FieldCount);
			fillListStore(listStore,dataReader); 
			dataReader.Close();
			treeView.Model = listStore;
		}

		if(notebook1.GetTabLabelText(notebook1.GetNthPage(1)).Equals("Categorias")){

			IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand();
			selectDbCommand.CommandText = "select * from categoria";
			IDataReader dataReader = selectDbCommand.ExecuteReader();

			for (int i = 0; i < dataReader.FieldCount; i++)
				treeView2.AppendColumn(dataReader.GetName (i),new CellRendererText(),"text",i);

			listStore2 = newListStore(dataReader.FieldCount);
			fillListStore(listStore2,dataReader); 
			dataReader.Close();
			treeView2.Model = listStore2;
		}
		editAction.Sensitive = false;

		//Boton EDIT
		editAction.Activated += delegate {
			if (!verificacionPag()){
				if (treeView.Selection.CountSelectedRows() == 0)
					return;
				//Se ha intentado hacer un messagedialog con entry para editar pero no he podido
				TreeIter treeIter;
				treeView.Selection.GetSelected(out treeIter);
				object id = listStore.GetValue(treeIter,0);
				object nombre = listStore.GetValue(treeIter,1);
				MessageDialog messageDialog = new MessageDialog(this,
				                  DialogFlags.DestroyWithParent,
				                  MessageType.Info,
				                  ButtonsType.Ok,
				                  "Ha seleccionado:\n Id= {0} Nombre= {1}", id, nombre);
				                  messageDialog.Title = "Editar";
				                  messageDialog.Run ();
				                  messageDialog.Destroy ();
			}
			if (verificacionPag()){
				if (treeView2.Selection.CountSelectedRows() == 0)
					return;

				TreeIter treeIter;
				treeView2.Selection.GetSelected(out treeIter);
				object id = listStore2.GetValue(treeIter,0);
				object nombre = listStore2.GetValue(treeIter,1);
				MessageDialog messageDialog = new MessageDialog(this,
				                                                DialogFlags.DestroyWithParent,
				                                                MessageType.Info,
				                                                ButtonsType.Ok,
				                                                "Ha seleccionado:\n Id= {0} Nombre de la categoria= {1}", id, nombre);
				messageDialog.Title = "Editar";
				messageDialog.Run ();
				messageDialog.Destroy ();
			}
	};

		//Boton DELETE
		deleteAction.Sensitive = false;

		deleteAction.Activated += delegate {
			if (!verificacionPag()){
			if (treeView.Selection.CountSelectedRows() == 0)
				return;

			TreeIter treeIter; 
			treeView.Selection.GetSelected(out treeIter);
			object id = listStore.GetValue (treeIter, 0);
			object nombre = listStore.GetValue(treeIter,1);
			MessageDialog md = new MessageDialog (this,
			                                      DialogFlags.DestroyWithParent,
			                                      MessageType.Question,
			                                      ButtonsType.YesNo,
					"¿Esta seguro de eliminar el articulo seleccionado?\n"+"ID: "+id+" - "+nombre);

				md.Title = "Eliminación de un articulo";
			ResponseType response = (ResponseType) md.Run ();
			if (response == ResponseType.Yes) {
				IDbCommand deleteDbCommand = App.Instance.DbConnection.CreateCommand();
				deleteDbCommand.CommandText = "delete from articulo where id=" + id;
				deleteDbCommand.ExecuteNonQuery();
			}
			md.Destroy();	
			} 
			if (verificacionPag()){
				TreeIter treeIter2;
				treeView2.Selection.GetSelected(out treeIter2);
				object idCategoria = listStore2.GetValue(treeIter2,0);
				object nombreCategoria = listStore2.GetValue(treeIter2,1);
				MessageDialog md2 = new MessageDialog (this,
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Question,
				                                      ButtonsType.YesNo,
					"¿Esta seguro de eliminar la categoria seleccionada?\n"+nombreCategoria);
				md2.Title = "Eliminación de categoria";
				ResponseType response2 = (ResponseType) md2.Run ();
				if (response2 == ResponseType.Yes) {
					IDbCommand deleteDbCommand = App.Instance.DbConnection.CreateCommand();
					deleteDbCommand.CommandText = "delete from categoria where id=" + idCategoria;
					deleteDbCommand.ExecuteNonQuery();
				}
				md2.Destroy();
			}
	};

		treeView.Selection.Changed += delegate {
			bool hasSelectedRows = treeView.Selection.CountSelectedRows() > 0;
			editAction.Sensitive = hasSelectedRows;
			deleteAction.Sensitive = hasSelectedRows;
		};

		treeView2.Selection.Changed += delegate {
			bool hasSelectedRows = treeView2.Selection.CountSelectedRows() > 0;
			editAction.Sensitive = hasSelectedRows;
			deleteAction.Sensitive = hasSelectedRows;
		};

		//Boton REFRESH

		refreshAction.Activated += delegate {
			if (!verificacionPag()){

			listStore.Clear ();
			IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand();
			selectDbCommand.CommandText = "select * from articulo";
			IDataReader dataReader = selectDbCommand.ExecuteReader();
			fillListStore(listStore,dataReader);
			dataReader.Close ();
			}

			if (verificacionPag()){

				listStore2.Clear();
				IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand();
				selectDbCommand.CommandText = "select * from categoria";
				IDataReader dataReader = selectDbCommand.ExecuteReader();
				fillListStore(listStore2,dataReader);
				dataReader.Close ();
			}
	};
		//Boton AÑADIR

		addAction.Activated += delegate {
			if (!verificacionPag()){
				ventanaArticulo();
				} 
			if (verificacionPag()){
				ventanaCategoria();
				}
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
			List<string> values = new List<string>();
			for (int i = 0; i < dataReader.FieldCount; i++)
				values.Add (dataReader.GetValue (i).ToString());
			listStore.AppendValues (values.ToArray());
		}
	}

	public bool verificacionPag(){
		return notebook1.CurrentPageWidget==notebook1.GetNthPage(1);
	}

	public void ventanaArticulo(){
		WindowArticulo windowArticulo = new WindowArticulo ();
		windowArticulo.ShowAll ();
	}

	public void ventanaCategoria(){
		WindowCategoria windowCategoria = new WindowCategoria ();
		windowCategoria.ShowAll ();
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}