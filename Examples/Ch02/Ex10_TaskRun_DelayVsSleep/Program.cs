using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Ex10_TaskRun_DelayVsSleep
{
    internal static class Program
    {
        private static int _delayTime = 2000;
        private static int _loops = 20;

        private static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Reset();
            timer.Start();

            var testType = 1; //切換 1: Task.Delay, 2: Thread.Sleep
            var tasks = new List<Task>();

            if (testType == 1)
            {
                Console.WriteLine("====== Task.Delay ======");
                for (int i = 0; i < _loops; i++)
                {
                    Task task = Task.Run(async () =>
                    {
                        await TestDelay();
                    });
                    tasks.Add(task);
                }
            }
            else
            {
                Console.WriteLine("====== Thread.Sleep ======");
                for (int i = 0; i < _loops; i++)
                {
                    Task task = Task.Run(() =>
                    {
                        TestSleep();
                    });
                    tasks.Add(task);
                }
            }

            for (int i = 0; i < 500; i++)
            {
                Console.Write(".");
            }

            // 確保非同步工作執行完畢之後才往下繼續執行。
            Task.WaitAll(tasks.ToArray());

            timer.Stop();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("====== Run Time : " + timer.Elapsed.TotalMilliseconds.ToString() + "ms");
        }

        private static void TestSleep()
        {
            var threadId = "[" + Thread.CurrentThread.ManagedThreadId + "]";

            for (int i = 0; i < 500; i++)
            {
                Console.Write(threadId);
            }

            Thread.Sleep(_delayTime);

            Console.WriteLine("{0} - Done", threadId);
        }

        private static async Task TestDelay()
        {
            var threadId = "[" + Thread.CurrentThread.ManagedThreadId + "]";

            for (int i = 0; i < 500; i++)
            {
                Console.Write(threadId);
            }

            await Task.Delay(_delayTime);

            Console.WriteLine("{0} - Done", threadId);
        }
    }
}