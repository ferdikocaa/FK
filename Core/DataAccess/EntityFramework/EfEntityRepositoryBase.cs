using Core.Entities;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
	 where TEntity : class, IEntity, new()
	 where TContext : DbContext, new()
	{
		public void Add(TEntity entity)
		{
			using var context = new TContext();
			var added = context.Entry(entity);
			added.State = EntityState.Added;
			context.SaveChanges();
		}

        public virtual async Task<int> AddWithCollection(TEntity entity)
        {
			using (var _dbContext = new TContext())
			{
				await _dbContext.Set<TEntity>().AddAsync(entity);
				var i = await _dbContext.SaveChangesAsync();
				_dbContext.Entry(entity).State = EntityState.Detached;

				_dbContext.ChangeTracker.Entries()
					.Where(x => x.State == EntityState.Unchanged)
					.ToList()
					.ForEach(x => { x.State = EntityState.Detached; });
				_dbContext.Entry(entity).State = EntityState.Detached;
				return i;
			}
		}

        public void Delete(TEntity entity)
		{
			using var context = new TContext();
			var deleted = context.Entry(entity);
			deleted.State = EntityState.Deleted;
			context.SaveChanges();
		}

		public TEntity Get(Expression<Func<TEntity, bool>> filter)
		{
			using var context = new TContext();
			return context.Set<TEntity>().FirstOrDefault(filter);
		}

		public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
		{
			using var context = new TContext();
			return filter == null
				? context.Set<TEntity>().ToList()
				: context.Set<TEntity>().Where(filter).ToList();
		}
		public List<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
		{
			using var context = new TContext();
			return filter == null
				? context.Set<TEntity>().ToList()
				: context.Set<TEntity>().Where(filter).ToList();
		}

		public virtual async Task<IEnumerable<TEntity>> Search(bool eager = false, IEnumerable<string> includes = null)
		{
			using (var _dbContext = new TContext())
			{
				var query = QueryWithInclude<TEntity>(_dbContext, eager);

				if (includes != null)
					foreach (var include in includes)
						query = query.Include(include);

				var result = await Task.Run(() =>
				query.ToList()
				);
				return result;
			}
		}

		public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate,bool eager = false, IEnumerable<string> includes = null)
		{
			using (var _dbContext = new TContext())
			{
				var query = QueryWithInclude<TEntity>(_dbContext, eager);

				if (predicate != null)
					query = query.Where(predicate);

				if (includes != null)
					foreach (var include in includes)
						query = query.Include(include);

				var result = await Task.Run(() =>
					query.AsEnumerable().ToList()
				);
				return result;
			}
		}

		public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, TEntity>> selector, Expression<Func<TEntity, bool>> predicate = null)
		{
			using (var _dbContext = new TContext())
			{
				var query = _dbContext.Set<TEntity>().AsQueryable().AsNoTracking();

				if (null != predicate)
					query = query.Where(predicate);

				if (null != selector)
					query = query.Select(selector);

				var result = await Task.Run(() =>
						query.AsEnumerable().ToList()
					);

				return result;
			}
		}

        public void Update(TEntity entity)
		{
			using var context = new TContext();
			var updated = context.Entry(entity);
			updated.State = EntityState.Modified;
			context.SaveChanges();
		}

		public async Task<int> UpdateFieldsSave(TEntity entity, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			using (var _dbContext = new TContext())
			{
				var dbEntry = _dbContext.Entry(entity);
				foreach (var includeProperty in includeProperties)
				{
					dbEntry.Property(includeProperty).IsModified = true;
                }
                int i = await _dbContext.SaveChangesAsync();
				return i;
			}
		}

		[Obsolete]
		public virtual async Task<int> UpdateWithCollections(TEntity entity, bool withCollections = true)
		{
			using var _dbContext = new TContext();
			var userId = entity.GetType().GetProperty(nameof(EntityMain.UpdatedBy))?.GetValue(entity);

			int id = (int)entity.GetType().GetProperty("Id")?.GetValue(entity, null);

			var dbEntity = await _dbContext.FindAsync<TEntity>(id);  
			var dbEntry = _dbContext.Entry(dbEntity);

			dbEntry.CurrentValues.SetValues(entity);  

			if (withCollections)
			{
				var navigations = _dbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations()  
			  .Where(property =>
			  typeof(TEntity).GetProperty(property.Name).GetCustomAttribute<JsonIgnoreAttribute>() == null &&
			  property.IsCollection);

				foreach (Navigation property in navigations)  
				{
					var propertyName = property.Name;
					var dbItemsEntry = dbEntry.Collection(propertyName);
					var accessor = dbItemsEntry.Metadata.GetCollectionAccessor();

					await dbItemsEntry.LoadAsync();  

					var dbItemsMap = new Dictionary<int, object>();
					foreach (var item in (System.Collections.IEnumerable)dbItemsEntry.CurrentValue)  
					{
						var keyName = _dbContext.Model.FindEntityType(item.GetType()).FindPrimaryKey().Properties.Select(x => x.Name).FirstOrDefault();
						var key = (int)item.GetType().GetProperty(keyName)?.GetValue(item, null);

						item.GetType().GetProperty(nameof(EntityMain.UpdatedDate))?.SetValue(item, DateTime.UtcNow);
						item.GetType().GetProperty(nameof(EntityMain.UpdatedBy))?.SetValue(item, userId);
						dbItemsMap.Add(key, item);
					}

					var items = (IEnumerable<EntityMain>)accessor.GetOrCreate(entity, true);  

					foreach (var item in items)  
					{
						var keyName = _dbContext.Model.FindEntityType(item.GetType()).FindPrimaryKey().Properties.Select(x => x.Name).Single();
						var key = (int)item.GetType().GetProperty(keyName)?.GetValue(item, null);

						if (!dbItemsMap.TryGetValue(key, out var oldItem))  
						{
							item.GetType().GetProperty(nameof(EntityMain.CreatedDate))?.SetValue(item, DateTime.UtcNow);
							item.GetType().GetProperty(nameof(EntityMain.CreatedBy))?.SetValue(item, userId);
							item.GetType().GetProperty(nameof(EntityMain.IsDeleted))?.SetValue(item, false);
							_dbContext.Entry(item).State = EntityState.Added;

							accessor.Add(dbEntity, item, true);
						}
						else  
						{
							item.GetType().GetProperty(nameof(EntityMain.CreatedBy))?.SetValue(item, oldItem.GetType().GetProperty(nameof(EntityMain.CreatedBy)).GetValue(oldItem, null));
							item.GetType().GetProperty(nameof(EntityMain.CreatedDate))?.SetValue(item, oldItem.GetType().GetProperty(nameof(EntityMain.CreatedDate)).GetValue(oldItem, null));
							_dbContext.Entry(oldItem).CurrentValues.SetValues(item);
							oldItem.GetType().GetProperty(nameof(EntityMain.UpdatedDate))?.SetValue(oldItem, DateTime.UtcNow);
							oldItem.GetType().GetProperty(nameof(EntityMain.UpdatedBy))?.SetValue(oldItem, userId);
							dbItemsMap.Remove(key);
						}
					}
					
					foreach (var oldItem in dbItemsMap.Values) 
					{
						oldItem.GetType().GetProperty(nameof(EntityMain.UpdatedDate))?.SetValue(oldItem, DateTime.UtcNow);
						oldItem.GetType().GetProperty(nameof(EntityMain.UpdatedBy))?.SetValue(oldItem, userId);
						oldItem.GetType().GetProperty(nameof(EntityMain.IsDeleted))?.SetValue(oldItem, true);
					}
				}
			}


			int i = await _dbContext.SaveChangesAsync();
			_dbContext.Entry(entity).State = EntityState.Detached;
			return i;
		}

		internal virtual IQueryable<TEntity> QueryWithInclude<T>(TContext _dbContext, bool eager = false)
		{
			var query = _dbContext.Set<TEntity>().AsQueryable().AsNoTracking();
			if (eager)
				foreach (var property in _dbContext.Model.FindEntityType(typeof(T)).GetNavigations())
				{
					PropertyInfo propertyInfo = typeof(T).GetProperty(property.Name);
					var jsonIgnore = propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>();
					if (jsonIgnore == null)
						query = query.Include(property.Name);
				}
			return query;
		}
	}
}
