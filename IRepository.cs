using System;
using System.Collections.Generic;
using System.IO;

namespace LabCsharp
{
    public interface IRepository
    {
        ProgState getProg();
        void PrintlogFile();
        void CloseFile();
    }
    public class Repository :IRepository
    {
        private ProgState[] progs;
        private StreamWriter log;
        
        public Repository(ProgState prog,string filename)
        {
            progs = new ProgState[1];
            progs[0] = prog;
            log = new StreamWriter(filename);
        }

        public ProgState getProg() { return progs[0];}

        private string To()
        {
            string str = null;
            foreach (KeyValuePair<int,Tuple> item in progs[0].getFile().getDict())
            {
                str = str + item.Key + "->" + item.Value.getName() + '\n';
            }
            return str;
        } 

        public void PrintlogFile()
        {
            log.WriteLine("----------------------------------------\n");
            log.WriteLine("EXE Stack :\n");
            log.WriteLine(progs[0].getStk().ToString());
            log.WriteLine("SYM Table:\n");
            log.WriteLine(progs[0].getDict().ToString());
            log.WriteLine("OUT List:\n");
            log.WriteLine(progs[0].getList().ToString());
            log.WriteLine("FILE Table:\n");
            log.WriteLine(To());
        }

        public void CloseFile() { log.Close(); }
    }
}
