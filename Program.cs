using System.Diagnostics;
using System.IO;

namespace CMDRunner
{
    class Runner
    {

        /*
            PARSER ROUTINE
              - check amount of args
              - parse first arg (command)
                - cmd if it starts with '-', macro if it doesn't
              - if unused, cut trailing args
        
            COMMAND STRUCTURE
              - register -> run -register <macro> <path-to-exe>
              - remove -> run -remove <macro>
              - help -> run -help
              - list -> run -list [MACRO - PROGRAM - PATH]

            ARGUMENT STRUCTURE
              cmd: [0] command, [1] macro, [2] path
              macro: [0] macro

            CONFIG FILE STRUCTURE
              <macro>,<path-to-exe>; ![NO CRLF]!
        */

        static int numArgs;
        static string cmd, macro, path;

        

        static string configPath = "C:\\Users\\" + Environment.UserName + "\\Documents\\cmdrunner-config\\";
        static string configFilePath = configPath + "config.yrun.cfg";
        // C:\Users\Benja\Documents\cmdrunner-config

        public static void Main(string[] args)
        {

            firstTimeSetup();

            try
            {
                numArgs = args.Length;
                if(numArgs > 0) parseInput(args);
            }
            catch(Exception ex)
            {
                error(ex.ToString());
            }
        }

        static void firstTimeSetup()
        {
            if(!Directory.Exists(configPath) && !File.Exists(configFilePath))
            {
                Directory.CreateDirectory(configPath);
                File.Create(configFilePath);
            }

            File.SetAttributes(configFilePath, 
                        (new FileInfo(configFilePath)).Attributes | FileAttributes.Normal);
        }

        static void parseInput(string[] _args)
        {
            if (_args[0][0] == '-') 
            {
                cmd = _args[0].Substring(1);
            
                switch(cmd)
                {
                    case "register":
                        registerProgram(_args[1], _args[2]);
                        break;

                    case "remove":
                        removeProgram(_args[1]);
                        break;

                    case "help":
                        showHelp();
                        break;

                    case "list":
                        showList();
                        break;


                    default:
                        error("Invalid command. \"run -help\" for help");
                        break;
                }
            }

            else
            {
                macro = _args[0];
                runProgram(macro);
            }
        }

        static void runProgram(string _macro)
        {
            StreamReader configReader = new StreamReader(configFilePath);
            string config;

            config = configReader.ReadLine();
            string[] macros = config.Split(';');

            for (int i = 0; i < macros.Length; i++)
            {
                string[] splitMacro = macros[i].Split(',');
                if (splitMacro[0] == _macro)
                {
                    Process.Start(splitMacro[1]);
                    break;
                }
            }
            configReader.Close();
        }

        static void registerProgram(string _macro, string _path)
        {
            StreamWriter configWriter = new StreamWriter(configFilePath, true);
            configWriter.Write(_macro + "," + _path + ";");
            configWriter.Close();
        }

        static void removeProgram(string _macro)
        {
            StreamReader configReader = new StreamReader(configFilePath);
            string config;

            config = configReader.ReadLine();
            string[] macros = config.Split(';');

            for (int i = 0; i < macros.Length; i++)
            {
                string[] splitMacro = macros[i].Split(',');
                if (splitMacro[0] == _macro)
                {
                    macros[i] = "";
                    break;
                }
            }
            configReader.Close();

            StreamWriter configWriter = new StreamWriter(configFilePath);
            for (int i = 0; i < macros.Length; i++)
            {
                configWriter.Write(macros[i] + ";");
            }
            configWriter.Close();
        }

        static void showHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Usage: run [COMMAND] ([MACRO] [PATH])");
            Console.WriteLine("Commands:");
            Console.WriteLine("\t-register <macro> <path-to-exe> \t| register a new program");
            Console.WriteLine("\t-remove <macro> \t\t\t| remove a program");
            Console.WriteLine("\t-help \t\t\t\t\t| show this help");
            Console.WriteLine("\t-list \t\t\t\t\t| list all configured macros");
            Console.WriteLine();
        }

        static void showList()
        {
            // list all macros

            Console.WriteLine();

            StreamReader configReader = new StreamReader(configFilePath);
            string config;

            config = configReader.ReadLine();
            string[] macros = config.Split(';');

            for (int i = 0; i < macros.Length; i++)
            {
                try {
                string[] splitMacro = macros[i].Split(',');
                Console.WriteLine("\t" + splitMacro[0] + " - " + splitMacro[1]);
                } catch {}
            }
        }

        static void error(string exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exception);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
