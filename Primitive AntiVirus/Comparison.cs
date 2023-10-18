using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Microsoft.VisualBasic.Devices;

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
        // Analysing the rest of the processes after sending to B/W list
        public static void AnalyseProcess(Process process)
        {
            List<string> BlackList = new List<string>();
            List<string> WhiteList = new List<string>();

            if (BlackList.Contains(process.ProcessName))
            {
                SystemMonitor.KillProcess(process);
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
                    float memoryUsage = GetMemoryUsage(process);
                    float cpuUsage = GetCpuUsage(process);

                    // Check if either memory or CPU usage is above 25%
                    if (memoryUsage > 1.0f || cpuUsage > 1.0f)
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
        private static float GetMemoryUsage(Process process)
        {
            try
            {
                // Refresh the process information to get updated memory usage
                process.Refresh();

                // Get the total physical memory of the system
                long totalMemory = (long)new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;

                // Calculate memory usage as a percentage
                float memoryUsagePercentage = (float)process.WorkingSet64 / totalMemory * 100.0f;

                return memoryUsagePercentage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting memory usage for process {process.Id}: {ex.Message}");
                return 0.0f;
            }
        }

        // Method to Get CPU Usage of a Process by Process ID
        private static float GetCpuUsage(Process process)
        {
            try
            {
                using (PerformanceCounter cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName))
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
                Console.WriteLine($"Error getting CPU usage for process {process.Id}: {ex.Message}");
                return 0.0f;
            }
        }

    }
}
