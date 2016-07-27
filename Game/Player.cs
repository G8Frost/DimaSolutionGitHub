﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace Game
{
    class Player : Hero

    {
        public int Karma = 0;


        public Player( string name, int hp ) : base( name, hp )
        {
            Dmg = 1;
        }

        public override int SharedDmg
        {
            get { return Dmg + ( CurrentWeapon == null ? 0 : CurrentWeapon.Dmg ); }
        }
    }
}
