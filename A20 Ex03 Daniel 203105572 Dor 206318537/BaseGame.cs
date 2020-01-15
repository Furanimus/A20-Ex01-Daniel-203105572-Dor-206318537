using Microsoft.Xna.Framework;
using A20_Ex03_Daniel_203105572_Dor_206318537.Interfaces;
using A20_Ex03_Daniel_203105572_Dor_206318537.Managers;
using A20_Ex03_Daniel_203105572_Dor_206318537.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace A20_Ex03_Daniel_203105572_Dor_206318537
{
     public abstract class BaseGame : Game
     {
          public BaseGame()
          {
               InitServices();
          }

          protected override void Update(GameTime i_GameTime)
          {
               this.GameTime = i_GameTime;

               base.Update(i_GameTime);
          }

          protected override void Initialize()
          {
               this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);
               this.Services.AddService(typeof(SpriteBatch), this.SpriteBatch);

               base.Initialize();
          }

          protected virtual void InitServices()
          {
               InputManager = new InputManager(this);
               CollisionsManager = new CollisionsManager(this);
               RandomBehavior = new RandomBehavior(this);
               LivesManager = new LivesManager(this);
               ScoreManager = new ScoreManager(this);
          }

          public GameTime GameTime { get; set; }

          protected IInputManager InputManager { get; set; }

          protected ICollisionsManager CollisionsManager { get; set; }

          protected IRandomBehavior RandomBehavior { get; set; }

          protected ILivesManager LivesManager { get; set; }

          protected IScoreManager ScoreManager { get; set; }

          protected SpriteBatch SpriteBatch { get; set; }
     }
}
