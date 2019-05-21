using System;
using System.Collections.Generic;
using System.Text;
using Services.ModelsManager.Models.Base;

namespace Services.ModelsManager.Metrics
{
    public interface IMetric
    {
        bool Calculate(IEnumerable<NetworkResponse> replies);
    }
}
