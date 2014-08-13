using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalSpeed.Server.Lib
{
    public interface ICommunication
    {
        void WriteLine(string input);
        string ReadLine();
    }
}
