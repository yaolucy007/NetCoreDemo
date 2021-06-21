using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IRoleRepository : IRepository<RoleExtension>
    {

    }

    public class RoleRepository : BaseRepository<RoleExtension>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {

        }

    }
}
