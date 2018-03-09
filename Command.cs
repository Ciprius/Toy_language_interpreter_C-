using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCsharp
{
    public abstract class Command
    {
        private string key, description;
        public Command(string key, string description) { this.key = key; this.description = description; }
        public abstract void execute();
        public string getKey() { return this.key; }
        public string getDescription() { return this.description; }
    }

    public class ExitCommand : Command
    {
        public ExitCommand(string key, string desc) : base(key,desc){ }
       
        public override void execute() { Environment.Exit(0); }
    }

    public class RunCommand : Command
    {
        private Controller ctrl;
        public RunCommand(string key, string desc, Controller ctrl) :base(key,desc ) { this.ctrl = ctrl; }
        public override void execute() { ctrl.allSteps(); }
    }

    public class TextMENU
    {
        private Dictionary<string, Command> menu;
        public TextMENU() { menu = new Dictionary<string, Command>(); }
        public void add(Command com) { menu.Add(com.getKey(), com); }

        private void PrintMenu()
        {
            foreach (var item in menu)
            {
                Console.WriteLine(item.Key+" "+item.Value.getDescription());
            }
        }

        public void show()
        {
            while (true)
            {
                Console.WriteLine("You can run a command ONLY once!!! \n");
                PrintMenu();
                Console.WriteLine("Give the option:");
                string key = Console.ReadLine();
                Command com = menu[key];
                if (com == null)
                {
                    Console.WriteLine("Invalid option!!");
                    continue;
                }
                com.execute();
            }
        }
    }
    }
