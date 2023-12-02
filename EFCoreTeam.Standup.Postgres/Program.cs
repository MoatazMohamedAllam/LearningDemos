using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

await using var ctx = new BlogContext();
await ctx.Database.EnsureDeletedAsync();
await ctx.Database.EnsureCreatedAsync();


//var tags = new[] { "foo", "bla" };

//_ = await ctx.Blogs.Include(x=>x.Tags).ToListAsync();
//_ = await ctx.Blogs.Where(x => x.Tags.Any(t => tags.Contains(t))).ToListAsync();
//_ = await ctx.Blogs.Where(x => x.Tags.Contains("bla")).ToListAsync();
_ = await ctx.Blogs.Where(x => x.Details.Views == 1).ToListAsync();

//await ctx.Blogs.AddAsync(new Blog{ Name = "test"});
//await ctx.SaveChangesAsync();

public class BlogContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
                //.UseSqlServer(@"Server=(localdb)\MSSqlLocalDb;Database=MyDb;Trusted_Connection=True")
                .UseNpgsql(@"Host=localhost;Database=Test_Db;Username=postgres;Password=2468")
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //this for type converter (when i sent to db the tags will be comma seprated and when i read from db it will be splited array by comma)
        modelBuilder.Entity<Blog>(b =>
        {
            //b.Property(x => x.Tags)
            //.HasConversion(
            //    t => string.Join(",", t),
            //    t => t.Split(',', StringSplitOptions.None).ToArray());

            //b.HasIndex(x => x.Tags).HasMethod("GIN"); // this to create generalized inverted idx on the tags col (this is for psotgres db)
        });
            
    }
}

public class Blog
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    //public List<Tag> Tags { get; set; }
    public string[] Tags { get; set; }
    //public IPAddress IPAddress { get; set; }
    [Column(TypeName ="jsonb")]
    public Details Details { get; set; }
}

public class Details
{
    public int Views { get; set; }
}

//public class Tag
//{
//    public int Id { get; set; }
//    public string TagName { get; set; }
//    public List<Blog> Blogs { get; set; }
//}