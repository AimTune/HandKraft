namespace HandKraft.Abstractions.Errors
{
    /// <summary>
    /// Represents a standard error with a code, description, and type.
    /// Immutable record with value-based equality.
    /// </summary>
    public sealed record Error : IEquatable<Error>
    {
        /// <summary>
        /// Represents a non-error (no error) instance.
        /// </summary>
        public static readonly Error None = new(string.Empty, string.Empty, ErrorTypes.Failure);

        /// <summary>
        /// Represents an error for null or missing values.
        /// </summary>
        public static readonly Error NullValue = new(
            "General.Null",
            "Null value was provided",
            ErrorTypes.Failure);

        /// <summary>
        /// Gets the unique error code identifier.
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets a human-readable description of the error.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Gets the type/category of the error.
        /// </summary>
        public ErrorTypes Type { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> record.
        /// </summary>
        /// <param name="code">Unique error code.</param>
        /// <param name="description">Error description message.</param>
        /// <param name="type">Type/category of the error.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="code"/> is null or whitespace.</exception>
        public Error(string code, string description, ErrorTypes type)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Error code cannot be null or empty.", nameof(code));

            Code = code;
            Description = description;
            Type = type;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Error"/> is equal to the current one.
        /// </summary>
        /// <param name="other">The other error to compare with.</param>
        /// <returns><c>true</c> if the errors are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(Error? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Code == other.Code &&
                   Description == other.Description &&
                   Type == other.Type;
        }

        /// <summary>
        /// Returns a hash code for the current <see cref="Error"/>.
        /// </summary>
        /// <returns>A hash code representing the current error.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Code, Description, Type);
        }

        /// <summary>
        /// Returns the string representation of the error, which is the error code.
        /// </summary>
        /// <returns>The error code.</returns>
        public override string ToString() => Code;
    }
}