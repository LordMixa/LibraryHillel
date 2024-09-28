using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private LibraryContext db;
        private bool disposed = false;
        public UnitOfWork()
        {
            db = new LibraryContext();
        }

        public void Save()
        {
            db.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public DbContext Context => db;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
