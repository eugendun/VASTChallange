using System.Data.Entity;

namespace VASTChallange.Core.Entities
{
    public class VASTChallangeContext : DbContext
    {
        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<CommData> CommData { get; set; }

        public DbSet<Movement> Movement { get; set; }

        public VASTChallangeContext()
            : base("VASTChallange")
        {

        }
    }
}
