// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from KF2Launcher INC. team.
//  
// Copyrights (c) 2014 KF2Launcher INC. All rights reserved.

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

using KF2Launcher.Properties;
// ReSharper disable InvertIf

namespace KF2Launcher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "KF2Launcher";

            if (!string.IsNullOrEmpty(Settings.Default.nickname))
            {
                var valid = false;
                do
                {
                    Console.WriteLine($"Votre pseudo: {Settings.Default.nickname}");
                    Console.Write("Voulez-vous le changer? (o/n) ");
                    var answer = Console.ReadKey();
                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch (answer.Key)
                    {
                        case ConsoleKey.O:
                            valid = true;
                            ChangeNick();
                            break;
                        case ConsoleKey.N:
                            valid = true;
                            RunGame();
                            break;
                        default:
                            valid = false;
                            Console.WriteLine("Réponse invalide.");
                            Thread.Sleep(1000);
                            Console.Clear();
                            break;
                    }
                }
                while (!valid);
            }
            else
            {
                ChangeNick();
            }
        }

        private static void RunGame()
        {
            Process.Start(@"Binaries\Win64\KFGame.exe");
        }

        private static void ChangeNick()
        {
            Console.Clear();
            Console.WriteLine("Entrez votre pseudo:");
            var nick = Console.ReadLine();
            Settings.Default.nickname = nick;
            Settings.Default.Save();
            var SettingsFile = File.ReadAllLines(@"Binaries\Win64\ALI213.ini");
            if (SettingsFile[12].StartsWith("PlayerName ="))
            {
                SettingsFile[12] = "PlayerName =" + nick;
                File.WriteAllLines(@"Binaries\Win64\ALI213.ini", SettingsFile);
                RunGame();
            }
            else
            {
                Console.WriteLine("Erreur dans le fichier de config.");
                Console.ReadKey();
            }
        }
    }
}