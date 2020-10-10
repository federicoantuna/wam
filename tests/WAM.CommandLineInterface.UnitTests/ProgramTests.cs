using System;
using Xunit;

namespace WAM.CommandLineInterface.UnitTests
{
    public class ProgramTests
    {
        [Fact]
        public void Main_()
        {
            // Arrange
            var args = new String[] { };

            // Act
            Program.Main(args);

            // Assert
        }
    }
}