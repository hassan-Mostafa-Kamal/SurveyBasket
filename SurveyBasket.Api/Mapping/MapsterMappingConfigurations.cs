using Mapster;
using SurveyBasket.Api.DTOs;

namespace SurveyBasket.Api.Mapping
{
    public class MapsterMappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // config.NewConfig<Poll, PollDto>();
        }
    }
}
