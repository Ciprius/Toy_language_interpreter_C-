using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    class StackExceptions : Exception
    {
        public StackExceptions(string name):base(name){}
    }

    class DictExceptions : Exception
    {
        public DictExceptions(string name) : base(name) { }
    }

    class StmtExceptions: Exception
    {
        public StmtExceptions(string name) : base(name) { }
        public StmtExceptions(DictExceptions e) : base(e.ToString()) { }
        public StmtExceptions(StmtExceptions e) : base(e.ToString()) { }
    }

    class DivizionByZeroException : Exception
    {
        public DivizionByZeroException(string name) : base(name) { }
    }

    class ControllerException : Exception
    {
        public ControllerException(string name) : base(name) { }
        public ControllerException(StmtExceptions e) : base(e.ToString()) { }
        public ControllerException(DivizionByZeroException e) : base(e.ToString()) { }
    }

    class StmtExecException:Exception
    {
        public StmtExecException(string name) : base(name) { }
        public StmtExecException(ControllerException e) : base(e.ToString()) { }
    }
}

