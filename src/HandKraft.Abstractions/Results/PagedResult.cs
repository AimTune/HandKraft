using HandKraft.Abstractions.Errors;

namespace HandKraft.Abstractions.Results
{
    /// <summary>
    /// Represents a paged result of an operation, including pagination metadata.
    /// </summary>
    /// <typeparam name="TValue">The type of the items in the paged result.</typeparam>
    public class PagedResult<TValue> : Result<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{TValue}"/> class.
        /// </summary>
        /// <param name="value">The paged data value (e.g. a collection or page data).</param>
        /// <param name="isSuccess">Whether the operation was successful.</param>
        /// <param name="error">The error details if failed; <see cref="Error.None"/> if successful.</param>
        /// <param name="totalCount">Total number of items available (for pagination).</param>
        /// <param name="pageNumber">Current page number (1-based).</param>
        /// <param name="pageSize">Size of each page.</param>
        public PagedResult(
            TValue? value,
            bool isSuccess,
            Error error,
            int totalCount,
            int pageNumber,
            int pageSize)
            : base(value, isSuccess, error)
        {
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// Gets the total number of items available across all pages.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Gets the current page number (1-based).
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Gets the size of each page.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Creates a successful paged result.
        /// </summary>
        /// <param name="value">The paged data value.</param>
        /// <param name="totalCount">Total number of items available.</param>
        /// <param name="pageNumber">Current page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <typeparam name="TValue">Type of the items.</typeparam>
        /// <returns>A successful <see cref="PagedResult{TValue}"/>.</returns>
        public static PagedResult<TValue> Success(
            TValue value,
            int totalCount,
            int pageNumber,
            int pageSize) =>
            new(value, true, Error.None, totalCount, pageNumber, pageSize);

        /// <summary>
        /// Creates a failed paged result.
        /// </summary>
        /// <param name="error">The error describing the failure.</param>
        /// <typeparam name="TValue">Type of the items.</typeparam>
        /// <returns>A failed <see cref="PagedResult{TValue}"/>.</returns>
        public static new PagedResult<TValue> Failure(Error error) =>
            new(default, false, error, 0, 0, 0);
    }
}
