namespace SchedulePlan.Application.Common.Models
{
    public class AppConfigurations
    {
        public JwtOptions Jwt { get; set; }
    }

    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireYear { get; set; }
        public string Key { get; set; }

    }

}