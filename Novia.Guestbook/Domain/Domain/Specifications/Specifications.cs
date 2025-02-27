﻿using Novia.Guestbook.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
namespace Novia.Guestbook.Domain.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T,object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}