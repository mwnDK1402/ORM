using MongoDB.Driver;
using Project.Login;
using UnityEngine;
using Zenject;

namespace Project.Database
{
    [CreateAssetMenu(menuName = "Installers/Create " + nameof(DatabaseInstaller), fileName = nameof(DatabaseInstaller))]
    public sealed class DatabaseInstaller : ScriptableObjectInstaller<DatabaseInstaller>
    {
        [SerializeField] private string url = "mongodb://localhost:27017";
        [SerializeField] private string database = "unity_orm";
        [SerializeField] private string userCollection = "user";

        public override void InstallBindings()
        {
            Container
                .Bind<MongoClient>()
                .FromMethod(() => new MongoClient(url))
                .AsSingle();
            Container
                .Bind<IMongoDatabase>()
                .FromResolveGetter<MongoClient>(client => client.GetDatabase(database))
                .AsSingle();
            Container
                .Bind<IMongoCollection<User>>()
                .FromResolveGetter<IMongoDatabase>(db => db.GetCollection<User>(userCollection))
                .AsSingle();
            Container
                .Bind<UserQueries>()
                .AsSingle();
        }
    }
}