﻿using Microsoft.Xna.Framework; // Настя, привет.
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    class Board
    {
        public Block[,] blocks { get; set; } //massiv of blocks
        public Coin[,] coins { get; set; } //massiv of coins
        public int columns { get; set; } 
        public int rows { get; set; }
        public Texture2D block_texture { get; set; }
    private SpriteBatch sb { get; set; }
        //cause the game has only one level therefore there is just one map
        public static Board CurrentBoard { get; private set; } 
        //ctor converts screen to massiv of blocks
        public Board(SpriteBatch sb, Texture2D block_texture, Texture2D coin, int columns, int rows)
        {
            this.sb = sb;
            this.block_texture = block_texture;
            this.columns = columns;
            this.rows = rows;
            Vector2 block_speed;
            block_speed = Vector2.Zero;
            blocks = new Block[columns, rows];
            coins = new Coin[columns, rows];
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Vector2 positionblock = new Vector2(i * block_texture.Width, j * block_texture.Height);
                    blocks[i, j] = new Block(block_texture, positionblock, sb, false, false, 0);
                    Vector2 positioncoin = new Vector2(i * coin.Width, j * coin.Height);
                    coins[i, j] = new Coin(coin, positioncoin, sb);

                }
            }
            FunctionsOfBlocks(); //fill up blocks
            Board.CurrentBoard = this;
        }
        private void FunctionsOfBlocks()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "GameHSeSurvival.Content.Map.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string row;
                int y = -1;
                while ((row = reader.ReadLine()) != null)
                {
                    string[] sprites = row.Split(';');
                    y++;
                        for (int x = 0; x < sprites.Length; x++)
                        {
                            switch (sprites[x])
                            {
                                case "1":
                                    blocks[x, y].blocked = true; break;

                                case "2":
                                    blocks[x, y].blocked = true;
                                    blocks[x, y].moving = true;
                                    blocks[x, y].speed = Vector2.UnitX * 2f;
                                    blocks[x, y].steps = 2;
                                    blocks[x, y].changeposition = blocks[x, y].steps * blocks[x, y].Sprite_texture.Width;
                                    break;

                                case "3":
                                coins[x, y].exist = true; break;
                            }
                        }
                }
            }
        }

        //draw blocks
        public void Draw()
        {
            foreach (var block in blocks)
            {
                block.Draw();
            }
            foreach (var coin in coins)
            {
                coin.Draw();
            }
        }
        public bool HasSpaceForRectangle(Rectangle rectangleToCheck)
        {
            foreach (var block in blocks)
            {
                if (block.blocked && block.rectangle.Intersects(rectangleToCheck))
                {
                    return false;
                }
            }
            return true;
        }
        public bool HasSpaceToJumpFromTheLeftSide(Player player, Rectangle rectangleToCheck)
        {
            foreach (var block in blocks)
            {
                if (block.blocked && block.moving && block.rectangle.Intersects(rectangleToCheck)
                    && block.Sprite_vector.X <= player.Sprite_vector.X + player.Sprite_texture.Width)
                {
                    return false;
                }
            }
            return true;
        }
        public bool HasSpaceToJumpFromTheRightSide(Player player, Rectangle rectangleToCheck)
        {
            foreach (var block in blocks)
            {
                if (block.blocked && block.moving && block.rectangle.Intersects(rectangleToCheck)
                    && block.Sprite_vector.X + block.Sprite_texture.Width >= player.Sprite_vector.X)
                {
                    return false;
                }
            }
            return true;
        }
        public bool[] StandingOnTheMovingPlatform(Player player, ref Vector2 speed, Rectangle rectangleToCheckDown, Rectangle rectangleToCheckRight)
        {
            bool[] bool_massive = new bool[2];
            bool_massive[0] = false; bool_massive[1] = false;
            foreach (var block in blocks)
            {
                if (block.blocked && block.moving
                  && rectangleToCheckDown.Intersects(block.rectangle))
                {
                    speed = block.speed;
                    bool_massive[0] = true;
                }
                if (block.blocked && rectangleToCheckRight.Intersects(block.rectangle)) { bool_massive[1] = true; }
            }

            return bool_massive;
        }
        public Vector2 WhereCanIGetTo(Vector2 originalPosition, Vector2 destination, Rectangle boundingRectangle)
        {
            Vector2 movementToTry = destination - originalPosition;
            Vector2 furthestAvailableLocationSoFar = originalPosition;
            int numberOfStepsToBreakMovementInto = (int)(movementToTry.Length() * 2) + 1;
            Vector2 oneStep = movementToTry / numberOfStepsToBreakMovementInto;

            for (int i = 1; i <= numberOfStepsToBreakMovementInto; i++)
            {
                Vector2 positionToTry = originalPosition + oneStep * i;
                Rectangle newBoundary =
                    CreateRectangle(positionToTry, boundingRectangle.Width, boundingRectangle.Height);
                if (HasSpaceForRectangle(newBoundary)) { furthestAvailableLocationSoFar = positionToTry; }
                else
                {
                    bool isDiagonalMove = movementToTry.X != 0 && movementToTry.Y != 0;
                    if (isDiagonalMove)
                    {
                        int stepsLeft = numberOfStepsToBreakMovementInto - (i - 1);

                        Vector2 remainingHorizontalMovement = oneStep.X * Vector2.UnitX * stepsLeft;
                        Vector2 finalPositionIfMovingHorizontally = furthestAvailableLocationSoFar + remainingHorizontalMovement;
                        furthestAvailableLocationSoFar =
                            WhereCanIGetTo(furthestAvailableLocationSoFar, finalPositionIfMovingHorizontally, boundingRectangle);

                        Vector2 remainingVerticalMovement = oneStep.Y * Vector2.UnitY * stepsLeft;
                        Vector2 finalPositionIfMovingVertically = furthestAvailableLocationSoFar + remainingVerticalMovement;
                        furthestAvailableLocationSoFar =
                            WhereCanIGetTo(furthestAvailableLocationSoFar, finalPositionIfMovingVertically, boundingRectangle);
                    }
                    break;
                }
            }
            return furthestAvailableLocationSoFar;
        }
        private Rectangle CreateRectangle(Vector2 positionToMove, int width, int height)
        {
            return new Rectangle((int)positionToMove.X, (int)positionToMove.Y, width, height);
        }
        public void Update() 
        {
            Block[,] blocks = Board.CurrentBoard.blocks;
            foreach (var block in blocks)
            {
                if (block.moving == true)
                {
                    if (block.changeposition < 0)
                    {
                        block.Sprite_vector += -block.speed;
                    }
                    else { block.Sprite_vector += block.speed; }
                    if (Math.Abs(block.startposition - block.Sprite_vector.X) >= Math.Abs(block.changeposition))
                    {
                        block.speed *= -1;
                    }
                }
            }
        }

    }
}
