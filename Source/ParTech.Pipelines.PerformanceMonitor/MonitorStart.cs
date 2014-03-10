namespace ParTech.Pipelines.PerformanceMonitor
{
    using Sitecore.Pipelines;
    using Sitecore.Pipelines.RenderField;

    /// <summary>
    /// Processor that is called as first processor in a pipeline.
    /// </summary>
    public class MonitorStart : MonitorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorStart"/> class.
        /// </summary>
        /// <param name="pipelineName"></param>
        public MonitorStart(string pipelineName)
            : base(pipelineName)
        {
        }

        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(PipelineArgs args)
        {
            this.Start();
        }

        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(RenderFieldArgs args)
        {
            this.Start(args.FieldName);
        }
    }
}