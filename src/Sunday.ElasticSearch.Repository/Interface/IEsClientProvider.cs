using Nest;

namespace Sunday.ElasticSearch
{
    public interface IEsClientProvider
    {
        /// <summary>
        /// 获取ElasticClient
        /// </summary>
        ElasticClient GetClient();

        /// <summary>
        /// 指定index获取ElasticClient
        /// </summary>
        ElasticClient GetClient(string indexName);
    }
}