using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Primitive_AntiVirus
{
    public class Process
    {
        int pID;
        string pName;
    }
    public class listOfCheckedProcess
    {
        public List<Process> PList;
    }

    class SystemMonitor
    {
        
        static void Main()
        {
            
            SystemBoot();
        }

        public static void SystemBoot()//gets called on load and sets up the rest of the system
        {
            var uncheckedList = GeneratePList();
            foreach (Process process in uncheckedList)
            {
                CheckProcess(uncheckedList.process);
            }
        }
        public static void CheckProcess(Process p)//quick check of a process
        {
            AnalyiseProcess(p);
        }
        public static void AllowedProcess()//maybe this should just be a call to the comparisonDB
        {

        }
        public static void PassToCompBD()
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
            Console.WriteLine($"{p} Is suspected of being suspicious ");
            UpdateList(p);
        }
        public static void UpdateList(Process p)
        {
            
            Console.WriteLine("Add Process to WhiteList [W] or kill process and add to BalckList [B] or any other key to Cancel");
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
