using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
	public class Menu : DrawableGameComponent
	{
		GraphicsDeviceManager _graphics;

		SpriteBatch _spriteBatch;
		SpriteFont _menuItem;
		SpriteFont _title;

		Texture2D[] _selectionBoxes;

		KeyboardState _oldState;

		bool gameStart_;
		int _selection;
		string[] _items;

		public Menu(Game game /*, GameLoop gameLoop */) : base(game)
		{
			// gameStart_ = gameStart;
		}

		public override void Initialize()
		{
			_spriteBatch = (SpriteBatch) Game.Services.GetService(typeof(SpriteBatch));
			_graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(GraphicsDeviceManager));

			_menuItem = Game.Content.Load<SpriteFont>("MenuItem");
			_title = Game.Content.Load<SpriteFont>("Title");

			_selection = 0;

			_items = new[] {"New Game", "High Scores", "Options", "Credits", "Quit"};

			_selectionBoxes = new Texture2D[ _items.Length ];

			for (int i = 0; i < _items.Length; i++)
			{
				_selectionBoxes[i] = new Texture2D(_graphics.GraphicsDevice, (int) _menuItem.MeasureString(_items[i]).X + 10,
					(int) _menuItem.MeasureString(_items[i]).Y + 5);
			}

			foreach (var select in _selectionBoxes)
			{
				Color[] data = new Color[ select.Width * select.Height ];

				for (int i = 0; i < data.Length; i++)
				{
					data[i] = Color.Chocolate;
					select.SetData(data);
				}
			}

			_oldState = Keyboard.GetState();

			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			KeyboardState newState = Keyboard.GetState();

			var newPressedKeys = from k in newState.GetPressedKeys()
				where !(_oldState.GetPressedKeys().Contains(k))
				select k;

			IEnumerable<Keys> pressedKeys = newPressedKeys as Keys[] ?? newPressedKeys.ToArray();

			if (pressedKeys.Contains(Keys.Down))
			{
				_selection++;
				_selection %= _items.Length;
			}
			else if (pressedKeys.Contains(Keys.Up))
			{
				_selection--;
				_selection = _selection < 0 ? _items.Length - 1 : _selection;
			}
			else if (pressedKeys.Contains(Keys.Enter))
			{
				MenuAction();
			}

			_oldState = newState;
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			_spriteBatch.Begin();

			_spriteBatch.DrawString(_title, "Fuck you guys", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 110, 75),
				Color.White);

			Vector2 itemPosition;
			itemPosition.X = _graphics.PreferredBackBufferWidth / 2 - 100;

			for (int i = 0; i < _items.Length; i++)
			{
				itemPosition.Y = _graphics.PreferredBackBufferHeight / 2 - 90 + 60 * i;

				if (i == _selection)
				{
					_spriteBatch.Draw(_selectionBoxes[i], new Vector2(itemPosition.X - 4, itemPosition.Y - 2), Color.White);
				}

				_spriteBatch.DrawString(_menuItem, _items[i], itemPosition, Color.Yellow);
			}

			_spriteBatch.End();
		}

		void MenuAction()
		{
			Game.Components.Remove(this);

			switch (_items[_selection])
			{
				case ("New Game"):
					break;
				case ("High Scores"):
					break;
				case ("Options"):
					break;
				case ("Credits"):
					break;
				case ("Quit"):
					Game.Exit();
					break;
				default:
					throw new ArgumentException("\"" + _items[_selection] + "\" is not a valid case");
			}
		}
	}
}