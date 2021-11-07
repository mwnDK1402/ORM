using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using static Project.Login.Password;

namespace Project.EditModeTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public sealed class Password
    {
        [TestCase("", ExpectedResult = ValidationResult.InvalidLength)]
        [TestCase("aaaabbb", ExpectedResult = ValidationResult.InvalidLength)]
        [TestCase("aaaabbbb", ExpectedResult = ValidationResult.Valid)]
        [TestCase("aaaa bbbb", ExpectedResult = ValidationResult.Valid)]
        [TestCase("aaaa愛bbbb", ExpectedResult = ValidationResult.Valid)]
        [TestCase("[aaaabbbb]", ExpectedResult = ValidationResult.Valid)]
        public ValidationResult Validate(string password) => Login.Password.Validate(password);
    }
}