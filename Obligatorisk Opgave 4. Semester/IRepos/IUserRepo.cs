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
        public void GetByUsername(string username);
        public void GetByEmail(string email);
        public void UpdateUser(User user);
        public void CreateUser(User user);
    }
}
