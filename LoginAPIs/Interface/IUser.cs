using LoginAPIs.Model;
using System.Threading.Tasks;
using System;

namespace LoginAPIs.Interface
{
    public interface IUser
    {
        public bool login(Users user);
        public string createUser(Users user);
        public Task<List<Users>> getAllUsers();
        public string deleteUser(int id);
        public Task<Users> updateUserInfo(Users user);
    }
}
