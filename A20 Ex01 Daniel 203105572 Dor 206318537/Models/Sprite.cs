using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Models.Animators;
using System;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public class Sprite : LoadableDrawableComponent
     {
          protected Color m_TintColor = Color.White;
          protected Vector2 m_Position = Vector2.Zero;
          protected Vector2 m_Scales = Vector2.One;

          public Sprite(string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder) 
               : base(i_AssetName, i_Game, i_UpdateOrder, i_DrawOrder)
          {
          }

          public Sprite(string i_AssetName, Game i_Game, int i_CallsOrder)
              : base(i_AssetName, i_Game, i_CallsOrder)
          {
          }

          public Sprite(string i_AssetName, Game i_Game)
              : base(i_AssetName, i_Game, int.MaxValue)
          {
          }

          public static Vector2 Right { get; } = new Vector2(1, 0);

          public static Vector2 Left { get; } = new Vector2(-1, 0);

          public static Vector2 Up { get; } = new Vector2(0, -1);

          public static Vector2 Down { get; } = new Vector2(0, 1);

          public CompositeAnimator Animations { get; set; }

          public Texture2D Texture { get; set; }

          public float Width
          {
               get { return WidthBeforeScale * m_Scales.X; }
               set { WidthBeforeScale = value / m_Scales.X; }
          }

          public float Height
          {
               get { return HeightBeforeScale * m_Scales.Y; }
               set { HeightBeforeScale = value / m_Scales.Y; }
          }

          public Vector2 StartingPosition { get; set; } = Vector2.Zero;

          public float WidthBeforeScale { get; set; }

          public float HeightBeforeScale { get; set; }

          public virtual Vector2 Position
          {
               get { return m_Position; }
               set
               {
                    if (m_Position != value)
                    {
                         m_Position = value;
                         OnPositionChanged();
                    }
               }
          }

          public Vector2 PositionOrigin { get; set; } = Vector2.Zero;

          public Vector2 RotationOrigin { get; set; } = Vector2.Zero;

          private Vector2 PositionForDraw
          {
               get { return this.Position - this.PositionOrigin + this.RotationOrigin; }
          }

          public Vector2 TopLeftPosition
          {
               get { return this.Position - this.PositionOrigin; }
               set { this.Position = value + this.PositionOrigin; }
          }

          public Rectangle Bounds
          {
               get
               {
                    return new Rectangle(
                        (int)TopLeftPosition.X,
                        (int)TopLeftPosition.Y,
                        (int)this.Width,
                        (int)this.Height);
               }
          }

          public Rectangle BoundsBeforeScale
          {
               get
               {
                    return new Rectangle(
                        (int)TopLeftPosition.X,
                        (int)TopLeftPosition.Y,
                        (int)this.WidthBeforeScale,
                        (int)this.HeightBeforeScale);
               }
          }

          public Rectangle SourceRectangle { get; set; }

          public Vector2 TextureCenter
          {
               get
               {
                    return new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
               }
          }

          public Vector2 SourceRectangleCenter
          {
               get
               {
                    return new Vector2((float)(SourceRectangle.Width / 2), (float)(SourceRectangle.Height / 2));
               }
          }
          public float Rotation { get; set; } = 0;

          public Vector2 Scales
          {
               get { return m_Scales; }
               set
               {
                    if (m_Scales != value)
                    {
                         m_Scales = value;
                         // Notify the Collision Detection mechanism:
                         OnPositionChanged();
                    }
               }
          }

          public Vector2 ViewDirection { get; set; } = Sprite.Down;

          public Vector2 MoveDirection { get; set; } = Sprite.Right;

          public Color TintColor
          {
               get { return m_TintColor; }
               set { m_TintColor = value; }
          }

          public float Opacity
          {
               get { return (float)m_TintColor.A / (float)byte.MaxValue; }
               set { m_TintColor.A = (byte)(value * (float)byte.MaxValue); }
          }

          public float LayerDepth { get; set; }

          public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;

          public Vector2 Velocity { get; set; }

          public float AngularVelocity { get; set; }

          protected override void InitBounds()
          {
               InitSourceRectangle();
               InitOrigins();
          }

          protected virtual void InitOrigins()
          {
               m_Position = StartingPosition;
          }

          protected virtual void InitSourceRectangle()
          {
               SourceRectangle = new Rectangle(0, 0, (int)WidthBeforeScale, (int)HeightBeforeScale);
          }

          private bool m_UseSharedBatch = true;

          protected SpriteBatch m_SpriteBatch;
          public SpriteBatch SpriteBatch
          {
               set
               {
                    m_SpriteBatch = value;
                    m_UseSharedBatch = true;
               }
          }

          public override void Initialize()
          {
               base.Initialize();

               Animations = new CompositeAnimator(this);
          }

          protected override void LoadContent()
          {
               Texture = Game.Content.Load<Texture2D>(m_AssetName);

               if (m_SpriteBatch == null)
               {
                    m_SpriteBatch =
                        Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

                    if (m_SpriteBatch == null)
                    {
                         m_SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
                         m_UseSharedBatch = false;
                    }
               }

               base.LoadContent();
          }

          public override void Update(GameTime i_GameTime)
          {
               float totalSeconds = (float)i_GameTime.ElapsedGameTime.TotalSeconds;

               this.Position += this.MoveDirection * this.Velocity * totalSeconds;
               this.Rotation += this.AngularVelocity * totalSeconds;

               if(this.Animations != null)
               {
                    this.Animations.Update(i_GameTime);
               }

               base.Update(i_GameTime);
          }

          public override void Draw(GameTime i_GameTime)
          {
               if (!m_UseSharedBatch)
               {
                    m_SpriteBatch.Begin();
               }

               m_SpriteBatch.Draw(Texture, this.PositionForDraw,
                    this.SourceRectangle, this.TintColor,
                   this.Rotation, this.RotationOrigin, this.Scales,
                   SpriteEffects.None, this.LayerDepth);

               if (!m_UseSharedBatch)
               {
                    m_SpriteBatch.End();
               }

               base.Draw(i_GameTime);
          }

          public virtual bool CheckCollision(ICollidable i_Source)
          {
               bool collided = false;
               ICollidable2D source = i_Source as ICollidable2D;

               if (source != null)
               {
                    collided = source.Bounds.Intersects(this.Bounds);
               }

               return collided;
          }

          public virtual void Collided(ICollidable i_Collidable)
          {
               this.Velocity *= -1;
          }

          public Sprite ShallowClone()
          {
               return this.MemberwiseClone() as Sprite;
          }

          protected override void DrawBoundingBox()
          {
          }
     }
}