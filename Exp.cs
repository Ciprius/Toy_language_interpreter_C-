using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions;

namespace LabCsharp
{
    public abstract class Exp
    {
        public abstract int eval(MyIDictionary<string,int> dict);
        public abstract override string ToString();
    }

    public class ConstExp : Exp
    {
        private int val;
        public ConstExp(int val) { this.val = val; }
        public override int eval(MyIDictionary<string, int> dict) { return val; }
        public override string ToString() { return "" + val + ""; }
    }

    public class VarExp : Exp
    {
        private string val;
        public VarExp(string val) { this.val = val; }
        public override int eval(MyIDictionary<string, int> dict)
        {
            try {return dict.lookup(val);}
            catch(DictExceptions e) {throw new StmtExceptions(e);}
        }
        public override string ToString() { return val; }
    }

    public class ArithmExp : Exp
    {
        private Exp exp1, exp2;
        private int op;

        public ArithmExp(Exp exp1, Exp exp2 ,int op)
        {
            this.exp1 = exp1;
            this.exp2 = exp2;
            this.op = op;
        }
        public override int eval(MyIDictionary<string, int> dict)
        {
            try
            {
                if ( op == 1 )
                    return exp1.eval(dict) + exp2.eval(dict);
                if ( op == 2 )
                    return exp1.eval(dict) - exp2.eval(dict);
                if ( op == 3 )
                    return exp1.eval(dict) * exp2.eval(dict);
                if (op == 4)
                    if (exp2.eval(dict) != 0)
                        return exp1.eval(dict) + exp2.eval(dict);
                    else
                        throw new DivizionByZeroException("Divizion by zero !!! \n");
                else
                    throw new StmtExceptions("The operator does not exist !!! \n");
            }
            catch(StmtExceptions e)
            {
                throw new StmtExceptions(e);
            }
        }

        public override string ToString()
        {
            if ( op == 1 ) 
                return exp1.ToString() + "+" + exp2.ToString();
            if ( op == 2 )
                return exp1.ToString() + "-" + exp2.ToString();
            if ( op == 3 )
                return exp1.ToString() + "*" + exp2.ToString();
            if ( op == 4 )
                return exp1.ToString() + "/" + exp2.ToString();
            else
                return null;
        }
    }
}
