namespace Sunday.ElasticSearch.Repository.Api.Data
{
    public class User : EsEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
    }
}