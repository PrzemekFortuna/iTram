using System.Collections.Generic;
using System.Threading.Tasks;
using DBConnection.DTO;

namespace Services.ModelsManager.Models.Base
{
    public interface INeuralModel
    {
        Task<NetworkResponse> Verify(IEnumerable<SensorsReadingDTO> sensorsReadings);
    }
}