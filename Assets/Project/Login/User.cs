using JetBrains.Annotations;
using MongoDB.Bson;

namespace Project.Login
{
    public sealed class User
    {
        [UsedImplicitly] public ObjectId Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}