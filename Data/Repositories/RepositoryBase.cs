using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public abstract class RepositoryBase : IDisposable
    {
        protected BooksContext context;

        protected bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
