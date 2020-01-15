using System;
using A20_Ex01_Daniel_203105572_Dor_206318537.Components;
using A20_Ex01_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex01_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
{
     public abstract class GameScreen : CompositeDrawableComponent<IGameComponent>
     {
          public event EventHandler Closed;

          private IInputManager m_InputManager;
          private IInputManager m_DummyInputManager = new DummyInputManager();
          private Texture2D m_BlankTexture;
          private Texture2D m_GradientTexture;

          public GameScreen(Game i_Game)
              : base(i_Game)
          {
               this.Enabled = false;
               this.Visible = false;
          }

          public bool IsModal { get; set; } = true;

          public bool IsOverlayed { get; set; }

          public GameScreen PreviousScreen { get; set; }

          public bool HasFocus { get; set; }

          public IInputManager InputManager
          {
               get { return this.HasFocus ? m_InputManager : m_DummyInputManager; }
          }

          public override void Initialize()
          {
               m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;

               if (m_InputManager == null)
               {
                    m_InputManager = m_DummyInputManager;
               }

               base.Initialize();
          }

          public void Activate()
          {
               this.Enabled = this.Visible = this.HasFocus = true;

               OnActivated();
          }

          protected virtual void OnActivated()
          {
               if (PreviousScreen != null && this.HasFocus)
               {
                    PreviousScreen.HasFocus = false;
               }
          }

          public void Deactivate()
          {
               this.Enabled = this.Visible = this.HasFocus = false;
          }

          protected void ExitScreen()
          {
               Deactivate();
               OnClosed();
          }

          protected virtual void OnClosed()
          {
               if (Closed != null)
               {
                    Closed.Invoke(this, EventArgs.Empty);
               }
          }

          public override void Update(GameTime gameTime)
          {
               if (PreviousScreen != null && !this.IsModal)
               {
                    PreviousScreen.Update(gameTime);
               }

               base.Update(gameTime);
          }

          public override void Draw(GameTime gameTime)
          {
               if (PreviousScreen != null && IsOverlayed)
               {
                    PreviousScreen.Draw(gameTime);

                    drawFadedDarkCoverIfNeeded();
               }

               base.Draw(gameTime);
          }

          public IScreensMananger ScreensManager { get; set; }

          public float BlackTintAlpha { get; set; }

          protected override void LoadContent()
          {
               base.LoadContent();
               //m_GradientTexture = this.ContentManager.Load<Texture2D>(@"Screens\gradient");
               //m_BlankTexture = this.ContentManager.Load<Texture2D>(@"Screens\blank");
          }

          public bool UseGradientBackground { get; set; }

          public void drawFadedDarkCover(byte i_Alpha)
          {
               Viewport viewport = this.GraphicsDevice.Viewport;
               Texture2D background = UseGradientBackground ? m_GradientTexture : m_BlankTexture;

               SpriteBatch.Begin();
               SpriteBatch.Draw(background, new Rectangle(0, 0, viewport.Width, viewport.Height),
                                new Color((byte)0, (byte)0, (byte)0, i_Alpha));
               SpriteBatch.End();
          }

          private void drawFadedDarkCoverIfNeeded()
          {
               if (BlackTintAlpha > 0 || UseGradientBackground)
               {
                    drawFadedDarkCover((byte)(BlackTintAlpha * byte.MaxValue));
               }
          }
     }
}
