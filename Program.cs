using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSystem
{
    public class Handler
    {
        /// <summary>
        /// List of default console comands. Cannot be modified, but can be overrided by custom commands
        /// </summary>
        public static readonly Command[] DefaultCommands = {
            new Command("echo", echo),
            new Command("cvarlist", cvarlist),
            new Command("cmdlist", cmdlist),
            new Command("exit", exit),
            new Command("quit", exit),
            new Command("bindlist", bindlist),
        };

        #region Default Command Calls 
        static void echo(string argument)
        {
            Console.WriteLine(argument);
        }

        static void cmdlist(string argument)
        {
            Console.WriteLine($"CMDX commands ({DefaultCommands.Length}):");
            foreach (Command cmd in DefaultCommands)
            {
                Console.WriteLine("\t" + cmd.Name);
            }

            Console.WriteLine($"Software commands ({CustomCommands.Count}):");
            foreach (Command cmd in CustomCommands)
            {
                Console.WriteLine("\t" + cmd.Name);
            }
        }

        static void cvarlist(string argument)
        {
            Console.WriteLine($"Default CMDX vars ({DefaultCVars.Length}):");
            foreach (CVar cvar in DefaultCVars)
            {
                Console.WriteLine($"\t{cvar.Name} \"{cvar.Value}\"");
            }

            /*Console.WriteLine($"Software commands ({CustomCommands.Count}):");
            foreach (Command cmd in CustomCommands)
            {
                Console.WriteLine("\t" + cmd.Name);
            }*/
        }

        static void bindlist(string argument)
        {

        }

        static void exit(string argument)
        {
            Environment.Exit(0);
        }
        #endregion

        public static readonly CVar[] DefaultCVars =
        {
            new CVar("Giuseppe"),
            new CVar("Lanna")
    };

        /// <summary>
        /// List of custom commands implemented by the user.
        /// </summary>
        public static List<Command> CustomCommands = new List<Command>();
        
        /// <summary>
        /// Represents a command that can be recognized by the Handler.
        /// </summary>
        public sealed class Command : IComparable<Command>, IEquatable<Command>
        {
            public string Name;
            public string Description;
            public CommandDelegate CommandAddresss;

            public delegate void CommandDelegate(string commandline);

            public Command(string name, CommandDelegate callback)
            {
                Name = name;
                CommandAddresss = callback;
            }
            
            public bool Equals(Command c)
            {
                return Name.Equals(c.Name);
            }

            public int CompareTo(Command c)
            {
                return Name.CompareTo(c.Name);
            }
        }

        /// <summary>
        /// Beginning of console.
        /// </summary>
        /// <param name="lines"></param>
        public static void Begin(params string[] lines)
        {
            Console.WriteLine("CommandX Console Handler Version 0.0.0.1 \n");
            foreach (string line in lines)
                Handle(line);

            while (true)
            {
                string line = Console.ReadLine();
                Handle(line);
            }
        }

        /// <summary>
        /// Handle a commandline.
        /// </summary>
        /// <param name="line"></param>
        static void Handle(string line)
        {
            char[] separators = { ' ', '\t' };
            string[] separed = line.Split(separators, 2, StringSplitOptions.RemoveEmptyEntries);

            if (separed.Length == 0)
                return;

            separed[0] = separed[0].ToLower();

            //Search for custom commands
            foreach (Command command in CustomCommands)
            {
                if (separed.Length > 1)
                    command.CommandAddresss(separed[1]);
                else command.CommandAddresss(null);
                return;
            }

            //Search for default commands
            foreach (Command command in DefaultCommands)
            {
                if (command.Name == separed[0])
                {
                    if (separed.Length > 1)
                        command.CommandAddresss(separed[1]);
                    else command.CommandAddresss(null);
                    return;
                }
            }
        }

        static void cb(string commandline)
        {

        }
    }
}
