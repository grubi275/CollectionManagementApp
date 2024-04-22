using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CollectionManagementApp
{
    public class DataManager
    {
        private const string DataFilePath = @"C:\Users\Filip\OneDrive\Pulpit\Collections.txt"; 
        private static DataManager _instance; 

        public List<Collection> Collections { get; private set; }

       
        private DataManager()
        {
            Collections = LoadCollections();
            Debug.WriteLine(DataFilePath);
        }

        public static DataManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }
            return _instance;
        }

        private List<Collection> LoadCollections()
        {
            if (!File.Exists(DataFilePath))
            {
                File.Create(DataFilePath).Close(); 
            }

            string[] lines = File.ReadAllLines(DataFilePath);
            List<Collection> collections = new List<Collection>();
            Collection currentCollection = null;

            foreach (string line in lines)
            {
                if (line.StartsWith("Collection: "))
                {
                  
                    string collectionName = line.Replace("Collection: ", "");
                    currentCollection = new Collection { Name = collectionName, Items = new List<CollectionItem>() };
                    collections.Add(currentCollection);
                }
                else if (!string.IsNullOrWhiteSpace(line) && currentCollection != null)
                {
                    
                    string[] itemData = line.Split('|');
                    if (itemData.Length == 2)
                    {
                        CollectionItem item = new CollectionItem { Name = itemData[0], Description = itemData[1] };
                        currentCollection.Items.Add(item);
                    }
                }
            }

            return collections;
        }

        
        public void SaveCollections()
        {
            List<string> lines = new List<string>();

            foreach (var collection in Collections)
            {
                lines.Add($"Collection: {collection.Name}");
                foreach (var item in collection.Items)
                {
                   
                    lines.Add($"{item.Name}|{item.Description}");
                }
                lines.Add("");
            }

            File.WriteAllLines(DataFilePath, lines);
        }

       
       
    }
}
