using BackendTest.Data;
using Data.Models;
using Models.ModelRequest;
using Dapper;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BackendTest.DataRepository
{
    public class RepositoryBase : IRepositoryBase
    {
        private readonly IDbConnection _db;

        public RepositoryBase(IOptions<ConnectionStringList> connectionStrings)
        {
            _db = new MySqlConnection(connectionStrings.Value.QoinTest);
        }

        public void Dispose()
        {
            _db.Close();
        }

        public async Task<Test01> GetById(int id)
        {
            try
            {
                string query = $"SELECT * FROM Test01 WHERE Id = {id}";
                return await _db.QuerySingleAsync<Test01>(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }

        }

        public async Task<Pagination<Test01>> GetListByPage(int page)
        {
            try
            {
                int pageSize = 20;
                int skipRecord = 20 * ((page < 1 ? 1 : page) - 1);
                string queryRecord = $"SELECT * FROM Test01 LIMIT {pageSize} OFFSET {skipRecord}";
                string totalRecords = $"SELECT COUNT('') AS totalRecord FROM Test01";

                return new Pagination<Test01>()
                {
                    CurrentPage = page,
                    Data = await _db.QueryAsync<Test01>(queryRecord),
                    TotalRecords = await _db.QuerySingleAsync<int>(totalRecords)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Dispose();
            }
        }

        public async Task<string> InsertTest01(Test01Request request)
        {
            _db.Open();
            using (var transaction = _db.BeginTransaction())
            {
                try
                {
                    string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                    string query = $@"INSERT INTO Test01(Nama, Status, Created, Updated)
                                  VALUES
                                 ('{request.Nama}', {request.Status}, 
                                  '{currentDateTime}', 
                                  '{currentDateTime}'
                                 )";
                    _db.Execute(query, transaction: transaction);
                    transaction.Commit();
                    return $"Data is Succesfuly Saved";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    Dispose();
                }
            }
        }

        public async Task<string> UpdateTest01(Test01Request request, int id)
        {
            _db.Open();
            using (var transaction = _db.BeginTransaction())
            {
                try
                {
                    string query = $@"UPDATE Test01
                                      SET    Nama    = '{request.Nama}', 
                                             Status  =  {request.Status}, 
                                             Updated = '{DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}'
                                      WHERE  Id = {id}
                                ";
                    _db.Execute(query, transaction: transaction);
                    transaction.Commit();
                    return $"Test01 with id {id} is Succesfuly Updated";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    Dispose();
                }
            }
        }

        public async Task<string> RemoveTest01(int id)
        {
            _db.Open();
            using (var transaction = _db.BeginTransaction())
            {
                try
                {

                    string query = $"DELETE FROM Test01 WHERE Id = {id}";
                    _db.Execute(query, transaction: transaction);
                    transaction.Commit();
                    return $"Test01 with Id {id} is Succesfuly Removed";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    Dispose();
                }
            }
        }
    }
}
