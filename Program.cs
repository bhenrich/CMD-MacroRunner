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

        

        static string configPath = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\cmd-run_yunii\\";
        static string configFilePath = configPath + "config.yrun.cfg";
        // C:\Users\Benja\AppData\Local\cmd-run_yunii

        public static void Main(string[] args)
        {

            firstTimeSetup();

            try
            {
                numArgs = args.Length;
                if(numArgs > 0) parseInput(args);
            }
            catch
            {
                error("Argument Exception.");
            }
        }

        static void firstTimeSetup()
        {
            if(!Directory.Exists(configPath) && !File.Exists(configFilePath))
            {
                Directory.CreateDirectory(configFilePath);
                File.Create(configFilePath);
            }
        }

        static void parseInput(string[] _args)
        {
            if (_args[0][0] == '-') cmd = _args[0].Substring(1);
            
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

        static void runProgram(string _macro)
        {

        }

        static void registerProgram(string _macro, string _path)
        {

        }

        static void removeProgram(string _macro)
        {

        }

        static void showHelp()
        {

        }

        static void showList()
        {

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