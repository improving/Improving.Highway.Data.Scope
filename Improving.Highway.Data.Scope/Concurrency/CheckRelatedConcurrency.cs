namespace Improving.Highway.Data.Scope.Concurrency
{
    using System.Data.Entity.Core;
    using System.Linq;
    using FluentValidation;
    using MediatR;

    public abstract class CheckRelatedConcurrency<TEntity, TRel>
        : AbstractValidator<RelationshipAction<TRel, int>>
        where TEntity : IEntity, IRowVersioned
        where TRel : Resource<int>
    {
        protected CheckRelatedConcurrency()
        {
            RuleFor(c => Entity)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .Must(MatchEntityVersion)
                .OverridePropertyName(typeof(TEntity).Name)
                .WithMessage("{0} with id {1} not expected.",
                    e => Entity.GetType().Name, e => Entity.Id);
        }

        public TEntity Entity { get; set; }

        private static bool MatchEntityVersion(
             RelationshipAction<TRel, int> action, TEntity entity)
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

    public abstract class CheckRelatedConcurrency<TEntity, TRelated, TRel>
        : AbstractValidator<UpdateRelationship<TRel, int>>
        where TEntity  : IEntity, IRowVersioned
        where TRelated : IEntity, IRowVersioned
        where TRel     : Resource<int>
    {
        protected CheckRelatedConcurrency()
        {
            RuleFor(c => Entity)
                .NotNull()
                .OverridePropertyName(typeof (TEntity).Name);

            Unless(c => Entity == null, () =>
            {
                RuleFor(c => Related)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .Must(HaveRelationship)
                    .Must(MatchRelatedVersion)
                    .OverridePropertyName(typeof (TRelated).Name)
                    .WithMessage("Related {0} with id {1} not expected.",
                        e => Related.GetType().Name, e => Related.Id);
            });
        }

        public TEntity  Entity  { get; set; }
        public TRelated Related { get; set; }

        protected abstract TRelated GetRelated(
            TEntity entity, UpdateRelationship<TRel, int> action);

        private bool HaveRelationship(UpdateRelationship<TRel, int> message, TRelated related)
        {
            Related = GetRelated(Entity, message);
            Env.Use(Related);
            return Related != null;
        }

        private static bool MatchRelatedVersion(
            UpdateRelationship<TRel, int> action, TRelated relatedEntity)
        {
            var related = action.Related;

            if (relatedEntity.Id != related?.Id)
                return false;

            if (related.RowVersion == null ||
                related.RowVersion.SequenceEqual(relatedEntity.RowVersion))
                return true;

            throw new OptimisticConcurrencyException(
                $"Concurrency exception detected for {relatedEntity.GetType()} with id {relatedEntity.Id}.");
        }
    }
}
