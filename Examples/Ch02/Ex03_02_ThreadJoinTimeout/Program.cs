using System;
using System.Threading;

namespace Ex03_02_ThreadJoinTimeout
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Thread t1 = new Thread(MyTask);
            Thread t2 = new Thread(MyTask);
            Thread t3 = new Thread(MyTask);

            t1.Start("T1");
            t2.Start("T2");
            t3.Start("T3");

            // 鎖定時間 < 執行時間回傳, 未執行完不在鎖定回傳 False, 並繼續執行
            var t1IsSuccess = t1.Join(1000);
            Console.WriteLine($"t1: {t1IsSuccess}");

            // 鎖定時間 > 執行時間回傳, 執行完成不在鎖定回傳 True
            var t2IsSuccess = t2.Join(3500);
            Console.WriteLine($"t2: {t2IsSuccess}");

            // 鎖定時間 < 執行時間回傳, 但會被 t2 影響到, 所以會等待 t2
            var t3IsSuccess = t3.Join(1000);
            Console.WriteLine($"t3: {t3IsSuccess}");

            Console.WriteLine($"是否執行完畢: t1: True, t2: {t2IsSuccess}, t3: {t3IsSuccess}");
            Console.ReadKey();
        }

        private static void MyTask(object param)
        {
            Console.WriteLine("{0} 已開始執行 MyTask()", param);
            Thread.Sleep(3000);
            Console.WriteLine("{0} 即將完成工作", param);
        }
    }
}
