using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitive_AntiVirus
{
    internal class Comparison
    {
       /* From PList, get the list of processes
        From System Monitor, get the processes

            function Compare
            compare the processes for matches

            if matching
            mark as Isolation and tracking = 1
            exit
       */


    public Comparison()
        {

        }

        // Method to Get the List of Processes from PList
        public List<string> GetProcessesFromPList()
        {
            List<string> processes = new List<string>();

            // Implement the logic to get processes from PList here
            // You might use file I/O or any other method to read the PList data

            return processes;
        }

        // Method to Get the List of Processes from System Monitor
        public List<string> GetProcessesFromSystemMonitor()
        {
            List<string> processes = new List<string>();

            // Implement the logic to get processes from System Monitor here
            // You might use the System.Diagnostics.Process class to query running processes

            return processes;
        }

        // Method to Compare Processes for Matches
        public void CompareProcesses(List<string> plistProcesses, List<string> systemMonitorProcesses)
        {
            // Implement the logic to compare processes for matches here
            // You can use loops and conditional statements to compare the lists

            foreach (string process in plistProcesses)
            {
                if (systemMonitorProcesses.Contains(process))
                {
                    // Mark the process as Isolation and tracking = 1
                    // Implement the logic to mark the process here

                    // Exit or continue as needed
                    return;

                }
            }
        }

        // Analysing the rest of the processes after sending to B/W list
        public void AnalyseProcesses()
        {



        }
    }
}
