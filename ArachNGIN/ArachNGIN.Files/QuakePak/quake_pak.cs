using System;
using System.IO;
using ArachNGIN.Files.Streams;
using System.Collections.Specialized;

namespace ArachNGIN.Files
{
	/// <summary>
	/// T��da na �ten� z PAK soubor� Quaka
	/// </summary>
	public class QuakePAK
	{
		private struct T_PakFAT
		{
			public string FileName;
			public int FileStart;
			public int FileLength;
		}

		private T_PakFAT[] PakFAT;
		private FileStream PakStream;
		private BinaryReader PakReader;
		private int p_filecount = 0;
		private int p_fatstart = 0;
		private static char[] PakID = new char[4] { 'P', 'A', 'C', 'K' };
		
		/// <summary>
		/// Seznam soubor� v PAKu
		/// </summary>
		public StringCollection PakFileList = new StringCollection();

		/// <summary>
		/// Konstruktor - otev�e pak soubor a na�te z n�j hlavi�ku.
		/// </summary>
		/// <param name="strFileName">jm�no pak souboru</param>
		public QuakePAK(string strFileName, bool bAllowWrite)
		{
			FileInfo info = new FileInfo(strFileName);
			if (info.Exists == false)
			{
				throw new FileNotFoundException("Can''t open "+strFileName);
			}
			// soubor existuje
			if (bAllowWrite)
			{
				PakStream = new FileStream(strFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			}
			else
			{
				PakStream = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			}
			PakReader = new BinaryReader(PakStream,System.Text.Encoding.GetEncoding("Windows-1250"));
			//
			if (!ReadHeader())
			{
				PakStream.Close();
				throw new FileNotFoundException("File "+strFileName+" has unsupported format");
			}

		}

		/// <summary>
		/// Neofici�ln� destruktor
		/// </summary>
		public void Close()
		{
			PakReader.Close();
			PakStream.Close();
		}

		/// <summary>
		/// Ofici�ln� Destruktor
		/// </summary>
		~QuakePAK()
		{
			try
			{ 
				Close();
			}
			catch
			{
				//
			}
		}

		private bool ReadHeader()
		{
			string p_header;
			PakStream.Position = 0;
			p_header = StreamHandling.PCharToString(PakReader.ReadChars(PakID.Length));
			if (p_header == StreamHandling.PCharToString(PakID))
			{
				// hned za hlavickou je pozice zacatku fatky
				p_fatstart = PakReader.ReadInt32();
				// a pak je pocet souboru * 64
				p_filecount = PakReader.ReadInt32() / 64;
				//
				// presuneme se na pozici fatky a nacteme ji
				PakStream.Position = p_fatstart;
				PakFAT = new T_PakFAT[p_filecount];
				// vymazneme filelist
				PakFileList.Clear();
				for (int i = 0; i < p_filecount; i++)
				{
					// my radi lowercase. v tom se lip hleda ;-)
					string sfile = StreamHandling.PCharToString(PakReader.ReadChars(56)).ToLower();
					sfile = sfile.Replace("/","\\"); // unixovy lomitka my neradi.
					// pridame soubor do filelistu a do PakFATky
					PakFileList.Add(sfile);
					PakFAT[i].FileName = sfile;
					PakFAT[i].FileStart = PakReader.ReadInt32();
					PakFAT[i].FileLength = PakReader.ReadInt32();
				}
				PakStream.Position = 0;
				//
				return true;
			}
			else
			{
				PakStream.Position = 0;
				return false;
			}
		}

		/// <summary>
		/// Zkontroluje, jestli je soubor zadan�ho jm�na v pak souboru
		/// </summary>
		/// <param name="strFileInPak">jm�no hledan�ho souboru</param>
		/// <returns>je/nen�</returns>
		public bool PakFileExists(string strFileInPak)
		{
			return GetFileIndex(strFileInPak) != -1;
		}

		private int GetFileIndex(string strFileInPak)
		{
			for (int i = 0; i < PakFAT.Length; i++)
			{
				if (PakFAT[i].FileName.ToLower() == strFileInPak.ToLower())
				{
					// soubor nalezen, vracime jeho cislo
					return i;
				}
			}
			// soubor nenalezen
			return -1;
		}

		/// <summary>
		/// Rozbal� soubor z paku do proudu
		/// </summary>
		/// <param name="strFileInPak">jm�no souboru v paku</param>
		/// <param name="s_Output">v�stupn� proud</param>
		public void ExtractStream(string strFileInPak, Stream s_Output)
		{
			int f_index = GetFileIndex(strFileInPak);
			if (f_index == -1) return; // soubor v paku neni, tudiz konec.
			s_Output.SetLength(0);
			PakStream.Seek((long)PakFAT[f_index].FileStart, SeekOrigin.Begin);
			Streams.StreamHandling.StreamCopy(PakStream, s_Output, (long)PakFAT[f_index].FileLength);
		}

		/// <summary>
		/// Rozbal� soubor z paku na disk
		/// </summary>
		/// <param name="strFileInPak">jm�no souboru v paku</param>
		/// <param name="strOutputFile">cesta k v�stupn�mu souboru</param>
		public void ExtractFile(string strFileInPak, string strOutputFile)
		{
			Stream f_output = new FileStream(strOutputFile,FileMode.OpenOrCreate,FileAccess.ReadWrite, FileShare.ReadWrite);
			ExtractStream(strFileInPak,f_output);
			f_output.Close();
		}

		public static bool CreateNewPak(string strFileName)
		{
			bool result = false;
			FileStream FS = new FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			FS.Position = 0;
			BinaryWriter BW = new BinaryWriter(FS, System.Text.Encoding.GetEncoding("Windows-1250"));
			BW.Write(PakID);
			Int32 p_fatstart = PakID.Length;
			p_fatstart += sizeof(Int32); // offset
			p_fatstart += sizeof(Int32); // length
			Int32 p_filecount=0;
			BW.Write(p_fatstart);
			BW.Write(p_filecount);
			BW.Close();
			FS.Close();
			return result;
		}

		private char[] PrepFileNameWrite(string filename)
		{
			char[] result = new char[56];
			// prepsat lomitka
			filename = filename.Replace("\\", "/");
			// kdyz to nekdo prepisk s nazvem souboru tak ho seriznout :-)
			if (filename.Length > 56)
			{
				filename.CopyTo(0, result, 0, 55);
			}
			else
			{
				filename.CopyTo(0, result, 0, filename.Length);
			}
			return result;
		}

		private void WriteFAT()
		{
			// naseekovat startovn� pozici fatky
			PakStream.Seek(p_fatstart, SeekOrigin.Begin);
			BinaryWriter bw = new BinaryWriter(PakStream, System.Text.Encoding.GetEncoding("Windows-1250"));
			foreach (T_PakFAT item in PakFAT)
			{
				// nazev souboru
				bw.Write(PrepFileNameWrite(item.FileName));
				bw.Write((int)item.FileStart);
				bw.Write((int)item.FileLength);
			}
			// naseekovat na pocet souboru a zapsat
			PakStream.Seek(PakID.Length + sizeof(Int32), SeekOrigin.Begin);
			bw.Write(p_filecount * 64); // krat 64. z neznamych duvodu
		   // bw.Close();
		}

		/// <summary>
		/// P�id� proud do PAKu
		/// </summary>
		/// <param name="stream">proud</param>
		/// <param name="pakFileName">jm�no souboru v PAKu</param>
        ///// <param name="writeFAT">m� se zapsat fatka? Pokud to nen� posledn� p�idan� soubor, tak ur�it� JO!</param>
		/// <returns>podle �sp�nosti bu� true nebo false</returns>
		public bool AddStream(Stream stream, string pakFileName, bool writeFAT /*=true*/)
		{
			// soubor uz existuje --> dal se nebavime!
			if (PakFileExists(pakFileName)) return false;
			// novy soubor zapisujeme na pozici fatky
			PakStream.Seek(p_fatstart, SeekOrigin.Begin);
			// vytvorit novou fatku a zapsat do ni novy soubor
			T_PakFAT[] OldPakFAT = PakFAT;
			p_filecount = OldPakFAT.Length + 1;
			PakFAT = new T_PakFAT[p_filecount];
			OldPakFAT.CopyTo(PakFAT, 0);
			PakFAT[p_filecount - 1].FileName = pakFileName;
			PakFAT[p_filecount - 1].FileLength = (int)stream.Length;
			PakFAT[p_filecount - 1].FileStart = p_fatstart;
			// zapsat soubor
			StreamHandling.StreamCopy(stream, PakStream, 0, PakStream.Position);
            //StreamHandling.StreamCopyAsync(stream, PakStream);
			// po dokonceni zapisovani zjistit pozici streamu
			p_fatstart = (int)PakStream.Position;
			//bw.Close();
			// zapsat fatku
            if (writeFAT)
            {
                PakStream.Seek(PakID.Length, SeekOrigin.Begin);
                BinaryWriter bw = new BinaryWriter(PakStream);
                // zapsat startovni pozici fatky
                bw.Write(p_fatstart);
                WriteFAT();
            }
			return true;
		}

		/// <summary>
		/// P�id� soubor do paku
		/// </summary>
		/// <param name="FileName">n�zev souboru (nap�. c:\windows\win.ini)</param>
		/// <param name="pakFileName">n�zev souboru v paku (nap� ini/win.ini)</param>
        /// <param name="writeFAT">m� se zapsat fatka? Pokud to nen� posledn� p�idan� soubor, tak ur�it� JO!</param>
		/// <returns>podle �sp�nosti bu� true nebo false</returns>
		public bool AddFile(string FileName, string pakFileName, bool writeFAT = true)
		{
			if (!File.Exists(FileName)) return false;
            bool result = false;
            try
            {
                Stream fstream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                result = AddStream(fstream, pakFileName, writeFAT);
                fstream.Close();
            }
            catch
            {
                result = false;
            }
			return result;
		}
        /// <summary>
        /// P�id� soubor do paku
        /// </summary>
        /// <param name="FileName">n�zev souboru (nap�. c:\windows\win.ini)</param>
        /// <param name="pakFileName">n�zev souboru v paku (nap� ini/win.ini)</param>
        /// <returns>podle �sp�nosti bu� true nebo false</returns>
        public bool AddFile(string FileName, string pakFileName)
        {
            return AddFile(FileName, pakFileName, true);
        }
	}
}
 

 
