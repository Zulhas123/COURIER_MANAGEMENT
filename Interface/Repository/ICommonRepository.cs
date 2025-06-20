﻿using System.Linq.Expressions;

namespace CourierManagementsystem.Interface.Repository
{
    public interface ICommonRepository<T> where T : class
    {
        bool Add(T entity);
        bool Add(ICollection<T> entities);

        bool Update(T entity);
        bool Update(ICollection<T> entities);

        bool Delete(T entity);
        bool Delete(ICollection<T> entities);

        ICollection<T> GetAll(params Expression<Func<T, object>>[] include);//All Table Data
        ICollection<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);//Get Data by Lambda Expression 
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);//Get Single Data
    }
}
