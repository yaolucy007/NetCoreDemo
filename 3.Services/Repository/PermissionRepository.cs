using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IPermissionRepository : IRepository<PermissionExtension>
    {

    }

    public class PermissionRepository : BaseRepository<PermissionExtension>, IPermissionRepository
    {
        public PermissionRepository(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {

        }
    }
}
