﻿namespace Improving.Highway.Data.Scope.Concurrency
{
    using System.Data.Entity.Core;
    using System.Linq;
    using FluentValidation;
    using MediatR;

    public abstract class CheckConcurrency<TEntity, TRes>
        : CheckConcurrency<TEntity, TRes, UpdateResource<TRes, int>>
        where TEntity : IEntity, IRowVersioned
        where TRes : Resource<int>
    {
    }

    public abstract class CheckConcurrency<TEntity, TRes, TAction>
        : AbstractValidator<TAction>
        where TEntity : IEntity, IRowVersioned
        where TAction : UpdateResource<TRes, int>
        where TRes    : Resource<int>
    {
        protected CheckConcurrency()
        {
            RuleFor(c => Entity)
                .NotNull()
                .OverridePropertyName(typeof (TEntity).Name);

            Unless(c => Entity == null || c.Resource?.RowVersion == null, () =>
            {
                RuleFor(c => Entity)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .Must(BeExpectedVersion)
                    .WithName(typeof(TEntity).Name)
                    .WithMessage("{0} is in an inconsitent state.",
                        e => typeof(TEntity).Name);
                });
        }

        public TEntity Entity { get; set; }

        private static bool BeExpectedVersion(TAction action, TEntity entity)
        {
            var resource = action.Resource;

            if (entity.Id != resource.Id)
                return false;

            if (resource.RowVersion == null ||
                resource.RowVersion.SequenceEqual(entity.RowVersion))
                return true;

            throw new OptimisticConcurrencyException(
                $"Concurrency exception detected for {entity.GetType()} with id {entity.Id}.");
        }
    }
}
