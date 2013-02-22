using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;

namespace ArachNGIN.Files.Strings
{
	/// <summary>
	/// T��da pro pr�ci se StringCollection (aka TStrings)
	/// </summary>
	public class StringCollections
	{
        /// <summary>
        /// T��dy se samejma statickejma fcema konstruktory nepot�ebujou :-)
        /// </summary>
		public StringCollections()
		{
			// hey there, nothin' here :-)
		}

        /// <summary>
        /// P�evede StringCollection na jeden string
        /// </summary>
        /// <param name="stringCollection">Vstupn� StringCollection</param>
        /// <returns>V�stupn� string</returns>
        public static string StringCollectionToString(StringCollection stringCollection)
        {
            string outputString = "";
            if (stringCollection != null && stringCollection.Count > 0)
            {
                foreach (string inputString in stringCollection)
                    outputString += inputString + "\r\n";
                outputString = outputString.Substring(0, outputString.Length - 1);

            }
            return outputString;
        }

		
		/// <summary>
		/// Ulo�� obsah StringCollection do souboru
		/// </summary>
		/// <param name="s_file">n�zev souboru</param>
		/// <param name="s_collection">StringCollection</param>
		public static void SaveToFile(string s_file, StringCollection s_collection)
		{
			FileInfo fi = new FileInfo(s_file);
			TextWriter writer;
			writer = fi.CreateText();
			StringEnumerator enu = s_collection.GetEnumerator();
			while(enu.MoveNext())
			{
				writer.WriteLine(enu.Current);
			}
			writer.Close();
		}

        /// <summary>
        /// Ulo�� obsah StringCollection do proudu
        /// </summary>
        /// <param name="s_output">proud</param>
        /// <param name="s_collection">obsah</param>
        public static void SaveToStream(Stream s_output, StringCollection s_collection)
        {
            StreamWriter writer = new StreamWriter(s_output);
            writer.AutoFlush = true;
            StringEnumerator enu = s_collection.GetEnumerator();
            while (enu.MoveNext())
            {
                writer.WriteLine(enu.Current);
            }
            writer.Flush();
            //writer.Close(); // nezavirat!
        }

        public static void SaveToFile(string s_file, ListView.ListViewItemCollection s_collection)
        {
            FileInfo fi = new FileInfo(s_file);
            TextWriter writer;
            writer = fi.CreateText();
            foreach (ListViewItem item in s_collection)
            {
                string s = "";
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    s = s + item.SubItems[i].ToString();
                    if (i < item.SubItems.Count - 1) s = s + " -> ";
                    s = s.Replace("ListViewSubItem:", "");
                    s = s.Replace("}", "");
                    s = s.Replace("{", "");
                }
                //ListViewItemConverter k = new ListViewItemConverter();
                //string s = k.ConvertToString(item);
                writer.WriteLine(s);
            }
            writer.Close();
        }

		/// <summary>
		/// Na�te soubor (textov�) do StringCollection
		/// </summary>
		/// <param name="s_file">n�zev souboru</param>
		/// <param name="s_collection">StringCollection do kter� se bude na��tat</param>
		/// <param name="b_append">p�ipojit k existuj�c�m prvk�m v kolekci</param>
		public static void LoadFromFile(string s_file, StringCollection s_collection, bool b_append)
		{
			FileInfo soubor_info = new FileInfo(s_file);
			TextReader reader;
			string s = "0";
			if(!soubor_info.Exists) return;
			// na��ty na��ty :-)
			reader = soubor_info.OpenText();
			if(!b_append) s_collection.Clear();
			while((s = reader.ReadLine()) != null)
			{
				s_collection.Add(s);
			}
			// na�teno	
			reader.Close();
		}

        /// <summary>
        /// Na�te proud do StringCollection
        /// </summary>
        /// <param name="s_input">proud</param>
        /// <param name="s_collection">v�stup</param>
        /// <param name="b_append">p�ipojit k existuj�c�m prvk�m v kolekci</param>
        public static void LoadFromStream(Stream s_input, StringCollection s_collection, bool b_append)
        {
            StreamReader reader = new StreamReader(s_input);
            string s = "0";
            if (!b_append) s_collection.Clear();
            while ((s = reader.ReadLine()) != null)
            {
                s_collection.Add(s);
            }
            // reader.Close(); // nezavirat!
        }

        /// <summary>
        /// Na�te proud do StringCollection
        /// </summary>
        /// <param name="s_input">proud</param>
        /// <param name="s_collection">v�stup</param>
        public static void LoadFromStream(Stream s_input, StringCollection s_collection)
        {
            LoadFromStream(s_input, s_collection, false);
        }

		/// <summary>
		/// Na�te soubor (textov�) do StringCollection
		/// </summary>
		/// <param name="s_file">n�zev souboru</param>
		/// <param name="s_collection">StringCollection do kter� se bude na��tat</param>
		public static void LoadFromFile(string s_file, StringCollection s_collection)
		{
			LoadFromFile(s_file,s_collection,false);
		}

        public static void SaveToFile(string s_file, ListView list)
        {
            StringBuilder listViewContent = new StringBuilder();

            foreach (ListViewItem item in list.Items)
            {
                listViewContent.Append(item.Text);
                listViewContent.Append(Environment.NewLine);
            }

            TextWriter tw = new StreamWriter(s_file);

            tw.WriteLine(listViewContent.ToString());

            tw.Close();

        }
	}
}
