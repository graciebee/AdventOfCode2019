using System;
using System.Linq;
using FluentAssertions;
using Helpers;
using Xunit;

namespace DayOne
{
    public class DayOne
    {
        public double GetFuelFromMass(double mass)
        {
            return Math.Floor(mass / 3) - 2;
        }

        public double GetFuelFromMassRecursive(double mass)
        {
            var fuel = Math.Floor(mass / 3) - 2;
            if (fuel <= 0) return 0;
            return GetFuelFromMassRecursive(fuel) + fuel;
        }

        [Fact]
        public void PartOne()
        {
            var inputArray = InputParsingHelpers.ParseFileAsDoubleArray("../../../input.txt");
            var sums = inputArray.Sum(GetFuelFromMass);

            sums.Should().Be(3087896);
        }

        [Fact]
        public void PartTwo()
        {
            var inputArray = InputParsingHelpers.ParseFileAsDoubleArray("../../../input.txt");
            var sums = inputArray.Sum(GetFuelFromMassRecursive);

            sums.Should().Be(4628989);
        }

        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void ShouldReturnFuel(double input, double expectedOutput)
        {
            GetFuelFromMass(input).Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void ShouldReturnFuelRecursive(double input, double expectedOutput)
        {
            GetFuelFromMassRecursive(input).Should().Be(expectedOutput);
        }
    }
}
