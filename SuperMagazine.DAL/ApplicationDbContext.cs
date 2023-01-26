using Microsoft.EntityFrameworkCore;
using SuperMagazine.Domain.Entities;
using SuperMagazine.Domain.Enums;

public class ApplicationDbContext : DbContext
{
	public DbSet<User> Users { get; set; } = null!;

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
		Database.EnsureCreated();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>().HasData(
			new User
			{
				Id = Guid.NewGuid(),
				Name = "Danila",
				Surname = "Semeshkin",
				Age = 21,
				Login = @"semyoshkin8v@mail.ru",
				Password = "admin228",
				ProfileImageUrl = "https://res.cloudinary.com/drjcc70gg/image/upload/v1671448065/%D1%81%D0%B0%D0%BC%D0%BE%D0%BB%D0%B5%D1%82_jztgzm.jpg",
				Role = Role.Admin
			});
	}
}

