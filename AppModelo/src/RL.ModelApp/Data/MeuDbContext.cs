using Microsoft.EntityFrameworkCore;

namespace RL.ModelApp.Data
{
    public class MeuDbContext : DbContext
    {

        public MeuDbContext(DbContextOptions options) : base(options)
        {
            
        }

    }
}
