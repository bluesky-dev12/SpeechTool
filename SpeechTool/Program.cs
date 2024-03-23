using SpeechTool;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Warfare Territory Login Server";
        Console.WriteLine("===============================================================================");
        Console.WriteLine("|                     SpeechTool Extractor and replacer                       |");
        Console.WriteLine("===============================================================================");

        if (args.Length == 3 && args[0] == "-Decompile" && args[1] == "-EVT")
        {
            if (args[2] == null)
            {
                Console.WriteLine($"The big file don't exist.");
                return;
            }
            if (Path.GetExtension(args[2]) != ".EVT")
            {
                Console.WriteLine("Not a evt file.");
                return;
            }
            string fileName = args[2];

            Console.WriteLine($"Decompiling {fileName}...");
            EVTFile.ReadEvt(fileName);
        }
        if (args.Length == 3 && args[0] == "-Decompile" && args[1] == "-IDX")
        {
            if (args[2] == null)
            {
                Console.WriteLine($"The big file don't exist.");
                return;
            }
            if (Path.GetExtension(args[2]) != ".IDX")
            {
                Console.WriteLine("Not a idx file.");
                return;
            }
            string fileName = args[2];
            Console.WriteLine($"Decompiling {fileName}...");
            IDXFile.ReadIDX(fileName);
        }
        if (args.Length == 3 && args[0] == "-Decompile" && args[1] == "-CSI")
        {
            if (args[2] == null)
            {
                Console.WriteLine($"The big file don't exist.");
                return;
            }
           
            if (Path.GetExtension(args[2]) != ".CSI")
            {
                Console.WriteLine("Not a csi file.");
                return;
            }
            string fileName = args[2];
            Console.WriteLine($"Decompiling {fileName}...");
            CSIFile.ReadCSI(fileName);
        }
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
            Console.WriteLine("This tool extract and replaces speech sounds.");
            Console.WriteLine("Created by BlueSkyWestSide.");
            Console.WriteLine("Usage: SpeechTool.exe -extractsamples BigFile.big. //Decompiles samples from speeches.");
            Console.WriteLine("Usage: SpeechTool.exe -recompilebig inputfolder BigFile.big. //Recompiles the big file.");
            Console.WriteLine("Usage: SpeechTool.exe -Decompile -IDX //Decompiles IDX files");
            Console.WriteLine("Usage: SpeechTool.exe -Decompile -CSI //Decompiles CSI files");
            Console.WriteLine("Usage: SpeechTool.exe -Decompile -EVT //Decompiles EVT files");
        }
        else if (args.Length == 1 && args[0] != "-h")
        {
            Console.WriteLine("Invalid command. Usage: SpeechTool.exe -h for help.");
        }
    }
}
