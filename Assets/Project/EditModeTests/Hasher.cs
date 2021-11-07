using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using static Project.Security.Hasher;

namespace Project.EditModeTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public sealed class Hasher
    {
        private static readonly Security.Hasher Instance = new(1, 1, 16);

        [TestCase("a", "a", ExpectedResult = true)]
        [TestCase("æøå", "æøå", ExpectedResult = true)]
        [TestCase("愛", "愛", ExpectedResult = true)]
        [TestCase("b", "a", ExpectedResult = false)]
        [TestCase("aeoeaa", "æøå", ExpectedResult = false)]
        [TestCase("ai`", "愛", ExpectedResult = false)]
        public bool Passwords_Are_Verified(string input, string password)
        {
            var salt = CreateSalt();
            var hash = Instance.HashPassword(password, salt);
            GC.Collect();
            return Instance.VerifyHash(input, salt, hash);
        }

        [Test]
        public void Empty_Password_Throws_ArgumentException()
        {
            var salt = CreateSalt();
            Assert.Throws<ArgumentException>(() => Instance.HashPassword(string.Empty, salt));
        }
    }
}