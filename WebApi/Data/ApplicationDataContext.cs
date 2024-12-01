using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class ApplicationDataContext : DbContext
{
    public ApplicationDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Message> Messages { get; set; }
}
