using System;

namespace Project.Login
{
    public static class Password
    {
        public const int MinLength = 8;
        public const int MaxLength = 128;

        public static ValidationResult Validate(string password)
        {
            if (password.Length < MinLength) return ValidationResult.InvalidLength;
            return ValidationResult.Valid;
        }

        public static string GetError(ValidationResult result) =>
            result switch
            {
                ValidationResult.InvalidLength => $"Password must be between {MinLength} and {MaxLength}",
                _ => throw new ArgumentOutOfRangeException(nameof(result), result,
                    $"No error text for this " + nameof(ValidationResult))
            };

        public enum ValidationResult
        {
            Valid,
            InvalidLength
        }
    }
}