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
        
        public Vector2 move { get; set; }
        private Vector2 oldPosition;

        public void Move(GameTime gameTime, Vector2 Sprite_vector, Rectangle rectangle)
        {
            
            KeyboardAction(rectangle);
            GravityEffect();
            move -= move * new Vector2(.1f, .1f);
            PossibleMoving(gameTime, Sprite_vector, rectangle);
            StopMovingIfBlocked(Sprite_vector);
        }
        private void KeyboardAction(Rectangle rectangle) //movements
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left)) { move += -Vector2.UnitX * 0.5f; }
            if (keyboardState.IsKeyDown(Keys.Right)) { move += Vector2.UnitX * 0.5f; }
            if (keyboardState.IsKeyDown(Keys.Space) && IsOnFirmGround(rectangle)) { move = -Vector2.UnitY * 32;/*подняли его вверх */ }
        }
        private bool IsOnFirmGround(Rectangle rectangle) //проверяем что стоим на земле
        {
            Rectangle onePixelLower = rectangle;
            onePixelLower.Offset(0, 1);
            return !Board.CurrentBoard.HasSpaceForRectangle(onePixelLower);
        }
        private void GravityEffect()
        {
            move += Vector2.UnitY * .65f;
        }
        private void PossibleMoving(GameTime gameTime, Vector2 Sprite_vector, Rectangle rectangle)
        {
            oldPosition = Sprite_vector;
            UpdatePosition(gameTime, Sprite_vector);
            Sprite_vector = Board.CurrentBoard.WhereCanIGetTo(oldPosition, Sprite_vector, rectangle);
        }
        private void UpdatePosition(GameTime gameTime, Vector2 Sprite_vector)
        {
            Sprite_vector += move * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;
        }
        private void StopMovingIfBlocked(Vector2 Sprite_vector)
        {
            Vector2 lastMovement = Sprite_vector - oldPosition;
            if (lastMovement.X == 0) { move *= Vector2.UnitY; }
            if (lastMovement.Y == 0) { move *= Vector2.UnitX; }
        }
    }
}
