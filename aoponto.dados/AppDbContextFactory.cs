using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace aoponto.dados
{
    public static class AppDbContextFactory 
    {
        public static AppDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql("Server=aoponto.mysql.uhserver.com;DataBase=aoponto;Uid=aoponto;Pwd=angus@12");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
