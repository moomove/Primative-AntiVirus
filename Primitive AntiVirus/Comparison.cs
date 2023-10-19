using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Primitive_AntiVirus
{
    public static class Comparison
    {
        static List<string> BlackList = new List<string>();
        static List<string> WhiteList = new List<string>();

        // Analysing the rest of the processes after sending to B/W list
        public static void AnalyseProcess(Process process)
        {
            //List<Process> BlackList = new List<Process>();
            //List<Process> WhiteList = new List<Process>();

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
                    if (memoryUsage > 2.0f || cpuUsage > 2.0f)
                    {
                        // Prompt the user to add the process to the BlackList
                        Console.WriteLine($"Process {process.ProcessName} has high resource usage. Do you want to add it to the BlackList? (yes/no)");
                        string userInput = Console.ReadLine();
                        if (userInput.Equals("yes", StringComparison.OrdinalIgnoreCase))
                        {
                            // Add the process to the BlackList
                            BlackList.Add(process.ProcessName);
                            Console.WriteLine(process.ProcessName + "added to BlackList and killed");
                            SystemMonitor.KillProcess(process);
                        }
                        else if (userInput.Equals("no", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("No Assumed");
                        }

                    }
                    else
                    {
                        // Add the process to the WhiteList
                        WhiteList.Add(process.ProcessName);
                        Console.WriteLine(process.ProcessName + "added to WhiteList");
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
        public static void printWBList(char c)
        {
            if (c == 'b')
            {
                Console.WriteLine("Printing WhiteList\n");
                foreach (string process in WhiteList)
                {
                    Console.WriteLine(process);
                }
            }
            else if (c == 'w')
            {
                Console.WriteLine("Printing WhiteList\n");
                foreach (string process in BlackList)
                {
                    Console.WriteLine(process);
                }
            }
        }

    }
}
