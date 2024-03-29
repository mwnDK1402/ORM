﻿using NUnit.Framework;
using Project.Database;
using Project.Security;
using Zenject;

namespace Project.EditModeTests
{
    public sealed class UserQueries : ZenjectUnitTestFixture
    {
        [Inject] private Database.UserQueries queries;
        
        [SetUp]
        public void Install()
        {
            HasherInstaller.InstallFromResource(Container);
            DatabaseInstaller.InstallFromResource(Container);
            Container.Inject(this);
        }
        
        [TestCase("ValidUsername", "ValidPassword", ExpectedResult = false)]
        public bool Find_User(string username, string password) => queries.FindUser(username, password) != default;
    }
}