using ClienteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Data
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> opt) : base (opt)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
