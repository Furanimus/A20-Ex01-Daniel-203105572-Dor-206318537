using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class BasePlayer : Entity
     {
          public BasePlayer(string i_GraphicsPath, Game i_Game) : base(i_GraphicsPath, i_Game)
          {
          }

          public BasePlayer(string i_GraphicsPath, Game i_Game, int i_CallsOrder) : base(i_GraphicsPath, i_Game, i_CallsOrder)
          {
          }

          public BasePlayer(string i_GraphicsPath, Game i_Game, int i_UpdateOrder, int i_DrawOrder) : base(i_GraphicsPath, i_Game, i_UpdateOrder, i_DrawOrder)
          {
          }

          public KeyboardState CurrKBState { get; set; }

          public KeyboardState PrevKBState { get; set; }

          public MouseState MovingMouseState { get; set; }

          protected MouseState PrevMovingMouseState { get; set; }

          public MouseState ShootingMouseState { get; set; }

          protected MouseState PrevShootingMouseState { get; set; }

          protected Vector2 getMousePositionDelta()
          {
               Vector2 retVal = Vector2.Zero;

               if (PrevMovingMouseState != null)
               {
                    retVal.X = (MovingMouseState.X - PrevMovingMouseState.X);
               }

               PrevMovingMouseState = MovingMouseState;

               return retVal;
          }

          protected Vector2 getMouseLocation()
          {
               Vector2 retVal = Vector2.Zero;

               if (PrevMovingMouseState != MovingMouseState)
               {
                    retVal.X = MovingMouseState.X;
                    retVal.Y = Position.Y;
               }
               else
               {
                    retVal = Position;
                    PrevMovingMouseState = MovingMouseState;
               }

               return retVal;
          }

          public override void Update(GameTime gameTime)
          {
               this.CurrKBState = Keyboard.GetState();
               this.MovingMouseState = Mouse.GetState();
               this.ShootingMouseState = Mouse.GetState();

               base.Update(gameTime);
          }

          public abstract void Destroyed();
     }
}