namespace Repo;

public sealed class CommitTreeContext : DbContext, ICommitTreeContext
{
    public CommitTreeContext(DbContextOptions<CommitTreeContext> options) : base(options) {}

    public DbSet<CommitData> CommitData => Set<CommitData>();

  //NOTE for later use, incase we need to specify type conversions susch as Enums.
  protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<CommitData>()
            .HasKey(h => h.HashCode);
    }
}