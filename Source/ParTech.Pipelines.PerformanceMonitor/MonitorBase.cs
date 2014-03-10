namespace ParTech.Pipelines.PerformanceMonitor
{
    using System;
    using System.IO;
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;

    /// <summary>
    /// Base class for monitoring processors.
    /// </summary>
    public abstract class MonitorBase
    {
        /// <summary>
        /// The file lock
        /// </summary>
        private static readonly object FileLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorBase"/> class.
        /// </summary>
        /// <param name="pipelineName">Name of the pipeline.</param>
        protected MonitorBase(string pipelineName)
        {
            this.PipelineName = pipelineName;
        }

        /// <summary>
        /// Gets or sets the name of the pipeline.
        /// </summary>
        public string PipelineName { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="MonitorBase"/> is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return Settings.GetBoolSetting("ParTech.Pipelines.PerformanceMonitor.Enabled", false); }
        }

        /// <summary>
        /// Gets the log file name format.
        /// </summary>
        public string LogFileNameFormat
        {
            get { return Settings.GetSetting("ParTech.Pipelines.PerformanceMonitor.LogFileNameFormat", "performance.*.txt"); }
        }

        /// <summary>
        /// Inidicates that a pipeline has started processing.
        /// </summary>
        protected void Start()
        {
            Profiler.StartOperation(this.PipelineName);
            this.WriteLog(string.Concat(this.PipelineName, " start"));
        }

        /// <summary>
        /// Inidicates that a pipeline has started processing.
        /// </summary>
        /// <param name="description">Extra description of the pipeline (e.g. the field that is being rendered in the renderField pipeline)</param>
        protected void Start(string description)
        {
            string d = string.Concat(this.PipelineName, " [", description, "]");

            Profiler.StartOperation(d);
            this.WriteLog(string.Concat(d, " start"));
        }

        /// <summary>
        /// Indicates that the last processor in a pipeline has been executed.
        /// </summary>
        protected void End()
        {
            Profiler.EndOperation(this.PipelineName);
            this.WriteLog(string.Concat(this.PipelineName, " end"));
        }

        /// <summary>
        /// Indicates that the last processor in a pipeline has been executed.
        /// </summary>
        /// <param name="description">Extra description of the pipeline (e.g. the field that is being rendered in the renderField pipeline)</param>
        protected void End(string description)
        {
            string d = string.Concat(this.PipelineName, " [", description, "]");

            Profiler.EndOperation(d);
            this.WriteLog(string.Concat(d, " end"));
        }

        /// <summary>
        /// Add a line to the log buffer
        /// </summary>
        /// <param name="message"></param>
        private void WriteLog(string message)
        {
            if (!this.Enabled)
            {
                return;
            }

            string fileName = this.LogFileNameFormat.Replace("*", DateTime.Today.ToString("yyyyMMdd"));
            string filePath = Path.Combine(Settings.LogFolder, fileName);
            string logLine = string.Format("[{0:HH:mm:ss.fff}] {1}\r\n", DateTime.Now, message);

            lock (FileLock)
            {
                File.AppendAllText(filePath, logLine);
            }
        }
    }
}