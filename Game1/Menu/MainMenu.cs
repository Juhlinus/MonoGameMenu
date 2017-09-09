using System;
using Microsoft.Xna.Framework;

namespace Game1.Menu
{
	class MainMenu : BaseMenu
	{
		private static readonly string[] Items = { "New Game", "High Scores", "Options", "Credits", "Quit" };

		readonly Game _game;

		public MainMenu(Game game) : base(game, Items)
		{
			_game = game;
		}

		public override void MenuAction()
		{
			Game.Components.Remove(this);

			switch (Items[Selection])
			{
				case "New Game":
					break;
				case "High Scores":
					break;
				case "Options":
					break;
				case "Credits":
					Game.Components.Add(new Credits(_game));
					break;
				case "Quit":
					Game.Exit();
					break;
				default:
					throw new ArgumentException("\"" + Items[Selection] + "\" is not a valid case");
			}
		}
	}
}
