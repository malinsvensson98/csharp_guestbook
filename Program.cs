/* 
Kod skriven av: Malin Svensson 
Program: Webbutveckling, Mittuniversitetet
Kurs: Programmering i C#.NET 
Skapad: 2020-11-25
*/


// Början av C#-fil
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

// Moment 3
namespace mom3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Boolean som sätts till värdet true
            bool showNav = true;
            while (showNav)
            {
                showNav = MainNav();
            }
        }
        private static bool MainNav()
        {
            // Skriv till användare
            Console.Clear();
            Console.WriteLine("M A L I N S  G Ä S T B O K");
            Console.WriteLine(" ");
            Console.WriteLine("1. Skapa inlägg");
            Console.WriteLine("2. Radera inlägg");
            Console.WriteLine("3. Avsluta");
            Console.WriteLine(" ");

            // Till JSON-filen
            string jsonPath = @"posts.json";
            var jsonData = System.IO.File.ReadAllText(jsonPath);

            // Deserialize
            var posts = JsonConvert.DeserializeObject<List<create>>(jsonData)
            ?? new List<create>();

            // Letar fil
            if (File.Exists(jsonPath))
            {
                int startindex = 1;
                // Värden
                foreach (var post in posts)
                {
                    // Skriver ut namn och inlägg
                    Console.WriteLine($"[{startindex}] {post.name}: {post.post}");
                    startindex++;
                }
            };

            // Switch-sats för att skapa nya inlägg
            switch (Console.ReadLine())
            {
                // Skapa 
                case "1":
                    // Skapar nytt inlägg
                    var newPost = new create(); // Lagrar namn och post
                    newPost.MakeAPost(out string Name, out string Post);
                 
                    // Skapa
                    posts.Add(new create()
                    {
                        name = Name,
                        post = Post,
                    });

                    // Konvertering (serialisering)
                    jsonData = JsonConvert.SerializeObject(posts);
                    File.WriteAllText(jsonPath, jsonData);

                    // Kontrollerar innehåll
                    if (File.Exists(jsonPath))
                    {
                        int startindex = 1;
                        foreach (var post in posts)
                        {
                            Console.WriteLine($"[{startindex}] {post.name}: {post.post}");
                            startindex++;
                        }
                    };
                    return true;



                // Radera inlägg
                case "2":
                var removepost = new DeletePosts();
                removepost.DeletePost();
                    return true;



                // Avsluta 
                case "3":
                    Console.Clear();
                    return false;
                default:
                    return true;
            }

        }

    }
}
