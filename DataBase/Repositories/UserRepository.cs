using Core.Abstractions.Repositories;
using Core.Domain;
using Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class UserRepository : IUserRepository
    {

        private const string fileName = "WebShopUsers.json";
        private Dictionary<int, User> _users; //= new Dictionary<int, User>();
        private int _id = 0;

        public UserRepository()
        {
            LoadDatabase();
        }

        public bool Delete(int id)
        {
            if(_users.Remove(id))
            {
                SaveDatabase();
                return true;
            }
            return false;
        }

        public List<User> GetAll()
        {
            return _users.Values.ToList();
        }

        public User GetUserByEmail(string email)
        {
            return _users
             .Values
            .SingleOrDefault(u =>
             u.Email.Contains(email, StringComparison.OrdinalIgnoreCase));


        }

        public User GetById(int id)
        {
            if(_users.ContainsKey(id))
            {
                return _users[id];
            }
            return null;
        }

        public User GetUserByUserName(string userName)
        {
            return _users
       .Values
       .SingleOrDefault(u =>
           u.UserName.Contains(userName, StringComparison.OrdinalIgnoreCase));
        }

        public bool Insert(User user)
        {
            if (GetUserByUserName(user.UserName) != null
                || GetUserByEmail(user.Email) != null) return false;
            user.Id = ++_id;
            _users.Add(_id, user);
            SaveDatabase();
            return true;
        }

        public bool Update(int userId, User user)
        {
            if(!_users.ContainsKey(userId))
            {
                return false;
            }

            var userByUserName = GetUserByUserName(user.UserName);

            if (userByUserName != null && userByUserName.Id != userId) return false;

            var userByEmail = GetUserByEmail(user.Email);
            
            if (userByEmail != null && userByEmail.Id != user.Id) return false;

            _users[userId] = user;
            SaveDatabase();

            return true;
        }

        private void LoadDatabase()
        {
            if (File.Exists(fileName))
            {
                try
                {
                    _users = JsonSerializer.Deserialize<Dictionary<int, User>>(
                        File.ReadAllText(fileName));
                }
                catch (Exception ex)
                {
                }
            }

            if (_users == null)
            {
                _users = new Dictionary<int, User>
                {
                    {
                        0,
                        new User
                        {
                            Id = 0,
                            Email = "admin@dualnoobrazovanje.com",
                            FirstName = "Admin",
                            LastName = "Admin",
                            Password = Encryption.Encrypt("admin"),
                            UserName = "admin"
                        }
                    }
                };
                SaveDatabase();
            }

            _id = _users.Count == 0
                ? 0
                : _users.Values.Select(p => p.Id).Max();
        }
        void SaveDatabase()
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(_users));
        }



    }

}
