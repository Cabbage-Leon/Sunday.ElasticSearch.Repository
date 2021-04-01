using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sunday.ElasticSearch.Extensions;
using Nest;

namespace Sunday.ElasticSearch
{
    public abstract class EsRepository<T> : IEsRepository<T> where T : EsEntity
    {
        protected IEsClientProvider _esClientProvider;
        public abstract string IndexName { get; }

        public EsRepository(IEsClientProvider provider)
        {
            _esClientProvider = provider;
        }

        public T Get(string id)
        {
            var client = _esClientProvider.GetClient(IndexName);
            GetResponse<T> response = client.Get<T>(id);
            if (response.IsValid)
            {
                return response.Source;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<T> GetAsync(string id)
        {
            var client = _esClientProvider.GetClient(IndexName);
            GetResponse<T> response = await client.GetAsync<T>(id);
            if (response.IsValid)
            {
                return response.Source;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public T Get(IGetRequest request)
        {
            var client = _esClientProvider.GetClient(IndexName);
            GetResponse<T> response = client.Get<T>(request);
            if (response.IsValid)
            {
                return response.Source;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<T> GetAsync(IGetRequest request)
        {
            var client = _esClientProvider.GetClient(IndexName);
            GetResponse<T> response = await client.GetAsync<T>(request);
            if (response.IsValid)
            {
                return response.Source;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public T Find(string id)
        {
            var response = _esClientProvider.GetClient(IndexName).Get<T>(id);
            if (response.IsValid)
            {
                return response.Source;
            }
            return null;
        }

        public async Task<T> FindAsync(string id)
        {
            var response = await _esClientProvider.GetClient(IndexName).GetAsync<T>(id);
            if (response.IsValid)
            {
                return response.Source;
            }
            return null;
        }

        public T Find(IGetRequest request)
        {
            var client = _esClientProvider.GetClient(IndexName);
            GetResponse<T> response = client.Get<T>(request);
            if (response.IsValid)
            {
                return response.Source;
            }
            return null;
        }

        public async Task<T> FindAsync(IGetRequest request)
        {
            var client = _esClientProvider.GetClient(IndexName);
            GetResponse<T> response = await client.GetAsync<T>(request);
            if (response.IsValid)
            {
                return response.Source;
            }
            return null;
        }

        public IEnumerable<T> GetMany(IEnumerable<string> ids)
        {
            IList<T> list = new List<T>();
            var client = _esClientProvider.GetClient(IndexName);
            IEnumerable<IMultiGetHit<T>> response = client.GetMany<T>(ids);
            foreach (var item in response)
            {
                list.Add(item.Source);
            }
            return list;
        }

        public async Task<IEnumerable<T>> GetManyAsync(IEnumerable<string> ids)
        {
            IList<T> list = new List<T>();
            var client = _esClientProvider.GetClient(IndexName);
            IEnumerable<IMultiGetHit<T>> response = await client.GetManyAsync<T>(ids);
            foreach (var item in response)
            {
                list.Add(item.Source);
            }
            return list;
        }

        public IEnumerable<T> Search(ISearchRequest request)
        {
            IList<T> list = new List<T>();
            var client = _esClientProvider.GetClient(IndexName);
            ISearchResponse<T> response = client.Search<T>(request);
            if (response.IsValid)
            {
                foreach (var hit in response.Hits)
                {
                    list.Add(hit.Source);
                }
                return list;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<IEnumerable<T>> SearchAsync(ISearchRequest request)
        {
            IList<T> list = new List<T>();
            var client = _esClientProvider.GetClient(IndexName);
            ISearchResponse<T> response = await client.SearchAsync<T>(request);
            if (response.IsValid)
            {
                foreach (var hit in response.Hits)
                {
                    list.Add(hit.Source);
                }
                return list;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public IEnumerable<T> Search(Func<SearchDescriptor<T>, ISearchRequest> selector)
        {
            IList<T> list = new List<T>();
            var client = _esClientProvider.GetClient(IndexName);
            ISearchResponse<T> response = client.Search(selector);
            if (response.IsValid)
            {
                foreach (var hit in response.Hits)
                {
                    list.Add(hit.Source);
                }
                return list;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<IEnumerable<T>> SearchAsync(Func<SearchDescriptor<T>, ISearchRequest> selector)
        {
            IList<T> list = new List<T>();
            var client = _esClientProvider.GetClient(IndexName);
            ISearchResponse<T> response = await client.SearchAsync(selector);
            if (response.IsValid)
            {
                foreach (var hit in response.Hits)
                {
                    list.Add(hit.Source);
                }
                return list;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public IEnumerable<IHit<T>> HitsSearch(ISearchRequest request)
        {
            var client = _esClientProvider.GetClient(IndexName);
            ISearchResponse<T> response = client.Search<T>(request);
            if (response.IsValid)
            {
                return response.Hits;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<IEnumerable<IHit<T>>> HitsSearchAsync(ISearchRequest request)
        {
            var client = _esClientProvider.GetClient(IndexName);
            ISearchResponse<T> response = await client.SearchAsync<T>(request);
            if (response.IsValid)
            {
                return response.Hits;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public IEnumerable<IHit<T>> HitsSearch(Func<SearchDescriptor<T>, ISearchRequest> selector)
        {
            var client = _esClientProvider.GetClient(IndexName);
            ISearchResponse<T> response = client.Search(selector);
            if (response.IsValid)
            {
                return response.Hits;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<IEnumerable<IHit<T>>> HitsSearchAsync(Func<SearchDescriptor<T>, ISearchRequest> selector)
        {
            var client = _esClientProvider.GetClient(IndexName);
            ISearchResponse<T> response = await client.SearchAsync(selector);
            if (response.IsValid)
            {
                return response.Hits;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public bool Insert(T t)
        {
            ElasticClient client = _esClientProvider.GetClient(IndexName);
            if (!client.Indices.Exists(IndexName).Exists)
            {
                client.CreateIndex<T>(IndexName);
            }
            var response = client.IndexDocument(t);
            if (response.IsValid)
            {
                return true;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<bool> InsertAsync(T t)
        {
            var client = _esClientProvider.GetClient(IndexName);
            if (!client.Indices.Exists(IndexName).Exists)
            {
                client.CreateIndex<T>(IndexName);
            }
            var response = await client.IndexDocumentAsync(t);
            if (response.IsValid)
            {
                return true;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public bool InsertMany(IList<T> tList)
        {
            var client = _esClientProvider.GetClient(IndexName);
            if (!client.Indices.Exists(IndexName).Exists)
            {
                client.CreateIndex<T>(IndexName);
            }
            var response = client.IndexMany(tList);
            //var response = client.Bulk(p => p.Index(IndexName).IndexMany(tList));
            if (response.IsValid)
            {
                return true;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<bool> InsertManyAsync(IList<T> tList)
        {
            var client = _esClientProvider.GetClient(IndexName);
            if (!client.Indices.Exists(IndexName).Exists)
            {
                client.CreateIndex<T>(IndexName);
            }
            var response = await client.IndexManyAsync(tList);
            if (response.IsValid)
            {
                return true;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public bool Update(T t)
        {
            ElasticClient client = _esClientProvider.GetClient(IndexName);
            UpdateResponse<T> response = client.Update<T>(t.Id, p => p.Doc(t));
            if (response.IsValid)
            {
                return true;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<bool> UpdateAsync(T t)
        {
            ElasticClient client = _esClientProvider.GetClient(IndexName);
            UpdateResponse<T> response = await client.UpdateAsync<T>(t.Id, p => p.Doc(t));
            if (response.IsValid)
            {
                return true;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public bool UpdatePart(T t, object partialEntity)
        {
            ElasticClient client = _esClientProvider.GetClient(IndexName);
            IUpdateRequest<T, object> request = new UpdateRequest<T, object>(t.Id)
            {
                Doc = partialEntity
            };
            UpdateResponse<T> response = client.Update(request);
            if (response.IsValid)
            {
                return true;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<bool> UpdatePartAsync(T t, object partialEntity)
        {
            ElasticClient client = _esClientProvider.GetClient(IndexName);
            IUpdateRequest<T, object> request = new UpdateRequest<T, object>(t.Id)
            {
                Doc = partialEntity
            };
            UpdateResponse<T> response = await client.UpdateAsync(request);
            if (response.IsValid)
            {
                return true;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public bool DeleteById(string id)
        {
            var client = _esClientProvider.GetClient(IndexName);
            var response = client.Delete<T>(id);
            if (response.IsValid)
            {
                return true;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var client = _esClientProvider.GetClient(IndexName);
            var response = await client.DeleteAsync<T>(id);
            if (response.IsValid)
            {
                return true;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public long GetTotalCount()
        {
            var client = _esClientProvider.GetClient(IndexName);
            var search = new SearchDescriptor<T>().MatchAll(); //指定查询字段 .Source(p => p.Includes(x => x.Field("Id")));
            var response = client.Search<T>(search);
            if (response.IsValid)
            {
                return response.Total;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<long> GetTotalCountAsync()
        {
            var client = _esClientProvider.GetClient(IndexName);
            var search = new SearchDescriptor<T>().MatchAll(); //指定查询字段 .Source(p => p.Includes(x => x.Field("Id")));
            var response = await client.SearchAsync<T>(search);
            if (response.IsValid)
            {
                return response.Total;
            }
            else
            {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public bool Exist(string id)
        {
            var client = _esClientProvider.GetClient(IndexName);

            var response = client.DocumentExists<T>(id);
            return response.Exists;
        }

        public async Task<bool> ExistAsync(string id)
        {
            var client = _esClientProvider.GetClient(IndexName);

            var response = await client.DocumentExistsAsync<T>(id);
            return response.Exists;
        }
    }
}