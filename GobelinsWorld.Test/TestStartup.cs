namespace GobelinsWorld.Test
{
    using AutoMapper;
    using GobelinsWorld.Web.Infrastructure.Mapping;

    public class TestStartup
    {
        private static bool testInitialized = false;

        public static void Initialize()
        {
            if (!testInitialized)
            {
                Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
                testInitialized = true;
            }
        }
    }
}
