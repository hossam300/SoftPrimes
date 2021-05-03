using System;
using System.Collections.Generic;
using System.Text;

namespace SoftPrimes.BLL.BaseObjects.ReSoftPrimesitoriesInterfaces
{
    /// <summary>
    /// Defines the interfaces for <see cref="IReSoftPrimesitory{TEntity}"/> interfaces.
    /// </summary>
    public interface IReSoftPrimesitoryFactory
    {
        /// <summary>
        /// Gets the specified reSoftPrimesitory for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="hasCustomReSoftPrimesitory"><c>True</c> if providing custom reSoftPrimesitry</param>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IReSoftPrimesitory{TEntity}"/> interface.</returns>
        IBaseRepository<TEntity> GetRepository<TEntity>(bool hasCustomReSoftPrimesitory = false) where TEntity : class;
    }
}
