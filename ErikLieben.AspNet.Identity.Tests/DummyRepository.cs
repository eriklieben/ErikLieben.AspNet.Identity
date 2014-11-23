using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErikLieben.AspNet.Identity.Tests
{
    using System.Collections;

    using ErikLieben.AspNet.Identity.Interfaces;
    using ErikLieben.Data.Repository;

    public class DummyRepository<T> : IRepository<T> where T : class, IUserKey<int>
    {
        public DummyRepository()
        {
            this.InnerList = new List<T>();
        }

        public List<T> InnerList { get; private set; }

        public void Add(T item)
        {
            this.InnerList.Add(item);
        }

        public void Delete(T item)
        {
            this.InnerList.Remove(item);
        }

        public IEnumerable<T> Find(ISpecification<T> specification, IFetchingStrategy<T> fetchingStrategy)
        {
            return this.InnerList.Where(specification.Predicate.Compile());
        }

        public T FindFirstOrDefault(ISpecification<T> specification, IFetchingStrategy<T> fetchingStrategy)
        {
            return this.InnerList.FirstOrDefault(specification.Predicate.Compile());
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.InnerList.GetEnumerator();
        }

        public void Update(T item)
        {
            this.InnerList.FirstOrDefault(i => i.Id == item.Id).UserName = item.UserName;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.InnerList.GetEnumerator();
        }
    }
}
