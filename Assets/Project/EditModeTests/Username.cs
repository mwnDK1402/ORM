using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using static Project.Login.Username;

namespace Project.EditModeTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public sealed class Username
    {
        [TestCase("", ExpectedResult = ValidationResult.InvalidLength)]
        [TestCase("a", ExpectedResult = ValidationResult.Valid)]
        [TestCase("aaaabbbbccccddddeeeeffffgggghhhh", ExpectedResult = ValidationResult.Valid)]
        [TestCase("aaaabbbbccccddddeeeeffffgggghhhhi", ExpectedResult = ValidationResult.InvalidLength)]
        [TestCase("aaaa bbbb", ExpectedResult = ValidationResult.Valid)]
        [TestCase("aaaa愛bbbb", ExpectedResult = ValidationResult.Valid)]
        [TestCase("[aaaabbbb]", ExpectedResult = ValidationResult.Valid)]
        public ValidationResult Validate(string username) => Login.Username.Validate(username);
    }
}