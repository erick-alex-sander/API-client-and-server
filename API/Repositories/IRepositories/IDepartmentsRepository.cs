﻿using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.IRepositories
{
    public interface IDepartmentsRepository
    {
        Task<IEnumerable<Department>> Get();
        Task<Department> Get(int id);

        int Create(Department department);
        int Update(int id, Department department);
        int Delete(int id);
    }
}
