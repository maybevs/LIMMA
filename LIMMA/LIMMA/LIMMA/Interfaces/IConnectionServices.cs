using System.Threading.Tasks;
using LIMMA.Helper;

namespace LIMMA.Interfaces
{
    public interface IConnectionServices
    {
        UserToken UserToken { get; set; }
        Task<string> GetConnection(IConfiguration config);

        Task<UserToken> GetUserToken(IConfiguration config);

        Task<UserToken> UpdateUserToken(IConfiguration config, UserToken token);
        Task<UserToken> GetCurrentToken(IConfiguration config);
    }
}