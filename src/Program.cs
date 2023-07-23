using System;
using System.IO;
using System.Text;

namespace TokenCli
{
    class Program
    {
        static void showHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("tokens.exe <filename>");
        }

        static void Main(string[] args)
        {
            foreach (string item in args)
            {
                Console.WriteLine(item);
            }

            if (args.Length != 1)
            {
                showHelp();
                return;
            }
            if (args[0] == "-h" || args[0] == "--help")
            {
                showHelp();
                return;
            }

            FileStream? fs = null;
            string textFromFile = "#NOTHING";

            try
            {
                fs = new FileStream(args[0], FileMode.Open);
                byte[] source = new byte[fs.Length];
                fs.Read(source, 0, source.Length);
                textFromFile = Encoding.Default.GetString(source);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {args[0]} not found");
            }
            finally
            {
                fs?.Close();
            }

            if (textFromFile == "#NOTHING")
            {
                return;
            }

            int tokenCount = TokenCounter.CountTokens(textFromFile);

            Console.Write("Token count: ");
            if (tokenCount < 730)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            } else if (tokenCount <= 1024){
                Console.ForegroundColor = ConsoleColor.Yellow;
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine(tokenCount);
            Console.ResetColor();
        }
    }
}
