using System;

namespace Project.Login
{
    public static class Username
    {
        public const int MinLength = 1;
        public const int MaxLength = 32;

        public static ValidationResult Validate(string username)
        {
            if (username.Length is < MinLength or > MaxLength) return ValidationResult.InvalidLength;
            return ValidationResult.Valid;
        }
        
        public static string GetError(ValidationResult result) =>
            result switch
            {
                ValidationResult.InvalidLength => $"Username must be between {MinLength} and {MaxLength}",
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