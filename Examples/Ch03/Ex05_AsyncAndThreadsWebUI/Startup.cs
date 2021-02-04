using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex05_AsyncAndThreadsWebUI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var text = new StringBuilder();
                    var content = string.Empty;
                    var fromResultOrRun = FromResultOrRun.FromResult;

                    text.Append(Log(1, "await 之前的程式"));

                    using (var webClient = new WebClient())
                    {
                        var task = webClient.DownloadStringTaskAsync("https://www.google.com");

                        text.Append(Log(2, "Start - DownloadStringTaskAsync()"));

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
                            text.Append(Log(3, $"使用 Task.{fromResultOrRun}() 模擬 await 期間的 Thread ID"));
                            return "";
                        }

                        content = await task;

                        text.Append(Log(4, $"End - DownloadStringTaskAsync()"));
                    }

                    text.Append($"網頁內容總共為 {content.Length} 個字元。");

                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(text.ToString());

                });
            });
        }

        public string Log(int num, string msg)
        {
            return ($"({num}) Thread - {Thread.CurrentThread.ManagedThreadId}: {msg} <br/> ");
        }
    }

    public enum FromResultOrRun
    {
        FromResult,
        Run
    }
}