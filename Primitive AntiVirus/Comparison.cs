using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Primitive_AntiVirus
{
    public static class Comparison
    {
        static List<string> BlackList = new List<string>();
        static List<string> WhiteList = new List<string>();

        // Analysing the rest of the processes after sending to B/W list
        public static void AnalyseProcess(Process process)
        {

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
                    if (memoryUsage > 25.0f || cpuUsage > 25.0f)
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
            if (c == 'w')
            {
                Console.WriteLine("Printing White-List\n");
                foreach (string process in WhiteList)
                {
                    Console.WriteLine(process);
                }
            }
            else if (c == 'b')
            {
                Console.WriteLine("Printing Black-List\n");
                foreach (string process in BlackList)
                {
                    Console.WriteLine(process);
                }
            }
        }
        public static void UpdateList()
        {
            Console.WriteLine("Enter Name of Process to move");
            string input = Console.ReadLine();

            var processes = Process.GetProcessesByName(input);
            input = processes[0].ProcessName;

            Console.WriteLine("Add Process to WhiteList [W] or kill process and add to BlackList [B] or any other key to Cancel");
            string c = Console.ReadLine();

            if (c == "w" || c == "W")
            {
                Console.WriteLine("Adding to WhiteList");
                try
                {
                    BlackList.Remove(input);
                    Console.WriteLine("Process Removed from Blacklist");
                }
                catch { }

                WhiteList.Add(input);
            }
            else if (c == "b" || c == "B")
            {
                Console.WriteLine("Killing and adding to BlackList");
                try
                {
                    WhiteList.Remove(input);
                    Console.WriteLine("Process Removed from Whitelist");
                }
                catch { }

                SystemMonitor.KillProcess(processes[0]);
                BlackList.Add(input);
            }
            else
            {
                Console.WriteLine("Cancelled");
            }

        }
        public static void AddTxtToBL() {
            string filePath = "blackList.txt";

            try
            {
                // Read all the lines from the file into an array
                string[] bProcesses = File.ReadAllLines(filePath);

                // Alternatively, you can read the entire content as a single string
                // string fileContent = File.ReadAllText(filePath);
                Console.WriteLine("\t9. Exit Program");
                // Display the content of the file
                Console.WriteLine("File Contents:");
                foreach (string process in bProcesses)
                {
                    BlackList.Add(process);
                }
                Console.WriteLine("\t9. Exit Program");
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the file: " + e.Message);
            }

        }

    }
}
