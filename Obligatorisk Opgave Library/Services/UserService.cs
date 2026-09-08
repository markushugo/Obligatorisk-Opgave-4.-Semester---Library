using Obligatorisk_Opgave_4._Semester.IRepos;
using Obligatorisk_Opgave_4._Semester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorisk_Opgave_4._Semester.Services
{
    public class UserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public User? GetById(int id) => _userRepo.GetById(id);

        public User? GetByUsername(string username) => _userRepo.GetByUsername(username);

        public User? GetByEmail(string email) => _userRepo.GetByEmail(email);

        public void CreateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(user.Username)) throw new ArgumentException("Username required", nameof(user));
            if (string.IsNullOrWhiteSpace(user.Password)) throw new ArgumentException("Password required", nameof(user));
            _userRepo.CreateUser(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            _userRepo.UpdateUser(user);
        }

        public void DeleteUserById(int id)
        {
            var user = _userRepo.GetById(id);
            if (user == null) return;
            _userRepo.DeleteUser(user);
        }
    }
}
