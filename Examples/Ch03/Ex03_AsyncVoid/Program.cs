using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ex03_AsyncVoid
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TestVoidAsync();
            }
            catch (Exception ex) // 捕捉不到 TestVoidAsync 方法所拋出的異常!
            {
                Console.WriteLine(ex);
            }
        }

        static async void TestVoidAsync()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            await Task.Delay(0); // 只是為了讓此方法有用到 await 陳述式。
            //await Task.Yield(); // 讓此方法有用到 await 陳述式，也可以改用 Task.Yield()，但使用 Task.Yield() 會更換 Thread

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);


            throw new Exception("error");
        }
    }
}
