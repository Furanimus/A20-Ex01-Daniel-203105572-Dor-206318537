using Microsoft.Xna.Framework;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class RedMotherShip : MotherShip
     {
          private const string k_AssetName = @"Sprites\MotherShip_32x120";

          private RedMotherShip(Game i_Game) : base(k_AssetName, i_Game)
          {
               TintColor = Color.Red;
               Score = 800;
               Velocity = new Vector2(100, 0);
               Width = 120;
               Height = 32;
               m_Position.X = -Width;
               m_Position.Y = 32;
          }
     }
}
