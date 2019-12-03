using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Helpers;
using Xunit;
using Xunit.Abstractions;

namespace DayThree
{
    public class DayThree
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public DayThree(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private static Dictionary<Tuple<int, int>, Tuple<string, int>> GetGrid(IEnumerable<string> wireOne, IEnumerable<string> wireTwo)
        {
            var map = new Dictionary<Tuple<int, int>, Tuple<string, int>>();
            map = ApplyMovements(map, wireOne, "+");
            map = ApplyMovements(map, wireTwo, "~");
            return map;
        }

        private static Dictionary<Tuple<int, int>, Tuple<string, int>> ApplyMovements(Dictionary<Tuple<int, int>, Tuple<string, int>> map, IEnumerable<string> movements, string character)
        {
            var currentPosition = Tuple.Create(0, 0);
            var distance = 0;
            foreach (var movement in movements)
            {
                var direction = movement.Substring(0, 1);
                var amount = int.Parse(movement.Substring(1));

                switch (direction)
                {
                    case "L":
                        for (var i = 0; i < amount; i++)
                        {
                            distance++;
                            currentPosition = Tuple.Create(currentPosition.Item1 - 1, currentPosition.Item2);
                            map = AddMovement(map, currentPosition, character, distance);
                        }

                        break;
                    case "R":
                        for (var i = 0; i < amount; i++)
                        {
                            distance++;
                            currentPosition = Tuple.Create(currentPosition.Item1 + 1, currentPosition.Item2);
                            map = AddMovement(map, currentPosition, character, distance);
                        }
                        break;
                    case "U":
                        for (var i = 0; i < amount; i++)
                        {
                            distance++;
                            currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2 + 1);
                            map = AddMovement(map, currentPosition, character, distance);
                        }
                        break;
                    case "D":
                        for (var i = 0; i < amount; i++)
                        {
                            distance++;
                            currentPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2 - 1);
                            map = AddMovement(map, currentPosition, character, distance);
                        }
                        break;
                }
            }

            return map;
        }

        private static Dictionary<Tuple<int, int>, Tuple<string, int>> AddMovement(Dictionary<Tuple<int, int>, Tuple<string, int>> map, Tuple<int, int> currentPosition, string character, int distance)
        {
            if (map.ContainsKey(currentPosition))
            {
                if (map[currentPosition].Item1 != character && map[currentPosition].Item1 != "X")
                {
                    var steps = Math.Abs(map[currentPosition].Item2) + Math.Abs(distance);
                    map[currentPosition] = new Tuple<string, int>("X", steps);
                }

            }
            else
            {
                map.Add(currentPosition, Tuple.Create(character, distance));
            }
            return map;
        }

        private static int GetLeastManhattanDistance(Dictionary<Tuple<int, int>, Tuple<string, int>> map)
        {
            var manhattanDistances = new List<int>();

            foreach (var ((xCoordinate, yCoordinate), (character, _)) in map)
            {
                if (character != "X") continue;
                var distance = Math.Abs(xCoordinate) + Math.Abs(yCoordinate);
                manhattanDistances.Add(distance);
            }
            return manhattanDistances.Min();
        }

        private static int GetLeastDistance(Dictionary<Tuple<int, int>, Tuple<string, int>> map)
        {
            var steps = new List<int>();

            foreach (var (_, (character, distance)) in map)
            {
                if (character != "X") continue;
                steps.Add(distance);
            }
            return steps.Min();
        }

        [Fact]
        public void PartOne()
        {
            var inputArray = InputParsingHelpers.ParseFileAsStringArray("../../../input.txt", '\n');

            var wireOne = inputArray.ElementAt(0).Split(',').ToList();
            var wireTwo = inputArray.ElementAt(1).Split(',').ToList();

            var map = GetGrid(wireOne, wireTwo);

            var result = GetLeastManhattanDistance(map);

            result.Should().Be(207);
            _testOutputHelper.WriteLine($"Day Three Part One: {result}");

        }

        [Fact]
        public void PartTwo()
        {
            var inputArray = InputParsingHelpers.ParseFileAsStringArray("../../../input.txt", '\n');

            var wireOne = inputArray.ElementAt(0).Split(',').ToList();
            var wireTwo = inputArray.ElementAt(1).Split(',').ToList();

            var map = GetGrid(wireOne, wireTwo);

            var result = GetLeastDistance(map);

            result.Should().Be(21196);
            _testOutputHelper.WriteLine($"Day Three Part Two: {result}");

        }

        [Theory]
        [InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 6)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void ShouldFindManhattanDistance(string wireOneString, string wireTwoString, int expectedOutput)
        {
            var wireOne = wireOneString.Split(',').ToList();
            var wireTwo = wireTwoString.Split(',').ToList();

            var map = GetGrid(wireOne, wireTwo);

            var result = GetLeastManhattanDistance(map);

            result.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData("R8,U5,L5,D3", "U7,R6,D4,L4", 30)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 610)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)]
        public void ShouldFindDistance(string wireOneString, string wireTwoString, int expectedOutput)
        {
            var wireOne = wireOneString.Split(',').ToList();
            var wireTwo = wireTwoString.Split(',').ToList();

            var map = GetGrid(wireOne, wireTwo);

            var result = GetLeastDistance(map);

            result.Should().Be(expectedOutput);
        }
    }
}
