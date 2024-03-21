using SpeechTool;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Warfare Territory Login Server";
        Console.WriteLine("===============================================================================");
        Console.WriteLine("|                     SpeechTool Extractor and replacer                       |");
        Console.WriteLine("===============================================================================");

        if (args.Length == 2 && args[0] == "-extractsamples")
        {
            if (args[1] == null)
            {
                Console.WriteLine($"The big file don't exist.");
                return;
            }
            string fileName = args[1];
            Console.WriteLine($"Extracting samples from {fileName}...");
            Extraction.ExtractSamples(fileName);
        }
        else if (args.Length == 3 && args[0] == "-recompilebig")
        {
            string fileName = args[2];
            string SampleFolder = args[1];
            Console.WriteLine($"Extracting samples from {SampleFolder}.Big...");
            Extraction.RecompileBigFile(SampleFolder, fileName);
        }
        else if (args.Length == 1 && args[0] == "-h")
        {
            Console.WriteLine("Usage: SpeechTool.exe -extractsamples BigFile.big");
            Console.WriteLine("Usage: SpeechTool.exe -recompilebig inputfolder BigFile.big .");
        }
        else
        {
            Console.WriteLine("Invalid command. Usage: SpeechTool.exe -h for help.");
        }
    }
}
