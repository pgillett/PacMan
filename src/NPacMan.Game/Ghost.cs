﻿using System.Linq;
using NPacMan.Game.GhostStrategies;

namespace NPacMan.Game
{

    public class Ghost
    {
        public string Name { get; }

        public CellLocation Location { get; }
        public CellLocation ScatterTarget { get; }

        public Direction Direction { get; }

        public CellLocation Home { get; }
        private IGhostStrategy ChaseStrategy { get; }
        private IGhostStrategy CurrentStrategy { get; }

        public bool Edible { get; }

        public Ghost(string name, CellLocation location, Direction direction, CellLocation scatterTarget, IGhostStrategy chaseStrategy)
        : this(name, location, location, direction, scatterTarget, chaseStrategy, chaseStrategy, false)
        {
        }

        private Ghost(string name, CellLocation homeLocation, CellLocation currentLocation, Direction direction, CellLocation scatterTarget, IGhostStrategy chaseStrategy, IGhostStrategy currentStrategy, bool edible)
        {
            Name = name;
            Home = homeLocation;
            Location = currentLocation;
            Direction = direction;
            ChaseStrategy = chaseStrategy;
            CurrentStrategy = currentStrategy;
            ScatterTarget = scatterTarget;
            Edible = edible;
        }

        internal Ghost Move(Game game, IReadOnlyGameState gameState)
        {
            if (Edible && gameState.TickCounter % 2 == 1) return this;

            if (game.GhostHouse.Contains(Location)) return this;
          
            var nextDirection = CurrentStrategy.GetNextDirection(this, game);
            if (nextDirection is Direction newDirection)
            {
                var newGhostLocation = Location + newDirection;
                if (game.Portals.TryGetValue(newGhostLocation, out var otherEndOfThePortal))
                {
                    newGhostLocation = otherEndOfThePortal + newDirection;
                }
                return WithNewLocationAndDirection(newGhostLocation, newDirection);
            }
            else
                return this;
        }

        internal Ghost Chase()
        {
            return WithNewCurrentStrategyAndDirection(ChaseStrategy, Direction.Opposite());
        }

        internal Ghost Scatter()
        {
            var strategy = new DirectToStrategy(new DirectToGhostScatterTarget(this));
            return WithNewCurrentStrategyAndDirection(strategy, Direction.Opposite());
        }

        internal Ghost SetToHome() => WithNewLocation(Home);

        internal Ghost SetToEdible(IDirectionPicker directionPicker)
        {
            var strategy = new RandomStrategy(directionPicker);
            return WithNewEdibleAndDirectionAndStrategy(true, Direction.Opposite(), strategy);
        }

        internal Ghost SetToNotEdible() => WithNewEdibleAndDirectionAndStrategy(false, Direction, ChaseStrategy);

        private Ghost WithNewEdibleAndDirectionAndStrategy(bool isEdible, Direction newDirection, IGhostStrategy newCurrentStrategy)
            => new Ghost(Name, Home, Location, newDirection, ScatterTarget, ChaseStrategy, newCurrentStrategy, isEdible);

        private Ghost WithNewCurrentStrategyAndDirection(IGhostStrategy newCurrentStrategy, Direction newDirection)
            => new Ghost(Name, Home, Location, newDirection, ScatterTarget, ChaseStrategy, newCurrentStrategy, Edible);

        private Ghost WithNewLocation(CellLocation newLocation)
            => new Ghost(Name, Home, newLocation, Direction, ScatterTarget, ChaseStrategy, CurrentStrategy, Edible);

        private Ghost WithNewLocationAndDirection(CellLocation newLocation, Direction newDirection)
            => new Ghost(Name, Home, newLocation, newDirection, ScatterTarget, ChaseStrategy, CurrentStrategy, Edible);

    }
}
