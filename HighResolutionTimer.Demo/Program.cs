using System;
using System.Diagnostics;

namespace HighResolutionTimer.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Process p = Process.GetCurrentProcess();
            p.PriorityBoostEnabled = true;  // every little helps ;)
            p.PriorityClass = ProcessPriorityClass.RealTime;

            Console.WriteLine("Priviledged processor time for process " + p.ProcessName + " is " + p.PrivilegedProcessorTime.TotalMilliseconds.ToString("#,##0.0") + " ms");
            Console.WriteLine("Number of ticks per second: " + Stopwatch.Frequency.ToString("#,##0"));

            UseMicroTimerClass();
            Console.ReadLine();
        }

        #region MicroTimer
        private static void UseMicroTimerClass()
        {
            Console.WriteLine("MICRO TIMER CLASS:");
            var lMicroTimer = new System.Timers.HighResolutionTimer(50, 3);
            lMicroTimer.OnTimer += OnTimer;
            lMicroTimer.OnStoped += OnStoped;
            lMicroTimer.OnSkipped += OnSkipped;
            lMicroTimer.Start();
        }

        static void OnSkipped(object sender, EventArgs args)
        {
            Console.WriteLine($"Cycle skipped");
        }

        static void OnStoped(object sender, EventArgs args)
        {
            Console.WriteLine("Cycle stopped.");
        }

        static void OnTimer(object sender, EventArgs args)
        {
            Console.WriteLine($"Cycle timer");
        }
        #endregion
    }
}
