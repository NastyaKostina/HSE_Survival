using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    class PlayerMovementSystem
    {
        
        public  Vector2 move { get; set; }
        private Vector2 oldPosition;
        Vector2 changeposition;

        public void Move(GameTime gameTime, Player player)
        {
            KeyboardAction(player);
            GravityEffect();
            move -= move * new Vector2(.1f, .1f);
            PossibleMoving(gameTime, player);
            StopMovingIfBlocked(player);
            MovingBlockChecking(player); // ПРОВЕРКА ДВИГАЮЩИХСЯ БЛОКОВ
            IsPlayerUnderGround(player);
        }
        private void KeyboardAction(Player player) //movements
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left)) { move += -Vector2.UnitX * 0.5f; }
            if (keyboardState.IsKeyDown(Keys.Right)) { move += Vector2.UnitX * 0.5f; }
            if (keyboardState.IsKeyDown(Keys.Space) && IsOnFirmGround(player)) { move = -Vector2.UnitY * 32;/*подняли его вверх */ }
        }
        private bool IsOnFirmGround(Player player) //проверяем что стоим на земле
        {
            Rectangle onePixelLower = player.rectangle;
            onePixelLower.Offset(0, 1);
            return !Board.CurrentBoard.HasSpaceForRectangle(onePixelLower);
        }
        private void GravityEffect()
        {
            move += Vector2.UnitY * .65f;
        }
        private void PossibleMoving(GameTime gameTime, Player player)
        {
            oldPosition = player.Sprite_vector;
            UpdatePosition(gameTime, player);
            player.Sprite_vector = Board.CurrentBoard.WhereCanIGetTo(oldPosition, player.Sprite_vector, player.rectangle);
        }
        private void UpdatePosition(GameTime gameTime, Player player)
        {
            player.Sprite_vector += move * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;
        }
        private void StopMovingIfBlocked(Player player)
        {
            Vector2 lastMovement = player.Sprite_vector - oldPosition;
            if (lastMovement.X == 0) { move *= Vector2.UnitY; }
            if (lastMovement.Y == 0) { move *= Vector2.UnitX; }
        }

        private void MovingBlockChecking(Player player)
        {
            if (IsMovingBlockOnTheRightSide(player)) { player.Sprite_vector = new Vector2(player.Sprite_vector.X - 2, player.Sprite_vector.Y); }
            if (IsMovingBlockOnTheLeftSide(player)) { player.Sprite_vector = new Vector2(player.Sprite_vector.X + 4, player.Sprite_vector.Y); }
            Rectangle onePixelDown = player.rectangle;
            Rectangle onePixelRight = player.rectangle;
            onePixelDown.Offset(0, 1);
            onePixelRight.Offset(1, 0);
            bool[] whatToDo = Board.CurrentBoard.StandingOnTheMovingPlatform(player, ref changeposition, onePixelDown, onePixelRight);
            if (whatToDo[0] && !whatToDo[1])
            {
                if (changeposition.X < 0)
                    player.Sprite_vector = new Vector2(player.Sprite_vector.X - 2, player.Sprite_vector.Y);
                else
                    player.Sprite_vector = new Vector2(player.Sprite_vector.X + 2, player.Sprite_vector.Y);
            }
            if (whatToDo[0] && whatToDo[1])
            {
                player.Sprite_vector = new Vector2(player.Sprite_vector.X - 1, player.Sprite_vector.Y);
            }
        }
        private bool IsMovingBlockOnTheRightSide(Player player)
        {
            Rectangle onePixelRighter = player.rectangle;
            onePixelRighter.Offset(1, 0);
            return !Board.CurrentBoard.HasSpaceToJumpFromTheLeftSide(player, onePixelRighter);
        } // НОВЫЙ МЕТОД
        private bool IsMovingBlockOnTheLeftSide(Player player)
        {
            Rectangle onePixelLefter = player.rectangle;
            onePixelLefter.Offset(-1, 0);
            return !Board.CurrentBoard.HasSpaceToJumpFromTheRightSide(player, onePixelLefter);
        }
        private void IsPlayerUnderGround(Player player)
        {
            if (player.Sprite_vector.Y >= Board.CurrentBoard.rows * Board.CurrentBoard.block_texture.Height)
            {
                player.Sprite_vector = new Vector2(550, 576 - player.Sprite_texture.Height);
            }
        }
    }
}
