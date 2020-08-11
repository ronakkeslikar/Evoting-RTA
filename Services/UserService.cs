using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using evoting.Persistence.Contexts;
using evoting.Persistence.Contexts.Sp_SQL_Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using static evoting.Persistence.Contexts.Sp_SQL_Objects.SP_objectParam;
using System.Data;
using Microsoft.Data.SqlClient;
using evoting.Domain.Models;

namespace evoting.Services
{
    public interface IUserService
    {
        
    }

    public class UserService : IUserService
    {
        //db context here
        protected readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }         
        
    }
}
 