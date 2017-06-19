using System;
using System.Collections.Generic;
using System.Linq;
using ForumBll.Interface.Models;
using ForumBll.Interface.Services;
using ForumBll.Mappers;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;

namespace ForumBll.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<DalRole> roleRepository;

        public RoleService(IRepository<DalRole> repository)
        {
            this.roleRepository = repository;
        }

        public IEnumerable<BllRole> GetAllRoles()
        {
            return roleRepository.GetByPredicate(r => true)
                .Select(r => r.ToBllRole());
        }

        public void Dispose()
        {
            roleRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
