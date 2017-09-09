using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
	public class Menu : DrawableGameComponent
	{
		GraphicsDeviceManager graphics_;

		SpriteBatch spriteBatch_;
		SpriteFont menuItem_;
		SpriteFont title_;

		Texture2D[] selectionBoxes_;

		KeyboardState oldState_;

		bool gameStart_;
		int selection_;
		string[] items_;

		public Menu(Game game /*, GameLoop gameLoop */) : base(game)
		{
			//gameStart_ = 
		}

		public override void Initialize()
		{
			spriteBatch_ = (SpriteBatch) Game.Services.GetService(typeof(SpriteBatch));
			graphics_ = (GraphicsDeviceManager)Game.Services.GetService(typeof(GraphicsDeviceManager));

			menuItem_ = Game.Content.Load<SpriteFont>("MenuItem");
			title_ = Game.Content.Load<SpriteFont>("Title");

			selection_ = 0;

			items_ = new[] {"New Game", "High Scores", "Options", "Credits", "Quit"};

			selectionBoxes_ = new Texture2D[ items_.Length ];

			for (int i = 0; i < items_.Length; i++)
			{
				selectionBoxes_[i] = new Texture2D(graphics_.GraphicsDevice, (int) menuItem_.MeasureString(items_[i]).X + 10,
					(int) menuItem_.MeasureString(items_[i]).Y + 5);
			}

			foreach (var select in selectionBoxes_)
			{
				Color[] data = new Color[ select.Width * select.Height ];

				for (int i = 0; i < data.Length; i++)
				{
					data[i] = Color.Chocolate;
					select.SetData(data);
				}
			}

			oldState_ = Keyboard.GetState();

			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			KeyboardState newState = Keyboard.GetState();

			var newPressedKeys = from k in newState.GetPressedKeys()
				where !(oldState_.GetPressedKeys().Contains(k))
				select k;

			IEnumerable<Keys> pressedKeys = newPressedKeys as Keys[] ?? newPressedKeys.ToArray();

			if (pressedKeys.Contains(Keys.Down))
			{
				selection_++;
				selection_ %= items_.Length;
			}
			else if (pressedKeys.Contains(Keys.Up))
			{
				selection_--;
				selection_ = selection_ < 0 ? items_.Length - 1 : selection_;
			}
			else if (pressedKeys.Contains(Keys.Enter))
			{
				//menuAction();
			}

			oldState_ = newState;
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			spriteBatch_.Begin();

			spriteBatch_.DrawString(title_, "Fuck you guys", new Vector2(graphics_.PreferredBackBufferWidth / 2 - 110, 75),
				Color.White);

			Vector2 itemPosition;
			itemPosition.X = graphics_.PreferredBackBufferWidth / 2 - 100;

			for (int i = 0; i < items_.Length; i++)
			{
				itemPosition.Y = graphics_.PreferredBackBufferHeight / 2 - 90 + 60 * i;

				if (i == selection_)
				{
					spriteBatch_.Draw(selectionBoxes_[i], new Vector2(itemPosition.X - 4, itemPosition.Y - 2), Color.White);
				}

				spriteBatch_.DrawString(menuItem_, items_[i], itemPosition, Color.Yellow);
			}

			spriteBatch_.End();
		}
	}
}