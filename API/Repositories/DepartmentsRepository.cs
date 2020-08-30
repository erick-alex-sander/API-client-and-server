using API.Context;
using API.Models;
using API.Repositories.IRepositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Repositories
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);
        DynamicParameters parameters = new DynamicParameters();

        public int Create(Department department)
        {
            var procedureName = "sp_insert_department";
            parameters.Add("Name", department.Name);
            var create = conn.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return create;
        }

        public int Delete(int id)
        {
            var procedureName = "sp_delete_department";
            parameters.Add("Id", id);
            var delete = conn.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return delete;
        }

        public async Task<IEnumerable<Department>> Get()
        {
            var procedureName = "sp_get_department";
            var get = await conn.QueryAsync<Department>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return get;
        }

        public async Task<Department> Get(int id)
        {
            var procedureName = "sp_getid_department";
            parameters.Add("Id", id);
            var get = await conn.QueryAsync<Department>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return get.First();
        }

        public int Update(int id, Department department)
        {
            var procedureName = "sp_update_department";
            parameters.Add("Id", id);
            parameters.Add("Name", department.Name);
            var get = conn.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return get;
        }
    }
}