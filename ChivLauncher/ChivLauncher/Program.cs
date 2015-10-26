// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from ChivLauncher INC. team.
//  
// Copyrights (c) 2014 ChivLauncher INC. All rights reserved.

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

using ChivLauncher.Properties;
// ReSharper disable InvertIf

namespace ChivLauncher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "ChivLauncher";

            if(!string.IsNullOrEmpty(Settings.Default.nickname))
            {
                var valid = false;
                do
                {
                    Console.WriteLine($"Votre pseudo: {Settings.Default.nickname}");
                    Console.Write("Voulez-vous le changer? (o/n) ");
                    var answer = Console.ReadKey();
                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch(answer.Key)
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
                while(!valid);
            }
            else
            {
                ChangeNick();
            }
        }

        private static void RunGame()
        {
            Console.WriteLine("Entrez l'IP du serveur:");
            var IP = Console.ReadLine();
            Process.Start(@"Binaries\Win32\UDK.exe", IP);
        }

        private static void ChangeNick()
        {
            Console.Clear();
            Console.WriteLine("Entrez votre pseudo:");
            var nick = Console.ReadLine();
            Settings.Default.nickname = nick;
            Settings.Default.Save();
            var SettingsFile = File.ReadAllLines(@"UDKGame\Config\DefaultGameUDK.ini");
            if(SettingsFile[17].StartsWith("Name="))
            {
                SettingsFile[17] = "Name=" + nick;
                File.WriteAllLines(@"UDKGame\Config\DefaultGameUDK.ini", SettingsFile);
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