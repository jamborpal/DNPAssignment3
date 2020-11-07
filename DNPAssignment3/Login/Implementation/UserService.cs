using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace Assignment2.Login
{
    public class UserService : IUserService
    {
        private List<User> users;
        private string userFile = "users.json";

        public UserService()
        {
            string content = File.ReadAllText(userFile);
            users = JsonSerializer.Deserialize<List<User>>(content); }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            
                User user=new User();
                foreach (var VARIABLE in users)
                {
                    if (VARIABLE.UserName.Equals(username) && VARIABLE.Password.Equals(password))
                    {
                        user = VARIABLE;
                    }
                }
                if (user != null)
                {
                    return user;
                } 
                throw new Exception("User not found");
            
        }

        public async Task<User> AddUserAsync(User user)
        {
            users.Add(user);
            writeUserFile(users);
            return user;
        }

        private void writeUserFile(List<User> user)
        {
            string productAsJson = JsonSerializer.Serialize(user);
            File.WriteAllText(userFile, productAsJson);
        }

        public async Task<IList<User>> getUsersAsync()
        {
            return users;
        }

        public async Task RemoveUserAsync(string usernametoRemove)
        {
            User toBeRemoved = users.First(t => t.UserName.Equals(usernametoRemove));
            users.Remove(toBeRemoved);
            writeUserFile(users);
        }
    }
}