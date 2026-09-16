using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CLIN
{
    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPinned { get; set; }

        public Note(string title, string content, bool isPinned = false)
        {
            Title = title;
            Content = content;
            IsPinned = isPinned;
        }
    }

    class Program
    {
        static string homefolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        static string filepath = Path.Combine(homefolder, ".notes.json");
        static List<Note> AllTasks = new List<Note>();
        static List<Note> PinnedTasks = new List<Note>();

        public static void New(string title, string content)
        {
            bool found = false;
            foreach (Note note in AllTasks)
            {
                if (note.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Note with title '{title}' already exists.");
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Note newNote = new Note(title, content);
                AllTasks.Add(newNote);
                Console.WriteLine($"Added Note: {title}");
            }
        }

        public static void Pin(string title)
        {
            bool isFound = false;

            foreach (Note note in AllTasks)
            {
                if (note.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    note.IsPinned = true;

                    if (!PinnedTasks.Contains(note))
                    {
                        PinnedTasks.Add(note);
                    }

                    Console.WriteLine($"Pinned Note: {note.Title}");
                    isFound = true;
                    break;
                }
            }

            if (!isFound)
            {
                Console.WriteLine("A note with that title doesn't exist.");
            }
        }

        public static void View()
        {
            if (AllTasks.Count == 0)
            {
                Console.WriteLine("No notes found.");
                return;
            }

            Console.WriteLine("Here's all your notes:");
            for (int i = 0; i < AllTasks.Count; i++)
            {
                Note task = AllTasks[i];
                string pinMarker = task.IsPinned ? "[PINNED] " : "";
                Console.WriteLine($"{i + 1}. {pinMarker}{task.Title}");
            }
        }

        public static void Open(string title)
        {
            bool found = false;

            foreach (Note note in AllTasks)
            {
                if (note.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Title: {note.Title}\n\nContent: {note.Content}");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Note with that title doesn't exist.");
            }
        }

        public static void Delete(string title)
        {
            bool found = false;

            foreach (Note note in AllTasks)
            {
                if (note.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    AllTasks.Remove(note);
                    PinnedTasks.Remove(note);

                    Console.WriteLine($"'{note.Title}' has been deleted.");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Note with that title doesn't exist.");
            }
        }

        public static void Unpin(string title)
        {
            bool found = false;

            foreach (Note note in AllTasks)
            {
                if (note.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    note.IsPinned = false;
                    PinnedTasks.Remove(note);

                    Console.WriteLine($"'{note.Title}' has been unpinned.");
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Note with that title doesn't exist.");
            }
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
            """);
        }

        

        public static void LoadDataFromDisk()
        {
            if (File.Exists(filepath))
            {
                string rawJson = File.ReadAllText(filepath);
                AllTasks = JsonSerializer.Deserialize<List<Note>>(rawJson) ?? new List<Note>();

                PinnedTasks.Clear();
                foreach (Note note in AllTasks)
                {
                    if (note.IsPinned)
                    {
                        PinnedTasks.Add(note);
                    }
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

            SaveDataToDisk();
        }
    }
}