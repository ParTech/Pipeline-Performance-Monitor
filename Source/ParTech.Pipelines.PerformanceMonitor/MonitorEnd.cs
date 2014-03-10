namespace ParTech.Pipelines.PerformanceMonitor
{
    using Sitecore.Pipelines;
    using Sitecore.Pipelines.RenderField;

    /// <summary>
    /// Processor that is called as last processor in a pipeline.
    /// </summary>
    public class MonitorEnd : MonitorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorEnd"/> class.
        /// </summary>
        /// <param name="pipelineName"></param>
        public MonitorEnd(string pipelineName)
            : base(pipelineName)
        {
        }

        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(PipelineArgs args)
        {
            this.End();
        }

        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(RenderFieldArgs args)
        {
            this.End(args.FieldName);
        }
    }
}