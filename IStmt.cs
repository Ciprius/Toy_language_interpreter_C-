using System;
using System.Collections.Generic;
using System.IO;
using Exceptions;

namespace LabCsharp
{
    public interface IStmt
    {
        ProgState execute(ProgState prog);
        string ToString();
    }

    public class CompStmt : IStmt
    {
        private IStmt st1, st2;

        public CompStmt(IStmt st1,IStmt st2)
        {
            this.st1 = st1;
            this.st2 = st2;
        }

        public ProgState execute(ProgState prog)
        {
            MyIStack<IStmt> stk=prog.getStk();
            stk.push(st2);
            stk.push(st1);
            return prog;
        }

        public override string ToString() {return "("+st1.ToString()+";"+st2.ToString()+")";}
    }

    public class AssignStmt: IStmt
    {
        private string id;
        private Exp exp;

        public AssignStmt(string id, Exp exp)
        {
            this.id = id;
            this.exp = exp;
        }

        public ProgState execute(ProgState prog)
        {
            MyIDictionary<string, int> dict = prog.getDict();

            try
            {
                if (dict.isDefined(this.id) == 1)
                    dict.update(id, exp.eval(dict));
                else
                    dict.add(id,exp.eval(dict));
                return prog;
            }
            catch(StmtExceptions e)
            {
                throw new ControllerException(e);
            }
            catch (DivizionByZeroException e)
            {
                throw new ControllerException(e);
            }
        }

        public override string ToString() { return id + "=" + exp.ToString(); }
    }

    public class IfStmt : IStmt
    {
        private Exp ex;
        private IStmt thenS;
        private IStmt elseS;

        public IfStmt(Exp ex, IStmt thenS,IStmt elseS)
        {
            this.ex = ex;
            this.thenS = thenS;
            this.elseS = elseS;
        }

        public ProgState execute(ProgState prog)
        {
            MyIStack<IStmt> stk=prog.getStk();
            MyIDictionary<string, int> dict=prog.getDict();
            
            try {
                if (ex.eval(dict) > 0)
                    stk.push(thenS);
                else
                    stk.push(elseS);
            } catch (StmtExceptions e) {
                throw new ControllerException(e);
            }
        return prog;
        }

        public override string ToString()
        {
            return "IF(" + ex.ToString() + ")THEN(" + thenS.ToString() + ")ELSE(" + elseS.ToString() + ")";
        }
    }

    public class PrintStmt : IStmt
    {
        private Exp ex;

        public PrintStmt(Exp ex) { this.ex = ex; }

        public ProgState execute(ProgState prog)
        {
            MyIList<int> list=prog.getList();
            MyIDictionary<string,int> dict= prog.getDict();
            
            try {
                    list.add(ex.eval(dict));
                    return prog;
            }
            catch (StmtExceptions e)
            {
                    throw new ControllerException(e);
            }
        }

        public override string ToString() { return "Print(" + ex.ToString() + ")"; }
    }

    public class OpenFile : IStmt
    {
        private string filename,varName;

        public OpenFile(string varName, string filename)
        {
            this.filename = filename;
            this.varName = varName;
        }

        public ProgState execute(ProgState prog)
        {
            MyIDictionary<string, int> dict = prog.getDict();
            MyIFileTable<Tuple> Tpl = prog.getFile();
            try
            {
                if (Tpl.isEmpty()==1)
                    for (int i=1; i<=Tpl.getKey();i++ )
                    {
                        Tuple tpl = Tpl.get(i);
                        if (tpl != null && tpl.getName() == filename)
                            throw new ControllerException("The file does not exist!! \n");
                    }
                StreamReader st = new StreamReader(filename);
                Tuple tup = new Tuple(filename,st);
                Tpl.add(tup);
                dict.add(varName,Tpl.getKey());
                return prog;
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch(DictExceptions e)
            { throw new ControllerException(e.ToString()); }
            return null;
        }

        public override string ToString(){ return "Opening:" + filename; }
    }

    public class ReadFile : IStmt
    {
        private Exp exp;
        private string name;

        public ReadFile(Exp exp, string name)
        {
            this.name = name;
            this.exp = exp;
        }

        public ProgState execute(ProgState prog)
        {
            MyIDictionary<string, int> dict = prog.getDict();
            MyIFileTable<Tuple> tpl = prog.getFile();

            try
            {
                int val = exp.eval(dict);
                Tuple tup = tpl.get(val);

                if (tup != null)
                {
                    string st = tup.getST().ReadLine();
                    if (st != null)
                    {
                        if (dict.isDefined(name) == 0)
                            dict.add(name, int.Parse(st));
                        else
                            dict.update(name, int.Parse(st));
                    }
                }
                else
                    throw new ControllerException("The file does not exist !!! \n");
            }
            catch(StmtExceptions e)
            {
                throw new ControllerException(e);
            }
            catch(IOException e)
            {
                Console.WriteLine(e.ToString());
            }

            return prog;
        }

        public override string ToString() { return "Reading from:" + exp.ToString(); }
    }

    public class CloseFile : IStmt
    {
        private Exp exp;

        public CloseFile(Exp exp) { this.exp = exp; }

        public ProgState execute(ProgState prog)
        {
            MyIDictionary<string, int> dict = prog.getDict();
            MyIFileTable<Tuple> Tpl = prog.getFile();

            try
            {
                int val = exp.eval(dict);
                Tuple tpl = Tpl.get(val);
                if (tpl != null)
                {
                    tpl.getST().Close();
                    Tpl.remove(val);
                }
                else
                    throw new ControllerException("The file does not exist!! \n");
            }
            catch (StmtExceptions e)
            { throw new ControllerException(e); }
            catch (IOException e)
            { Console.WriteLine(e.ToString()); }
            return prog;
        }

        public override string ToString() { return "Closing the file\n"; }
    }
}
