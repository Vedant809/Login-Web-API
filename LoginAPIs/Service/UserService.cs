using LoginAPIs.Interface;
using LoginAPIs.Service;
using LoginAPIs.Repository;
using LoginAPIs.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.AspNetCore.Http.HttpResults;


namespace LoginAPIs.Service
{
    public class UserService:IUser
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        //Login
        public bool login(Users user)
        {
            var authenticated_user = _context.Users.Where(u => u.username == user.username).Where(p => p.password == user.password);
            if(authenticated_user.Any())
            {
                return true;
            }
            return false;
        }

        //Create a User
        public string createUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChangesAsync();

            return user.username;

        }

        //Get All Users
        public async Task<List<Users>> getAllUsers()
        {
            var userList = await _context.Users.ToListAsync();
            return userList;
        }

        public string deleteUser(int id)
        {
            var users = _context.Users.Where(x=>x.UserId == id).ToList();
            _context.Users.Remove(users[0]);
            _context.SaveChangesAsync();
            return users[0].username;
        }

        public async Task<Users> updateUserInfo(Users user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


    }
}
