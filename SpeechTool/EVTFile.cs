using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using static SpeechTool.EVT;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpeechTool
{
    public class EVTFile
    {
        public static StructEVTHeader ReadFromFile(string filePath)
        {
            StructEVTHeader data = new StructEVTHeader();

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[Marshal.SizeOf<StructEVTHeader>()];
                    int bytesRead = fs.Read(buffer, 0, buffer.Length);

                    if (bytesRead == Marshal.SizeOf<StructEVTHeader>())
                    {
                        GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                        data = Marshal.PtrToStructure<StructEVTHeader>(handle.AddrOfPinnedObject());
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
        public static void ReadEvt(string Evtfilepath)
        {
            string filePath = Evtfilepath;
            StructEVTHeader StructEVTHeaderData = ReadFromFile(filePath);

            var StructEVTHeaderJson = new
            {
                majorRev = StructEVTHeaderData.majorRev,
                minorRev = StructEVTHeaderData.minorRev,
                release = StructEVTHeaderData.release,
                prerelease = StructEVTHeaderData.prerelease,
                csisOffset = StructEVTHeaderData.csisOffset,
                projectId = StructEVTHeaderData.projectId,
                datId = StructEVTHeaderData.datId,
                bolloRev = StructEVTHeaderData.bolloRev,
                csisResolved = StructEVTHeaderData.csisResolved,
                saveIncrement = StructEVTHeaderData.saveIncrement,
                generateID = StructEVTHeaderData.generateID,
                numEvents = StructEVTHeaderData.numEvents,
                numGlobalMatchParms = StructEVTHeaderData.numGlobalMatchParms,
                pad1 = StructEVTHeaderData.pad1,
                eventFilterLength = StructEVTHeaderData.eventFilterLength,
                eventFilterPriority = StructEVTHeaderData.eventFilterPriority,
            };

            string jsonString = JsonSerializer.Serialize(StructEVTHeaderJson);

            // Remove the extension from Evtfilepath
            string directoryPath = Path.GetDirectoryName(Evtfilepath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Evtfilepath);

            // Construct the new file path without the previous extension
            string jsonfile = Path.Combine(directoryPath, fileNameWithoutExtension + "_Event" + ".json");

            // Write the JSON string to a file
            File.WriteAllText(jsonfile, jsonString);

            Console.WriteLine("JSON has been written: " + jsonfile);
        }
    }
}
