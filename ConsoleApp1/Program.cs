using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDesWeakEncryptor;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = new byte[] { 0x30, 030, 0x30, 030, 0x30, 030, 0x30, 030, 0x30, 030, 0x30, 030, 0x30, 030, 0x30, 030 };
            var data = new byte[] { 0x02, 0x31, 0xa1, 0x04, 0x34, 0x62, 0x43, 0x03, 0xc1, 0x3d };
            var result = WeakEncryptor.TDes(data, key);
        }
    }
}
