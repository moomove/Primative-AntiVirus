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
            Comparison.AddTxtToBL();
            SystemBoot();
            MainSystem();
            Console.ReadLine();
           
        }

        public static void SystemBoot()//gets called on load and sets up the rest of the system
        {
            var uncheckedList = ProcessList.GetRunningProcesses();
            Console.WriteLine("Found Processes:");
            foreach (Process process in uncheckedList)
            {
                Console.WriteLine("\t"+process.ProcessName);
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
                Console.WriteLine("\t1. Scan System");
                Console.WriteLine("\t2. View BlackListed Processes");
                Console.WriteLine("\t3. View WhiteListed Processes");
                Console.WriteLine("\t4. Kill Process");
                Console.WriteLine("\t5. Print current running Process");
                Console.WriteLine("\t6. Trace Process");
                Console.WriteLine("\t7. Continuous System Scan");
                Console.WriteLine("\t8. Add Process to white or black List");
                Console.WriteLine("\t9. Exit Program");
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
                    Console.WriteLine("Enter Name of Process to kill");
                    input = Console.ReadLine();

                    var processes = Process.GetProcessesByName(input);
                    KillProcess(processes[0]);

                }
                else if (input == "5")
                {
                    var pList = ProcessList.GetRunningProcesses();

                    Console.WriteLine("Process ID\tProcess Name\t Process Paged Memory Size(bytes)");
                    foreach (Process p in pList) {
                        Console.WriteLine(p.Id + "\t" + p.ProcessName + "\t" + p.PagedMemorySize64);
                    }
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

                    try
                    {
                        waitPeriod = Convert.ToInt32(userInput);
                    }
                    catch {
                        Console.WriteLine("Input not recognised period set to 30");
                    }


                    while (true)
                    {
                        Console.WriteLine("System Being Monitored");
                        Thread.Sleep((waitPeriod*60000));//60000 is milliseconds in a minute
                        SystemBoot();
                    }
                }
                else if (input == "8")
                {
                    Comparison.UpdateList();
                }
                else if (input == "9")
                {
                    run = false;
                }
                else
                {
                    Console.WriteLine("Input not recognized");
                }
            }
        }


        public static void KillProcess(Process p)
        {

            string pToTrack = p.ProcessName;
            var processes = Process.GetProcessesByName(pToTrack);

            foreach (Process process in processes)
            {
                try
                {
                    process.Kill();
                    Console.WriteLine(pToTrack + " killed");
                }
                catch {
                    Console.WriteLine("unable to kill " + process.ProcessName + "Attempting to track");
                    TrackingAndIso.StartTracking(process);
                }
            }
        }

    }
}
