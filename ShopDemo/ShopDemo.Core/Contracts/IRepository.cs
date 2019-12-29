using System.Linq;
using ShopDemo.Core.Models;

namespace ShopDemo.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string id);
        T Find(string id);
        void Insert(T t);
        void update(T t);
    }
}