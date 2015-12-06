namespace PrettyCats.Database
{
    using System.Data.Entity;

    public partial class Storage : DbContext
    {
        public Storage()
#if DEBUG
            : base("DBConnectionDebug")
#else
            :base("DBConnection")
#endif
        {
        }

        public virtual DbSet<Owners> Owners { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<PetBreeds> PetBreeds { get; set; }
        public virtual DbSet<Pets> Pets { get; set; }
        public virtual DbSet<Pictures> Pictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owners>()
                .HasMany(e => e.Pets)
                .WithRequired(e => e.Owners)
                .HasForeignKey(e => e.OwnerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pages>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<PetBreeds>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PetBreeds>()
                .HasMany(e => e.Pets)
                .WithRequired(e => e.PetBreeds)
                .HasForeignKey(e => e.BreedID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pets>()
                .Property(e => e.UnderThePictureText)
                .IsUnicode(false);

            modelBuilder.Entity<Pets>()
                .HasMany(e => e.Pets1)
                .WithOptional(e => e.Pets2)
                .HasForeignKey(e => e.MotherID);

            modelBuilder.Entity<Pets>()
                .HasMany(e => e.Pets11)
                .WithOptional(e => e.Pets3)
                .HasForeignKey(e => e.FatherID);

            modelBuilder.Entity<Pictures>()
                .Property(e => e.Image)
                .IsUnicode(false);
        }
    }
}
