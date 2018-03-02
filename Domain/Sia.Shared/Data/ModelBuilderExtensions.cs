using Sia.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Microsoft.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        public static void AddManyToManyAssociation<TLeft, TAssociation, TRight>(this ModelBuilder builder
            , Expression<Func<TLeft, IEnumerable<TAssociation>>> associationsFromLeft
            , Expression<Func<TRight, IEnumerable<TAssociation>>> associationsFromRight
            )
            where TAssociation : BidrectionalAssociation<TLeft, TRight>
            where TLeft : class, IEntity
            where TRight : class, IEntity
        {
            builder.Entity<TAssociation>()
                .HasOne(assoc => assoc.Left)
                .WithMany(associationsFromLeft)
                .HasForeignKey(assoc => assoc.LeftId);
            builder.Entity<TAssociation>()
                .HasOne(assoc => assoc.Right)
                .WithMany(associationsFromRight)
                .HasForeignKey(assoc => assoc.RightId);
            builder.Entity<TAssociation>()
                .HasKey(assoc => new { assoc.LeftId, assoc.RightId });
        }
    }
}
