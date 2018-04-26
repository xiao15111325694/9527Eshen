using AutoMapper;

namespace MVCEchartsManager.ProfileMVC
{
    public class AutoMapper
    {
        public static void Start()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<SourceProfile>();
            });
        }
    }
}