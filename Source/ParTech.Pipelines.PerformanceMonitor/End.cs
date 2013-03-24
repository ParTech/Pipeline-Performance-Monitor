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
    public class End : MonitorBase
    {
        public End(string pipelineName)
            : base(pipelineName)
        {
        }

        public void Process(PipelineArgs args)
        {
            End();
        }

        public void Process(RenderFieldArgs args)
        {
            End(args.FieldName);
        }
    }
}