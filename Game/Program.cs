﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace Game
{
    class Program
    {
        private static LocationFactory _locationFactory;

        public static void PvE( Player mainplayer, Monster monster )
        {
            if ( !mainplayer.IsLive )
            {
                Console.WriteLine( "Вы победили" );
                return;
            }
            if ( !monster.IsLive )
            {
                Console.WriteLine( "Вы проиграли" );
                return;
            }

            Battle( mainplayer, monster );

            PvE( mainplayer, monster );
        }

        private static void Battle( Player mainplayer, Monster monster )
        {
            if ( mainplayer.IsLive )
                monster.GetDamage( mainplayer.SharedDmg );
            if ( monster.IsLive )
                mainplayer.GetDamage( monster.Dmg );
        }


        static void Main( string[] args )
        {
            _locationFactory = new LocationFactory();

            var Peasant = new Player("", 100);
            Peasant.Str = 1;
            Peasant.Int = 1;
            Peasant.Agi = 1;
            Peasant.Prot = 0;
            Peasant.MP = 0;


            Peasant.CurrentLocation = _locationFactory.CreateDreamfallLocation();


            Console.WriteLine( "Dreamfall" );
            Console.WriteLine(
                "Предыстория: В 1537 году произошёл катаклизм, который 10 лет спустя станет известен как Слияние. После череды таинственных событий совместились два мира: Мир Людей(Нирн) и Мир Демонов(Ад). Последствия оказались катастрофическими. В Нирн просочилась энергия Ада, превращая людей в страшных чудовищ. Однако многие люди смогли выжить и сохранить человеческий облик. Они начали строить королевства, успешно защищаясь от чудищ, во многом благодаря поддержке Сил Рая(Эллизиума). Некоторые воины смогли получить способности, черпая энергию из Эллизиума и становясь Паладинами, а многие из наиболее отважных воинов смогли подчинить себе силу Ада и стали Демоническими Рыцарями. Начинают появляться Кошмары - воины, являющиеся концентрацией силы ранее скрытого четвёртого мира: Тьмы. Многие воины вооружились копьями, а некоторые смогли объединить в себе частичку сил Тьмы и Ада, став Разрушителями. Раньше магов недолюбливали, но теперь они являются желанными гостями в любом королевстве. Многие маги в основном сражаются при помощи магии стихий, но некторые черпают силы из Эллизиума(Капеллан) или из Тьмы(Некроманты). Ассассинов теперь часто нанимают, чтобы они убили определённого сильного Демона, но некоторые сами охотятся на них, используя силы Тьмы. Таких называют Повелителями Смерти. Лучники славятся своей возможностью поразить врага стрелой с огромного расстояния, вследствии чего они так же являются частью любой армии или просто желанными гостями." );
            Console.WriteLine(
                "  Нынешнее время: 1547 год. Воина продолжается. Люди успешно сражаются с силами Ада и, если приходится, Тьмы. В одном из королевств, именуемым Дримфоллом, живёт один совершенно обычный крестьянин, собирающийся сражаться с Демонами, поскольку они убили его жену и похитили сына. Но в армию он идти не собирается, так что ему самому придётся обучаться. Пока ещё никто, даже он сам, не знает о том, что он был избран древними силами." );


            Start( Peasant );
        }

        private static void Start( Player Peasant )
        {
            var location = Peasant.CurrentLocation;
            Console.WriteLine( location.Name );
            Console.WriteLine( location.Description );

            if ( location.NPC != null )
            {
                location.NPC.Dialogue( Peasant );
            }
            if ( location.Loot != null )
            {

            }
            if ( location.Quest != null )
            {
                Peasant.Quests.Add( location.Quest );
                Console.WriteLine( location.Quest.Name );
                Console.WriteLine( location.Quest.Description );
            }

            if ( location.Action != null )
            {
                location.Action( Peasant );
            }

            if ( location.Locations != null )
            {
                if ( location.Locations.Count == 1 )
                {
                    Peasant.CurrentLocation = location.Locations[0];
                }
                else
                {
                    location.SelectLocation( Peasant );
                }
                Start( Peasant );
            }
            else
            {
                Console.WriteLine("Вы прошли игру. Поздравляем!!!");
            }
        }

        private void OldLogic( Player Peasant )
        {
            var Club = new Weapon();
            Club.Dmg = 5;
            var Pitchfork = new Weapon();
            Pitchfork.Dmg = 7;
            var ChangedHuman = new Monster("", 30);
            ChangedHuman.Dmg = 3;
            ChangedHuman.Prot = 0;


            Console.WriteLine( "Новый квест: " );


            Console.Write( "  Вы нашли развилку. Выберите, куда пойти: 1)Налево. 2)Направо: " );
            String choice = Console.ReadLine();
            switch ( choice )
            {
                case "1":
                    Console.WriteLine( "  Монстр! Защищайтесь!" );
                    PvE( Peasant, ChangedHuman );
                    Peasant.EXP = Peasant.EXP + 10;
                    Console.WriteLine( "Вы получили 10 ед. опыта. Опыт: " + Peasant.EXP );
                    Peasant.Karma = Peasant.Karma + 3;
                    Console.WriteLine(
                        "  После победы над монстром из кустов вышла старая женщина, со своими детьми или внуками. Отблагодарив вас, она ушла. (Начислена положительная карма) " +
                        Peasant.Karma );
                    Console.WriteLine( "  Вы вернулись назад." );
                    break;
                case "2":
                    Console.WriteLine(
                        "  Пройдя пару шагов, вы услышали странные звуки, а затем крики о помощи. Вы молниеносно метнулись на помощь кричавшему. Путь вам преградил монстр." );
                    PvE( Peasant, ChangedHuman );
                    Peasant.EXP = Peasant.EXP + 10;
                    Console.WriteLine( "Вы получили 10 ед. опыта. Опыт: " + Peasant.EXP );
                    Peasant.Karma = Peasant.Karma + 1;
                    Console.WriteLine(
                        "  Крестьянка: Вы спасли нас, спасибо. Чудище успело убить моего внука. Эх, бедный Димитр. Спасибо вам ещё раз. (Начислена положительная карма) " +
                        Peasant.Karma );
                    Console.WriteLine( "  Вы вернулись назад к развилке." );
                    break;
            }
            Console.WriteLine(
                "  Поскольку впереди был тупик, вам ничего не осталось, кроме как пойти направо. Вы пришли в какую-то деревню, на табличке было написано Лэйквуд. К вам сразу вышел мужчина, вероятно староста." );
            Console.Write(
                "  Вальтер(Сюжетный квест!): Эй! Ты ведь из Дримфолла, верно? Ах, ну да. Я староста этой деревни, Вальтер. Наши разведчики доложили, что к нам идёт кто-то из дримфола. 1)Разведчики? Как тут у вас всё серьёзно. 2)Разведчики?! Какого чёрта?! Вы следили за мной?! (Выберите ответ): " );
            String answer4 = Console.ReadLine();
            switch ( answer4 )
            {
                case "1":
                    Peasant.Karma++;
                    Console.WriteLine(
                        "  Вальтер(Сюжетный квест!): Хех, ещё как серьёзно. Шпионы следят в основном за демонами, однако докладывают обо всём, что видят или слышат. (Начислена положительная карма) " +
                        Peasant.Karma );
                    break;
                case "2":
                    Peasant.Karma--;
                    Console.WriteLine(
                        "  Вальтер(Сюжетный квест!): Шпионы обязаны докладывать мне обо всём, что видят. И я бы не советовал тебе хамить мне и повышать на меня голос. Ты в моей деревне, парниша. (Начислена отрицательная карма) " +
                        Peasant.Karma );
                    break;
            }
            Console.WriteLine( "  Вальтер(Сюжетный квест!): Итак, что привело тебя ко мне в деревню?" );
            Console.WriteLine(
                "  Я хочу уничтожить главного демона. Он убил мою жену и зачем-то похитил сына, говоря про какой-то ритуал. Ты не знаешь, где их... логово?" );
            Console.WriteLine(
                "  Вальтер(Сюжетный квест!): Давай договоримся. Скажем, услуга за услугу. Доберись до Пещер Смерти, что в горах Орбах. Там обитает демон. Демон-Принц, если быть точнее. Он и его прихвостни уже давно досаждают нам. Именно из-за них появились шпионы. Убей его для нас, а я дам тебе наводку. Идёт? 1)Только ради семьи. Шантажист хренов. 2)Идёт. (Выберите ответ): " );
            String answer5 = Console.ReadLine();
            switch ( answer5 )
            {
                case "1":
                    Peasant.Karma--;
                    Console.WriteLine(
                        "  Вальтер(Сюжетный квест!): Иди уже. И что б не возвращался, пока эта тварь не сдохнет. Или пока не сдохнешь ты. (Начислена отрицательная карма) " +
                        Peasant.Karma );
                    break;
                case "2":
                    Peasant.Karma++;
                    Console.WriteLine(
                        "  Вальтер(Сюжетный квест!): Ну, в добрый путь. Удачи. Только пока не убьёшь тварь, не возвращайся. (Начислена положительная карма) " +
                        Peasant.Karma );
                    break;
            }
            Console.WriteLine( "Выполнен квест: В путь!" );
            Peasant.EXP = Peasant.EXP + 20;
            Console.WriteLine( "Вы получили 20 ед. опыта. Опыт: " + Peasant.EXP );
            Console.WriteLine( "Новый квест: Услуга за услугу." );
            Console.Write(
                "  Вы вышли из деревни и прошли к развилке. Развилка переходит в три тропы, но путь направо закрыт магическим барьером, так что варианта только два:  1)Вперёд. 2)Налево. :" );
            String choice2 = Console.ReadLine();
            switch ( choice2 )
            {
                case "1":
                    Console.WriteLine(
                        "  Пройдя пару шагов, вы нашли лишь тупик в виде гор и какие-то вилы. Вилы вы взяли себе в качстве оружия." );
                    Peasant.CurrentWeapon = Pitchfork;
                    Console.WriteLine( "Текущий урон: " + Peasant.SharedDmg );
                    Console.WriteLine( "  Вы вернулись на развилку" );
                    break;
                case "2":
                    Console.WriteLine(
                        "  Когда вы прошли немного дальше по тропе, проход сзади закрыл магический барьер. Когда вы ринулись назад, вы поняли, что пути назад нет. Вы отправились навстречу неизвестности." );
                    Console.WriteLine(
                        "  Пока вы шли, вы заметили, что местность резко стала меняться. Позже вы обнаружили разлом между Нирном и Тьмой. Из разлома буквально сочилась энергия Тьмы. Вы решили вернуться сюда позже. Вы пошли дальше по тропе." );
                    Console.WriteLine(
                        "  Пройдя пару шагов, вы нашли лишь тупик в виде гор и какие-то вилы. Вилы вы взяли себе в качстве оружия." );
                    Peasant.CurrentWeapon = Pitchfork;
                    Console.WriteLine( "Текущий урон: " + Peasant.SharedDmg );
                    Console.WriteLine( "  Вы вернулись на развилку по тропе." );
                    break;
            }
            Console.WriteLine( "  Барьер исчез, а вам открылся путь." );
            Console.ReadLine();
        }
    }
}

