# Ex05_AsyncAndThreadsWebUI

測試 ASP NET.Core 的 UI Thread ID 和 DownloadStringTaskAsync Thread ID 的使用情況。

Thread 切換的部分，測試的結果，以下前 四種情境 都有可能會發生。

- Step 3 使用 Task.FromResult() 情境一和情境二 的機會較高。
- Step 3 使用 Task.Run() 情境三和情境四 的機會較高，偶而還會發生 情境五 。

此測試可以知道 control flow(控制流)，數量不一定等於 Thread 數量，多個 control flow 有可能共用一個 Thread，.Net 的 CLR 會自己依不同的情境來決定最佳做法。

=== 情境一 ===
1. Thread 1 -> await 之前的程式
2. Thread 1 -> webClient.MyDownloadPageAsync() 未完成
3. Thread 1 -> 使用 Task.FromResult() or Task.Run() 模擬 await 期間的 Thread ID
4. Thread 1 -> webClient.MyDownloadPageAsync() 完成後

=== 情境二 ===
1. Thread 1 -> await 之前的程式
2. Thread 1 -> webClient.DownloadStringTaskAsync() 未完成
3. Thread 1 -> 使用 Task.FromResult() or Task.Run() 模擬 await 期間的 Thread ID
4. Thread 2 -> webClient.DownloadStringTaskAsync() 完成後

=== 情境三 ===
1. Thread 1 -> await 之前的程式
2. Thread 1 -> webClient.DownloadStringTaskAsync() 未完成
3. Thread 3 -> 使用 Task.FromResult() or Task.Run() 模擬 await 期間的 Thread ID
4. Thread 1 -> webClient.DownloadStringTaskAsync() 完成後

=== 情境四 ===
1. Thread 1 -> await 之前的程式
2. Thread 1 -> webClient.DownloadStringTaskAsync() 未完成
3. Thread 3 -> 使用 Task.FromResult() or Task.Run() 模擬 await 期間的 Thread ID
4. Thread 2 -> webClient.DownloadStringTaskAsync() 完成後

=== 情境五 ===
1. Thread 1 -> await 之前的程式
2. Thread 1 -> webClient.DownloadStringTaskAsync() 未完成
3. Thread 2 -> 使用 Task.FromResult() or Task.Run() 模擬 await 期間的 Thread ID
4. Thread 2 -> webClient.DownloadStringTaskAsync() 完成後

- [使用 .NET Async/Await 的常見錯誤](https://blog.darkthread.net/blog/common-async-await-mistakes)
- [There Is No Thread](https://blog.stephencleary.com/2013/11/there-is-no-thread.html)
- [Correcting Common Async/Await Mistakes in .NET - 影片](https://www.youtube.com/watch?v=J0mcYVxJEl0)
- [Correcting Common Async Await Mistakes in .NET - 投影片](https://www.slideshare.net/secret/M1kWxIKW7q20ku)