using Microsoft.Xna.Framework;
using NesJamGame.GameContent.Entities;
using NesJamGame.GameContent.Scenes;
using System;
using System.Collections.Generic;

namespace NesJamGame.GameContent
{
    public static class EnemySpawner
    {
        static List<string[]> formations;
        static Random random;

        public static void Initialize()
        {
            random = new Random();
            formations = new List<string[]>()
            {
                new string[]
                {
                    "CCC",
                    "CGC",
                    "SSS"
                },
                new string[]
                {
                    "GGG"
                },
                new string[]
                {
                    "CCC",
                    "CCC",
                    "CCC"
                },
                new string[]
                {
                    "SSSSS"
                },
                new string[]
                {
                    "S S S",
                    " S S "
                },
                new string[]
                {
                    "g",
                    "g",
                    "g"
                },
                new string[]
                {
                    "gg",
                    "ss"
                },
                new string[]
                {
                    "ccc",
                    "c c",
                    "ccc"
                },
                new string[]
                {
                    "C C",
                    " C ",
                    "C C"
                },
                new string[]
                {
                    "CCCCC",
                    "CGCGC",
                    "GSGSG",
                    "SSSSS"
                },
                new string[]
                {
                    "sssss"
                },
                new string[]
                {
                    "ggggg  g",
                    "sssss  s"
                },
                new string[]
                {
                    "G   G",
                    "S   S"
                },
                new string[]
                {
                    "c   c",
                    " c c ",
                    "  c  ",
                    " c c ",
                    "c   c"
                },
            };
        }

        public static void SpawnFormation(Point location, int? formationNum = null)
        {
            if (formationNum == null)
            {
                formationNum = random.Next(0, formations.Count);
            }

            for (int j = 0; j < formations[(int)formationNum].Length; j++)
            {
                for (int i = 0; i < formations[(int)formationNum][j].Length; i++)
                {
                    bool moveIt = char.IsLower(formations[(int)formationNum][j][i]);
                    switch (formations[(int)formationNum][j][i])
                    {
                        case 'c':
                        case 'C':
                            GameScene.AddEntity(new ClassicEnemy(1, location.Y + j*2, location.X + i*2, moveIt));
                            break;

                        case 'g':
                        case 'G':
                            GameScene.AddEntity(new ShootingEnemy(random.NextDouble(), 1, location.Y + j*2, location.X + i*2, moveIt));
                            break;

                        case 's':
                        case 'S':
                            GameScene.AddEntity(new ShieldEnemy(1, location.Y + j*2, location.X + i*2, moveIt));
                            break;
                    }
                }
            }
        }

        public static void SpawnZooming(int xPos, double speed)
        {
            GameScene.AddEntity(new ShieldEnemy(speed, 34, xPos));
        }

        public static void SpawnNonSpecial(int yPos, int enemy)
        {
            switch (enemy)
            {
                case 0:
                    GameScene.AddEntity(new ClassicEnemy(0.5, yPos));
                    break;

                case 1:
                    GameScene.AddEntity(new ShieldEnemy(0.5, yPos));
                    break;

                case 2:
                    GameScene.AddEntity(new ShootingEnemy(random.NextDouble(), 0.5, yPos));
                    break;
            }
        }
    }
}
