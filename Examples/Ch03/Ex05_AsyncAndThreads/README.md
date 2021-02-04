# Ex05_AsyncAndThreads

測試 Console 的 Thread ID 和 DownloadStringTaskAsync Thread ID 的使用情況。

自己新增了一段 Step 4 使用 Task.FromResult() or Task.Run() 模擬 await 期間的 Thread ID

Thread 切換的部分，Step 4 使用 Task.FromResult() 結果會如下

(1) T1: 正要起始非同步工作 MyDownloadPageAsync()。
(2) T1: 正要呼叫 WebClient.DownloadStringTaskAsync()。
(3) T1: 已起始非同步工作 DownloadStringTaskAsync()。
(4) T1: 使用 Task.FromResult() 模擬 await 期間的 Thread ID
(5) T1: 已從 MyDownloadPageAsync() 返回，但尚未取得工作結果。
(6) T4: 已經取得 DownloadStringTaskAsync() 的結果。
(7) T4: 已經取得 MyDownloadPageAsync() 的結果。

Step 4 使用 Task.Run() 就會出現以下情境

(4) T3: 使用 Task.Run() 模擬 await 期間的 Thread ID  
or  
(4) T4: 使用 Task.Run() 模擬 await 期間的 Thread ID

- [使用 .NET Async/Await 的常見錯誤](https://blog.darkthread.net/blog/common-async-await-mistakes)
- [There Is No Thread](https://blog.stephencleary.com/2013/11/there-is-no-thread.html)
- [Correcting Common Async/Await Mistakes in .NET - 影片](https://www.youtube.com/watch?v=J0mcYVxJEl0)
- [Correcting Common Async Await Mistakes in .NET - 投影片](https://www.slideshare.net/secret/M1kWxIKW7q20ku)