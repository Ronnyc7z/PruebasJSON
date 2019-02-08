using Microsoft.EntityFrameworkCore;
using PruebasJSON.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebasJSON.Interfaces
{
    public interface IRepository<T, I> where T : class
    {
        T Insert(T entity);
        //Task<IEnumerable<T>> InsertMultiple(IEnumerable<T> entities);
        //void Delete(T entity);
        //T GetById(I Id);
        //void Reload();
        //T Update(T entity, I Id);
        //IList<T> SearchWith(Func<T, bool> predicate);
        IList<T> GetAll();

    }
    public class Repository<T, I> : IRepository<T, I> where T : class
    {
        static DbContextOptions<DataContext> _contextOptions;
        protected DbContext _EF { get; set; }
        protected readonly DbSet<T> _DbSet;

        public Repository(DbContext context)
        {
            _EF = context;
            _DbSet = _EF.Set<T>();
        }

        public T Insert(T entity)
        {
            try
            {
                _DbSet.Add(entity);
                _EF.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                _DbSet.Remove(entity);
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> InsertMultiple(IEnumerable<T> entities)
        {
            try
            {
                IList<T> data = entities.ToList();
                List<Task> tasks = new List<Task>
                {
                    _DbSet.AddRangeAsync(data),
                    _EF.SaveChangesAsync()
                };
                await Task.WhenAll(tasks);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                _DbSet.Remove(entity);
                _EF.SaveChanges();
            }
            catch (Exception ex)
            {
                _DbSet.Add(entity);
                throw ex;
            }
        }

        public T GetById(I Id) => _DbSet.Find(Id);

        public async Task<T> GetByIdAsync(I Id) => await _DbSet.FindAsync(Id);

        public void Reload()
        {
            var refreshTableObjects = _EF.ChangeTracker.Entries().Select(c => c.Entity).ToList();
        }

        public T Update(T entity, I Id)
        {
            try
            {
                var original = GetById(Id);
                if (original != null)
                {
                    _EF.Entry(original).CurrentValues.SetValues(entity);
                    _EF.SaveChanges();
                    return entity;
                }
                return entity;
            }
            catch (Exception ex)
            {
                _DbSet.Remove(entity);
                throw ex;
            }
        }

        public IList<T> SearchWith(Func<T, bool> predicate) =>
            _DbSet.Where(predicate).ToList();

        public IList<T> GetAll() =>
            _DbSet.ToList();
    }
}
