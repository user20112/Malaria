using Malaria2.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Malaria2.Entities
{
    internal class Player : IEntity
    {
        public float Posx { get; set; }
        public float Posy { get; set; }
        private int ID;
        private double CharSpeed = 20;
        private double FallSpeed = 20;
        private double CurXSpeed = 0;
        private double CurYSpeed = 0;
        private double MaxSpeed = 20;
        private bool FaceingLeft = false;
        public World CurWorld;

        public Player(int id)
        {
            ID = id;
        }

        public void SetLocation(World world, int xpos, int ypos)
        {
            if (CurWorld != null)
                CurWorld.Entities.Remove(this);
            CurWorld = world;
            Posx = xpos;
            Posy = ypos;
            CurWorld.Entities.Add(this);
        }

        public void Draw(int x, int y, SpriteBatch g, EntitySpriteSheet sheet, float SpriteDepth)
        {
            if (CurXSpeed > 0)
                FaceingLeft = false;
            if (CurXSpeed < 0)
                FaceingLeft = true;
            g.Draw(sheet.Sheet, new Rectangle(x, y, SpriteSheet.SpriteSize, SpriteSheet.SpriteSize * 2), sheet.GetSprite(ID), Color.White, 0, new Vector2(0, 0), FaceingLeft ? SpriteEffects.None : SpriteEffects.FlipHorizontally, SpriteDepth);
        }

        public void Update(GameTime gametime, ITile[] SuroundingTiles)
        {
            /*
             0 1 2
             3 4 5
             6 7 8
             9 10 11
             */
            KeyboardState kstate = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            bool CanWalkRight = SuroundingTiles[5].Passable && SuroundingTiles[8].Passable;
            bool CanWalkLeft = SuroundingTiles[4].Passable && SuroundingTiles[7].Passable;
            bool CanJump = SuroundingTiles[1].Passable;
            bool CanFall = SuroundingTiles[10].Passable && SuroundingTiles[11].Passable;
            bool WantRight = kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D) || gamePadState.ThumbSticks.Left.X > 0;
            bool WantLeft = kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A) || gamePadState.ThumbSticks.Left.X < 0;
            bool Jump = ((kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W)) || kstate.IsKeyDown(Keys.Space) || gamePadState.IsButtonDown(Buttons.A) || gamePadState.ThumbSticks.Left.Y > 0) && CurYSpeed == 0;
            if (CanWalkLeft)
            {
                if (CurXSpeed < 0)
                    Posx += (float)(CurXSpeed * gametime.ElapsedGameTime.Milliseconds / 1000);
            }
            else
            {
                if (CurXSpeed < 0)
                {
                    CurXSpeed = 0;
                    Posx = (int)Posx + 1;
                }
            }
            if (CanWalkRight)
            {
                if (CurXSpeed > 0)
                    Posx += (float)(CurXSpeed * gametime.ElapsedGameTime.Milliseconds / 1000);
            }
            else
            {
                if (CurXSpeed > 0)
                {
                    CurXSpeed = 0;
                    Posx = (int)Posx;
                }
            }
            if (CanFall)
            {
                if (CurYSpeed < 0)
                    Posy += (float)(CurYSpeed * gametime.ElapsedGameTime.Milliseconds / 1000);
            }
            else
            {
                if (CurYSpeed < 0)
                    CurYSpeed = 0;
            }
            if (CanJump)
            {
                if (CurYSpeed > 0)
                    Posy += (float)(CurYSpeed * gametime.ElapsedGameTime.Milliseconds / 1000);
            }
            else
            {
                if (CurYSpeed > 0)
                    CurYSpeed = 0;
            }
            if (CanWalkRight)
            {
                if (WantRight)
                {
                    if (CurXSpeed < MaxSpeed)
                        CurXSpeed += CharSpeed * gametime.ElapsedGameTime.Milliseconds / 1000;
                }
                else
                {
                    if (CurXSpeed > 0)
                    {
                        CurXSpeed *= .7;
                        if (CurXSpeed < MaxSpeed * .2 && CurXSpeed > -1 * MaxSpeed * .2)
                            CurXSpeed = 0;
                    }
                }
            }
            if (CanWalkLeft)
            {
                if (WantLeft)
                {
                    if (CurXSpeed > MaxSpeed * -1)
                        CurXSpeed -= CharSpeed * gametime.ElapsedGameTime.Milliseconds / 1000;
                }
                else
                {
                    if (CurXSpeed < 0)
                    {
                        CurXSpeed *= .7;
                        if (CurXSpeed < MaxSpeed * .2 && CurXSpeed > -1 * MaxSpeed * .2)
                            CurXSpeed = 0;
                    }
                }
            }
            if (CanJump)
            {
                if (Jump)
                    if (CurYSpeed < MaxSpeed)
                        CurYSpeed += CharSpeed;
            }
            if (CanFall)
            {
                //if((kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W)))
                //{
                //}
                //if (gamePadState.ThumbSticks.Left.Y < 0)
                //{
                //}
                if (CurYSpeed > MaxSpeed * -1)
                    CurYSpeed -= FallSpeed * gametime.ElapsedGameTime.Milliseconds / 1000;
            }
        }
    }
}