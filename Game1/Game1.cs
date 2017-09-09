using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
	    readonly GraphicsDeviceManager _graphics;
	    SpriteBatch _spriteBatch;
	    /*
		AudioEngine audioEngine_;
	    WaveBank waveBank_;
	    SoundBank soundBank_;
		*/

		public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

	        Components.Add(new Menu.MainMenu(this));

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

			//audioEngine_ = new AudioEngine("Content/Audio/YEPAudio.xgs");
	        //waveBank_ = new WaveBank(audioEngine_, "Content/Audio/Wave Bank.xwb");
	        //soundBank_ = new SoundBank(audioEngine_, "Content/Audio/Sound Bank.xsb");
	        //Services.AddService(typeof(AudioEngine), audioEngine_);
	        //Services.AddService(typeof(SoundBank), soundBank_);

	        _spriteBatch = new SpriteBatch(GraphicsDevice);
	        Services.AddService(typeof(SpriteBatch), _spriteBatch);
	        Services.AddService(typeof(GraphicsDeviceManager), _graphics);

			base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MonoGameOrange);

            // TODO: Add your drawing code here
	        _spriteBatch.Begin();
	        //spriteBatch_.Draw(Ship, Vector2.Zero, Color.Pink);
	        //spriteBatch_.DrawString(font, "Your momma so fat..", new Vector2(200, 100), Color.White);
	        _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
