using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Primitive_AntiVirus
{
    class TrackingAndIso
    {
        public Process maliciousProcess;
        public bool success;
        public bool processSource;

        public class ProcessTracker
        {
            private List<Process> runningProcesses;

            public ProcessTracker()
            {
                runningProcesses = new List<Process>();
            }

            public void StartTracking()
            {
                // Get all running processes on the system
                Process[] allProcesses = Process.GetProcesses();

                // Add all running processes to the list
                foreach (Process process in allProcesses)
                {
                    runningProcesses.Add(process);
                }

              
            }

            private void OnTimerTick(object sender)
            {
                // Get all running processes on the system
                Process[] allProcesses = Process.GetProcesses();

                // Check for new processes
                foreach (Process process in allProcesses)
                {
                    if (!runningProcesses.Contains(process))
                    {
                        // Add the new process to the list
                        runningProcesses.Add(process);
                    }
                }

                // Check for stopped processes
                foreach (Process process in runningProcesses)
                {
                    if (process.HasExited)
                    {
                        // The process has stopped
                        runningProcesses.Remove(process);

                    }
                }
            }

            
        }
    } 
    }

    