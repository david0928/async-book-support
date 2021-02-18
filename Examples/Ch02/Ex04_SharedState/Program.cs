using System;
using System.Threading;

namespace Ex04_SharedState_Lock
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            new SharedStateDemo().Run();
            Console.ReadLine();
        }
    }

    public class SharedStateDemo
    {
        private int itemCount = 0;   // 已加入購物車的商品數量。

        public void Run()
        {
            var t1 = new Thread(AddToCart);
            var t2 = new Thread(AddToCart);

            t1.Start(0); // 試試改傳入 0
            t2.Start(100);
        }

        private void AddToCart(object simulateDelay)
        {
            itemCount++;

            /*
             * 用 Thread.Sleep 來模擬這項工作所花的時間，時間長短
             * 由呼叫端傳入的 simulateDelay 參數指定，以便藉由改變
             * 此參數來觀察共享變數值的變化。
             */

            var isTestSleep = true;
            if (isTestSleep)
            {
                Thread.Sleep((int)simulateDelay);
            }
            else
            {
                // 額外測試 Thread.Sleep(0); 和 沒用 Thread.Sleep() 是不同的結果
                if ((int)simulateDelay != 0)
                {
                    Thread.Sleep((int)simulateDelay);
                }
            }
            
            Console.WriteLine("Items in cart: {0}, TheadId: {1}", itemCount, Thread.CurrentThread.ManagedThreadId);
        }
    }
}