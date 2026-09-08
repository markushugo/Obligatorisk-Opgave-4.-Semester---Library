using Obligatorisk_Opgave_4._Semester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorisk_Opgave_4._Semester.IRepos
{
    public interface IUserRepo
    {
        User? GetById(int id);
        User? GetByUsername(string username);
        User? GetByEmail(string email);
        void UpdateUser(User user);
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}
