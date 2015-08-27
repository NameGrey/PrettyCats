using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BookIt.DAL.Entities;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
    public class GenericRepository<TBusinessEntity, TDataEntity> where TBusinessEntity : class
                                                                 where TDataEntity:  class, IEntity
    {
        protected readonly DbContext Context;
        private readonly IMapper<TBusinessEntity, TDataEntity> _mapper;
        private readonly DbSet<TDataEntity> _dbSet;

        public GenericRepository(DbContext context, IMapper<TBusinessEntity, TDataEntity> mapper)
        {
            Context = context;
            _mapper = mapper;
            _dbSet = context.Set<TDataEntity>();
        }

        public virtual IEnumerable<TBusinessEntity> Get()
        {
            return _dbSet.Select(_mapper.Map).ToList();
        }

        public virtual TBusinessEntity GetByID(object id)
        {
            return _mapper.Map(_dbSet.Find(id));
        }

        public virtual void Insert(TBusinessEntity entity)
        {
            _dbSet.Add(_mapper.UnMap(entity));
        }

        public virtual void Delete(object id)
        {
            TDataEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TBusinessEntity entityToDelete)
        {
            var dataEntity = _mapper.UnMap(entityToDelete);

            var local = Context.Set<TDataEntity>().Local.FirstOrDefault(x => x.ID == dataEntity.ID);
            if (local != null)
                Context.Entry(local).State = EntityState.Detached;
            

            if (Context.Entry(dataEntity).State == EntityState.Detached)
            {
                _dbSet.Attach(dataEntity);
            }
            _dbSet.Remove(dataEntity);
        }

        public virtual void Update(TBusinessEntity entityToUpdate)
        {
            var dataEntity = _mapper.UnMap(entityToUpdate);

            var local = Context.Set<TDataEntity>().Local.FirstOrDefault(x => x.ID == dataEntity.ID);
            if (local != null)
                Context.Entry(local).State = EntityState.Detached;

            _dbSet.Attach(dataEntity);
            Context.Entry(dataEntity).State = EntityState.Modified;
        }
    }
}
