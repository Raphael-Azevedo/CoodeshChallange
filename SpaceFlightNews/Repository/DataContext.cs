using Microsoft.EntityFrameworkCore;
using SpaceFlightNews.Models;

namespace SpaceFlightNews.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Launch> Launches { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            modelBuilder.Entity<Launch>().HasKey(x => x.Id);

        
            modelBuilder.Entity<Launch>().Property(x => x.Provider).IsRequired();

            modelBuilder.Entity<Launch>()
                        .HasOne<Article>(o => o.Article)
                        .WithMany(c => c.Launches)
                        .HasForeignKey(s => s.ArticleId);

            modelBuilder.Entity<Event>().HasKey(x => x.Id);

 
            modelBuilder.Entity<Event>().Property(x => x.Provider).IsRequired();

            modelBuilder.Entity<Event>()
                     .HasOne<Article>(o => o.Article)
                     .WithMany(c => c.Events)
                     .HasForeignKey(s => s.ArticleId);


            modelBuilder.Entity<Article>().HasKey(x => x.Id);

 
            modelBuilder.Entity<Article>().Property(x => x.Title).IsRequired();


            modelBuilder.Entity<Article>().Property(x => x.Url).IsRequired();


            modelBuilder.Entity<Article>().Property(x => x.ImageUrl).IsRequired();


            modelBuilder.Entity<Article>().Property(x => x.NewsSite).IsRequired();

            modelBuilder.Entity<Article>().Property(x => x.Summary).HasMaxLength(1000);
            modelBuilder.Entity<Article>().Property(x => x.Summary).IsRequired();

            modelBuilder.Entity<Article>().Property(x => x.PublishedAt).HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Article>().Property(x => x.Featured).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }

}