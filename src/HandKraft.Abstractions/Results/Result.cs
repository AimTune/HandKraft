using HandKraft.Abstractions.Errors;
using System.Diagnostics.CodeAnalysis;

namespace HandKraft.Abstractions.Results;

/// <summary>
/// Represents the outcome of an operation, indicating success or failure.
/// </summary>
public class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess">Whether the operation was successful.</param>
    /// <param name="error">The error details if failed; <see cref="Error.None"/> if successful.</param>
    /// <exception cref="ArgumentException">Thrown if the combination of success and error is invalid.</exception>
    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error.", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Indicates whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// The error associated with this result, or <see cref="Error.None"/> if successful.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Creates a successful result without a value.
    /// </summary>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">The error describing why the operation failed.</param>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Creates a successful result containing a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value.</param>
    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, Error.None);

    /// <summary>
    /// Creates a failed result containing a value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="error">The error describing why the operation failed.</param>
    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);
}

/// <summary>
/// Represents the outcome of an operation that returns a value.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public class Result<TValue>(TValue? value, bool isSuccess, Error error)
    : Result(isSuccess, error)
{
    /// <summary>
    /// Gets the value if the result is successful.
    /// Throws an <see cref="InvalidOperationException"/> if accessed when the result is a failure.
    /// </summary>
    [NotNull]
    public TValue Value =>
        IsSuccess
            ? value!
            : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    /// <summary>
    /// Implicitly converts a value to a successful result.
    /// Returns a failure with <see cref="Error.NullValue"/> if the value is null.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

    /// <summary>
    /// Creates a validation failure result.
    /// </summary>
    /// <param name="error">The validation error.</param>
    public static Result<TValue> ValidationFailure(Error error) =>
        new(default, false, error);
}