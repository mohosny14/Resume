using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastrucure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;

        private DbSet<T> tabel = null;
        public GenericRepository(DataContext context)
        {
            _context = context;
            tabel = _context.Set<T>();
        }

        public void Delete(object id)
        {
            // call GetById Function
            T item = GetById(id);
            tabel.Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            return tabel.ToList();
        }

        public T GetById(object id)
        {
            return tabel.Find(id);
        }

        public void Insert(T entity)
        {
            tabel.Add(entity);
        }
        public void Update(T entity)
        {
            tabel.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
