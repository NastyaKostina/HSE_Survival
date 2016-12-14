using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    class Player:Sprite
    {
        public Vector2 move { get; set; }
        private Vector2 oldPosition;
        //public List<SoundEffect> sound_effects;
        public List<int> Score = new List<int>();
        Vector2 changeposition;

        public Player(Texture2D texture, Vector2 position, SpriteBatch spritebatch) //List<SoundEffect> sounds)
        : base(texture, position, spritebatch)
        {
            //sound_effects = sounds;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardAction();
            GravityEffect();
            MovingBlockChecking(); // ПРОВЕРКА ДВИГАЮЩИХСЯ БЛОКОВ
            move -= move * new Vector2(.1f, .1f);
            PossibleMoving(gameTime);
            StopMovingIfBlocked();
        }
        private void KeyboardAction() //movements
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left)) { move += -Vector2.UnitX * 0.5f; }
            if (keyboardState.IsKeyDown(Keys.Right)) { move += Vector2.UnitX * 0.5f; }
            if (keyboardState.IsKeyDown(Keys.Space) && IsOnFirmGround()) { move = -Vector2.UnitY * 32;/* sound_effects[0].Play();  подняли его вверх */ }
        }
        private void GravityEffect()
        {
            move += Vector2.UnitY * .65f;
        }
        private void UpdatePosition(GameTime gameTime)
        {
            Sprite_vector += move * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;
        }
        private void PossibleMoving(GameTime gameTime)
        {
            oldPosition = Sprite_vector;
            UpdatePosition(gameTime);
            Sprite_vector = Board.CurrentBoard.WhereCanIGetTo(oldPosition, Sprite_vector, rectangle);
        }
        public bool IsOnFirmGround() //проверяем что стоим на земле
        {
            Rectangle onePixelLower = rectangle;
            onePixelLower.Offset(0, 1);
            return !Board.CurrentBoard.HasSpaceForRectangle(onePixelLower);
        }
        private bool IsMovingBlockOnTheRightSide()
        {
            Rectangle onePixelRighter = rectangle;
            onePixelRighter.Offset(1, 0);
            return !Board.CurrentBoard.HasSpaceToJumpFromTheLeftSide(this, onePixelRighter);
        } // НОВЫЙ МЕТОД
        private bool IsMovingBlockOnTheLeftSide()
        {
            Rectangle onePixelLefter = rectangle;
            onePixelLefter.Offset(-1, 0);
            return !Board.CurrentBoard.HasSpaceToJumpFromTheRightSide(this, onePixelLefter);
        } // НОВЫЙ МЕТОД
        private void MovingBlockChecking()
        {
            if (IsMovingBlockOnTheRightSide()) { Sprite_vector = new Vector2(Sprite_vector.X - 2, Sprite_vector.Y); }
            if (IsMovingBlockOnTheLeftSide()) { Sprite_vector = new Vector2(Sprite_vector.X + 4, Sprite_vector.Y); }
            Rectangle onePixelDown = rectangle;
            Rectangle onePixelRight = rectangle;
            onePixelDown.Offset(0, 1);
            onePixelRight.Offset(1, 0);
            bool[] whatToDo = Board.CurrentBoard.StandingOnTheMovingPlatform(this, ref changeposition, onePixelDown, onePixelRight);
            if (whatToDo[0] && !whatToDo[1])
            {
                if (changeposition.X < 0)
                    Sprite_vector = new Vector2(Sprite_vector.X - 2, Sprite_vector.Y);
                else
                    Sprite_vector = new Vector2(Sprite_vector.X + 2, Sprite_vector.Y);
            }
            if (whatToDo[0] && whatToDo[1])
            {
                Sprite_vector = new Vector2(Sprite_vector.X - 1, Sprite_vector.Y);
            }
        } // НОВЫЙ МЕТОД
        private void StopMovingIfBlocked()
        {
            Vector2 lastMovement = Sprite_vector - oldPosition;
            if (lastMovement.X == 0) { move *= Vector2.UnitY; }
            if (lastMovement.Y == 0) { move *= Vector2.UnitX; }
        }
    }
}
