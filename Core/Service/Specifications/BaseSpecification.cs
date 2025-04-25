using System.Linq.Expressions;
using DomainLayer.Contracts;
using DomainLayer.Models;

namespace Service.Specifications
{
    abstract class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public BaseSpecification(Expression<Func<TEntity, bool>>? CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => IncludeExpressions.Add(includeExpression);

        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public void AddOrderBy(Expression<Func<TEntity, object>> orderByExp) => OrderBy = orderByExp;
        public void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExp) => OrderByDescending = orderByDescExp;
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        #endregion

        #region Pagination
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; set; }

        public void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;

        }
        #endregion

    }


}
