﻿using marketplace.Context;
using marketplace.DTO.UserDTO;
using marketplace.Models;
using marketplace.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Repositories
{
    public class UserRepository : GenericRepository<User, AppDbContext>, IUserRepository
    {
        readonly AppDbContext AppDbContext;
		private readonly IConfiguration _configuration;

		public UserRepository(AppDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            AppDbContext = dbContext;
			_configuration = configuration;
		}

        public User AuthenticateUser(LoginDTO loginCredentials)
        {
			string key = _configuration.GetSection("Encrypt")["Key"];
			User user = AppDbContext.Users.Where(x => x.username == loginCredentials.username).Include(user=> user.Role).FirstOrDefault();
            return user;
        }

        public User GetByEmail(string email)
        {
            return AppDbContext.Users.FirstOrDefault(c => c.email == email && c.deleted == false);
        }

        public User GetByName(string name)
        {
            return AppDbContext.Users.FirstOrDefault(c => c.name == name && c.deleted == false);
        }

        public User GetByUsername(string username)
        {
            return AppDbContext.Users.FirstOrDefault(c => c.username == username && c.deleted == false);
        }

        public List<User> GetAll()
        {
            List<User> users = AppDbContext.Users.Where(user => user.deleted == false).OrderBy(user => user.name).ToList();
            return users;
        }
    }
}
