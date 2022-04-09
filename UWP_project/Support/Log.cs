using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroLog;

namespace UWP_project.Support
{
    internal class Log
    {
        //stworzenie singleton dla logów
        static object logLock = new object();
        static Log log = null;
        private ILogger MetroLogger;
        LinkedList<string> Queue = new LinkedList<string> ();

        public Log()
        {
            MetroLogger = LogManagerFactory.DefaultLogManager.GetLogger<App>();
        }

        public static void InitializeMetroLog()
        {
            bool debug = false;
#if DEBUG
            debug = true;
#endif
            if (debug || Utility.LoadSettings<bool?>("debug") == true)
            {
                LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, new MetroLog.Targets.StreamingFileTarget());
            }
            GlobalCrashHandler.Configure();
        }

        public static Log Instance
        {
            get
            {
                lock(logLock)
                {
                    if (log == null)
                    {
                        log = new Log();
                    }
                    return log;
                }
            }
        }
        public static string NextMessage
        {
            get
            {
                if (Instance.Queue.Count== 0)
                {
                    return null;
                }
                string message = Instance.Queue.First.Value;
                Instance.Queue.RemoveFirst();
                return message;
            }
        }

        public static void debug(object context, string message)
        {
            string debug = "D:" + context.ToString() + ": " + message;
            Instance.Queue.AddLast(debug);
            Instance.MetroLogger.Debug(debug);
        }

        public static void info(object context, string message)
        {
            string info = "I:" + context.ToString() + ": " + message;
            Instance.Queue.AddLast(info);
            Instance.MetroLogger.Info(info);
        }

        public static void err(object context, string message)
        {
            string err = "E:" + context.ToString() + ": " + message;
            Instance.Queue.AddLast(err);
            Instance.MetroLogger.Error(err);
        }


    }
}
