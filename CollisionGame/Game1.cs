using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CollisionGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D m_crapFace;
        GameLogic logic;
       
        public Game1()
        {
            logic = new GameLogic();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        private void DoSomethingWithSpriteBatch(SpriteBatch sb)
        {

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
            m_crapFace = this.Content.Load<Texture2D>("Crapface");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            logic.player.left = Keyboard.GetState().IsKeyDown(Keys.Left);
            logic.player.up = Keyboard.GetState().IsKeyDown(Keys.Up);
            logic.player.down = Keyboard.GetState().IsKeyDown(Keys.Down);
            logic.player.right = Keyboard.GetState().IsKeyDown(Keys.Right);

            // TODO: Add your update logic here

            base.Update(gameTime);
            float dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            logic.tick(dt);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if(logic.isDead)
            {
                GraphicsDevice.Clear(Color.Red);
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
           
            spriteBatch.Begin();          
            for(int x = 0; x<logic.all.Count;x++)
            {
                Rectangle rc = new Rectangle((int)logic.all[x].x - 10, (int)logic.all[x].y - 10, 20, 20);
                spriteBatch.Draw(m_crapFace, rc, Color.White);
            }
            spriteBatch.End();
        }
    }
}
