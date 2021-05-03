using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using SoftPrimes.BLL.BaseObjects.ReSoftPrimesitoriesInterfaces;
using System.ComponentModel.DataAnnotations;

namespace SoftPrimes.BLL.BaseObjects
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IUnitOfWork"/> and <see cref="IUnitOfWork{TContext}"/> interface.
    /// </summary>
    /// <typeparam name="TContext">The type of the db context.</typeparam>
    public class UnitOfWork<TContext> : IReSoftPrimesitoryFactory, IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;
        private bool disSoftPrimesed = false;
        private Dictionary<Type, object> reSoftPrimesitories;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the db context.
        /// </summary>
        /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
        public TContext DbContext => _context;

        /// <summary>
        /// Changes the database name. This require the databases in the same machine. NOTE: This only work for MySQL right now.
        /// </summary>
        /// <param name="database">The database name.</param>
        /// <remarks>
        /// This only been used for supporting multiple databases in the same model. This require the databases in the same machine.
        /// </remarks>
        //public void ChangeDatabase(string database)
        //{
        //    var connection = _context.Database.GetDbConnection();
        //    if (connection.State.HasFlag(ConnectionState.Open))
        //    {
        //        connection.ChangeDatabase(database);
        //    }
        //    else
        //    {
        //        var connectionString = Regex.Replace(connection.ConnectionString.Replace(" ", ""), @"(?<=[Dd]atabase=)\w+(?=;)", database, RegexOptions.Singleline);
        //        connection.ConnectionString = connectionString;
        //    }

        //    // Following code only working for mysql.
        //    IEnumerable<IEntityType> items = _context.Model.GetEntityTypes();
        //    foreach (var item in items)
        //    {
        //        if (item.Relational() is RelationalEntityTypeAnnotations extensions)
        //        {
        //            extensions.Schema = database;
        //        }
        //    }
        //}

        /// <summary>
        /// Gets the specified reSoftPrimesitory for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="hasCustomReSoftPrimesitory"><c>True</c> if providing custom reSoftPrimesitry</param>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IReSoftPrimesitory{TEntity}"/> interface.</returns>
        public IBaseRepository<TEntity> GetRepository<TEntity>(bool hasCustomReSoftPrimesitory = false) where TEntity : class
        {
            if (reSoftPrimesitories == null)
            {
                reSoftPrimesitories = new Dictionary<Type, object>();
            }

            // what's the best way to support custom reSoftPrimesity?
            if (hasCustomReSoftPrimesitory)
            {
                var customRepo = _context.GetService<IBaseRepository<TEntity>>();
                if (customRepo != null)
                {
                    return customRepo;
                }
            }

            var type = typeof(TEntity);
            if (!reSoftPrimesitories.ContainsKey(type))
            {
                reSoftPrimesitories[type] = new BaseRepository<TEntity>(_context);
            }

            return (IBaseRepository<TEntity>)reSoftPrimesitories[type];
        }

      
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if save changes ensure auto record the change history.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            var entities = from e in this.DbContext.ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }
            return _context.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the database.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if save changes ensure auto record the change history.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous save operation. The task result contains the number of state entities written to database.</returns>
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            //if (ensureAutoHistory)
            //{
            //    _context.EnsureAutoHistory();
            //}

            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves all changes made in this context to the database with distributed transaction.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if save changes ensure auto record the change history.</param>
        /// <param name="unitOfWorks">An optional <see cref="IUnitOfWork"/> array.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous save operation. The task result contains the number of state entities written to database.</returns>
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks)
        {
            using (var ts = new TransactionScope())
            {
                var count = 0;
                foreach (var unitOfWork in unitOfWorks)
                {
                    count += await unitOfWork.SaveChangesAsync(ensureAutoHistory);
                }

                count += await SaveChangesAsync(ensureAutoHistory);

                ts.Complete();

                return count;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disSoftPrimesing">The disSoftPrimesing.</param>
        protected virtual void Dispose(bool disSoftPrimesing)
        {
            if (!disSoftPrimesed)
            {
                if (disSoftPrimesing)
                {
                    // clear reSoftPrimesitories
                    if (reSoftPrimesitories != null)
                    {
                        reSoftPrimesitories.Clear();
                    }

                    // disSoftPrimese the db context.
                    _context.Dispose();
                }
            }

            disSoftPrimesed = true;
        }

        public void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback)
        {
            _context.ChangeTracker.TrackGraph(rootEntity, callback);
        }
    }
}
