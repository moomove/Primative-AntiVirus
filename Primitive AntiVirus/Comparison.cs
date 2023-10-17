using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
//using Microsoft.VisualBasic.Devices;

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
        /*
        // Method to Get the List of Processes from PList
        public List<ProcessList.SysProcess> GetProcessesFromPList()
        {
            List<ProcessList.SysProcess> processes = new List<ProcessList.SysProcess>();

            // Assuming that pList is an instance variable or property in ProcessList
            if (ProcessList.pList != null)
            {
                // Iterate through the list of SysProcess objects in pList and add them to the result list
                processes.AddRange(ProcessList.pList);
            }

            return processes;
        }

        // Method to Get the List of Processes from System Monitor
        public List<string> GetProcessesFromSystemMonitor()
        {
            List<string> processes = new List<string>();

            // Implement the logic to get processes from System Monitor here
            // You might use the System.Diagnostics.Process class to query running processes

            try
            {
                // Use the Process class to get a list of running processes
                Process[] allProcesses = Process.GetProcesses();

                foreach (Process process in allProcesses)
                {
                    // Add the process name to the processes list
                    processes.Add(process.ProcessName);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during process retrieval
                Console.WriteLine("Error getting processes from System Monitor: " + ex.Message);
            }

            return processes;
        }

        // Method to Compare Processes for Matches
        public void CompareProcesses(List<ProcessList.SysProcess> pListProcesses, List<string> systemMonitorProcesses)
        {
            List<ProcessList.SysProcess> isolatedProcesses = new List<ProcessList.SysProcess>();
            List<ProcessList.SysProcess> whiteListedProcesses = new List<ProcessList.SysProcess>();
            List<ProcessList.SysProcess> blackListedProcesses = new List<ProcessList.SysProcess>();


            // Iterate through the processes obtained from PList
            foreach (ProcessList.SysProcess pListProcess in pListProcesses)
            {
                // Check if the process name from PList exists in the system monitor processes
                if (systemMonitorProcesses.Contains(pListProcess.ProcessName))
                {
                    // Mark the process as Isolation and tracking = 1
                    // Implement the logic to mark the process here

                    // Check if the process is white-listed
                    if (pListProcess.WhiteListed)
                    {
                        // Add the process to the "WhiteList" and skip further checks
                        whiteListedProcesses.Add(pListProcess);
                        continue;
                    }

                    // Check if the process is black-listed
                    if (pListProcess.BlackListed)
                    {
                        // Add the process to the "BlackList" and skip further checks
                        blackListedProcesses.Add(pListProcess);
                        continue;
                    }

                    // If the process is not white-listed or black-listed, mark it as Potentially Malicious
                    pListProcess.PotentallyMalicous = true;

                    // Add the process to the "Isolation" list
                    isolatedProcesses.Add(pListProcess);
                }

            // Exit or continue as needed
            return;

            }
        }
        */

        //Create the two lists for the BlackList and the Whitelist
        List<string> BlackList = new List<string>();
        List<string> WhiteList = new List<string>();

        // Analysing the rest of the processes after sending to B/W list
        public void AnalyseProcesses(SysProcess process)
        {
            if (BlackList.Contains(process.ProcessName))
            {
                process.KillProcess();
            }
            else
            {
                if (WhiteList.Contains(process.ProcessName))
                {
                    return;
                }
                else
                {
                    // The process is not in either list, so analyze memory and CPU usage
                    float memoryUsage = GetMemoryUsage(process.ProcessId);
                    float cpuUsage = GetCpuUsage(process.ProcessId);

                    // Check if either memory or CPU usage is above 25%
                    if (memoryUsage > 25.0f || cpuUsage > 25.0f)
                    {
                        // Prompt the user to add the process to the BlackList
                        Console.WriteLine($"Process {process.ProcessName} has high resource usage. Do you want to add it to the BlackList? (yes/no)");
                        string userInput = Console.ReadLine();
                        if (userInput.Equals("yes", StringComparison.OrdinalIgnoreCase))
                        {
                            // Add the process to the BlackList
                            BlackList.Add(process.ProcessName);
                        }
                        else if (userInput.Equals("no", StringComparison.OrdinalIgnoreCase))
                        {
                            // Add the process to the WhiteList
                            WhiteList.Add(process.ProcessName);
                        }

                    }
                }
            }
        }




    
        
        // Method to Get Memory Usage of a Process by Process ID
        private float GetMemoryUsage(int processID)
        {
            try
            {
                using (Process process = Process.GetProcessById(processID))
                {
                    // Refresh the process information to get updated memory usage
                    process.Refresh();

                    // Get the total physical memory of the system
                    long totalMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;

                    // Calculate memory usage as a percentage
                    float memoryUsagePercentage = (float)process.WorkingSet64 / totalMemory * 100.0f;

                    return memoryUsagePercentage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting memory usage for process {processID}: {ex.Message}");
                return 0.0f;
            }
        }

        // Method to Get CPU Usage of a Process by Process ID
        private float GetCpuUsage(int processID)
        {
            try
            {
                using (PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", processID.ToString()))
                {
                    // Calculate CPU usage as a percentage
                    float cpuUsage = cpuCounter.NextValue();

                    // Wait a moment to get a more accurate reading
                    System.Threading.Thread.Sleep(1000);

                    cpuUsage = cpuCounter.NextValue();
                    return cpuUsage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting CPU usage for process {processID}: {ex.Message}");
                return 0.0f;
            }
        }

    }
}
