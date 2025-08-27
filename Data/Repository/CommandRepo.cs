using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backendapi.Data.Interfaces;
using backendapi.Models;
using Microsoft.EntityFrameworkCore;

namespace backendapi.Data.Repository
{
    public class CommandRepo : ICommander
    {
        private readonly CommandContext _context;
        public CommandRepo(CommandContext context)
        {
            _context = context;
        }

        public async Task CreateCommand(Command cmd)
        {
            if (cmd == null) throw new ArgumentNullException(nameof(cmd));

            await _context.Commands.AddAsync(cmd);

        }

        public async Task DeleteCommand(int id)
        {

            var command = await GetCommandById(id);

            if (command == null) throw new KeyNotFoundException("command not found ");

            _context.Commands.Remove(command);
        }

        public async Task<IEnumerable<Command>> GetAllCommands() => await _context.Commands.ToListAsync();

        public async Task<Command> GetCommandById(int id) => await _context.Commands.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<bool> SaveChanges() => await _context.SaveChangesAsync() > 0;
        public void UpdateCommand(Command cmd)
        {
            //Nothing
        }


    }
}