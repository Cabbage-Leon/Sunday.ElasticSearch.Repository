using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Sunday.ElasticSearch
{
    public class EsConfig : IOptions<EsConfig>
    {
        public List<string> Urls { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public EsConfig Value => this;
    }
}
