using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using Wrappers;
namespace AntiJarCrash
{
    internal class Boot
    {

        private static readonly HttpClient Client = new(new HttpClientHandler
        {
            UseCookies = false,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        });

        private static int consoleTopPosition;

        static Boot()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", $"Mozilla/5.0 ({Utils.RandomString(25)})");
            Client.DefaultRequestHeaders.Host = "vr-jar.github.io";

            consoleTopPosition = Console.CursorTop;
        }


        private static void Thb(bool state)
        {
            string hostsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts");
            string targetDomain = "0.0.0.0 vr-jar.github.io";
            List<string> savedFile = File.ReadAllLines(hostsFilePath).ToList();

            if (state)
            {
                if (savedFile.Contains(targetDomain)) return;
                savedFile.Add(targetDomain);
            }
            else
            {
                if (!savedFile.Contains(targetDomain)) return;
                savedFile.Remove(targetDomain);
            }

            File.WriteAllLines(hostsFilePath, savedFile);
        }

        public static void Main()
        {
            Console.Title = "Anti Fuck Me";
            Thb(true);
            Console.WriteLine("blocked. Press Enter to stop");
            Console.ReadLine();
            Thb(false);
        }

    }
}

