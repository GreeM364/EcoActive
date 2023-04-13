﻿using AutoMapper;
using EcoActive.DAL.Data;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Identity;
using EcoActive.DAL.Repository.IRepository;
using EcoActive.Utility;
using Microsoft.AspNetCore.Identity;

namespace EcoActive.DAL.Repository
{
    public class ActivistRepository : Repository<Activist>, IActivistRepository
    {
        private readonly EcoActiveDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public ActivistRepository(EcoActiveDbContext db, UserManager<ApplicationUser> userManager,
                                  IMapper mapper) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateAsync(Activist entity, string password)
        {
            entity.CreatedDate = DateTime.Now;
            //TODO entity.CreatedBy 

            _db.Activists.Add(entity);
            _db.BaseUsers.Add(entity.User);

            var applicationUser = _mapper.Map<User, ApplicationUser>(entity.User);
            var identityResult = await _userManager.CreateAsync(applicationUser, password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(applicationUser, new List<string>()
                {
                    CustomRoles.User,
                    CustomRoles.Activist,
                });
            }
            else
            {
                throw new Exception("Error while creating Activist account"); // TODO: probably create IdentityException
            }

            await _db.SaveChangesAsync();
        }

        public async Task<Activist> UpdateAsync(Activist entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            //TODO entity.LastModifiedBy

            _db.Activists.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}