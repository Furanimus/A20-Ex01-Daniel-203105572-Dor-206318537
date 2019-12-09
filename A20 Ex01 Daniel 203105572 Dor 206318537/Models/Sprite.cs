using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class Sprite : DrawableGameComponent
     {
          private bool m_UseSharedSpriteBatch = false;
          protected Vector2 m_Position = Vector2.Zero;
          protected SpriteBatch m_SpriteBatch = null;

          public Sprite(string i_GraphicsPath, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
              : base(i_Game)
          {
               this.GraphicsPath = i_GraphicsPath;
               this.UpdateOrder = i_UpdateOrder;
               this.DrawOrder = i_DrawOrder;
          }

          public Sprite(string i_GraphicsPath, Game i_Game, int i_CallsOrder)
              : this(i_GraphicsPath, i_Game, i_CallsOrder, i_CallsOrder)
          { }

          public Sprite(string i_GraphicsPath, Game i_Game)
              : this(i_GraphicsPath, i_Game, int.MaxValue)
          { }

          public static Vector2 Right { get; } = new Vector2(1, 0);

          public static Vector2 Left { get; } = new Vector2(-1, 0);

          public static Vector2 Up { get; } = new Vector2(0, -1);

          public static Vector2 Down { get; } = new Vector2(0, 1);

          public Texture2D Graphics { get; set; }

          public SpriteBatch SpriteBatch {
               set
               {
                    m_SpriteBatch = value;
                    m_UseSharedSpriteBatch = true;
               }
          }

          public string GraphicsPath { get; set; }

          public GameEnvironment GameEnvironment { get; set; } = Singelton<GameEnvironment>.Instance;

          public Color TintColor { get; set; } = Color.White;

          public int Width {get; set; }

          public int Height { get; set; }

          public float Velocity { get; set; }

          public Vector2 Direction { get; set; }

          public Vector2 Position
          {
               get
               {
                    return m_Position;
               }
               set
               {
                    value.X = MathHelper.Clamp(value.X, 0, GameEnvironment.WindowWidth - this.Width);
                    m_Position = value;
               }
          }

          public override void Initialize()
          {
               base.Initialize();
          }

          protected override void LoadContent()
          {
               Graphics = Game.Content.Load<Texture2D>(GraphicsPath);
               this.Width = Graphics.Width;
               this.Height = Graphics.Height;

               if (m_SpriteBatch == null)
               {
                    m_SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
                    m_UseSharedSpriteBatch = false;
               }

               base.LoadContent();
          }

          public override void Update(GameTime gameTime)
          {
               this.Position += Direction * this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

               base.Update(gameTime);
          }

          public override void Draw(GameTime gameTime)
          {
               if(!m_UseSharedSpriteBatch)
               {
                    m_SpriteBatch.Begin();
               }

               m_SpriteBatch.Draw(Graphics, Position, TintColor);

               if (!m_UseSharedSpriteBatch)
               {
                    m_SpriteBatch.End();
               }

               base.Draw(gameTime);
          }

     }
}
