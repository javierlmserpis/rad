using System;
using System.Collections.Generic;
using Gtk;
using System.Data;

namespace Serpis.Ad
{
	public class TreeViewFiller
	{
		public TreeView treeView;
		public ListStore listStore;
		
		public void Fill (TreeView treeView, IDbConnection dbConnection, string selectText){
		IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand();
		selectDbCommand.CommandText = selectText;
		IDataReader dataReader = selectDbCommand.ExecuteReader();
			
		for (int i = 0; i < dataReader.FieldCount; i++)
			treeView.AppendColumn(dataReader.GetName (i),new CellRendererText(),"text",i);
		
		listStore = createListStore(dataReader.FieldCount);
		fillListStore(listStore,dataReader); 
			dataReader.Close();
			treeView.Model = listStore;
		}
		
		private ListStore createListStore (int fieldCount){
			Type[] types = new Type[fieldCount];
			for (int i = 0; i < fieldCount; i++)
				types [i] = typeof(string);
			
			return new ListStore(types);
		}
		
		private void fillListStore (ListStore listStore, IDataReader dataReader){
		while (dataReader.Read()){
				List<string> values = new List<string>();
				for (int i = 0; i < dataReader.FieldCount; i++)
					values.Add (dataReader.GetValue (i).ToString());
				listStore.AppendValues (values.ToArray());
			}
		}
		
		public ListStore getListStore {
			get {
				return this.listStore;
			}
	}
}}
