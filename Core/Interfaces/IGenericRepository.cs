﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{   
    // common operations in all Models 
    public interface IGenericRepository<T> where T : class
    {
        
        IEnumerable<T> GetAll(); // act as list to return all items

        T GetById(object id);

        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
