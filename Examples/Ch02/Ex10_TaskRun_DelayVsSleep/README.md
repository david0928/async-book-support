# Ex10_TaskRun_DelayVsSleep

使用 Task.Run 時，比較 Task.Delay 和 Thread.Sleep 的效能差異。

- Thread.Sleep: Block 目前的 Thread，目前的程式區域裡會變成同步執行。
- Task.Delay: 遇到 await 時會將目前的 Thread 還給 Thread Pool，另外開啟一個非同步來等待，等待完成後重新從 Thread Pool 取得 Thread，而不是 Block 目前的 Thread。

使用 Thread.Sleep 造成 Block 後，會有 Context Switch 的問題(執行緒數量 > CPU 核心數時，更容易發生)，所以 Task.Delay 的效能會比較好。

- [When to use Task.Delay, when to use Thread.Sleep?](https://stackoverflow.com/questions/20082221/when-to-use-task-delay-when-to-use-thread-sleep)
- [Thread.Sleep 與 Task.Delay 是完全不一樣的東西](http://slashview.com/archive2016/20160201.html)
- [用.NET展現多核威力(1) - 從 ThreadPool 翻船談起](https://blog.darkthread.net/blog/multicore-1/)
- [用.NET展現多核威力(2) - 一核一緒 王者之道?](https://blog.darkthread.net/blog/multicore-2/)
- [用.NET展現多核威力(2A) - 一核一緒補充包](https://blog.darkthread.net/blog/multicore-2a/)
- [用.NET展現多核威力(3) - 佛心 TPL 之 Parallel.For 好威](https://blog.darkthread.net/blog/multicore-3/)
- [Parallel.For 翻船事件剖析 - 使用 Concurrency Visualizer](https://blog.darkthread.net/blog/concurrency-visualizer/)