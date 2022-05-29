using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ANZ.ToyRobot.Model
{
    public class Robot
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool isPlaced { get; set; }
        public Direction Direction { get; set; }

        public Robot()
        {
            
        }
            
        public void Place(int x, int y, string direction)
        {
            this.PositionX = x;
            this.PositionY = y;
            this.isPlaced = true;
            this.Direction = (Direction)Enum.Parse(typeof(Direction), direction, true);
        }

        public void Move()
        {
            switch (Direction)
            {
                case Direction.EAST:
                    PositionX++;
                    break;
                case Direction.WEST:
                    PositionX--;
                    break;
                case Direction.NORTH:
                    PositionY++;
                    break;
                case Direction.SOUTH:
                    PositionY--;
                    break;
            }
        }

        public void RotateLeft()
        {
            switch (Direction)
            {
                case Direction.EAST:
                    Direction = Direction.NORTH;
                    break;
                case Direction.WEST:
                    Direction = Direction.SOUTH;
                    break;
                case Direction.NORTH:
                    Direction = Direction.WEST;
                    break;
                case Direction.SOUTH:
                    Direction = Direction.EAST;
                    break;
            }
        }

        public void RotateRight()
        {
            switch (Direction)
            {
                case Direction.EAST:
                    Direction = Direction.SOUTH;
                    break;
                case Direction.WEST:
                    Direction = Direction.NORTH;
                    break;
                case Direction.NORTH:
                    Direction = Direction.EAST;
                    break;
                case Direction.SOUTH:
                    Direction = Direction.WEST;
                    break;
            }
        }

        public string Report()
        {
            return PositionX + " - " + PositionY + " - " + Direction.ToString();
        }


    }

    public class ToyRobotValidator : AbstractValidator<Robot>
    {
        public ToyRobotValidator()
        {
            RuleFor(x  => x.PositionX).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5);
            RuleFor(x => x.PositionY).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5);
            RuleFor(x => x.Direction).IsInEnum();
        }

    }

    public enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }
}
