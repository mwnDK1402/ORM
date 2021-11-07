using System;
using JetBrains.Annotations;
using MongoDB.Driver;
using Project.Login;
using Project.Security;

namespace Project.Database
{
    [UsedImplicitly]
    public sealed class UserQueries
    {
        private readonly IMongoCollection<User> users;
        private readonly Hasher hasher;

        public UserQueries(IMongoCollection<User> users, Hasher hasher)
        {
            this.users = users;
            this.hasher = hasher;
        }

        public void Insert(string username, string password)
        {
            var salt = Hasher.CreateSalt();
            var hash = hasher.HashPassword(password, salt);

            if (CheckUsernameIsTaken(username))
                throw new InvalidOperationException($"User {username} already exists");
            
            users
                .InsertOne(new User
                {
                    Username = username,
                    HashedPassword = Convert.ToBase64String(hash),
                    Salt = Convert.ToBase64String(salt)
                });
        }

        private bool CheckUsernameIsTaken(string username) =>
            users.CountDocuments(Builders<User>.Filter.Eq(u => u.Username, username)) > 0;

        public bool TryLogin(string username, string password, out User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            user = users.Find(filter).SingleOrDefault();
            if (user == default) return false;

            var salt = Convert.FromBase64String(user.Salt);
            var actualPassword = Convert.FromBase64String(user.HashedPassword);
            return hasher.VerifyHash(password, salt, actualPassword);
        }
    }
}