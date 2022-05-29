using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Action[] actions = new Action[2];
            actions[0] = () => Task1("1");
            actions[1] = Task2;

            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 3;
            options.TaskScheduler = new MyTaskScheduler();

            //TODO Как сделать так чтобы основной поток не приостанавливался?
            Task task = new Task((() =>
            {
                Parallel.Invoke(options, actions);
            }));
            task.Start();

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(20);
                Console.WriteLine("M");
            }
            Console.ReadKey();
        }

        static void Task1(string input)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(20);
                Console.WriteLine("\t" + input);
            }
        }

        static void Task2()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(20);
                Console.WriteLine("\t\t2");
            }
        }
    }

    internal class MyTaskScheduler : TaskScheduler
    {
        protected override void QueueTask(Task task)
        {
            ThreadPool.QueueUserWorkItem((_) => TryExecuteTask(task), null);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return Enumerable.Empty<Task>();
        }
    }
}