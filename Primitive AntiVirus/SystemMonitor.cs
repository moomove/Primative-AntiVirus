using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Primitive_AntiVirus
{

    class SystemMonitor
    {
        //public List<ProcessList.SysProcess> CheckedProccess;
       
        static void Main()
        {            
            SystemBoot();

            MainSystem();
            Console.ReadLine();
           
        }

        public static void SystemBoot()//gets called on load and sets up the rest of the system
        {
            var uncheckedList = ProcessList.GetRunningProcesses();
            foreach (Process process in uncheckedList)
            {
                Console.WriteLine(process.WorkingSet64 +" "+ process.ProcessName );
                Comparison.AnalyseProcess(process);
                                
            }
        }
        public static void MainSystem()
        {
            bool run = true;
            while (run) {
                Console.WriteLine("...Anti-Virus Menu...");
                Console.WriteLine("");
                Console.WriteLine("Select an operations you would like to run");
                Console.WriteLine("");
                Console.WriteLine("\t1. Rescan System");
                Console.WriteLine("\t2. View BlackListed Processes");
                Console.WriteLine("\t3. View WhiteListed Processes");
                Console.WriteLine("\t4. Kill Process");
                Console.WriteLine("\t5. Shutdown Self");
                Console.WriteLine("\t6. Trace Process");
                Console.WriteLine("\t7. Continuous System Scan");
                Console.WriteLine("");

                string input = Console.ReadLine();
                
                if(input == "1")
                {
                    SystemBoot();
                }
                else if(input == "2")
                {
                    Comparison.printWBList('b');
                }
                else if (input == "3")
                {
                    Comparison.printWBList('w');
                }
                else if (input == "4")
                {
                    Console.WriteLine("Enter Process ID");
                    input = Console.ReadLine();
                    
                    int.TryParse(input, out int pID);
                    KillProcess(Process.GetProcessById(pID));
                }
                else if (input == "5")
                {
                    run = false;
                }
                else if (input == "6")
                {
                    string pToTrack = Console.ReadLine();
                    var processes = Process.GetProcessesByName(pToTrack);

                    foreach (Process process in processes)
                    {
                        TrackingAndIso.StartTracking(process);
                    }
                }
                else if (input == "7")
                {
                    int waitPeriod = 30;
                    Console.WriteLine("How Frequently would you like in minutes would you like for the system to be scanned");
                    string userInput = Console.ReadLine();

                    int.TryParse(userInput, out int waitperiod);

                    while (true)
                    {
                        Console.WriteLine("System Being Monitored");
                        Thread.Sleep((waitPeriod*60000));//60000 is microseconds in a minute
                    }
                }
                else
                {
                    Console.WriteLine("Input not recognized");
                }
            }
        }


        public static void CheckProcess(Process p)//quick check of a process
        {
            AnalyiseProcess(p);
        }
        public static void AllowedProcess()//maybe this should just be a call to the comparisonDB
        {

        }
        public static void PassToCompBD()//useless i think we delete
        {

        }
        public static void AnalyiseProcess(Process p)//indepth check of a process
        {

        }
        public static void KillProcess(Process p)
        {
            //int pId = p.Id;
            //Console.WriteLine($"Killing {p}");
            //p.Kill(true);

            //p.WaitForExit(3000);

            //if (p == null)
            //{
            //    Console.WriteLine("Successfully killed process");
            //}
            //else
            //{
            //    Console.WriteLine("Unable to kill Process");
            //}
            string pToTrack = p.ProcessName;
            var processes = Process.GetProcessesByName(pToTrack);

            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
        public static void PromptUser(Process p)
        {
            Console.WriteLine($"{p.ProcessName} Is suspected of being suspicious ");
            UpdateList(p);
        }
        public static void UpdateList(Process p)
        {
            
            Console.WriteLine("Add Process to WhiteList [W] or kill process and add to BlackList [B] or any other key to Cancel");
            string c = Console.ReadLine();

            if(c == "w" ||c == "W")
            {
                Console.WriteLine("Adding to WhiteList");

            }
            else if (c == "b" || c == "B")
            {
                Console.WriteLine("Adding to BlackList");

            }
            else
            {
                Console.WriteLine("Cancelled");
            } 

        }
    }
}
