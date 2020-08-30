using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.IRepositories
{
    interface IDivisionsRepository
    {
        Task<IEnumerable<Division>> Get();
        Task<Division> Get(int id);

        int Create(Division division);
        int Update(int id, Division division);
        int Delete(int id);
    }
}
