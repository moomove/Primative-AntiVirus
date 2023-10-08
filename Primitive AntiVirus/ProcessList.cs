using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Primitive_AntiVirus
{
    class ProcessList
    {
        public class SysProcess
        {
            int ProcessID;
            string ProcessName;
            bool PotentallyMalicous;
            bool WhiteListed;
            bool BlackListed;
        }
        public List<SysProcess> pList;

        public static void GeneratePList()
        {

        }
    }
}
