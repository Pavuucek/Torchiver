using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Reflection;
using System.Globalization;

namespace ArachNGIN.Files
{
	/// <summary>
	/// T��da pro ukl�d�n� nastaven� do xml souboru
	/// </summary>
	[XmlRoot("xml_def")]
	public class XmlSettings
	{
		[XmlAttribute("FileName")]
			private string m_file; //= "conf.xml";
		[XmlAttribute("AssemblyName")]
			private string m_asm = string.Empty;
		[XmlAttribute("CreationDate")]
			private DateTime m_creationdate = DateTime.Now;
		[XmlElement("Settings")]
			private Hashtable m_settingstable;

		#region  priv�tn� podp�rn� fce 
		/// <summary>
		/// fce na zji�t�n� cesty k aplikaci
		/// </summary>
		/// <returns>cesta k aplikaci</returns>
		private string GetAppPath()
		{
			return strAddSlash(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName));
		}

		/// <summary>
		/// Zjist� jestli cesta kon�� lom�tkem, kdy� ne, tak ho p�id�
		/// </summary>
		/// <param name="strString">cesta</param>
		/// <returns>cesta s lom�tkem</returns>
		private string strAddSlash(string strString)
		{
			// zapamatovat si: lom�tko je 0x5C!
			string s = strString;
			if(s[s.Length-1] != (char)0x5C) return s+(char)0x5C;
			else return s;
		}
		#endregion
		/// <summary>
		/// Konstruktor - bez jm�na souboru
		/// </summary>
		public XmlSettings()
		{
			m_settingstable = new Hashtable();
			Assembly asm = Assembly.GetExecutingAssembly();
			m_asm = asm.GetName().ToString();

			int indexOf = m_asm.IndexOf(",");
			m_asm = m_asm.Substring(0, indexOf);
			m_file = /*Path.ChangeExtension(m_asm,".conf")*/ m_asm + ".conf";

			DirectoryInfo dir = new DirectoryInfo(".");
			foreach (FileInfo f in dir.GetFiles(m_file)) 
			{
				m_file = f.FullName;
				m_creationdate = f.CreationTime;
				break;
			}
			m_file = GetAppPath() + m_file;
			LoadFromFile();
		}

		/// <summary>
		/// Konstruktor, se specifikov�n�m jm�na souboru
		/// </summary>
		/// <param name="strFileName">jm�no souboru</param>
		public XmlSettings(string strFileName)
		{
			m_settingstable = new Hashtable();
			Assembly asm = Assembly.GetExecutingAssembly();
			m_asm = asm.GetName().ToString();
			int indexOf = m_asm.IndexOf(",");
			m_asm = m_asm.Substring(0, indexOf);

			m_file = strFileName;
			//
			LoadFromFile();
		}

		/// <summary>
		/// Vlastnost ud�vaj�c� jm�no souboru
		/// </summary>
		public string FileName
		{
			get
			{
				return m_file;
			}
			set
			{
				m_file = value;
			}
		}

		/// <summary>
		/// Na�t� xml nastaven� ze souboru
		/// </summary>
		public void LoadFromFile()
		{
			XmlTextReader reader = null;
			try
			{
				reader = new XmlTextReader(m_file);
				FormatXML(reader, m_file);
			}
			catch (Exception e)
			{
				string str = e.Message;
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}
		}

		/// <summary>
		/// Ulo�� xml nastaven� do souboru
		/// </summary>
		public void SaveToFile()
		{
			FileInfo info = new FileInfo(m_file);
			//if(!info.Exists) info.Create();
			try
			{
				XmlTextWriter writer = new XmlTextWriter(m_file, System.Text.Encoding.UTF8);
				writer.WriteStartDocument();
				//
				writer.WriteStartElement("xml_def");
				//
				writer.WriteAttributeString("","AssemblyName","",m_asm);
				writer.WriteAttributeString("","FileName","",Path.GetFileName(m_file));
				writer.WriteAttributeString("","CreationDate","",m_creationdate.ToString());
				//
				foreach (string line in m_settingstable.Keys)
				{
					writer.WriteStartElement("Item");
					writer.WriteAttributeString("","Name","",line);
					writer.WriteAttributeString("","Value","",m_settingstable[line].ToString());
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
				writer.WriteEndDocument();
				writer.Flush();
				writer.Close();
			}
			catch (Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message.ToString());
			}
		}

		private void FormatXML(XmlReader reader, string FileName)
		{
			while (reader.Read())
			{
				if(reader.NodeType == XmlNodeType.Element)
				{
					string sName = "";
					string sValue = "";
					if(reader.Name == "Item")
					{
						if (reader.HasAttributes)
						{
							while (reader.MoveToNextAttribute())
							{
								if (reader.Name == "Name")
								{
									sName = reader.Value;
								}
								else if (reader.Name == "Value")
								{
									sValue = reader.Value;
								}
							}
						}
						if (sName != "") m_settingstable.Add(sName,sValue);
					}
				}

			}
		}

		/// <summary>
		/// Na�te nastaven� zadan�ho jm�na
		/// </summary>
		/// <param name="m_name">jm�no nastaven�</param>
		/// <param name="strDefault">defaultn� hodnota</param>
		/// <returns>hodnota nastaven�, nebo defaultn� hodnota</returns>
		public string GetSetting(string m_name, string strDefault)
		{
			if (m_settingstable.ContainsKey(m_name))
			{
				return (string)m_settingstable[m_name];
			}
			else
			{
				SetSetting(m_name,strDefault);
				return strDefault;
			}

		}

		/// <summary>
		/// Na�te nastaven� zadan�ho jm�na
		/// </summary>
		/// <param name="m_name">jm�no nastaven�</param>
		/// <returns>hodnota nastaven�, nebo pr�zdn� �et�zec kdy� nen� nalezeno</returns>
		public string GetSetting(string m_name)
		{
			return GetSetting(m_name,"");
		}
		/// <summary>
		/// Ulo�� nastaven� zadan�ho jm�na a hodnoty
		/// </summary>
		/// <param name="m_name">jm�no nastaven�</param>
		/// <param name="m_value">hodnota nastaven�</param>
		public void SetSetting(string m_name, string m_value)
		{
			if(m_settingstable.ContainsKey(m_name))
			{
				m_settingstable[m_name] = m_value;
			}
			else
			{
				m_settingstable.Add(m_name,m_value);
			}
		}

		/// <summary>
		/// Ulo�� �et�zec zadan�ho jm�na a hodnoty
		/// </summary>
		/// <param name="m_name">jm�no nastaven�</param>
		/// <param name="m_value">hodnota nastaven�</param>
		public void SetString(string m_name, string m_value)
		{
			SetSetting(m_name, m_value);
		}

		/// <summary>
		/// Ulo�� ��slo zadan�ho jm�na a hodnoty
		/// </summary>
		/// <param name="m_name">jm�no nastaven�</param>
		/// <param name="m_value">hodnota nastaven�</param>
		public void SetInt(string m_name, int m_value)
		{
			SetSetting(m_name, m_value.ToString());
		}

		/// <summary>
		/// Ulo�� hodnotu ano/ne zadan�ho jm�na a hodnoty
		/// </summary>
		/// <param name="m_name">jm�no nastaven�</param>
		/// <param name="m_value">hodnota nastaven�</param>
		public void SetBool(string m_name, bool m_value)
		{
			SetSetting(m_name, m_value.ToString());
		}

		/// <summary>
		/// Na�te �et�zec z nastaven�
		/// </summary>
		/// <param name="m_name">jm�no nastaven�</param>
		/// <param name="strDefault">defaultn� hodnota</param>
		/// <returns>hodnota nebo defaultn� hodnota</returns>
		public string GetString(string m_name, string strDefault)
		{
			return GetSetting(m_name,strDefault);
		}

		/// <summary>
		/// Na�te ��slo z nastaven�
		/// </summary>
		/// <param name="m_name">jm�no nastaven�</param>
		/// <param name="intDefault">defaultn� hodnota</param>
		/// <returns>hodnota nebo defaultn� hodnota</returns>
		public int GetInt(string m_name, int intDefault)
		{
			string str = GetSetting(m_name,intDefault.ToString());
			int r = intDefault;
			try
			{
				r = Convert.ToInt32(str);
			}
			catch
			{
				r = intDefault;
			}
			return r;
		}

		/// <summary>
		/// Na�te ��slo z nastaven�
		/// </summary>
		/// <param name="m_name">jm�no nastaven�</param>
		/// <returns>hodnota</returns>
		public int GetInt(string m_name)
		{
			return GetInt(m_name,0);
		}

		/// <summary>
		/// Na�te hodnotu ano/ne z nastaven�
		/// </summary>
		/// <param name="m_name">jm�no nastaven�</param>
		/// <param name="boolDefault">defaultn� hodnota</param>
		/// <returns>hodnota nebo defaultn� hodnota</returns>
		public bool GetBool(string m_name, bool boolDefault)
		{
			string str = GetSetting(m_name,boolDefault.ToString());
			bool r = boolDefault;
			try
			{
				r = Convert.ToBoolean(str);
			}
			catch
			{
				r = boolDefault;
			}
			return r;
		}

		/// <summary>
		/// Na�te hodnotu ano/ne z nastaven�
		/// </summary>
		/// <param name="m_name">jm�no nastaven�</param>
		/// <returns>hodnota</returns>
		public bool GetBool(string m_name)
		{
			return GetBool(m_name,true);
		}
	}
}
