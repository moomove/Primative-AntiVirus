using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Primitive_AntiVirus
{
    //public class SysProcess
    //{
    //    int ProcessID;
    //    string ProcessName;
    //    bool PotentallyMalicous;
    //    bool WhiteListed;
    //    bool BlackListed;
    //}

    class ProcessList
    {
      //  List<Process> pList = new List<Process>();

        static public List<Process> GetRunningProcesses()
        {
            List<Process> processes = new List<Process>();

            foreach (Process process in Process.GetProcesses())
            {
                processes.Add(process);
            }

            return processes;
        }

 
    }
}


//public void AddToWhiteList(string processName)
//{
//    whiteList.Add(processName);
//}

//public void AddToBlackList(string processName)
//{
//    blackList.Add(processName);
//}

//public void AddToGrayList(string processName)
//{
//    grayList.Add(processName);
//}

//public bool IsProcessAllowed(string processName)
//{
//    if (whiteList.Contains(processName))
//    {
//        return true;
//    }
//    else if (blackList.Contains(processName))
//    {
//        return false;
//    }
//    else
//    {
//        // If the process is not in the whitelist or blacklist, it is in the greylist.
//        // We can either allow the process or block it based on our policies.
//        // For example, we can allow the process if it is signed by a trusted publisher.
//        return true;
//    }
//}
//
//public void ScanProcesses()
//{
//    List<Process> processes = GetRunningProcesses();

//    foreach (Process process in processes)
//    {
//        if (!IsProcessAllowed(process.ProcessName))
//        {
//            The process is not allowed, so we can block it.
//           process.Kill();
//        }
//    }
//}
