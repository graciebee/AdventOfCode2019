using System;
using System.Linq;
using FluentAssertions;
using Helpers;
using Xunit;
using Xunit.Abstractions;

namespace DayFive
{
    public class DayFive
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public DayFive(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public Instruction InterpretInstruction(int instruction)
        {
            var instructionString = instruction.ToString();
            var opCode = instructionString.Length > 1 
                ? int.Parse(instructionString.Substring(instructionString.Length - 2)) 
                : int.Parse(instructionString.Substring(instructionString.Length - 1));

            var parsedInstruction = new Instruction
            {
                FirstParamMode = 0,
                SecondParamMode = 0,
                OpCode = opCode
            };
            if (instructionString.Length - 2 > 0)
            {
                parsedInstruction.FirstParamMode = int.Parse(instructionString[instructionString.Length - 3].ToString());
            }
            if (instructionString.Length - 3 > 0)
            {
                parsedInstruction.SecondParamMode = int.Parse(instructionString[instructionString.Length - 4].ToString());
            }

            return parsedInstruction;
        }

        public string ReadInstructions(int[] program, int input)
        {
            var instructionPointer = 0;
            var output = "";
            while (program[instructionPointer] != 99)
            {
                var instruction = InterpretInstruction(program[instructionPointer]);
                switch (instruction.OpCode)
                {
                    case 1:
                        var paramA = instruction.FirstParamMode == 0 ? program[program[instructionPointer + 1]] : program[instructionPointer + 1];
                        var paramB = instruction.SecondParamMode == 0 ? program[program[instructionPointer + 2]] : program[instructionPointer + 2];
                        program[program[instructionPointer + 3]] = paramA + paramB;
                        instructionPointer += 4;
                        break;
                    case 2:
                        paramA = instruction.FirstParamMode == 0 ? program[program[instructionPointer + 1]] : program[instructionPointer + 1];
                        paramB = instruction.SecondParamMode == 0 ? program[program[instructionPointer + 2]] : program[instructionPointer + 2];
                        program[program[instructionPointer + 3]] = paramA * paramB;
                        instructionPointer += 4;
                        break;
                    case 3:
                        program[program[instructionPointer + 1]] = input;
                        instructionPointer += 2;
                        break;
                    case 4:
                        output += instruction.FirstParamMode == 0 ? program[program[instructionPointer + 1]] : program[instructionPointer + 1];
                        instructionPointer += 2;
                        break;
                    case 5:
                        paramA = instruction.FirstParamMode == 0 ? program[program[instructionPointer + 1]] : program[instructionPointer + 1];
                        paramB = instruction.SecondParamMode == 0 ? program[program[instructionPointer + 2]] : program[instructionPointer + 2];
                        if (paramA != 0)
                        {
                            instructionPointer = paramB;
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 6:
                        paramA = instruction.FirstParamMode == 0 ? program[program[instructionPointer + 1]] : program[instructionPointer + 1];
                        paramB = instruction.SecondParamMode == 0 ? program[program[instructionPointer + 2]] : program[instructionPointer + 2];
                        if (paramA == 0)
                        {
                            instructionPointer = paramB;
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 7:
                        paramA = instruction.FirstParamMode == 0 ? program[program[instructionPointer + 1]] : program[instructionPointer + 1];
                        paramB = instruction.SecondParamMode == 0 ? program[program[instructionPointer + 2]] : program[instructionPointer + 2];
                        program[program[instructionPointer + 3]] = paramA < paramB ? 1 : 0;
                        instructionPointer += 4;
                        break;
                    case 8:
                        paramA = instruction.FirstParamMode == 0 ? program[program[instructionPointer + 1]] : program[instructionPointer + 1];
                        paramB = instruction.SecondParamMode == 0 ? program[program[instructionPointer + 2]] : program[instructionPointer + 2];
                        program[program[instructionPointer + 3]] = paramA == paramB ? 1 : 0;
                        instructionPointer += 4;
                        break;
                    case 99:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return output;
        }

        [Fact]
        public void PartOne()
        {
            var instructions = InputParsingHelpers.ParseFileAsIntArray("../../../input.txt", ',');
            var output = ReadInstructions(instructions.ToArray(), 1);

            output.Should().Be("0000000006731945");
            _testOutputHelper.WriteLine(output);
        }

        [Fact]
        public void PartTwo()
        {
            var instructions = InputParsingHelpers.ParseFileAsIntArray("../../../input.txt", ',');
            var output = ReadInstructions(instructions.ToArray(), 5);

            output.Should().Be("9571668");
            _testOutputHelper.WriteLine(output);
        }

        [Theory]
        [InlineData(7,999)]
        [InlineData(5,999)]
        [InlineData(8,1000)]
        [InlineData(9,1001)]
        [InlineData(20,1001)]
        public void ShouldOutputValue(int value, int expectedOutput)
        {
            var instructions = new[]
            {
                3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31,
                1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104,
                999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
            };

            var output = ReadInstructions(instructions, value);

            int.Parse(output).Should().Be(expectedOutput);
        }
    }
}
