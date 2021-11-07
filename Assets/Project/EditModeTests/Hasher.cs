using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Project.Security;
using Zenject;
using static Project.Security.Hasher;

namespace Project.EditModeTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public sealed class Hasher : ZenjectUnitTestFixture
    {
        [Inject] private Security.Hasher hasher;

        [SetUp]
        public void Install()
        {
            HasherInstaller.InstallFromResource(Container);
            Container.Inject(this);
        }
        
        [TestCase("a", "a", ExpectedResult = true)]
        [TestCase("æøå", "æøå", ExpectedResult = true)]
        [TestCase("愛", "愛", ExpectedResult = true)]
        [TestCase("b", "a", ExpectedResult = false)]
        [TestCase("aeoeaa", "æøå", ExpectedResult = false)]
        [TestCase("ai`", "愛", ExpectedResult = false)]
        public bool Passwords_Are_Verified(string input, string password)
        {
            var salt = CreateSalt();
            var hash = hasher.HashPassword(password, salt);
            GC.Collect();
            return hasher.VerifyHash(input, salt, hash);
        }

        [Test]
        public void Empty_Password_Throws_ArgumentException()
        {
            var salt = CreateSalt();
            Assert.Throws<ArgumentException>(() => hasher.HashPassword(string.Empty, salt));
        }
    }
}