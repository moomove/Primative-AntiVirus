using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitive_AntiVirus
{
    class MainView
    {
        //PList StoppedProcess
        //PList RunnningProcess
        //PList WhitelistedProcess
        //Process ProblemProcess
        // Float represents PLIST/Process

        public static void FetchProcesses()
        {

            // StoppedProcess ==PLIST
            // RunnningProcess ==PLIST
            // WhitelistedProcess ==PLIST
        }
        public static void SelectKillProcess()
        {
            float p;
            //asks what process you want to kill
            SystemMonitor.KillProcess(p);
        }
        public static void TraceProcess()
        {
            float p;
            //asks what process you want to trace
            TrackingAndIso.TraceProcess(p);
        }



    }
}
