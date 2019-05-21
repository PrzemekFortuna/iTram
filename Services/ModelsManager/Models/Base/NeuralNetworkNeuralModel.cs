using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DBConnection.DTO;
using Newtonsoft.Json;

namespace Services.ModelsManager.Models.Base
{
    public abstract class NeuralModel : INeuralModel
    {
        private readonly IMapper _mapper;
        protected Type modelType;
        protected string URL;

        protected NeuralModel(IMapper mapper, Type type, string url)
        {
            _mapper = mapper;
            modelType = type;
            URL = url;
        }

        public async Task<NetworkResponse> Verify(IEnumerable<SensorsReadingDTO> sensorsReadings)
        {
            // send post and return reply

            var xd = _mapper.Map(sensorsReadings, typeof(IEnumerable<SensorsReadingDTO>), typeof(List<>).MakeGenericType(modelType));

            var json = JsonConvert.SerializeObject(xd);

            using (var client = new HttpClient())

            {
                var response = await client.PostAsync(URL, new StringContent(json, Encoding.UTF8, "application/json"));
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<NetworkResponse>(responseString);
            }  
        }
    }
}