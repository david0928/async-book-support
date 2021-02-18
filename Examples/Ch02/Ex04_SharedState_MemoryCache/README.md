# Ex04_SharedState_MemoryCache

參考 [ASP.NET Core Memory Cache - Is the GetOrCreate method thread-safe?](https://blog.novanet.no/asp-net-core-memory-cache-is-get-or-create-thread-safe/) 
，測試 MemoryCache 的 GetOrCreate 是否符合 Thread Safe。

- [Memory Cache](https://docs.microsoft.com/zh-tw/aspnet/core/performance/caching/memory): 符合 Thread Safe，但可能因為微軟文件中所提到的以下事項造成異常。
  - 由於回呼未完成，因此多個要求可以找到空白的快取索引鍵值。
  - 這可能會導致數個執行緒重新填入快取的專案。
- [LazyCache](https://github.com/alastairtree/LazyCache): 符合 Thread Safe，並且不會產生異常。

參考: 
- [Interlocked](https://docs.microsoft.com/zh-tw/dotnet/api/system.threading.interlocked)
- [用.NET展現多核威力(1) - 從 ThreadPool 翻船談起](https://blog.darkthread.net/blog/multicore-1/)
- [用.NET展現多核威力(2) - 一核一緒 王者之道?](https://blog.darkthread.net/blog/multicore-2/)
- [用.NET展現多核威力(2A) - 一核一緒補充包](https://blog.darkthread.net/blog/multicore-2a/)
- [用.NET展現多核威力(3) - 佛心 TPL 之 Parallel.For 好威](https://blog.darkthread.net/blog/multicore-3/)
- [Parallel.For 翻船事件剖析 - 使用 Concurrency Visualizer](https://blog.darkthread.net/blog/concurrency-visualizer/)