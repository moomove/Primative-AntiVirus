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
        public static void StartTracking(Process process)
        {
            try
            {
                string fullPath = process.MainModule.FileName;
                Console.WriteLine(fullPath);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error getting file path for process {process.ProcessName}: {ex.Message}");
                return;
            }
           
        }
    } 
}

    