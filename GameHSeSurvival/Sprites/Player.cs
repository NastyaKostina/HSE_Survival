using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameHSeSurvival
{
    
    class Player:Sprite
    {
        PlayerMovementSystem Movements = new PlayerMovementSystem();
        public Vector2 move;
        public int Score { get; set; }  

        //Vector2 changeposition;

        public Player(Texture2D texture, Vector2 position, SpriteBatch spritebatch) //List<SoundEffect> sounds)
        : base(texture, position, spritebatch)
        {
            //sound_effects = sounds;
        }
        public void Update(GameTime gameTime)
        {
            move = Movements.move;
            Movements.Move(gameTime, this);
            //MovingBlockChecking(); // ПРОВЕРКА ДВИГАЮЩИХСЯ БЛОКОВ
            //IsPlayerUnderGround();
        }
        
        //private bool IsMovingBlockOnTheRightSide()
        //{
        //    Rectangle onePixelRighter = rectangle;
        //    onePixelRighter.Offset(1, 0);
        //    return !Board.CurrentBoard.HasSpaceToJumpFromTheLeftSide(this, onePixelRighter);
        //} // НОВЫЙ МЕТОД
        //private bool IsMovingBlockOnTheLeftSide()
        //{
        //    Rectangle onePixelLefter = rectangle;
        //    onePixelLefter.Offset(-1, 0);
        //    return !Board.CurrentBoard.HasSpaceToJumpFromTheRightSide(this, onePixelLefter);
        //} // НОВЫЙ МЕТОД
        //private void MovingBlockChecking()
        //{
        //    if (IsMovingBlockOnTheRightSide()) { Sprite_vector = new Vector2(Sprite_vector.X - 2, Sprite_vector.Y); }
        //    if (IsMovingBlockOnTheLeftSide()) { Sprite_vector = new Vector2(Sprite_vector.X + 4, Sprite_vector.Y); }
        //    Rectangle onePixelDown = rectangle;
        //    Rectangle onePixelRight = rectangle;
        //    onePixelDown.Offset(0, 1);
        //    onePixelRight.Offset(1, 0);
        //    bool[] whatToDo = Board.CurrentBoard.StandingOnTheMovingPlatform(this, ref changeposition, onePixelDown, onePixelRight);
        //    if (whatToDo[0] && !whatToDo[1])
        //    {
        //        if (changeposition.X < 0)
        //            Sprite_vector = new Vector2(Sprite_vector.X - 2, Sprite_vector.Y);
        //        else
        //            Sprite_vector = new Vector2(Sprite_vector.X + 2, Sprite_vector.Y);
        //    }
        //    if (whatToDo[0] && whatToDo[1])
        //    {
        //        Sprite_vector = new Vector2(Sprite_vector.X - 1, Sprite_vector.Y);
        //    }
        //} // НОВЫЙ МЕТОД
        
        //private void IsPlayerUnderGround()
        //{
        //    if (Sprite_vector.Y >= Board.CurrentBoard.rows * Board.CurrentBoard.block_texture.Height)
        //    {
        //        Sprite_vector = new Vector2(550, 576 - Sprite_texture.Height);
        //    }
        //}
    }
}
