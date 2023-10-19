using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Primitive_AntiVirus
{
    

    public class listOfCheckedProcess
    {
       
    }

    class SystemMonitor
    {
        //public List<ProcessList.SysProcess> CheckedProccess;
       
        static void Main()
        {            
            SystemBoot();

            MaintainSystem();
            Console.ReadLine();
           
        }

        public static void SystemBoot()//gets called on load and sets up the rest of the system
        {
            var uncheckedList = ProcessList.GetRunningProcesses();
            foreach (Process process in uncheckedList)
            {
                Console.WriteLine(process.WorkingSet64 +" "+ process.ProcessName );
                Comparison.AnalyseProcess(process);
                TrackingAndIso.StartTracking(process);
                
            }
        }
        public static async void MaintainSystem() {
            bool run = true;
            while (run)
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                CancellationToken token = tokenSource.Token;

                Console.WriteLine("...System being monitored...");
                Console.WriteLine("");
                Console.WriteLine("Select an operations you would like to run");
                Console.WriteLine("");
                Console.WriteLine("\t1. Rescan System");
                Console.WriteLine("\t2. View BlackListed Processes");
                Console.WriteLine("\t3. View WhiteListed Processes");
                Console.WriteLine("\t4. Kill Process");
                Console.WriteLine("\t5. Shutdown Self");
                Console.WriteLine("\t6. Trace Process");
                Console.WriteLine("");
                Console.WriteLine("");
                Task userInputTask = WaitForUserInputAsync(token);

                int waitMinutes = 1;//how many minutes to wait
                int refreshPeriod = waitMinutes * 60000; //the amount of microseconds in a minute
                Task timeoutTask = Task.Delay(refreshPeriod);

                // Wait for either user input or a timeout
                Task completedTask = await Task.WhenAny(userInputTask, timeoutTask);

                if (completedTask == userInputTask)
                {
                    // User provided input
                    Console.WriteLine("...System Monitoring Paused...");
                    string option = Console.ReadLine();

                    if(option == "1")
                    {
                        Console.WriteLine("1234123");
                    }
                    else
                    {
                        Console.WriteLine("1234");
                    }
                }
                else
                {
                    // Timeout occurred
                    //rescan system
                    Console.WriteLine("rescanning");
                }

                // Optionally cancel the user input task if it's still running
                if (!userInputTask.IsCompleted)
                {
                    tokenSource.Cancel();
                }
            }
        }
        static async Task WaitForUserInputAsync(CancellationToken token)
        {
            var inputTask = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey(intercept: true); // Read and discard the key press
                        break;
                    }
                }
            });
            await inputTask;
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
            Console.WriteLine($"Killing {p}");
            p.Kill();
            Thread.Sleep(3000);//sleep to see if the process reboots itself
            CheckProcess(p);
            if (p == null)
            {
                Console.WriteLine("Successfully killed process");
            }
            else
            {
                Console.WriteLine("Unable to kill Process");
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
