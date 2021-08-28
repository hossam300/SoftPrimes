using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HelperServices.LinqHelpers
{
    public static partial class QueryableExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, Sort sort)
        {
            //if (sort == null || !sort.Any())
            //{
            //    sort = new Sort[] { new Sort() { Field = "1", Dir = "" } };
            //}
            if (sort != null)
            {
                // Create ordering expression e.g. Field1 asc, Field2 desc
                var ordering = String.Join(",", sort.ToExpression());

                // Use the OrderBy method of Dynamic Linq to sort the data
                foreach (var item in sort.Sorts)
                {
                    ordering = String.Join(",", sort.ToExpression());

                    // Use the OrderBy method of Dynamic Linq to sort the data
                    queryable = queryable.OrderBy(x => ordering);
                }
                return queryable.OrderBy(x => ordering);

            }

            return queryable;
        }
    }
}