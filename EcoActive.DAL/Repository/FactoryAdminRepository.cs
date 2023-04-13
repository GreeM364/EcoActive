using AutoMapper;
using EcoActive.DAL.Data;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Identity;
using EcoActive.DAL.Repository.IRepository;
using EcoActive.Utility;
using Microsoft.AspNetCore.Identity;

namespace EcoActive.DAL.Repository
{
    public class FactoryAdminRepository : Repository<FactoryAdmin>, IFactoryAdminRepository
    {
        private readonly EcoActiveDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public FactoryAdminRepository(EcoActiveDbContext db, UserManager<ApplicationUser> userManager,
                                      IMapper mapper) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task CreateAsync(FactoryAdmin entity, string password)
        {
            entity.CreatedDate = DateTime.Now;
            //TODO entity.CreatedBy 

            _db.FactoryAdmins.Add(entity);
            _db.BaseUsers.Add(entity.User);

            var applicationUser = _mapper.Map<User, ApplicationUser>(entity.User);
            var identityResult = await _userManager.CreateAsync(applicationUser, password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(applicationUser, new List<string>()
                {
                    CustomRoles.User,
                    CustomRoles.FactoryAdmin,
                });
            }
            else
            {
                throw new Exception("Error while creating Factory Admin account"); // TODO: probably create IdentityException
            }

            await _db.SaveChangesAsync();
        }

        public async Task<FactoryAdmin> UpdateAsync(FactoryAdmin entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            //TODO entity.LastModifiedBy

            _db.FactoryAdmins.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
