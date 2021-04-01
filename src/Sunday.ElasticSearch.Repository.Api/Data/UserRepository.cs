using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunday.ElasticSearch.Repository.Api.Data
{
    public class UserRepository : EsRepository<User>, IUserRepository
    {
        //ES的Index相当于MySql的Table
        public override string IndexName => "User";

        public UserRepository(IEsClientProvider provider) : base(provider)
        {
        }

        public async Task<IEnumerable<User>> GetUserList(IEnumerable<string> userIds)
        {
            return await GetManyAsync(userIds);
        }
    }

    public interface IUserRepository : IEsRepository<User>
    {
        Task<IEnumerable<User>> GetUserList(IEnumerable<string> hotelIds);
    }
}