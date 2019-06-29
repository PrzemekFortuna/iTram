using System.Collections.Generic;

namespace DBConnection.DTO
{
    public class AmIInTramDto
    {
        public IEnumerable<SensorsReadingUnitsDTO> SensorsReadings { get; set; }
        public bool UseNeuralNetwork { get; set; }
        public bool UseLocation { get; set; } = true;
    }
}