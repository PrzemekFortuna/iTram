using AutoMapper;
using Services.ModelsManager.Models.Base;

namespace Services.ModelsManager.Models
{
    public class NeuralModelA : NeuralModel
    {
        public NeuralModelA(IMapper mapper) : base(mapper, typeof(ModelA), @"https://rocky-shore-34219.herokuapp.com/predict")
        {
        }
    }

    public class ModelA
    {
        public double? ax { get; set; }
        public double? ay { get; set; }
        public double? az { get; set; }
        public double? gx { get; set; }
        public double? gy { get; set; }
        public double? gz { get; set; }
    }
}