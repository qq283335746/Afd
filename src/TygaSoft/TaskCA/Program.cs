using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Text;
using TygaSoft.TaskProcessor;

namespace TygaSoft.TaskCA
{
    class Program
    {
        static void Main(string[] args)
        {
            OnStart();

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("终止服务请按任意键！");
            Console.ReadLine();
        }

        private static void OnStart()
        {
            string afdRunQueue = ConfigurationManager.AppSettings["AfdRunQueue"];
            if (!MessageQueue.Exists(afdRunQueue)) MessageQueue.Create(afdRunQueue, true);

            BaseTask.Run();
        }
    }
}
