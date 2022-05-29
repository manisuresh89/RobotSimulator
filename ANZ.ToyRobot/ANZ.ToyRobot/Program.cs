using ANZ.ToyRobot.Model;
using ANZ.ToyRobot.Utils;
using System;
using static ANZ.ToyRobot.Utils.Common;

namespace ANZ.ToyRobot
{
   public class Program
    {
        static void Main(string[] args)
        {
            const int maxCommands = 20;
            Robot toyRobot = new Robot();
            Surface surface = new Surface(5, 5);
            Program program = new Program();


            Console.WriteLine(@"Enter a Simulation command:

            PLACE X, Y, DIRECTION
            MOVE
            RIGHT
            LEFT
            REPORT
            -------------
            ");

            for (int cmdIterator = 1; cmdIterator <= maxCommands; cmdIterator++)
            {
                Console.WriteLine("Enter Command " + cmdIterator);
                var command = Console.ReadLine();
                string errorMsg = program.CommandHandler(command, toyRobot, surface);

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    Console.WriteLine(errorMsg);
                }
            }

        }

        public string CommandHandler(string command, Robot toyRobot, Surface surface)
        {
            ErrorMessagesHelper msgHelper = new ErrorMessagesHelper();
            bool isPlaced = toyRobot.isPlaced;
            string errMsg = string.Empty;

            switch (command)
            {
                case var cmd when cmd.StartsWith("PLACE"):

                    string[] commandparts = command.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (commandparts.Length == 4)
                    {
                        int positionX;
                        int positionY;

                        bool validPositionX = Int32.TryParse(commandparts[1], out positionX);
                        bool validPositionY = Int32.TryParse(commandparts[2], out positionY);
                        bool validDirection = Enum.IsDefined(typeof(Direction), commandparts[3]);


                        if (!validDirection)
                        {
                            errMsg = msgHelper.GetErrorMessage(ErrorType.InvalidDirection); ;
                            return errMsg;
                        }

                        toyRobot.Place(positionX, positionY, commandparts[3]);

                        var validator = new ToyRobotValidator();
                        var validRes = validator.Validate(toyRobot);

                        if (!validRes.IsValid)
                        {
                            toyRobot = null;
                            errMsg = msgHelper.GetErrorMessage(ErrorType.InvalidLocation);
                            return errMsg;
                        }
                    }
                    else
                    {
                        errMsg = msgHelper.GetErrorMessage(ErrorType.InvalidCommand);
                    }

                    break;

                case var cmd when cmd.StartsWith("MOVE") && isPlaced:

                    if (surface.isValidPosition(toyRobot.PositionX + 1, toyRobot.PositionY + 1)
                        && ( surface.isValidPosition(toyRobot.PositionX - 1, toyRobot.PositionY - 1) 
                        ))
                    {
                        toyRobot.Move();
                    }
                    else
                    {
                        errMsg = msgHelper.GetErrorMessage(ErrorType.InvalidLocation);
                    }

                    break;

                case var cmd when cmd.StartsWith("RIGHT") && isPlaced:

                    toyRobot.RotateRight();

                    break;

                case var cmd when cmd.StartsWith("LEFT") && isPlaced:

                    toyRobot.RotateLeft();

                    break;

                case var cmd when cmd.StartsWith("REPORT") && isPlaced:

                    var report = toyRobot.Report();
                    Console.WriteLine(report);

                    break;

                default:

                    errMsg = msgHelper.GetErrorMessage(ErrorType.InvalidCommand);

                    break;
            }

            return errMsg;
        }
    }
}
