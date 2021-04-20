﻿using GoRogue.GameFramework;
using SadRogue.Integration;
using SadRogue.Integration.Components;
using SadRogue.Primitives;

namespace ExampleGame
{
    public class Player : RogueLikeEntity
    {
        /// <summary>
        /// The sight radius of the player.
        /// </summary>
        public int FOVRadius { get; private set; }

        public Player(Point position, int fovRadius = 10)
            : base(position, 1, false)
        {
            // Set FOV radius we will use for calculating FOV
            FOVRadius = fovRadius;

            // Set hook so that FOV is recalculated when the player moves
            Moved += OnMoved;

            // Add component for controlling player movement via keyboard
            var motionControl = new PlayerControlsComponent();
            AllComponents.Add(motionControl);

            // Ensure player receives input
            IsFocused = true;
        }

        /// <summary>
        /// Calculate FOV if a player is part of a map.
        /// </summary>
        public void CalculateFOV()
        {
            CurrentMap?.PlayerFOV.Calculate(Position, FOVRadius, Distance.Chebyshev);
        }

        // If the player is added to a map, update the player FOV when the player moves
        private void OnMoved(object? sender, GameObjectPropertyChanged<Point> e)
        {
            CalculateFOV();
        }
    }
}
