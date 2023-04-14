﻿using EcoActive.DAL.Data;
using EcoActive.DAL.Entities;
using EcoActive.DAL.Identity;
using EcoActive.DAL.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using EcoActive.Utility;
using EcoActive.DAL.Infrastructure;

namespace EcoActive.DAL.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly EcoActiveDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public EmployeeRepository(EcoActiveDbContext db, UserManager<ApplicationUser> userManager,
                                  IMapper mapper) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateAsync(Employee entity, string password)
        {
            entity.CreatedDate = DateTime.Now;

            _db.FactoryEmployees.Add(entity);
            _db.BaseUsers.Add(entity.User);

            var applicationUser = _mapper.Map<User, ApplicationUser>(entity.User);
            var identityResult = await _userManager.CreateAsync(applicationUser, password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(applicationUser, new List<string>()
                {
                    CustomRoles.User,
                    CustomRoles.Employee,
                });
            }
            else
            {
                throw new IdentityException("Error while creating Employee account");
            }

            await _db.SaveChangesAsync();
        }

        public async Task<Employee> UpdateAsync(Employee entity)
        {
            entity.LastModifiedDate = DateTime.Now;

            _db.FactoryEmployees.Update(entity);

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
