
using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Boil an Egg", Line = "Boil Water", Platform = "Kettle and Pan" };
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command { Id = 0, HowTo = "Boil an Egg", Line = "Boil Water", Platform = "Kettle and Pan" },
                new Command { Id = 1, HowTo = "Cut bread", Line = "Get a Knife", Platform = "Knife and chopping bread" },
                new Command { Id = 2, HowTo = "Make Cup of Tea", Line = "Place Tea Bag in Cup", Platform = "Kettle and Cup" }
            };
            return commands ;
        }

        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}