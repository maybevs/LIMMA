using System.Threading.Tasks;

namespace LIMMA.Interfaces
{
    public interface IConnectionServices
    {
        Task<string> GetConnection(IConfiguration config);
    }
}