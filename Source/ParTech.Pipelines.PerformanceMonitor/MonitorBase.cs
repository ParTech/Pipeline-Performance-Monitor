using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParTech.Pipelines.PerformanceMonitor
{
    public abstract class MonitorBase
    {
        public static object FileLock = new object();

        private const string LogFileNameFormat = "performance.{0:yyyyMMdd}.txt";

        public string PipelineName { get; set; }

        public bool Enabled
        {
            get
            {
                return Sitecore.Configuration.Settings.GetBoolSetting("ParTech.PerformanceMonitor.Enabled", false);
            }
        }

        /// <summary>
        /// Constructor that sets the pipelinename.
        /// </summary>
        /// <param name="pipelineName"></param>
        public MonitorBase(string pipelineName)
        {
            PipelineName = pipelineName;
        }

        /// <summary>
        /// Inidicates that a pipeline has started processing.
        /// </summary>
        protected void Start()
        {
            WriteLog(string.Concat(PipelineName, " start"));
        }

        /// <summary>
        /// Inidicates that a pipeline has started processing.
        /// </summary>
        protected void Start(string info)
        {
            WriteLog(string.Concat(PipelineName, " [", info, "] start"));
        }

        /// <summary>
        /// Indicates that the last processor in a pipeline has been executed.
        /// </summary>
        protected void End()
        {
            WriteLog(string.Concat(PipelineName, " end"));
        }

        /// <summary>
        /// Indicates that the last processor in a pipeline has been executed.
        /// </summary>
        protected void End(string info)
        {
            WriteLog(string.Concat(PipelineName, " [", info, "] end"));
        }

        /// <summary>
        /// Add a line to the log buffer
        /// </summary>
        /// <param name="message"></param>
        private void WriteLog(string message)
        {
            if (!Enabled)
            {
                return;
            }

            string fileName = Path.Combine(Sitecore.Configuration.Settings.DataFolder, "logs", string.Format(LogFileNameFormat, DateTime.Now));
            string line = string.Format("[{0:HH:mm:ss.fff}] {1}\r\n", DateTime.Now, message);

            lock (FileLock)
            {
                File.AppendAllText(fileName, line);
            }
        }
    }
}