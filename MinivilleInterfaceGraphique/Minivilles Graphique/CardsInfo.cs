using System;

namespace Minivilles_Graphique
{
	public struct CardsInfo
	{
		public CardsInfo(int Id, string Color, int Cost, string Name, int Number, string Effect, int MinDice, int MaxDice, int Gain)
		{
			this.Id = Id;
			this.Color = Color;
			this.Cost = Cost;
			this.Name = Name;
			this.Number = Number;
			this.Effect = Effect;
			this.MinDice = MinDice;
			this.MaxDice = MaxDice;
			this.Gain = Gain;
		}

		public int Id { get; set; }
		public string Color { get; set; }
		public int Cost { get; set; }
		public string Name { get; set; }
		public int Number { get; set; }
		public string Effect { get; set; }
		public int MinDice { get; set; }
		public int MaxDice { get; set; }
		public int Gain { get; set; }
	}
}
