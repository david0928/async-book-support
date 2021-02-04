using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Ex05_AsyncAndThreads
{
    static class Program
    {
        static void Log(int num, string msg)
        {
            Console.WriteLine("({0}) T{1}: {2}", 
                num, Thread.CurrentThread.ManagedThreadId, msg);
        }

        static async Task Main()
        {
            Log(1, "正要起始非同步工作 MyDownloadPageAsync()。");

            var task = MyDownloadPageAsync("https://www.huanlintalk.com");

            Log(5, "已從 MyDownloadPageAsync() 返回，但尚未取得工作結果。");

            string content = await task;

            Log(7, "已經取得 MyDownloadPageAsync() 的結果。");

            Console.WriteLine("網頁內容總共為 {0} 個字元。", content.Length);
        }

        static async Task<string> MyDownloadPageAsync(string url)
        {
            Log(2, "正要呼叫 WebClient.DownloadStringTaskAsync()。");

            using (var webClient = new WebClient())
            {
                var task = webClient.DownloadStringTaskAsync(url);

                Log(3, "已起始非同步工作 DownloadStringTaskAsync()。");

                await MockThread();

                string content = await task;

                Log(6, "已經取得 DownloadStringTaskAsync() 的結果。");

                return content;
            }
        }

        static async Task MockThread()
        {
            var fromResultOrRun = FromResultOrRun.Run;

            // 測試 FromResult 和 Run 使用的 Thread
            // (使用 FromResult 比較容易使用相同的 Thread ID，推斷 FromResult 的執行時間較短所導致)
            switch (fromResultOrRun)
            {
                case FromResultOrRun.FromResult:
                    await Task.FromResult<string>(TestAwaitArea());
                    break;
                case FromResultOrRun.Run:
                    await Task.Run(() => TestAwaitArea());
                    break;
            }
            string TestAwaitArea()
            {
                // 無法得知 DownloadStringTaskAsync() 執行期間的 Thread ID，自己另啟一個 Task 試看看
                // (不一定會跟 DownloadStringTaskAsync() 一樣的結果)
                Log(4, $"使用 Task.{fromResultOrRun}() 模擬 await 期間的 Thread ID");
                return "";
            }
        }
    }

    public enum FromResultOrRun
    {
        FromResult,
        Run
    }
}
