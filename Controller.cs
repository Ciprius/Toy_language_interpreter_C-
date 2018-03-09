using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions;

namespace LabCsharp
{
    public class Controller
    {
        private IRepository repo;
        private int flag;
        public Controller(IRepository repo,int flag) { this.repo = repo; this.flag = flag; }

        public ProgState onestep(ProgState prog)
        {
            MyIStack<IStmt> stk = prog.getStk();
            try { stk.isEmpty(); }
            catch(StackExceptions ) { throw new StmtExecException("The program is over!!! \n"); }

            IStmt st = stk.pop();
            if (flag == 1)
                System.Console.WriteLine(st.ToString());

            try { return st.execute(prog); }
            catch(ControllerException e) { throw new StmtExecException(e); }
        }

        public void allSteps()
        {
            ProgState prg = repo.getProg();
            try
            {
                while (true)
                {
                    onestep(prg);
                    repo.PrintlogFile();
                }
            }
            catch(StmtExecException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally { repo.CloseFile(); }
        }

        public void Print()
        {
            System.Console.WriteLine(repo.getProg().getDict().ToString());
            System.Console.WriteLine(repo.getProg().getList().ToString());
        }
    }
}
