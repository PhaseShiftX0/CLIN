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
        static string filepath = "notes.json";
        static List<Note> AllTasks = new List<Note>();
        static List<Note> PinnedTasks = new List<Note>();

        
        public static void New(string title, string content)
        {
            bool found = false;
            foreach(Note title in AllTasks){
                if(title.Title.Equals(title)){
                    Console.WriteLine($"Note with {title} already exists");
                    found = true;
                    break;
                }
            }
            if(!found){
                Note note = new Note(title, content);
                AllTasks.Add(note.title);
                Console.WriteLine($"Added Note: {title}");
            }
        }
        
        public static void Pin(string title)
        {
         
            bool isFound = false;

            foreach (Note note in AllTasks)
            {
                
                if (note.Title.Equals(title))
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
                Console.WriteLine("A note with that title doesn't exist. Pherhaps you forgot capitalization");
            }
        }
        public static void View()
{
    if (AllTasks.Count == 0)
    {
        Console.WriteLine("No notes found.");
        return;
    }

    
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

        static void Main(string[] args)
        {
            
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: CLIN <command> "\<title>\" "\<note>\"");
                return;
            }

            LoadDataFromDisk();

            string command = args[0].ToLower();
            SaveDataToDisk();
        }
    }
}