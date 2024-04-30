using DAL.DataAccess;
using Entities.Models;

namespace BLL.Services
{
    public class UserService
    {
        private readonly UserDao userDao;  
        public UserService(UserDao userDao) => this.userDao = userDao;  
        public async Task<bool> SaveUserAsync( User user)=> await userDao.CreateUserAsync(user);
        public async Task<User> GetUserAsync(User user) => await userDao.GetUser(user);
    }
}
