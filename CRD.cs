/* 
Kod skriven av: Malin Svensson 
Program: Webbutveckling, Mittuniversitetet
Kurs: Programmering i C#.NET 
Skapad: 2020-11-25
*/


// Början av C#-filen 
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

// Namn på arbetet
namespace mom3
{
    // För att skapa
    public class create
    {
        public string name { get; set; }
        public string post { get; set; }
        public void MakeAPost(out string Name, out string Post)
        {
            do
            {
                // Rensa det som stod sedan innan
                Console.Clear();
                // Fråga om vem som ska skriva inlägget
                Console.Write("Ange namn: ");
                // Spara input i "Name" 
                Name = Console.ReadLine();
            }
            // Ej tomt
            while (string.IsNullOrEmpty(Name.Trim()));
            do
            {
                // Fråga om vad som ska stå
                Console.Write("Inlägg: ");
                // Spara värdet i "Post"
                Post = Console.ReadLine();
            } while (string.IsNullOrEmpty(Post.Trim())); 

        }
    }

    // För att radera
    public class DeletePosts
    {
        public void DeletePost()
        {
            // Rensa
            Console.Clear();

            // Radera värdet ställs till true
            var delete = true;
            do{ 
            // Fråga om vilket inlägg som ska raderas
            Console.Write("Inlägg som ska raderas: ");
            
            // Json
            string jsonPath = @"posts.json";
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            
            // Konvertering
            var posts = JsonConvert.DeserializeObject<List<create>>(jsonData)
            ?? new List<create>();

            try
            {
                // För string
                int delIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                // Konvertering
                posts = JsonConvert.DeserializeObject<List<create>>(jsonData)
                ?? new List<create>();

                // För att radera
                posts.RemoveAt(delIndex);

                // Serialisering 
                jsonData = JsonConvert.SerializeObject(posts);
                File.WriteAllText(jsonPath, jsonData);

                // Sätter värdet till false 
                delete = false;
            }
            // Felmeddelande vid fel format
            catch (FormatException)
            {   
                // Rensar konsollen
                Console.Clear();
                // Felmeddelande
                Console.WriteLine("Ange siffra i formatet '1'");
                delete = true;
            }
            // Felmeddelande vid värde som ej finns
             catch (ArgumentOutOfRangeException)
                {
                    Console.Clear();
                    Console.WriteLine("Ange ett värde som finns lagrat");
                    delete = true;
                }
            }while(delete);
     

        }
    }

}
