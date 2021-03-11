using System.Threading.Tasks;

namespace DbAccess_Library.Contracts.Strategies
{
    public interface IDeleteOrderStrategy
    {
        Task Delete(int id);
    }
}