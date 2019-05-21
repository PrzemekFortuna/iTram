using System;
using System.Collections.Generic;
using System.Linq;
using Services.ModelsManager.Models.Base;

namespace Services.ModelsManager.Metrics
{
    public class HighestCertainty : IMetric
    {
        public bool Calculate(IEnumerable<NetworkResponse> replies)
        {
            return replies.OrderByDescending(x => x.certainty).First().isInTram;
        }
    }
}