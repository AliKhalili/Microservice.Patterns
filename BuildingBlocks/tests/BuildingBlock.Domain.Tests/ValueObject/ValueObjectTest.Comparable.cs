using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace BuildingBlock.Domain.Tests.ValueObject
{
    public class ValueObjectTest
    {

        [Fact]
        public void Should_SortNumbersAscending_When_CompareSimpleNumberValueObjects()
        {
            var sortedNumbers = SimpleNumberValueObject.UnorderedNumbers.OrderBy(x => x).ToArray();

            sortedNumbers.Should().BeInAscendingOrder(x => x.Number);
        }

        [Fact]
        public void Should_SortNumbersDescending_When_CompareSimpleNumberValueObjects()
        {
            var sortedNumbers = SimpleNumberValueObject.UnorderedNumbers.OrderByDescending(x => x).ToArray();

            sortedNumbers.Should().BeInDescendingOrder(x => x.Number);
        }

        [Fact]
        public void Should_SortCitiesAscending_When_CompareCitiesValueObjects()
        {
            var sortedNumbers = CityValueObject.UnorderedCities.OrderBy(x => x.Country).ThenBy(x => x).ToArray();

            sortedNumbers.Should().BeInAscendingOrder(x => x.Country).And.ThenBeInAscendingOrder(x => x.CityName);
        }

        [Fact]
        public void Should_SortCitiesDescending_When_CompareCitiesValueObjects()
        {
            var sortedNumbers = CityValueObject.UnorderedCities.OrderByDescending(x => x).ToArray();

            sortedNumbers.Should().BeInDescendingOrder(x => x.Country).And.ThenBeInDescendingOrder(x => x.CityName);
        }

        [Fact]
        public void Should_ThrowException_When_CompareValueObjectsInDifferentType()
        {
            var unsortedValueObjectInDifferentValueType = new BuildingBlocks.Domain.ValueObject[] { new SimpleNumberValueObject(1), new CityValueObject("Paris", "France") };
            
            var exercise = FluentActions.Invoking(() => unsortedValueObjectInDifferentValueType.OrderBy(x => x).ToArray());

            exercise.Should().Throw<InvalidOperationException>().WithInnerException<NotSupportedException>();
        }

        private record SimpleNumberValueObject(int Number) : BuildingBlocks.Domain.ValueObject
        {
            public static readonly SimpleNumberValueObject[] UnorderedNumbers = { new(5), new(1), new(3), new(4), new(2) };
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Number;
            }
        }
        private record CityValueObject(string CityName, string Country) : BuildingBlocks.Domain.ValueObject
        {
            public static readonly CityValueObject[] UnorderedCities =
            {
                new("London","United Kingdom"),
                new("Tehran","Iran"),
                new("Mashhad", "Iran"),
                new("Reading", "United Kingdom"),
                new("Birmingham", "United Kingdom"),
                new("Isfahan", "Iran")
            };

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Country;
                yield return CityName;
            }
        }

        //private record IncomparableInnerValueObject
        //private record IncomparableValueObject(int Id,):BuildingBlocks.Domain.ValueObject
    }
}