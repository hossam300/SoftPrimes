using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
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
      if (sort != null && sort.Field != "" && sort.Field != null && sort.Dir != "" && sort.Dir != null)
      {
        // Create ordering expression e.g. Field1 asc, Field2 desc
        var ordering = String.Join(",", sort.ToExpression());
        queryable = queryable.OrderBy(ordering);
        // Use the OrderBy method of Dynamic Linq to sort the data
        foreach (var item in sort.Sorts)
        {
          ordering = String.Join(",", sort.ToExpression());

          // Use the OrderBy method of Dynamic Linq to sort the data
          queryable = queryable.OrderBy(ordering);
        }
        return queryable;
      }

      return queryable;
    }
  }
}