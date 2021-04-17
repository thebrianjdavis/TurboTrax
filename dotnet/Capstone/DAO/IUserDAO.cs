using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IUserDAO
    {
        User GetUser(string username);
        User AddUser(string username, string firstName, string lastName, string password, string role);
        List<ReturnUser> ReturnUserList();
    }
}
