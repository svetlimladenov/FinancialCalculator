using Api.Models;
using AutoMapper;
using Contracts;

namespace Api.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateCreditModel, CreateCreditDTO>();

            CreateMap<BonusPointsModel, BonusPointsDTO>();

            CreateMap<RefinanceCreditModel, RefinanceCreditDTO>();
        }
    }
}
