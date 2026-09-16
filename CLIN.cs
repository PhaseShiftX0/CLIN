using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CLIN
{
    public class Note
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPinned { get; set; }

        
        public Note() { }

        public Note(string title, string content, bool isPinned = false)
        {
            Title = title;
            Content = content;
            IsPinned = isPinned;
        }
    }

    class Program
    {
        static string homeFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        static string filepath = Path.Combine(homeFolder, ".clin_notes.json");

        static List<Note> AllTasks = new List<Note>();

        public static void New(string title, string content)
        {
            foreach (Note note in AllTasks)
            {
                if (note.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Note with title '{title}' already exists.");
                    return;
                }
            }

            AllTasks.Add(new Note(title, content));
            Console.WriteLine($"Added Note: {title}");
            SaveDataToDisk();
        }

        public static void Pin(string title)
        {
            foreach (Note note in AllTasks)
            {
                if (note.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    note.IsPinned = true;
                    Console.WriteLine($"Pinned Note: {note.Title}");
                    SaveDataToDisk();
                    return;
                }
            }

            Console.WriteLine("A note with that title doesn't exist.");
        }

        public static void Unpin(string title)
        {
            foreach (Note note in AllTasks)
            {
                if (note.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    note.IsPinned = false;
                    Console.WriteLine($"'{note.Title}' has been unpinned.");
                    SaveDataToDisk();
                    return;
                }
            }

            Console.WriteLine("Note with that title doesn't exist.");
        }

        public static void Delete(string title)
        {
            for (int i = 0; i < AllTasks.Count; i++)
            {
                if (AllTasks[i].Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    string deletedTitle = AllTasks[i].Title;
                    AllTasks.RemoveAt(i);
                    Console.WriteLine($"'{deletedTitle}' has been deleted.");
                    SaveDataToDisk();
                    return;
                }
            }

            Console.WriteLine("Note with that title doesn't exist.");
        }

        public static void View()
        {
            if (AllTasks.Count == 0)
            {
                Console.WriteLine("No notes found.");
                return;
            }

            Console.WriteLine("Here's all your notes:");
            int displayIndex = 1;

            // 1. Display Pinned Notes
            foreach (Note note in AllTasks)
            {
                if (note.IsPinned)
                {
                    Console.WriteLine($"{displayIndex}. [PINNED] {note.Title}");
                    displayIndex++;
                }
            }

            
            foreach (Note note in AllTasks)
            {
                if (!note.IsPinned)
                {
                    Console.WriteLine($"{displayIndex}. {note.Title}");
                    displayIndex++;
                }
            }
        }

        public static void Open(string title)
        {
            foreach (Note note in AllTasks)
            {
                if (note.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Title: {note.Title}\n\nContent: {note.Content}");
                    return;
                }
            }

            Console.WriteLine("Note with that title doesn't exist.");
        }

        public static void Help()
        {
            Console.WriteLine("""
            Thanks for choosing CLIN!
            I hope You have a good experience and give me support on GitHub!

            Usage: CLIN <command> "<title>" "<note>"
            ---
            Available commands:

            CLIN new "<title>" "<note>": create a new note.
            CLIN open "<title>": view note content.
            CLIN view: list all note titles.
            CLIN pin "<title>": pin a note.
            CLIN unpin "<title>": unpin a note.
            CLIN delete "<title>": delete a note.
            CLIN help: display this guide.

            Note that every note should have a different title, regardless of capitalization.
            """);
        }

        public static void LoadDataFromDisk()
        {
            if (File.Exists(filepath))
            {
                string rawJson = File.ReadAllText(filepath);

                if (string.IsNullOrWhiteSpace(rawJson))
                {
                    AllTasks = new List<Note>();
                    return;
                }

                try
                {
                    AllTasks = JsonSerializer.Deserialize<List<Note>>(rawJson) ?? new List<Note>();
                }
                catch (JsonException)
                {
                    AllTasks = new List<Note>();
                }
            }
        }

        public static void SaveDataToDisk()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonText = JsonSerializer.Serialize(AllTasks, options);
            File.WriteAllText(filepath, jsonText);
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Help();
                return;
            }

            LoadDataFromDisk();

            string command = args[0].ToLower();

            if (command == "help")
            {
                Help();
            }
            else if (command == "view")
            {
                View();
            }
            else if (command == "open" && args.Length >= 2)
            {
                Open(args[1]);
            }
            else if (command == "pin" && args.Length >= 2)
            {
                Pin(args[1]);
            }
            else if (command == "unpin" && args.Length >= 2)
            {
                Unpin(args[1]);
            }
            else if (command == "delete" && args.Length >= 2)
            {
                Delete(args[1]);
            }
            else if (command == "new" && args.Length >= 3)
            {
                New(args[1], args[2]);
            }
            else
            {
                Help();
            }
        }
    }
}