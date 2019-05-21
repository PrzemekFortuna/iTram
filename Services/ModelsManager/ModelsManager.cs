using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DBConnection.DTO;
using DBConnection.Entities;
using DBConnection.Entities.Sensors;
using Services.ModelsManager.Metrics;
using Services.ModelsManager.Models.Base;

namespace Services.ModelsManager
{
    public class ModelsManager
    {
        private readonly IEnumerable<INeuralModel> _neuralModels;
        private readonly IMapper _mapper;
        private readonly IMetric _metric;

        public ModelsManager(IEnumerable<INeuralModel> neuralModels, IMapper mapper, IMetric metric)
        {
            _neuralModels = neuralModels;
            _mapper = mapper;
            _metric = metric;
        }

        public async Task<bool> IsInTram(IEnumerable<SensorsReading> sensorsReading)
        {
            var replies = await GetReplies(sensorsReading);
            return _metric.Calculate(replies);
        }

        private async Task<IEnumerable<NetworkResponse>> GetReplies(IEnumerable<SensorsReading> sensorsReading)
        {
            var ret = new List<NetworkResponse>();
            foreach (var method in _neuralModels)
            {
                ret.Add(await method.Verify(_mapper.Map<IEnumerable<SensorsReadingDTO>>(sensorsReading)));
            }

            return ret;
        }
    }
}
