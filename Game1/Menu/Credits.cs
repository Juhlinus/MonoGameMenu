using System;
using Microsoft.Xna.Framework;

namespace Game1.Menu
{
	class Credits : BaseMenu
	{
		static readonly string[] Items = { "Who did it", "Back" };

		readonly Game _game;

		public Credits(Game game) : base(game, Items)
		{
			_game = game;
		}

		public override void MenuAction()
		{
			Game.Components.Remove(this);

			switch (Items[Selection])
			{
				case ("Who did it"):
					break;
				case ("Back"):
					Game.Components.Add(new MainMenu(_game));
					break;
				default:
					throw new ArgumentException("\"" + Items[Selection] + "\" is not a valid case");
			}
		}
	}
}
