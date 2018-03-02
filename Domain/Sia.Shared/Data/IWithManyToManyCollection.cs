using Sia.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sia.Shared.Data
{
    public interface IWithManyToManyCollection<TThis, TAssociation, TAssociated>
        where TAssociation : IAssociation<TThis, TAssociated>, new()
    {
        ICollection<TAssociation> AssociationCollection { get; }
        ManyToManyCollection<TThis, TAssociation, TAssociated> ManyToManyCollection { get; }
    }
}

namespace Microsoft.EntityFrameworkCore
{
    public static class MTMExtensions
    {
        public static IQueryable<TRecord> Include<TRecord, TAssociation, TAssociated>(this DbSet<TRecord> table)
            where TRecord : class, IEntity, IWithManyToManyCollection<TRecord, TAssociation, TAssociated>, new()
            where TAssociation : BidrectionalAssociation<TRecord, TAssociated>, new()
            where TAssociated : IEntity
            => table
                .Include(record => record.AssociationCollection)
                    .ThenInclude(assoc => assoc.Right)
                .Include(record => record.AssociationCollection)
                    .ThenInclude(assoc => assoc.Left);
    }
}
