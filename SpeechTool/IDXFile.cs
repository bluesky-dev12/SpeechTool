using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static SpeechTool.IDX;

namespace SpeechTool
{
    public interface IDXFile
    {
        public static StructIDXHeader ReadFromFile(string filePath)
        {
            StructIDXHeader data = new StructIDXHeader();

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[Marshal.SizeOf<StructIDXHeader>()];
                    int bytesRead = fs.Read(buffer, 0, buffer.Length);

                    if (bytesRead == Marshal.SizeOf<StructIDXHeader>())
                    {
                        GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                        data = Marshal.PtrToStructure<StructIDXHeader>(handle.AddrOfPinnedObject());
                        handle.Free();
                    }
                    else
                    {
                        Console.WriteLine("Error: Incomplete read from file.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading from file: " + ex.Message);
            }

            return data;
        }
        public static void ReadIDX(string IDXfilepath)
        {
            string filePath = IDXfilepath;
            StructIDXHeader StructIDXHeaderData = ReadFromFile(filePath);

            var StructIDXHeaderJson = new
            {
                Signature = StructIDXHeaderData.Signature,
                TotalFileSize = StructIDXHeaderData.TotalFileSize,
                Version = StructIDXHeaderData.Version,
                ArchivesCount = StructIDXHeaderData.ArchivesCount,
                FilesCount = StructIDXHeaderData.FilesCount,
                Unknown = StructIDXHeaderData.Unknown,
                OffsetToFileDescriptors = StructIDXHeaderData.OffsetToFileDescriptors,
                OffsetToNamesTable = StructIDXHeaderData.OffsetToNamesTable,
            };

            string jsonString = JsonSerializer.Serialize(StructIDXHeaderJson);

            // Remove the extension from Evtfilepath
            string directoryPath = Path.GetDirectoryName(IDXfilepath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(IDXfilepath);

            // Construct the new file path without the previous extension
            string jsonfile = Path.Combine(directoryPath, fileNameWithoutExtension + "_indextable" + ".json");

            // Write the JSON string to a file
            File.WriteAllText(jsonfile, jsonString);

            Console.WriteLine("JSON has been written: " + jsonfile);
        }
    }
}
