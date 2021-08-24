using System;

namespace BuildingBlocks.Domain
{
    /// <summary>
    /// The implementation of the <see cref="IComparable.CompareTo"/> method must return an Int32 that has one of three values.
    /// <remarks> for more information see <see href="https://docs.microsoft.com/en-us/dotnet/api/system.icomparable?view=net-5.0"/></remarks>
    /// </summary>
    public enum ComparableResult
    {
        /// <summary>
        /// The current instance precedes the object specified by the CompareTo method in the sort order.
        /// </summary>
        Precede = -1,

        /// <summary>
        /// This current instance occurs in the same position in the sort order as the object specified by the CompareTo method.
        /// </summary>
        Same = 0,

        /// <summary>
        /// This current instance follows the object specified by the CompareTo method in the sort order.
        /// </summary>
        Follow = 1
    }
}