using A20_Ex01_Daniel_203105572_Dor_206318537.Models.Animators.ConcreteAnimator;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens;
using A20_Ex03_Daniel_203105572_Dor_206318537.Screens.Animators.ConcreteAnimator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace A20_Ex03_Daniel_203105572_Dor_206318537.Utils
{
     public class StrokeSpriteFont : Sprite
     {
          private const float k_PulsePerSec = 1.5f;
          private const float k_TargetScale = 1.03f;
          private const string k_FontAssetName = @"Fonts\InstructionFont";
          private SpriteFont m_SpriteFont;
          private float m_Scale = -1;
          private Color m_StrokeColor;

          public StrokeSpriteFont(string i_Text, GameScreen i_GameScreen) 
               : this(k_FontAssetName, i_Text, i_GameScreen)
          {
          }

          public StrokeSpriteFont(string i_FontAssetName, string i_Text, GameScreen i_GameScreen) 
               : base("", i_GameScreen, int.MaxValue)
          {
               Text = i_Text;
               m_SpriteFont = this.Game.Content.Load<SpriteFont>(i_FontAssetName);
               m_StrokeColor = new Color(Color.Black, this.TintColor.A);
               this.BlendState = BlendState.NonPremultiplied;
          }

          public string Text { get; set; }

          public Color StrokeColor
          {
               get
               {
                    m_StrokeColor.A = TintColor.A;
                    return m_StrokeColor;
               }

               set
               {
                    m_StrokeColor = value;
               }
          }

          public float StrokeSize { get; set; } = 0;

          public float Scale
          {
               get
               {
                    m_Scale = Scales.X;
                    return m_Scale;
               }

               set
               {
                    m_Scale = value;
               }
          }

          public bool IsInitialized { get; private set; }

          public override void Initialize()
          {
               if (!IsInitialized)
               {
                    base.Initialize();

                    IsInitialized = true;
                    this.Animations.Add(new PulseAnimator(TimeSpan.Zero, k_TargetScale, k_PulsePerSec));
                    this.Animations.Add(new WaypointsAnimator(100, TimeSpan.FromSeconds(0.2f), false, this.Position + new Vector2(-10, 0)));
                    this.Animations.Enabled = false;
               }
          }

          protected override void OnDraw()
          {
               if (StrokeSize > 0)
               {
                    SpriteBatch.DrawString(m_SpriteFont, Text, PositionForDraw + new Vector2(StrokeSize * this.Scale, -StrokeSize * this.Scale), this.StrokeColor, 0, this.PositionOrigin, this.Scale, SpriteEffects.None, 1f);
                    SpriteBatch.DrawString(m_SpriteFont, Text, PositionForDraw + new Vector2(-StrokeSize * this.Scale, -StrokeSize * this.Scale), this.StrokeColor, 0, this.PositionOrigin, this.Scale, SpriteEffects.None, 1f);

                    SpriteBatch.DrawString(m_SpriteFont, Text, PositionForDraw + new Vector2(0, -StrokeSize * this.Scale), this.StrokeColor, 0, this.PositionOrigin, this.Scale, SpriteEffects.None, 1f);
                    SpriteBatch.DrawString(m_SpriteFont, Text, PositionForDraw + new Vector2(0, StrokeSize * this.Scale), this.StrokeColor, 0, this.PositionOrigin, this.Scale, SpriteEffects.None, 1f);

                    SpriteBatch.DrawString(m_SpriteFont, Text, PositionForDraw + new Vector2(StrokeSize * this.Scale, StrokeSize * this.Scale), this.StrokeColor, 0, this.PositionOrigin, this.Scale, SpriteEffects.None, 1f);
                    SpriteBatch.DrawString(m_SpriteFont, Text, PositionForDraw + new Vector2(-StrokeSize * this.Scale, StrokeSize * this.Scale), this.StrokeColor, 0, this.PositionOrigin, this.Scale, SpriteEffects.None, 1f);
               }

               SpriteBatch.DrawString(m_SpriteFont, Text, PositionForDraw, this.TintColor, 0, this.PositionOrigin, this.Scale, SpriteEffects.None, 0f);
          }
     }
}
