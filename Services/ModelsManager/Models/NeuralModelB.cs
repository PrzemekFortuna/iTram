using AutoMapper;
using Services.ModelsManager.Models.Base;

namespace Services.ModelsManager.Models
{
    public class NeuralModelB : NeuralModel
    {
        public NeuralModelB(IMapper mapper) : base(mapper, typeof(ModelB), @"https://rocky-shore-34219.herokuapp.com/predict/adam")
        {
        }
    }

    public class ModelB
    {
        public double? ax { get; set; }
        public double? ay { get; set; }
        public double? az { get; set; }
        public double? gx { get; set; }
        public double? gy { get; set; }
        public double? gz { get; set; }
    }
}
