using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Helpers;
using Xunit;
using Xunit.Abstractions;

namespace DayTwo
{
    public class DayTwo
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public DayTwo(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private static IEnumerable<int> GetOutputArray(IEnumerable<int> inputArray, int elementAtOne, int elementAtTwo)
        {
            var inputList = inputArray.ToList();
            inputList[1] = elementAtOne;
            inputList[2] = elementAtTwo;
            var currentIndex = 0;

            while (inputList[currentIndex] != 99)
            {
                if (inputList[currentIndex] == 1)
                {
                    inputList[inputList[currentIndex + 3]] = inputList[inputList[currentIndex + 1]] + inputList[inputList[currentIndex + 2]];
                }
                else
                {
                    inputList[inputList[currentIndex + 3]] = inputList[inputList[currentIndex + 1]] * inputList[inputList[currentIndex + 2]];
                }
                currentIndex += 4;
            }
            return inputList;
        }

        [Fact]
        public void PartOne()
        {
            var inputArray = InputParsingHelpers.ParseFileAsIntArray("../../../input.txt", ',');

            var result = GetOutputArray(inputArray, 12, 2).ToList();

            result.ElementAt(0).Should().Be(3706713);
            _testOutputHelper.WriteLine($"Day Two Part One: {result.ElementAt(0)}");

        }

        [Fact]
        public void PartTwo()
        {
            var inputArray = InputParsingHelpers.ParseFileAsIntArray("../../../input.txt", ',');
            var noun = 0;
            var verb = 0;

            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var resultArray = GetOutputArray(inputArray, i, j).ToList();
                    if (resultArray.ElementAt(0) != 19690720) continue;
                    noun = resultArray.ElementAt(1);
                    verb = resultArray.ElementAt(2);
                    i = 99;
                    break;
                }
            }
            var result = 100 * noun + verb;

            result.Should().Be(8609);
            _testOutputHelper.WriteLine($"Day Two Part Two: {result}");

        }

        [Theory]
        [InlineData("1,0,0,0,99", "2,0,0,0,99")]
        [InlineData("2,3,0,3,99", "2,3,0,6,99")]
        [InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        [InlineData("1,9,10,3,2,3,11,0,99,30,40,50", "3500,9,10,70,2,3,11,0,99,30,40,50")]
        public void ShouldReturnOpCodeArray(string input, string expectedOutput)
        {
            var inputArray = input.Split(",").Select(int.Parse).ToList();
            var outputArray = expectedOutput.Split(",").Select(int.Parse).ToList();

            var result = GetOutputArray(inputArray, inputArray.ElementAt(1), inputArray.ElementAt(2)).ToList();
            for (var i = 0; i < result.Count(); i++)
            {
                result.ElementAt(i).Should().Be(outputArray.ElementAt(i));
            }
        }

    }
}
