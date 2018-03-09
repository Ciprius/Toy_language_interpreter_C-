using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCsharp
{
    public class ProgState
    {
        private MyIStack<IStmt> stk;
        private MyIDictionary<string, int> dict;
        private MyIList<int> list;
        private MyIFileTable<Tuple> fileT;

        public ProgState(MyIStack<IStmt> stk, MyIDictionary<string,int> dict, MyIList<int> list, MyIFileTable<Tuple> fileT, IStmt prog)
        {
            this.stk = stk;
            this.dict = dict;
            this.list = list;
            this.fileT = fileT;
            stk.push(prog);
        }

        public MyIStack<IStmt> getStk() { return stk; }
        public void setStk(MyIStack<IStmt> stk) { this.stk = stk; }

        public MyIDictionary<string, int> getDict() { return dict; }
        public void setDict(MyIDictionary<string, int> dict) { this.dict = dict; }

        public MyIList<int> getList() { return list; }
        public void setList(MyIList<int> list) { this.list = list; }

        public MyIFileTable<Tuple> getFile() { return fileT; }
        public void setFile(MyIFileTable<Tuple> fileT) { this.fileT = fileT; }
    }
}
