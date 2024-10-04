using LibraryDAL;
using LibraryDAL.Entities;
using LibraryDAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryHillelEF
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var menu = new ProgramMenu();
            await menu.MainMenu();
        }
    }
}
