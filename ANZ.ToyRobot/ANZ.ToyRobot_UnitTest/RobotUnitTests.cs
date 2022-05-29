using ANZ.ToyRobot;
using ANZ.ToyRobot.Model;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using Xunit;
using Program = ANZ.ToyRobot.Program;

namespace ANZ.ToyRobot_UnitTest
{
    public class RobotUnitTests
    {
        [Fact]
        public void Test_PlaceCommandSuccess()
        {
            var program = new Program();
            var toyRobot = new Robot();
            var surface = new Surface(5, 5);

            string errMsg = program.CommandHandler("PLACE 1,1,NORTH", toyRobot, surface);

            Assert.True(string.IsNullOrEmpty(errMsg));
        }

        [Fact]
        public void Test_PlaceCommandFail()
        {
            var program = new Program();
            var toyRobot = new Robot();
            var surface = new Surface(5, 5);

            string errMsg = program.CommandHandler("PLACE 6,6,NORTH", toyRobot, surface);

            Assert.NotNull(errMsg);
        }

        [Fact]
        public void Test_MoveCommandSuccess()
        {
            var program = new Program();
            var toyRobot = new Robot();
            var surface = new Surface(5, 5);

            program.CommandHandler("PLACE 1,1,NORTH", toyRobot, surface);
            program.CommandHandler("MOVE", toyRobot, surface);

            Assert.True(toyRobot.PositionY.Equals(2) && toyRobot.PositionX.Equals(1));
        }

        [Fact]
        public void Test_RotateLeftCommandSuccess()
        {
            var program = new Program();
            var toyRobot = new Robot();
            var surface = new Surface(5, 5);

            program.CommandHandler("PLACE 1,1,NORTH", toyRobot, surface);
            program.CommandHandler("LEFT", toyRobot, surface);

            Assert.True(toyRobot.Direction.Equals(Direction.WEST));
        }

        [Fact]
        public void Test_RotateRightsCommandSuccess()
        {
            var program = new Program();
            var toyRobot = new Robot();
            var surface = new Surface(5, 5);

            program.CommandHandler("PLACE 1,1,NORTH", toyRobot, surface);
            program.CommandHandler("RIGHT", toyRobot, surface);

            Assert.True(toyRobot.Direction.Equals(Direction.EAST));
        }

        [Fact]
        public void Test_ReportCommandSuccess()
        {
            var program = new Program();
            var toyRobot = new Robot();
            var surface = new Surface(5, 5);

            program.CommandHandler("PLACE 1,1,NORTH", toyRobot, surface);
            var error = program.CommandHandler("REPORT", toyRobot, surface);

            Assert.True(string.IsNullOrEmpty(error));
        }
    }
}
