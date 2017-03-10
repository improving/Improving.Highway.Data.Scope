namespace Improving.Highway.Data.Scope.Concurrency
{
    using System;
    using System.Data.Entity.Core;
    using System.Linq;
    using Scope;

    public static class ConcurrencyExtensions
    {
        public static bool CheckVersion<T>(
            this T entity, byte[] version, bool require = true)
            where T : IEntity, IRowVersioned
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var matched = version == null || version.SequenceEqual(entity.RowVersion);
            if (!matched && require)
            {
                throw new OptimisticConcurrencyException(
                    $"Concurrency exception detected for {entity.GetType()} with id {entity.Id}.");
            }

            return matched;
        }
    }
}
