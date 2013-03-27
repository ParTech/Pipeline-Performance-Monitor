using Sitecore.Configuration;
using Sitecore.Diagnostics;
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

        public string PipelineName { get; set; }

        public bool Enabled
        {
            get
            {
                return Settings.GetBoolSetting("ParTech.PerformanceMonitor.Enabled", false);
            }
        }

        public string LogFileNameFormat
        {
            get
            {
                return Settings.GetSetting("ParTech.PerformanceMonitor.LogFileNameFormat", "performance.*.txt");
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
            Profiler.StartOperation(PipelineName);
            WriteLog(string.Concat(PipelineName, " start"));
        }

        /// <summary>
        /// Inidicates that a pipeline has started processing.
        /// </summary>
        /// <param name="description">Extra description of the pipeline (e.g. the field that is being rendered in the renderField pipeline)</param>
        protected void Start(string description)
        {
            string d = string.Concat(PipelineName, " [", description, "]");

            Profiler.StartOperation(d);
            WriteLog(string.Concat(d, " start"));
        }

        /// <summary>
        /// Indicates that the last processor in a pipeline has been executed.
        /// </summary>
        protected void End()
        {
            Profiler.EndOperation(PipelineName);
            WriteLog(string.Concat(PipelineName, " end"));
        }

        /// <summary>
        /// Indicates that the last processor in a pipeline has been executed.
        /// </summary>
        /// <param name="description">Extra description of the pipeline (e.g. the field that is being rendered in the renderField pipeline)</param>
        protected void End(string description)
        {
            string d = string.Concat(PipelineName, " [", description, "]");

            Profiler.EndOperation(d);
            WriteLog(string.Concat(d, " end"));
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

            string fileName = LogFileNameFormat.Replace("*", DateTime.Today.ToString("yyyyMMdd"));
            string filePath = Path.Combine(Settings.LogFolder, fileName);
            string logLine = string.Format("[{0:HH:mm:ss.fff}] {1}\r\n", DateTime.Now, message);

            lock (FileLock)
            {
                File.AppendAllText(filePath, logLine);
            }
        }
    }
}