using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DBConnection;
using DBConnection.DTO;
using DBConnection.Entities;
using Microsoft.Extensions.Options;
using Services.Helpers;

namespace Services
{
    public class BeaconTokenService
    {
        private int numberOfTokensToGenerate;
        private int tokenDurabilityInMinutes;
        private int tokenLength;
        private readonly TramContext _tramContext;
        private readonly IMapper _mapper;

        public BeaconTokenService(TramContext tramContext, IMapper mapper, IOptions<BeaconTokenSettings> beaconTokenOptions)
        {
            _tramContext = tramContext;
            _mapper = mapper;

            tokenDurabilityInMinutes = beaconTokenOptions.Value.TokenDurabilityInMinutes;
            tokenLength = beaconTokenOptions.Value.TokenLength;

            numberOfTokensToGenerate = 24 * 60 / beaconTokenOptions.Value.TokenDurabilityInMinutes;
        }
        public void RenewTokens(object state)
        {
            var nextStartTime = DateTime.Now;
            for (int i = 0; i < numberOfTokensToGenerate; i++)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                var arraySize = Math.Ceiling((double)tokenLength / 2);
                byte[] tokenArray = new byte[(int)arraySize];
                rand.NextBytes(tokenArray);
                if (tokenLength % 2 == 1)
                    tokenArray[0] = Convert.ToByte(tokenArray[0] & 0xF | (0 << 4)); // sets first half of byte to 0

                var tokenDurability = TimeSpan.FromMinutes(tokenDurabilityInMinutes);
                BeaconToken beaconToken = new BeaconToken(tokenArray, nextStartTime, nextStartTime + tokenDurability);
                _tramContext.Add(beaconToken);
                nextStartTime += tokenDurability + TimeSpan.FromMilliseconds(1);
            }

            _tramContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BeaconTokenDto>> GetActiveBeaconTokens()
        {
            var ret = await Task.FromResult(_tramContext.BeaconTokens.OrderByDescending(x => x.ValidFrom).Take(numberOfTokensToGenerate));
            return _mapper.Map<IEnumerable<BeaconTokenDto>>(ret);
        }
    }
}