using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using System.Threading;

namespace GalaxyRim.Controllers
{
    public class Dispatcher : IDisposable
    {
        //public enum DispatchType { PermanentTask, LoadTask, ShortTask }


        private struct ThreadPoolThreadData
        {
            public Thread thread;
            public AutoResetEvent wait;

            public ThreadPoolThreadData(Thread thread, AutoResetEvent wait)
            {
                this.thread = thread; this.wait = wait;
            }
        }

        private struct ProcessThreadData
        {
            public AutoResetEvent wait;

            public ProcessThreadData(ref AutoResetEvent wait)
            {
                this.wait = wait;
            }
        }

        public struct Task
        {
            public TaskFunction function;
            public object Arg;

            public Task(TaskFunction function) { this.function = function; Arg = null; }
            public Task(TaskFunction function, object Arg) { this.function = function; this.Arg = Arg; }

        }

        private System.Collections.Concurrent.ConcurrentQueue<Task> tasks = new System.Collections.Concurrent.ConcurrentQueue<Task>();
        //private System.Collections.Concurrent.ConcurrentQueue<Task> LoadTasks = new System.Collections.Concurrent.ConcurrentQueue<Task>();

        static UInt32 ThreadCount => 8;


        public delegate void TaskFunction(object Context);

        private readonly List<ThreadPoolThreadData> threads = new List<ThreadPoolThreadData>();
        //private readonly List<ThreadPoolThreadData> permanentTaskThread = new List<ThreadPoolThreadData>();
        //private Thread LoadThread;

        private readonly BitArray availableThreads = new BitArray((int)ThreadCount, true);

        public Dispatcher()
        {
            for (int i = 0; i < ThreadCount; i++)
            {
                Thread thread = new Thread(Process);
                AutoResetEvent startProcessing = new AutoResetEvent(false);
                threads.Add(new ThreadPoolThreadData(thread, startProcessing));
                thread.Start(new ProcessThreadData(ref startProcessing));
            }
            //LoadThread = new Thread(LoadProcess);
            //LoadThread.Start();
        }

        public void Dispatch(Task task)
        {
            lock (this)
                for (int i = 0; i < ThreadCount; i++)
                    if (threads[i].thread.ThreadState == ThreadState.WaitSleepJoin)//availableThreads[i] == true)
                    {
                        tasks.Enqueue(task);
                        threads[i].wait.Set();
                        break;
                    }
        }


        //public void Dispatch(Task task, DispatchType type)
        //{
        //    lock (this)
        //    {
        //        switch (type)
        //        {
        //            case DispatchType.ShortTask:

        //                for (int i = 0; i < ThreadCount; i++)
        //                    if (availableThreads[i] == true)
        //                    {
        //                        tasks.Enqueue(task);
        //                        threads[i].wait.Set();
        //                        break;
        //                    }
        //                break;
        //            case DispatchType.PermanentTask:
        //                //Thread thread;
        //                //thread = new Thread(task.function);
        //                AutoResetEvent startProcessing = new AutoResetEvent(false);
        //                // permanentTaskThread.Add(new ThreadPoolThreadData(thread, startProcessing));
        //                //thread.Start(new ProcessThreadData(ref startProcessing));
        //                break;
        //            case DispatchType.LoadTask:
        //                LoadTasks.Enqueue(task);
        //                if (LoadThread.ThreadState == ThreadState.WaitSleepJoin)
        //                    LoadThread.Interrupt();
        //                break;
        //        }
        //    }
        //}

        public void Dispose()
        {
            foreach (var thread in threads)
                thread.thread.Abort();
            //LoadThread.Abort();
        }

        void Process(Object rawData)
        {
            ProcessThreadData data = (ProcessThreadData)rawData;
            while (true)
            {
                data.wait.WaitOne(100);
                Task task = new Task();
                tasks.TryDequeue(out task);
                task.function?.Invoke(task.Arg);
            }
        }

        //void LoadProcess(Object rawData)
        //{
        //    TimeSpan timeSpan = new TimeSpan(5, 0, 0);
        //    while (true)
        //    {
        //        try
        //        {
        //            Thread.Sleep(timeSpan);
        //        }
        //        catch (ThreadInterruptedException _)
        //        {
        //            while (!LoadTasks.IsEmpty)
        //                if (LoadTasks.TryDequeue(out Task task))
        //                    task.function(task.Arg);
                        
        //            _.HelpLink = "";
        //        }
        //    }
        //}


    }
}
