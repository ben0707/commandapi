using System.Collections.Generic;
using System.Threading.Tasks;
using backendapi.Models;

namespace backendapi.Data.Interfaces
{
    public interface ICommander
    {
        Task<IEnumerable<Command>> GetAllCommands();
        Task<Command> GetCommandById(int id);

        Task CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        Task DeleteCommand(int id);
        Task<bool> SaveChanges();

    }
}