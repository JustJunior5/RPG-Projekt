using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Xml;
using System.Runtime.ConstrainedExecution;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System;
using System.ComponentModel.Design;

namespace RPG_Projekt
{
    class Ability
    {
        public static Ability Block = new Ability() { Name = "Block", Desc = "If you successfully block the incoming attack,\ndeal 2 damage and stun the enemy for 1 turn!", LvlReq = 1 };
        public static Ability Riskshot = new Ability() { Name = "Riskshot", Desc = "Has a 30% chance of dealing double damage to the enemy,\notherwise, deal no damage!", LvlReq = 1 };
        public static Ability Frostball = new Ability() { Name = "Frostball", Desc = "Shoots a Ball of frost dealing half the damage,\nbut has a 50% chance to stun the opponent for 1 turn!", LvlReq = 1 };

        public static Ability[] k_Abilities = { Block };
        public static Ability[] a_Abilities = { Riskshot };
        public static Ability[] w_Abilities = { Frostball };

        public string Name { get; set; }
        public string Desc { get; set; }
        public int LvlReq { get; set; }
    }
    class Item
    {
        public static Item GoldBundle = new Item() { Name = "Gold Bundle", Description = "Gives you 5 gold, how nice!" };
        public static Item aHealthPotion = new Item() { Name = "Lesser Health Potion", Description = "Heals you 3 health!", Worth = 2};
        public static Item Mallet = new Item() { Name = "Toy Mallet", Description = "Bonk the enemy in order to stun them for 1 turn!\n(Does not stack with other items or abilities!)", Worth = 2};
        public static Item CowardShoes = new Item() { Name = "Coward's Shoes", Description = "Run away from Battle! (only works against normal enemies)", Worth = 5};
        public string Name { get; set; }
        public string Description { get; set; }
        public int Worth {  get; set; }
    }
    class Klass
    {
        public double health { get; set; }
        public int power { get; set; }
        public double armor { get; set; }
        public string Typ { get; set; }
        public static double Attack(string Typ1, int power, string Typ2, double health, double armor)
        {
            Console.WriteLine(Typ1 + " attacks with " + power + " power and " + Typ2 + " has " + armor + " armor!");
            if (power - armor < 0)
            { return 0; }
            else { return power - armor; }
        }
    }
    class Character : Klass
    {
        public int Weapon { get; set; }
        public int chest { get; set; }
        public int Ring { get; set; }
        public string WeaponTyp { get; set; }
        public int maxhp { get; set; }
        public int dhealth { get; set; }
        public int dpower { get; set; }
        public int darmor { get; set; }
        public int lvl { get; set; }
        public int xp { get; set; }
        public double gold { get; set; }
        public string Name { get; set; }
    }
    class Knight : Character
    {
        public Knight()
        {
            WeaponTyp = "Sword";
            dhealth = 10; dpower = 4; darmor = 3; lvl = 1; maxhp = 10;
            health = 10; power = 4; armor = 3; Typ = "Knight";
        }
    }
    class Archer : Character
    {
        public Archer()
        {
            WeaponTyp = "Bow";
            dhealth = 5; dpower = 6; darmor = 2; lvl = 1; maxhp = 5;
            health = 5; power = 6; armor = 2; Typ = "Archer";
        }
    }
    class Wizard : Character
    {
        public Wizard()
        {
            WeaponTyp = "Staff";
            dhealth = 6; dpower = 7; darmor = 1; lvl = 1; maxhp = 6;
            health = 6; power = 7; armor = 1; Typ = "Wizard";
        }
    }
    class Enemy : Klass
    {
        public int xpgain { get; set; }
        public int goldgain { get; set; }
        public Item drop { get; set; }
        public bool stun { get; set; }
    }
    class aLvl : Enemy
    {
        public aLvl()
        {
            Random rand = new Random();
            health = rand.Next(5, 9); // 5-8
            power = rand.Next(2, 6); // 2-5
            armor = rand.Next(0, 3); // 0-2
            xpgain = rand.Next(2, 5); // 2-4
            goldgain = rand.Next(2, 5); // 2-4
            string[] enemyNamn = { "Chicken", "Bat", "Rat", "Bee", "Dragonfly", "Toad" };
            Typ = enemyNamn[rand.Next(0, 6)];
        }
    }
    class bLvl : Enemy
    {
        public bLvl()
        {
            Random rand = new Random();
            health = rand.Next(10, 14); // 10-13
            power = rand.Next(6, 10); // 6-9
            armor = rand.Next(2, 5); // 2-4
            xpgain = rand.Next(5, 8); // 5-7
            goldgain = rand.Next(5, 8); // 5-7
            string[] enemyNamn = { "Slime", "Goblin", "Zombie", "Skeleton", "Snake", "Wasp" };
            Typ = enemyNamn[rand.Next(0, 6)];
        }
    }
    class cLvl : Enemy
    {
        public cLvl()
        {
            Random rand = new Random();
            health = rand.Next(15, 21); // 15-20
            power = rand.Next(10, 13); // 10-12
            armor = rand.Next(4, 7); // 4-6
            xpgain = rand.Next(9, 12); // 9-11
            goldgain = rand.Next(9, 12); // 9-11
            string[] enemyNamn = { "Ogre", "Thief", "Mummy", "Banshee", "Bear", "Imp" };
            Typ = enemyNamn[rand.Next(0, 6)];
        }
    }
    class dLvl : Enemy
    {
        public dLvl()
        {
            Random rand = new Random();
            health = rand.Next(21, 28); // 21-27
            power = rand.Next(13, 17); // 13-16
            armor = rand.Next(6, 11); // 6-10
            xpgain = rand.Next(13, 18); // 13-17
            goldgain = rand.Next(13, 18); // 13-17
            string[] enemyNamn = { "Golem", "Vampire", "Witch", "Minotaur", "Werewolf", "Wraith" };
            Typ = enemyNamn[rand.Next(0, 6)];
        }
    }
    class eLvl : Enemy
    {
        public eLvl()
        {
            Random rand = new Random();
            health = rand.Next(28, 36); // 28- 35
            power = rand.Next(17, 22); // 17-21
            armor = rand.Next(9, 13); // 9-12
            xpgain = rand.Next(18, 26); // 18-25
            goldgain = rand.Next(18, 26); // 18-25
            string[] enemyNamn = { "Demon", "Yeti", "Xantippa", "Cyclops", "Phoenix", "Wyvern" };
            Typ = enemyNamn[rand.Next(0, 6)];
        }
    }

    class Game
    {
        public static void Main(string[] args)
        {
            Random rnd = new Random();
            Console.ForegroundColor = ConsoleColor.White;
            string namn;

            List<Item> Inventory = new List<Item>();
            Character Player = new Character();
            List<Ability> PlayerAbilities = new List<Ability>();
            Console.WriteLine("--- Hello and welcome to RPG World! ---");
            Thread.Sleep(500);
            Console.Write("\nTell me your name adventurer: ");
            while (true)
            {
                Player.Name = Console.ReadLine();
                if (Player.Name == "") { Console.Write("Tell me your name!: "); }
                else { namn = Player.Name; break; }

            }
            Thread.Sleep(500);
            Console.WriteLine("\nNow please pick your starting class!");
            while (true)
            {
                Console.WriteLine("archer | knight | wizard");
                string klassval = Console.ReadLine();
                while (true)
                {
                    if (klassval != "archer" && klassval != "knight" && klassval != "wizard")
                    {
                        Console.WriteLine("Please try again...");
                    }
                    else { break; }
                    klassval = Console.ReadLine();
                }
                if (klassval == "archer") { Player = new Archer(); for (int i = 0; i < Ability.a_Abilities.Length; i++) { PlayerAbilities.Add(Ability.a_Abilities[i]); } }
                if (klassval == "knight") { Player = new Knight(); for (int i = 0; i < Ability.k_Abilities.Length; i++) { PlayerAbilities.Add(Ability.k_Abilities[i]); } }
                if (klassval == "wizard") { Player = new Wizard(); for (int i = 0; i < Ability.w_Abilities.Length; i++) { PlayerAbilities.Add(Ability.w_Abilities[i]); } }
                Player.Name = namn;
                Console.WriteLine("\n Starting stats:\n Health: " + Player.health + "\n Power: " + Player.power + "\n Armor: " + Player.armor);
                Thread.Sleep(1000);
                Console.WriteLine("\nStarting Adventure...");
                Thread.Sleep(2000);
                Console.Clear();
                break;
            }
            while (true)
            {

                Console.WriteLine("You wake up in the middle of a blossoming forest..."); Thread.Sleep(600);
                Console.WriteLine("A dirt path is in front of your eyes, but also a small silhouette."); Thread.Sleep(600);
                Console.Write("You walk closer and closer, until suddenly, "); Thread.Sleep(600);
                Encounter();
                Thread.Sleep(500);
                Console.WriteLine("As you continue down the path after your first encounter,"); Thread.Sleep(600);
                Console.WriteLine("you see a crossroad laying in front of your eyes..."); 
                while (true)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("\nChoose path (Left | Right)");
                    string val = Console.ReadLine();
                    if (val == "Left" || val == "Right")
                    {
                        Console.WriteLine("You head towards the " + val + " side and right in front of your eyes... ");
                        Encounter();
                    } else { Console.WriteLine("Not the right path..."); }
                }
                // Methods
                void Encounter()
                {
                    Thread.Sleep(1000);
                    
                    int val = rnd.Next(1, 2);
                    switch (val)
                    {
                        case 1:
                            Console.WriteLine("an enemy appears!");
                            Thread.Sleep(1000);
                            Battle(Player, EnemyCreate(Player.lvl), Inventory, PlayerAbilities);
                            break;
                        case 2:
                            Console.WriteLine("a lovely merchant appears!");
                            Shop(Player, Inventory);
                            break;
                    }
                }


                static double Battle(Character Player, Enemy Enemy, List<Item> Inventory, List<Ability> PlayerAbilities)
                {
                    Thread.Sleep(500);
                    Console.Clear();
                    Enemy.stun = false;
                    while (true)
                    {
                        while (true)
                        {
                            Console.WriteLine(Enemy.Typ + ": " + "Health = " + Enemy.health + "   Power = " + Enemy.power + "    Armor = " + Enemy.armor + "");
                            Console.WriteLine(Player.Name + ": " + "Health = " + Player.health + "   Power = " + Player.power + "    Armor = " + Player.armor + "");
                            if (Enemy.stun == true) { Console.WriteLine("Enemy is stunned for this turn!"); }
                            Console.WriteLine("\nAbilities = 'a'\nInventory = 'i'\nStats = 's'\nAttack = Enter");
                            string Battleval = Console.ReadLine();

                            if (Battleval == "i")
                            { InventoryCheck(Player, Enemy, Inventory); }

                            else if (Battleval == "s")
                            { Console.Clear(); StatCheck(Player); Console.Clear(); }

                            else if (Battleval == "a")
                            {
                                string ability = Abilities(Player, PlayerAbilities);
                                if (ability != "cancel")
                                {
                                    Thread.Sleep(500); AbilityUse(ability, Enemy, Player); break;
                                }
                                else if (ability == "cancel") { }
                                else { break; }
                            }
                            else { Enemy.health -= Klass.Attack(Player.Name, Player.power, Enemy.Typ, Enemy.health, Enemy.armor); break; }
                        }
                        Console.WriteLine("The " + Enemy.Typ + " has " + Enemy.health + " health left!\n");
                        Thread.Sleep(1000);
                        if (Enemy.health <= 0)
                        {
                            Console.WriteLine("Victory! You have defeated a " + Enemy.Typ + "!");
                            Thread.Sleep(1000);
                            Console.WriteLine("Rewards gained!");
                            Console.WriteLine("  " + Enemy.xpgain + " xp");
                            Thread.Sleep(500);
                            Console.WriteLine("  " + Enemy.goldgain + " gold\n");
                            Player.xp += Enemy.xpgain;
                            if (Player.xp >= (10 * Player.lvl))
                            {
                                Player.xp -= (10 * Player.lvl);
                                Player.lvl += 1;
                                Console.WriteLine("Level Up!\nNew level: " + Player.lvl + "\nAdditional stats gained!\n +2 Max Health\n +1 Power\n +0.5 armor\n");
                                Player.maxhp += 2;
                                Player.power += 1;
                                Player.armor += 0.5;
                            }
                            Thread.Sleep(500);
                            if (Enemy.health < -1) { Console.WriteLine("Overkill Bonus:\n" + (Enemy.health * -1) / 2 + " extra gold gained!"); Thread.Sleep(500); }
                            Random dropper = new Random();
                            int chans = dropper.Next(1, 2);
                            double dropbool = dropper.Next(1, chans);
                            double odds = (110 - (chans * 10));
                            Console.WriteLine("Enemy dropchance = " + odds + "%");
                            Thread.Sleep(1000);
                            if (dropbool == 1)
                            {
                                Console.WriteLine("Item dropped!");
                                Console.WriteLine(Enemy.drop.Name + " added to inventory!");
                                Inventory.Add(Enemy.drop);
                            }
                            else
                            {
                                Console.WriteLine("No item dropped!");
                            }
                            Player.gold += Enemy.goldgain + ((Enemy.health * -1) / 2);
                            Thread.Sleep(1000);
                            Console.WriteLine("Press to continue...");
                            Console.ReadKey(true);
                            Console.Clear();
                            return Player.health;
                        }
                        if (Enemy.stun == false)
                        {
                            Player.health -= Klass.Attack(Enemy.Typ, Enemy.power, Player.Name, Player.health, Player.armor);
                            Console.WriteLine("You have " + Player.health + " health left!");
                        }
                        else { Console.WriteLine("Enemy is stunned!"); Enemy.stun = false; }
                        if (Player.health <= 0)
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("\nI am sorry " + Player.Name + ", but you have been defeated...\n");
                            StatCheck(Player);
                            Thread.Sleep(600);
                            Environment.Exit(0);
                            return 0;
                        }
                        Console.WriteLine("Press to continue...\n");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                }
            }


            static Enemy EnemyCreate(int level)
            {
                Enemy Enemy = new Enemy();
                Item[] aDrops = { Item.aHealthPotion, Item.Mallet, Item.GoldBundle };
                Item[] bDrops = { };
                Item[] cDrops = { };
                Item[] dDrops = { };
                Item[] eDrops = { };
                Random rnd = new Random();
                if (level <= 3)
                { Enemy = new aLvl { drop = aDrops[rnd.Next(0, aDrops.Length)] }; }
                if (level > 3 && level <= 6)
                { Enemy = new bLvl { drop = bDrops[rnd.Next(0, bDrops.Length)] }; }
                if (level > 6 && level <= 9)
                { Enemy = new cLvl { drop = cDrops[rnd.Next(0, cDrops.Length)] }; }
                if (level > 9 && level <= 12)
                { Enemy = new dLvl { drop = dDrops[rnd.Next(0, dDrops.Length)] }; }
                else if (level > 12)
                { Enemy = new eLvl { drop = eDrops[rnd.Next(0, eDrops.Length)] }; }
                return Enemy;
            }


            static void Shop(Character Player, List<Item> Inventory)
            {
                Item[] ShopItems = { Item.aHealthPotion, Item.Mallet };
                Item[] ShopSpecial = { Item.CowardShoes };
                Console.Clear();
                Console.WriteLine("Hello there traveler, Please take a look at my fine wares!"); Thread.Sleep(1000);
                for (int i = 0; i < Player.lvl; i++)
                {
                    Random rnd = new Random();
                    int ShopItem = rnd.Next(0, Player.lvl);

                }
            }


            static void InventoryCheck(Character Player, Enemy enemy, List<Item> Inventory)
            { 
                int q = 0;
                while (q == 0)
                {
                    Console.Clear(); Thread.Sleep(500);
                    if (Inventory.Count == 0)
                    {
                        Console.WriteLine("You have no items in your inventory!");
                        Console.WriteLine("Press to continue...");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("┌───────────────────────────┐");
                        Console.WriteLine("│ All your collected items! │");
                        Console.WriteLine("└───────────────────────────┘");
                        for (int i = 0; i < Inventory.Count; i++)
                        {
                            Console.Write(Inventory[i].Name + " (Value = " + Inventory[i].Worth + "):"); Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine(" " + Inventory[i].Description); Console.ForegroundColor = ConsoleColor.White;
                            Thread.Sleep(300);
                        }
                        Console.WriteLine("\nIn order to use an item, simply type the name of the item!\nIf you want to go back to the Battle menu, write 'cancel'");
                        string InventoryVal = Console.ReadLine();
                        for (int i = 0; i < Inventory.Count; i++)
                        {
                            if (InventoryVal == Inventory[i].Name && Inventory.Contains(Inventory[i]))
                            {
                                Console.WriteLine(Player.Name + " used a " + Inventory[i].Name + "!");
                                ItemUse(Player, Inventory, InventoryVal, enemy);
                                Console.Clear(); q++;
                            }
                            else if (InventoryVal == "cancel") { q++; }
                            else { Console.WriteLine("You do not have that item!"); Thread.Sleep(1000); Console.Clear(); }
                        }  
                    }
                }
            }


            static void ItemUse(Character Player, List<Item> Inventory, string InventoryVal, Enemy enemy)
            {
                Thread.Sleep(500);
                if (InventoryVal == "Health Potion")
                {
                    double hpgammal = Player.health;
                    Player.health += 3;
                    if (Player.health >= Player.maxhp)
                    {
                        Inventory.Remove(Item.aHealthPotion);
                        Player.health = Player.maxhp;
                        Console.WriteLine("Player health increased from " + hpgammal + " -> " + Player.health + "(Max Health!)");
                    }
                    else if (hpgammal == Player.maxhp)
                    {
                        Console.WriteLine("You are already at Max Health!");
                    }
                    else
                    {
                        Inventory.Remove(Item.aHealthPotion);
                        Console.WriteLine("Player health increased from " + hpgammal + " -> " + Player.health + "!\n");
                    }
                    Console.WriteLine("Press to continue..."); Console.ReadKey(true);
                }

                if (InventoryVal == "Toy Mallet")
                {
                    Inventory.Remove(Item.Mallet);
                    Console.WriteLine("You stunned the " + enemy.Typ + "!");
                    enemy.stun = true;
                    Console.WriteLine("Press to continue..."); Console.ReadKey(true);
                }

                if (InventoryVal == "Gold Bundle")
                {
                    Inventory.Remove(Item.GoldBundle);
                    Player.gold += 5;
                    Console.WriteLine("You acquired 5 gold from the bundle!\n");
                    Console.WriteLine("Press to continue..."); Console.ReadKey(true);

                }
            }


            static void StatCheck(Character Player)
            {   Thread.Sleep(300);
                Console.WriteLine(Player.Name + ": " + Player.Typ + " Level " + Player.lvl); Thread.Sleep(300);
                Console.Write("Xp: " + Player.xp); Thread.Sleep(300);
                Console.WriteLine(" Gold: " + Player.gold); Thread.Sleep(300);
                Console.WriteLine("Health: " + Player.health); Thread.Sleep(300);
                Console.WriteLine("Power: " + Player.power); Thread.Sleep(300);
                Console.WriteLine("Armor: " + Player.armor + "\n"); Thread.Sleep(1000);
                /*Console.WriteLine("Equipped items:");
                Console.WriteLine("Weapon: " + );
                Console.WriteLine("Chest armor: " + );
                Console.WriteLine("Ring: "+ );*/

                Console.WriteLine("\nPress to continue...");
                Console.ReadKey(true);
            }


            static string Abilities(Character Player, List<Ability> PlayerAbilities)
            {
                Console.Clear();
                Console.WriteLine("┌─────────────────────────┐");
                Console.WriteLine("│ All acquired abilities! │");
                Console.WriteLine("└─────────────────────────┘");
                Console.WriteLine("Class: " + Player.Typ + "\n");
                Thread.Sleep(600);
                for (int i = 0; i < Player.lvl; i++)
                {
                    Console.Write(PlayerAbilities[i].Name + ": "); Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(PlayerAbilities[i].Desc); Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(300);
                }
                while (true)
                {
                    Console.WriteLine("\nType the name of your abilty in order to use it!\nType 'cancel' if you want to go back to the Battle menu! ");
                    string abilityval = Console.ReadLine();
                    for (int i = 0; i < PlayerAbilities.Count; i++)
                    {
                        if (abilityval == PlayerAbilities[i].Name && Player.lvl >= PlayerAbilities[i].LvlReq)
                        {
                            Console.WriteLine("\n" + Player.Name + " used " + abilityval + "!");
                            return abilityval;
                        }
                    }
                    if (abilityval == "cancel") { Console.Clear(); break; }
                    Console.WriteLine("\nIf that is a spell, you definitely haven't learned that yet.");
                    Thread.Sleep(300);
                }
                return "cancel";
            }


            static void AbilityUse(string ability, Enemy Enemy, Character Player)
            {
                if (ability == "Block")
                {
                    if (Enemy.power <= Player.armor - 1)
                    {
                        Console.WriteLine("Successful stun! " + Player.Name + " dealt 2 damage to the " + Enemy.Typ + "!");
                        Enemy.health -= 2; Enemy.stun = true;
                    }
                    else
                    {
                        Console.WriteLine("Block failed!"); Enemy.stun = false;
                    }
                }
                if (ability == "Riskshot")
                {
                   Random rand = new Random();
                    int riskchans = rand.Next(1, 11);
                    if (riskchans <= 3)
                    {
                       Console.WriteLine("Critical Hit! " + Player.power * 2 + " damage was dealt to the " + Enemy.Typ + "!");
                       Enemy.health -= Player.power * 2;
                    }
                    else { Console.WriteLine(Player.Name + " missed the Riskshot!"); }
                }
                if (ability == "Frostball")
                {
                    Console.WriteLine(Player.power / 2 + " damage was dealt to the " + Enemy.Typ + "!");
                    Enemy.health -= Player.power / 2;
                    Console.Write("Stun: "); Thread.Sleep(500);
                    Random rand = new Random();
                    int riskchans = rand.Next(1, 3);
                    if (riskchans == 1) { Console.WriteLine("Succesful!"); Enemy.stun = true; } else { Console.WriteLine("Failed!"); Enemy.stun = false; }
                }
                Thread.Sleep(800);
            }
        }
    }
}