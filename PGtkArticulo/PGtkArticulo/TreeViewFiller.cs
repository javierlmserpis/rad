using System;
using System.Collections.Generic;
using Gtk;
using System.Data;

namespace Serpis.Ad
{
	public class TreeViewFiller
	{
		public static void Fill (TreeView treeView, IDbConnection dbConnection, string selectText){
			
		IDbCommand selectDbCommand = App.Instance.DbConnection.CreateCommand();
		selectDbCommand.CommandText = selectText;
		IDataReader dataReader = selectDbCommand.ExecuteReader();
			
		for (int i = 0; i < dataReader.FieldCount; i++)
			treeView.AppendColumn(dataReader.GetName (i),new CellRendererText(),"text",i);
			
			Type[] types = new Type(dataReader.FieldCount);
			for (int i = 0; i < dataReader.FieldCount; i++){
				types [i] = typeof(string);
			}
			
		}
			
	}
}

