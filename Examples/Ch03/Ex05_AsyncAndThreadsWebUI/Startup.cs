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

                    text.Append(Log(1, "await ���e���{��"));

                    using (var webClient = new WebClient())
                    {
                        var task = webClient.DownloadStringTaskAsync("https://www.google.com");

                        text.Append(Log(2, "Start - DownloadStringTaskAsync()"));

                        // ���� FromResult �M Run �ϥΪ� Thread
                        // (�ϥ� FromResult ����e���ϥάۦP�� Thread ID�A���_ FromResult ������ɶ����u�ҾɭP)
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
                            // �L�k�o�� DownloadStringTaskAsync() ��������� Thread ID�A�ۤv�t�Ҥ@�� Task �լݬ�
                            // (���@�w�|�� DownloadStringTaskAsync() �@�˪����G)
                            text.Append(Log(3, $"�ϥ� Task.{fromResultOrRun}() ���� await ������ Thread ID"));
                            return "";
                        }

                        content = await task;

                        text.Append(Log(4, $"End - DownloadStringTaskAsync()"));
                    }

                    text.Append($"�������e�`�@�� {content.Length} �Ӧr���C");

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