﻿using System;
using thebotchallenge;

namespace Example1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init a new game instance with the using block in order to automatically
            // call Dispose for our object
            using (Game game = new Game())
            {
                // Login and change to aes encrypted connection
                game.Authorize("zTxyW4q6T_vr");

                // Run our GameProcedure method till the game has finished
                try
                {
                    while (true)
                        GameProcedure(game.Player);
                }
                catch (GameEndedException)
                {
                    Console.WriteLine("Finished game! You can review your results in the web app!");
                }
            }
        }

        /// <summary>
        /// A method that we call as log as the game is running
        /// </summary>
        /// <param name="player">Our player instance</param>
        static void GameProcedure(Player player)
        {
            // Get the amount of players in our chunk
            int radar = player.Radar();

            // Get a 3x3 matrix of block types 
            BlockType[,] aroundMe = player.GetSurrounding();
            // If there is a player at our right we attack him { x = 2, y = 1 }
            if (aroundMe[2, 1] == BlockType.Opponent)
                player.Attack(Direction.Right);

            // Count the players in the map
            int horizontal = player.Scout(Alignment.Horizontal);
            int vertical = player.Scout(Alignment.Vertical);

            // Move on block upwards
            player.Move(Direction.Up);

            // Change the defend mode of our player
            player.Defend();
            player.Undefend();
        }
    }
}