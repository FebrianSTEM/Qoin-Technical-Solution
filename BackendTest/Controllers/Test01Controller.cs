using BackendTest.DataRepository;
using BackendTest.Data;
using Data.Models;
using Models.ModelRequest;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace BackendTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Test01Controller : ControllerBase
    {
        private readonly RepositoryBase _repository;

        public Test01Controller(IOptions<ConnectionStringList> connectionStrings)
        {
            _repository = new RepositoryBase(connectionStrings);
        }

        /// <summary>
        /// Get Single Data of Test01 by its Id
        /// </summary>
        [HttpGet("id/{id}")]
        public async Task<Test01> GetById([FromRoute] int id)
        {
            try
            {
                return await _repository.GetById(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get List of Test01 by its page number with 20 record per size
        /// </summary>
        [HttpGet("page/{number}")]
        public async Task<Pagination<Test01>> GetByPage([FromRoute] int number)
        {
            try
            {
                return await _repository.GetListByPage(number);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert one record to Test01
        /// </summary>
        [HttpPost("save")]
        public async Task<string> SaveTest01([FromBody] Test01Request request)
        {
            try
            {
                return await _repository.InsertTest01(request);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update One Record of Test01
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<string> UpdateTest01([FromRoute] int id, [FromBody] Test01Request request)
        {
            try
            {
                return await _repository.UpdateTest01(request, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Remove one Record of Test01
        /// </summary>
        [HttpDelete("remove/{id}")]
        public async Task<string> RemoveTest01([FromRoute] int id)
        {
            try
            {
                return await _repository.RemoveTest01(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
