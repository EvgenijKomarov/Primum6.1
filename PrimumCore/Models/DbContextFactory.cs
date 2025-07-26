using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimumCore.Models
{
    public class DbContextFactory<TContext> : IDisposable where TContext : IPrimumContext
    {
        private readonly DbContextOptions _options;
        private readonly SemaphoreSlim _saveChangesSemaphore;
        private bool _disposed;

        public DbContextFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string cannot be null or empty.", nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder()
                .UseSqlite(connectionString);

            _options = optionsBuilder.Options;
            _saveChangesSemaphore = new SemaphoreSlim(1, 1);
        }

        public virtual TContext CreateDbContext()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DbContextFactory<TContext>));

            var context = (TContext)Activator.CreateInstance(typeof(TContext), _options);
            return context;
        }

        public virtual async Task<int> SafeSaveChangesAsync(TContext context, CancellationToken cancellationToken = default)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (_disposed)
                throw new ObjectDisposedException(nameof(DbContextFactory<TContext>));

            await _saveChangesSemaphore.WaitAsync(cancellationToken);
            try
            {
                return await context.SaveChangesAsync(cancellationToken);
            }
            finally
            {
                _saveChangesSemaphore.Release();
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _saveChangesSemaphore.Dispose();
            _disposed = true;
        }
    }
}
