using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace DayFour
{
    public class DayFour
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public DayFour(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public bool TestPasswordAgainstCriteria(int password)
        {
            var hasDouble = false;

            var digitArray = password.ToString().Select(x => int.Parse(x.ToString())).ToList();
            var currentDigit = digitArray.ElementAt(0);

            for (var i = 1; i < 6; i++)
            {
                if (currentDigit == digitArray.ElementAt(i))
                {
                    hasDouble = true;
                } else if (currentDigit > digitArray.ElementAt(i))
                {
                    return false;
                }

                currentDigit = digitArray.ElementAt(i);
            }

            return hasDouble;
        }
        public bool TestPasswordAgainstNewCriteria(int password)
        {
            var hasDouble = false;

            var longerThanDouble = false;
            var previousDigit = -1;

            var digitArray = password.ToString().Select(x => int.Parse(x.ToString())).ToList();
            var currentDigit = digitArray.ElementAt(0);

            for (var i = 1; i < 6; i++)
            {
                if (currentDigit > digitArray.ElementAt(i))
                {
                    return false;
                }

                if (currentDigit == digitArray.ElementAt(i))
                {
                    if (previousDigit != digitArray.ElementAt(i))
                    {
                        previousDigit = currentDigit;
                    }
                    else
                    {
                        longerThanDouble = true;
                    }
                }
                else
                {
                    if (currentDigit == previousDigit && !longerThanDouble)
                    {
                        hasDouble = true;
                    }
                    longerThanDouble = false;
                }
                currentDigit = digitArray.ElementAt(i);
            }

            if (!longerThanDouble && currentDigit == digitArray.ElementAt(digitArray.Count-2))
            {
                return true;
            }

            return hasDouble;
        }

        [Fact]
        public void PartOne()
        {
            const int min = 264793;
            const int max = 803935;
            var count = 0;

            for (var i = min; i <= max; i++)
            {
                if (TestPasswordAgainstCriteria(i))
                {
                    count++;
                }
            }

            count.Should().Be(966);
            _testOutputHelper.WriteLine($"Day Four Part One: {count}");
        }

        [Fact]
        public void PartTwo()
        {
            const int min = 264793;
            const int max = 803935;
            var count = 0;

            for (var i = min; i <= max; i++)
            {
                if (TestPasswordAgainstNewCriteria(i))
                {
                    count++;
                }
            }

            count.Should().Be(628);
            _testOutputHelper.WriteLine($"Day Four Part Two: {count}");
        }

        [Theory]
        [InlineData(111111, true)]
        [InlineData(223450, false)]
        [InlineData(123789, false)]
        [InlineData(123876, false)]
        public void ShouldTestPasswordCriteria(int password, bool expectedOutput)
        {
            var result = TestPasswordAgainstCriteria(password);

            result.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(112233, true)]
        [InlineData(123444, false)]
        [InlineData(111122, true)]  
        public void ShouldTestNewPasswordCriteria(int password, bool expectedOutput)
        {
            var result = TestPasswordAgainstNewCriteria(password);

            result.Should().Be(expectedOutput);
        }
    }
}
