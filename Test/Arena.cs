﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	class Arena
	{
		public void PvP( Hero player1, Hero player2 )
		{
			if (!player1.IsLive)
			{
				Console.WriteLine( "Победил: " + player2.Name );
				return;
			}
			if (!player2.IsLive)
			{
				Console.WriteLine( "Победил: " + player1.Name );
				return;
			}

			Battle( player1, player2 );

			PvP( player1, player2 );
		}

		private void Battle( Hero player1, Hero player2 )
		{
			if ( player1.IsLive )
				player2.GetDamage( player1.SharedDmg );
			if ( player2.IsLive )
				player1.GetDamage(player2.SharedDmg);
		}

		public void DeathMatch( Hero[] heroes )
		{
			while ( AliveHeroCount( heroes ) > 1 )
			{
				heroes = AliveHero( heroes );

				for ( int i = 0; i < heroes.Length; i++ )
				{
					if ( i == heroes.Length - 1 )
					{
						Battle( heroes[heroes.Length - 1], heroes[0] );
					}
					else
					{
						if (heroes[i].IsLive && heroes[i + 1].IsLive)
						{
							Battle( heroes[i], heroes[i + 1]);
						}

						
					}
				}
			}
			heroes = AliveHero( heroes );
			Console.WriteLine( "Победитель смертельного поединка: " + heroes[0].Name );
		}

		public void TeamMatch( Hero[] red, Hero[] blue )
		{
			Console.WriteLine("В разработке");
		}


		private int AliveHeroCount( Hero[] heroes )
		{
			int Count = 0;
			foreach ( Hero hero in heroes )
			{
				if ( hero.IsLive )
				{
					Count++;
				}

			}
			return Count;
		}

		private Hero[] AliveHero( Hero[] heroes )
		{
			return heroes.Where( h => h.IsLive ).ToArray();
		}
        public void Tournament(Hero[] heroes)
        {
            if (heroes.Length != 8)
            {
                Console.WriteLine("Победил: " + heroes[0].Name);
                Console.ReadLine();
                return;
            }

            Hero[] winner1 = new Hero[4];
            Hero[] winner2 = new Hero[2];

            Console.WriteLine("Четверть финала");
            PvP(heroes[0], heroes[1]);
            if (heroes[0].IsLive)
            {
                Console.WriteLine("Победил: " + heroes[1].Name);
                Console.ReadLine();
                return;
            }
            if (heroes[1].IsLive)
            {
                Console.WriteLine("Победил: " + heroes[2].Name);
                Console.ReadLine();
                return;
            }
            PvP(heroes[2], heroes[3]);
            if (heroes[2].IsLive)
            {
                Console.WriteLine("Победил: " + heroes[3].Name);
                Console.ReadLine();
                return;
            }
            PvP( heroes[6], heroes[7]);
            if (heroes[6].IsLive)
            {
                Console.WriteLine("Победил: " + heroes[4].Name);
                Console.ReadLine();
                return;
            }
            if (heroes[7].IsLive)
            {
                Console.WriteLine("Победил: " + heroes[5].Name);
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Полуфинал");
            PvP(winner1[0], winner1[1]);
            if (winner1[0].IsLive)
            {
                Console.WriteLine("Победил: " + heroes[6].Name);
                Console.ReadLine();
                return;
            }
            if (winner1[1].IsLive)
            {
                Console.WriteLine("Победил: " + heroes[7].Name);
                Console.ReadLine();
                return;
            }
            PvP(winner1[2], winner1[3]);
            if ( winner1[2].IsLive )
            {
                Console.WriteLine("Победил: " + heroes[8].Name);
                Console.ReadLine();
                return;
            }
            if ( winner1[3].IsLive )
            {
                Console.WriteLine("Победил: " + heroes[9].Name);
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Финал");
            PvP(winner2[0], winner2[1]);
            Console.ReadLine();
        }
	}
}


