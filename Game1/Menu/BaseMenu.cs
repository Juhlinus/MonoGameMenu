﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.Menu
{
	public abstract class BaseMenu : DrawableGameComponent
	{
		GraphicsDeviceManager _graphics;

		SpriteBatch _spriteBatch;
		SpriteFont _menuItem;
		SpriteFont _title;

		Texture2D[] _selectionBoxes;

		KeyboardState _oldState;

		/*
		bool _gameStart;
		*/
		protected int Selection;
		string MenuTitle;
		private readonly string[] _items;

		protected BaseMenu(Game game, string[] items, string menuTitle) : base(game)
		{
			_items = items;
			MenuTitle = menuTitle;
		}

		public override void Initialize()
		{
			_spriteBatch = (SpriteBatch) Game.Services.GetService(typeof(SpriteBatch));
			_graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(GraphicsDeviceManager));

			_menuItem = Game.Content.Load<SpriteFont>("MenuItem");
			_title = Game.Content.Load<SpriteFont>("Title");

			Selection = 0;

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
				where !_oldState.GetPressedKeys().Contains(k)
				select k;

			IEnumerable<Keys> pressedKeys = newPressedKeys as Keys[] ?? newPressedKeys.ToArray();

			if (pressedKeys.Contains(Keys.Down))
			{
				Selection++;
				Selection %= _items.Length;
			}
			else if (pressedKeys.Contains(Keys.Up))
			{
				Selection--;
				Selection = Selection < 0 ? _items.Length - 1 : Selection;
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

			_spriteBatch.DrawString(_title, MenuTitle, new Vector2(_graphics.PreferredBackBufferWidth / 2 - 110, 75),
				Color.White);

			Vector2 itemPosition;
			itemPosition.X = _graphics.PreferredBackBufferWidth / 2 - 100;

			for (int i = 0; i < _items.Length; i++)
			{
				itemPosition.Y = _graphics.PreferredBackBufferHeight / 2 - 90 + 60 * i;

				if (i == Selection)
				{
					_spriteBatch.Draw(_selectionBoxes[i], new Vector2(itemPosition.X - 4, itemPosition.Y - 2), Color.White);
				}

				_spriteBatch.DrawString(_menuItem, _items[i], itemPosition, Color.Yellow);
			}

			_spriteBatch.End();
		}

		public virtual void MenuAction() {	}
	}
}