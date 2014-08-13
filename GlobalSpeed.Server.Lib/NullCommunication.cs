using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalSpeed.Server.Lib
{
    public class NullCommunication: ICommunication
    {
        public void WriteLine(string input) { }
        public string ReadLine() { return String.Empty; }
    }
}
