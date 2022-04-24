using Data.Models;
using Models.ModelRequest;
using System.Threading.Tasks;

namespace BackendTest.DataRepository
{
    internal interface IRepositoryBase
    {
        Task<Test01> GetById(int id);
        Task<Pagination<Test01>> GetListByPage(int page);
        Task<string> InsertTest01(Test01Request request);
        Task<string> UpdateTest01(Test01Request request, int id);
        Task<string> RemoveTest01(int id);
    }
}