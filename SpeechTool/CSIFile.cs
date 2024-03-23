using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SpeechTool.CSI;

namespace SpeechTool
{
    public class CSIFile
    {
        public static StructCSIHeader ReadFromFile(string filePath)
        {
            StructCSIHeader data = new StructCSIHeader();

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[Marshal.SizeOf<StructCSIHeader>()];
                    int bytesRead = fs.Read(buffer, 0, buffer.Length);

                    if (bytesRead == Marshal.SizeOf<StructCSIHeader>())
                    {
                        GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                        data = Marshal.PtrToStructure<StructCSIHeader>(handle.AddrOfPinnedObject());
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
        public static void ReadCSI(string Evtfilepath)
        {
            string filePath = Evtfilepath;
            StructCSIHeader StructCSIHeaderData = ReadFromFile(filePath);
            string signatureAsString = "\"" + StructCSIHeaderData.Signature.ToString() + "\"";

            var StructCSIHeaderJson = new
            {
                Signature = signatureAsString,
                FileFormatVersion = StructCSIHeaderData.FileFormatVersion,
                CSISXLibraryMajorVersion = StructCSIHeaderData.CSISXLibraryMajorVersion,
                CSISXLibraryMinorVersion = StructCSIHeaderData.CSISXLibraryMinorVersion,
                CSISXLibraryPatchVersion = StructCSIHeaderData.CSISXLibraryPatchVersion,
                Platform = StructCSIHeaderData.Platform,
                ResolvedFlag = StructCSIHeaderData.ResolvedFlag,
                FunctionsCount = StructCSIHeaderData.FunctionsCount,
                ClassesCount = StructCSIHeaderData.ClassesCount,
                GlobalVariablesCount = StructCSIHeaderData.GlobalVariablesCount,
                SystemLevelCRC = StructCSIHeaderData.SystemLevelCRC,
                Padding = StructCSIHeaderData.Padding,
                FunctionsPointer = StructCSIHeaderData.FunctionsPointer,
                ClassesPointer = StructCSIHeaderData.ClassesPointer,
                GlobalVariablesPointer = StructCSIHeaderData.GlobalVariablesPointer,
                NextEntryPointer = StructCSIHeaderData.NextEntryPointer,
                PreviousEntryPointer = StructCSIHeaderData.PreviousEntryPointer,
            };

            string jsonString = JsonSerializer.Serialize(StructCSIHeaderJson);

            // Remove the extension from Evtfilepath
            string directoryPath = Path.GetDirectoryName(Evtfilepath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Evtfilepath);

            // Construct the new file path without the previous extension
            string jsonfile = Path.Combine(directoryPath, fileNameWithoutExtension + "_Moir" + ".json");

            // Write the JSON string to a file
            File.WriteAllText(jsonfile, jsonString);

            Console.WriteLine("JSON has been written: " + jsonfile);
        }
    }
}
