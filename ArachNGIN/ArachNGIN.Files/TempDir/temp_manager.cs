using System;
using System.IO;
using System.Windows.Forms;
using ArachNGIN.Files.Strings;

namespace ArachNGIN.Files
{
	/// <summary>
	/// T��da pro obstar�v�n� temp adres��e a podobn� v�ci
	/// </summary>
	public class TempManager
	{

		private string s_AppDir;
		private string s_AppTempDir;
		private string s_TempDir;

		/// <summary>
		/// property vracej�c� adres�� aplikace
		/// </summary>
		public string AppDir
		{
			get
			{
				return s_AppDir;
			}
		}
		
		/// <summary>
		/// property vracej�c� adres�� aplikace v tempu
		/// (nap�. c:\windows\temp\aplikace_035521152515)
		/// posledn� ��st je guid (aby se 2 instance aplikace/t�to t��dy
		/// neh�daly o 1 adres��)
		/// </summary>
		public string AppTempDir
		{
			get
			{
				return s_AppTempDir;
			}
		}
		
		/// <summary>
		/// property vracej�c� tempov� adres��
		/// (nap�. c:\windows\temp)
		/// </summary>
		public string TempDir
		{
			get
			{
				return s_TempDir;
			}
		}
		
		/// <summary>
		/// Konstruktor t��dy
		/// vytvo�� adres�� v tempu
		/// </summary>
		/// <returns>instance t��dy</returns>
		public TempManager()
		{
			Guid g_guid = Guid.NewGuid();
			s_TempDir = StringUtils.strAddSlash(Environment.GetEnvironmentVariable("TEMP"));
			string str = Path.GetFileName(Application.ExecutablePath).ToLower();
			str = str.Replace(@".",@"_");
			str = str + @"_" + g_guid.ToString();
            s_AppTempDir = StringUtils.strAddSlash(s_TempDir + str.ToLower());
            s_AppDir = StringUtils.strAddSlash(Path.GetDirectoryName(Application.ExecutablePath));
			Directory.CreateDirectory(s_AppTempDir);
		}

		/// <summary>
		/// Sma�e adres�� v tempu
		/// </summary>
		public void Close()
		{
			Directory.Delete(s_AppTempDir, true);
		}
		
		/// <summary>
		/// Destruktor t��dy vyvol� fci Close(); a potla�uje v�jimky
		/// </summary>
		~TempManager()
		{
			try
			{
				Close();
			}
			catch {}
		}
	}
}
