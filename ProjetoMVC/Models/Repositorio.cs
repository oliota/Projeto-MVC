using System.Collections.Generic;
using System.Web.Configuration;

namespace ProjetoMVC.Models
{
    public abstract class Repositorio<TEntity, TKey>
    where TEntity : class
    {
        protected string StringConnection { get; } = WebConfigurationManager.ConnectionStrings["BDProjetoMVC"].ConnectionString;

        public abstract List<TEntity> GetAll();
        public abstract TEntity GetById(TKey id);
        public abstract TEntity GetByName(string nome);
        public abstract List<TEntity> GetByRef(TKey id);
        public abstract void Save(TEntity entity);
        public abstract void Update(TEntity entity);
        public abstract void Delete(TEntity entity);
        public abstract void DeleteById(TKey id);
    }
}