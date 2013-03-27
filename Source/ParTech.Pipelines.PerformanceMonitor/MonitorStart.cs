using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Jobs;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderField;

namespace ParTech.Pipelines.PerformanceMonitor
{
    public class MonitorStart : MonitorBase
    {
        public MonitorStart(string pipelineName)
            : base(pipelineName)
        {
        }

        public void Process(PipelineArgs args)
        {
            Start();
        }

        public void Process(RenderFieldArgs args)
        {
            Start(args.FieldName);
        }
    }
}