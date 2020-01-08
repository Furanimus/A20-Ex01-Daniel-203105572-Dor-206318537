using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Bullet : BaseBullet
     {
          private const string k_GraphicPath = @"Sprites\Bullet";

          public Bullet(Game i_Game) : this(Color.Red, i_Game)
          {
          }

          public Bullet(Color i_TintColor, Game i_Game) : base(k_GraphicPath, i_Game)
          {
               this.Width = 6;
               this.Height = 16;
               this.Enabled = false;
               this.Visible = false;
               this.TintColor = i_TintColor;
               this.Velocity = new Vector2(0, 160);
          }
     }
}