using System;

namespace Sunday.ElasticSearch
{
    public abstract class EsEntity
    {
        public string Id { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
