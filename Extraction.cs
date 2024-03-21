using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTool
{
    public class Extraction
    {
        static bool Match(string pattern)
        {
            if (pattern == "SCHl")
                return true;
            return false;
        }

        public static void ExtractSamples(string BigFile)
        {
            // Create a folder with the name of the big file
            string folderName = Path.GetFileNameWithoutExtension(BigFile);
            Directory.CreateDirectory(folderName);

            using (FileStream input = new FileStream(BigFile, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[4];
                int count = 0;
                bool matching = false;
                FileStream output = null;

                while (input.Read(buffer, 0, buffer.Length) > 0)
                {
                    string bufString = System.Text.Encoding.Default.GetString(buffer);

                    if (Match(bufString))
                    {
                        if (matching)
                            output.Close();

                        count++;
                        string outFile = Path.Combine(folderName, $"ASF{count:D5}.ASF");

                        Console.WriteLine($"Extracting {outFile} from {Path.GetFileName(BigFile)}");
                        output = new FileStream(outFile, FileMode.Create, FileAccess.Write);

                        matching = true;
                    }

                    if (matching)
                    {
                        output.Write(buffer, 0, buffer.Length);
                    }
                }

                output?.Close();
            }
        }
        // Function to recompile the big file from a collection of ASF files
        public static void RecompileBigFile(string folderPath, string outputFile)
        {
            // Get all ASF files in the specified folder
            string[] asfFiles = Directory.GetFiles(folderPath, "*.ASF");

            // Create or overwrite the output file
            using (FileStream output = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            {
                foreach (string asfFile in asfFiles)
                {
                    Console.WriteLine(asfFile);
                    // Read the content of each ASF file and write it to the output file
                    byte[] fileContent = File.ReadAllBytes(asfFile);
                    output.Write(fileContent, 0, fileContent.Length);
                }
            }

            Console.WriteLine($"Recompiled big file saved as: {outputFile}");
        }
    }
}

