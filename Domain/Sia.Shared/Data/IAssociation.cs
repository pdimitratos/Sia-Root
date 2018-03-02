using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Sia.Shared.Data
{
    public interface IAssociation<TLeft, TRight>
    {
        void Associate(TLeft left, TRight right);
        KeyValuePair<TLeft, TRight> AssociatesBetween();
    }

    public abstract class Association<TLeft, TRight>
        : IAssociation<TLeft, TRight>
        where TLeft : IEntity
        where TRight : IEntity
    {
        public virtual long LeftId { get; set; }
        public virtual long RightId { get; set; }
        public virtual TLeft Left { get; set; }
        public virtual TRight Right { get; set; }
        public void Associate(TLeft left, TRight right)
        {
            Left = left;
            Right = right;
            LeftId = left.Id;
            RightId = right.Id;
        }

        public KeyValuePair<TLeft, TRight> AssociatesBetween()
            => new KeyValuePair<TLeft, TRight>(Left, Right);
    }

    public abstract class BidrectionalAssociation<TLeft, TRight>
        : Association<TLeft, TRight>,
        IAssociation<TRight, TLeft>
        where TLeft : IEntity
        where TRight : IEntity
    {
        protected BidrectionalAssociation()
        {
        }

        void IAssociation<TRight, TLeft>.Associate(TRight left, TLeft right)
            => Associate(right, left);


        KeyValuePair<TRight, TLeft> IAssociation<TRight, TLeft>.AssociatesBetween()
            => new KeyValuePair<TRight, TLeft>(Right, Left);
    }
}