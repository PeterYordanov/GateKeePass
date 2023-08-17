using AutoMapper;

namespace GateKeePass.Core
{
    public static class QuickMapper
    {
        public static Destination Map<Source, Destination>(Source obj)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Source, Destination>();
            });

            IMapper mapper = config.CreateMapper();

            var mapped = mapper.Map<Destination>(obj);

            return mapped;
        }
    }
}
