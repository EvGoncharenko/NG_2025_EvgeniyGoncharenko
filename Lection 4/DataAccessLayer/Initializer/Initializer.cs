using DataAccessLayer.DatabaseContext;

namespace DataAccessLayer.Initializer
{
    public static class Initializer
    {
        public static void InitializerDb(CrowdfundingDbContext ctx)
        {
            ctx.Database.EnsureCreated();
        }
    }
}
