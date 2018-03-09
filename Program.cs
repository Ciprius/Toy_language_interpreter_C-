using System;
using System.IO;

namespace LabCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            MyIDictionary<string, int> dict = new MyDictionary<string, int>();
            MyIList<int> list = new MyList<int>();
            MyIStack<IStmt> stk = new MyStack<IStmt>();
            MyIFileTable<Tuple> file = new MyFileTable<Tuple>();
            IStmt st = new CompStmt(new AssignStmt("v", new ConstExp(2)),new CompStmt(
                new AssignStmt("v", new ConstExp(4)), new CompStmt(new AssignStmt("a", new ConstExp(40)), new PrintStmt(new VarExp("v")))));
            ProgState prog = new ProgState(stk, dict, list, file,st);
            IRepository repo = null;
            try
            {
                repo=new Repository(prog, "D:\\Faculta\\OOP projects\\LabCsharp\\LabCsharp\\state1.txt");
            }
            catch (IOException e) { Console.WriteLine(e.ToString()); }
            Controller cont = new Controller(repo, 1);
            //cont.allSteps();

            MyIDictionary<string, int> dict2 = new MyDictionary<string, int>();
            MyIList<int> list2 = new MyList<int>();
            MyIStack<IStmt> stk2 = new MyStack<IStmt>();
            MyIFileTable<Tuple> file2 = new MyFileTable<Tuple>();
            IStmt st2 = new CompStmt(new OpenFile("f", "D:\\Faculta\\OOP projects\\LabCsharp\\LabCsharp\\Log1.txt"),
                new CompStmt(new ReadFile(new VarExp("f"), "g"),
                        new CompStmt(new PrintStmt(new VarExp("g")), new CloseFile(new VarExp("f")))));

            ProgState state2 = new ProgState(stk2, dict2, list2, file2,st2);
            IRepository repo2 = null;
            try
            {
                repo2 = new Repository(state2, "D:\\Faculta\\OOP projects\\LabCsharp\\LabCsharp\\state2.txt");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            Controller cont2 = new Controller(repo2, 1);

            TextMENU text = new TextMENU();
            text.add(new ExitCommand("0","EXIT"));
            text.add(new RunCommand("1", st.ToString(), cont));
            text.add(new RunCommand("2", st2.ToString(), cont2));
            text.show();

            Console.ReadLine();
        }
    }
}
