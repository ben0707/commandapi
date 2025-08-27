using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backendapi.Models;
using Microsoft.EntityFrameworkCore;

namespace backendapi.Data
{
    public class CommandContext : DbContext
    {

        public CommandContext(DbContextOptions<CommandContext> opt) : base(opt)
        {

        }

        public DbSet<Command> Commands { get; set; }
    }
}