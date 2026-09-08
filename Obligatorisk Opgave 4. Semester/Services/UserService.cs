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

        public User? GetById(int id)
        {
            return _userRepo.GetById(id);
        }

        public User? GetByUsername(string username)
        {
            return _userRepo.GetByUsername(username);
        }

        public User? GetByEmail(string email)
        {
            return _userRepo.GetByEmail(email);
        }

        public void CreateUser(User user)
        {
            // basic validation
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(user.Username)) throw new ArgumentException("Username is required", nameof(user));
            if (string.IsNullOrWhiteSpace(user.Password)) throw new ArgumentException("Password is required", nameof(user));

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
