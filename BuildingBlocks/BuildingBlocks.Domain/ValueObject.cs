#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildingBlocks.Domain
{
    /// <summary>
    /// Here we use record that introduced in C# 9 as value object.
    /// <para>
    /// Value object is actually a concept that measures, quantifies, or otherwise describes a thing in the domain.
    /// </para>
    /// Value object characteristics that mentioned by Vaughn in his book, "Implementing Domain-Driven Design".
    /// <list type="bullet">
    /// <item>Immutable: An object that is a value is unchangeable after it has been created.</item>
    /// <item>Conceptual Whole: Object may possess a number of individual attributes, each of which is related to the others.</item>
    /// <item>Replaceability: The entire value is completely replaced with new value that does represent the currently correct whole.</item>
    /// <item>Value Equality: If both the types and value objects attribute are equal, the value object are consider equal.</item>
    /// <item>Side-Effect-Free Behavior: A function is an operation of an object that produce output but without modifying its own state.
    /// The methods of an immutable value objects must all be Side-Effect-Free Functions.</item>
    /// </list>
    /// <para>
    /// There are several problem in using Record as value object in domain.
    /// Valdimir has a nice post to describe several problems for record when used as Value object in DDD.
    /// you can take look at the Valdimir post from <see href="https://enterprisecraftsmanship.com/posts/csharp-records-value-objects/"/>
    /// <list type="bullet">
    /// <item><see cref="IComparable"/> interface: records don't implement <see cref="IComparable"/>. if you try order or sort value objects in a array, runtime throw exception.</item>
    /// <item>Encapsulation: stand for protection of application invariants: you should not be able to instantiate a value object in an invalid state</item>
    /// <item>Precise control over equality checks: Exclusion of equality components, Comparison precision, Collection comparison</item>
    /// <item>The presence of the base class:  inheriting form a base value object shows that is part of the domain model unambiguously</item>
    /// <item>The problem with with: we can bypass the invariant check and create an invalid value object</item>
    /// </list>
    /// </para>
    /// </summary>
    [Serializable]
    public abstract record ValueObject : IComparable, IComparable<ValueObject>
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        #region Comparable
        public int CompareTo(object? obj)
        {
            if (obj == null)
                return 0;

            Type thisType = GetType();
            Type otherType = obj.GetType();

            if (thisType != otherType)
                throw new NotSupportedException("CompareTo is not supported for different value object type.");

            ValueObject other = (ValueObject)obj;

            if (this == other)
                return (int)ComparableResult.Same;

            foreach (var (thisComponent, otherComponent) in GetEqualityComponents().Zip(other.GetEqualityComponents()))
            {
                if (thisComponent is IComparable comparable1 && otherComponent is IComparable comparable2)
                {
                    var compareResult = (ComparableResult)comparable1.CompareTo(comparable2);
                    if (compareResult != ComparableResult.Same)
                        return (int)compareResult;
                }
                else
                    throw new NotSupportedException("CompareTo is not supported for different value object type.");
            }
            return (int)ComparableResult.Same;
        }

        public int CompareTo(ValueObject other)
        {
            return CompareTo(other as object);
        }
        #endregion

    }
}