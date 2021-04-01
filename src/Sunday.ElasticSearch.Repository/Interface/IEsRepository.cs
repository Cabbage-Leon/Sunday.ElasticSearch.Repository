using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Sunday.ElasticSearch
{
    public interface IEsRepository<T> where T : class
    {
        T Get(string id);

        Task<T> GetAsync(string id);

        T Get(IGetRequest request);

        Task<T> GetAsync(IGetRequest request);

        Task<T> FindAsync(string id);

        T Find(string id);

        T Find(IGetRequest request);

        Task<T> FindAsync(IGetRequest request);

        IEnumerable<T> GetMany(IEnumerable<string> ids);

        Task<IEnumerable<T>> GetManyAsync(IEnumerable<string> ids);

        IEnumerable<T> Search(ISearchRequest request);

        Task<IEnumerable<T>> SearchAsync(ISearchRequest request);

        IEnumerable<T> Search(Func<SearchDescriptor<T>, ISearchRequest> selector);

        Task<IEnumerable<T>> SearchAsync(Func<SearchDescriptor<T>, ISearchRequest> selector);

        bool Insert(T t);

        Task<bool> InsertAsync(T t);

        bool InsertMany(IList<T> tList);

        Task<bool> InsertManyAsync(IList<T> tList);

        bool Update(T t);

        Task<bool> UpdateAsync(T t);

        bool UpdatePart(T t, object partialEntity);

        Task<bool> UpdatePartAsync(T t, object partialEntity);

        long GetTotalCount();

        Task<long> GetTotalCountAsync();

        bool DeleteById(string id);

        Task<bool> DeleteByIdAsync(string id);

        bool Exist(string id);

        Task<bool> ExistAsync(string id);
    }
}