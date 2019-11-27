﻿using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
     public class EnemyPink : Enemy, IShooter
     {
          Bullet m_Bullet;

          private EnemyPink()
          {
               GraphicsPath = @"Sprites\Enemy0101_32x32";
               Score = 250;
               m_RandomBehavior = new RandomBehavior(5, 0, 1000);
               m_Bullet = new Bullet();
          }

          public IGun Gun { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

          public void Shoot()
          {
               if (m_RandomBehavior.Roll())
               {
                    //m_Bullet.Position.X = Position.X + Width * 0.5;
                    //m_Bullet.Position.Y = Position.Y + Height;
               }
          }
     }
}
