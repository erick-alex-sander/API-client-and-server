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

namespace API.Repositories
{
    public class DivisionsRepository : IDivisionsRepository
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);
        DynamicParameters parameters = new DynamicParameters();

        public int Create(Division division)
        {
            var procedureName = "sp_insert_division";
            parameters.Add("Name", division.Name);
            parameters.Add("department_Id", division.Department.Id);
            var create = conn.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return create;
        }

        public int Delete(int id)
        {
            var procedureName = "sp_delete_division";
            parameters.Add("Id", id);
            var delete = conn.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return delete;
        }

        public async Task<IEnumerable<Division>> Get()
        {
            var procedureName = "sp_get_division";
            var get = await conn.QueryAsync<Division, Department, Division>(procedureName, (division, department) => {
                division.Department = department;
                return division;}
            ,parameters, commandType: CommandType.StoredProcedure);
            return get;
        }

        public async Task<Division> Get(int id)
        {
            var procedureName = "sp_getid_division";
            parameters.Add("Id", id);
            var get = await conn.QueryAsync<Division, Department, Division>(procedureName, (division, department) =>
            {
                division.Department = department;
                return division;
            },
            parameters, commandType: CommandType.StoredProcedure);
            return get.First();
        }

        public int Update(int id, Division division)
        {
            var procedureName = "sp_update_division";
            parameters.Add("Id", id);
            parameters.Add("Name", division.Name);
            parameters.Add("Department_Id", division.Department.Id);
            var update = conn.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return update;
        }
    }
}