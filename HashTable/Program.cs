using System;

namespace HashTable
{
    class Item
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool isDeleted;
        public Item(string key, string value)
        {
            Key = key;
            Value = value;
            isDeleted = false;
        }
        public override string ToString()
        {
            return $"{Key} - {Value}";
        }
    }
    class HashTable
    {
        int startCapacity = 4; 
        int currentCapacity;
        int size;
        double loadFactor = 0.75;
        Item[] KeyValuePairs;
        Item[] Buffer;
        public HashTable()
        {
            currentCapacity = startCapacity;
            KeyValuePairs = new Item[startCapacity];
        }
        public void PrintTable()
        {
            for (int i = 0; i < KeyValuePairs.Length; i++)
            {
               if(KeyValuePairs[i]!=null && KeyValuePairs[i].isDeleted != true) Console.WriteLine($"{i} - {KeyValuePairs[i]}");
            }
        }
        private void ReHash()
        {
            size = 0;
            currentCapacity *= 2;
            Buffer = new Item[currentCapacity];
            for (int i = 0; i < KeyValuePairs.Length; i++)
            {
                if (KeyValuePairs[i] == null || KeyValuePairs[i].isDeleted == true) continue;
                Add(KeyValuePairs[i]);
            }
            KeyValuePairs = new Item[currentCapacity];
            for (int i = 0; i < currentCapacity; i++)
            {
                if (Buffer[i] != null) KeyValuePairs[i] = Buffer[i];
            }
            Buffer = null;

        }
        private void Add(Item it)
        {
            size++;
            int index = GetHash(it.Key);
            while (Buffer[index] != null)
            {
                if (Buffer[index].isDeleted == true) break;
                index = (index + 1) % currentCapacity;
            }
            Buffer[index] = it;
        }
        private int GetHash(string key)
        {
            int sum = 0;
            for (int i = 0; i < key.Length; i++)
            {
                sum += key[i];
            }
            sum %= currentCapacity;
            return sum;
        }
        public string Find(string key)
        {
            int index = GetHash(key);
            while (key != KeyValuePairs[index].Key)
            {
               index = (index + 1) % currentCapacity;
            }
            if (KeyValuePairs[index].isDeleted != true) return KeyValuePairs[index].Value;
            else throw new ArgumentException("такого элемента нет");

        }
        public void Delete(string key)
        {
            int index = GetHash(key);
            while (true)
            {
                if (KeyValuePairs[index] == null) continue;
                if (KeyValuePairs[index].Key == key && KeyValuePairs[index].isDeleted == false) break;
                index = (index + 1) % currentCapacity;
            }
            KeyValuePairs[index].isDeleted = true;
        }
        public void Add(string key, string value)
        {
            size++;
            if((double)size/currentCapacity> loadFactor) ReHash();

            Item it = new Item(key, value);
            int index = GetHash(key);
            while (KeyValuePairs[index] != null)
            {
                if (KeyValuePairs[index].isDeleted == true) break;
                index = (index + 1) % currentCapacity;
            }
            KeyValuePairs[index] = it;
        }
       
    }
    class Program
    {
        static void Main(string[] args)
        {
            HashTable hashTable = new HashTable();
            hashTable.Add("evgeniy", "+79509216619");
            hashTable.Add("vadim", "+79031234431");
            hashTable.Add("1", "+70344243554");
            hashTable.Add("2", "+4384242889");
            hashTable.Add("3", "+79031112255");
            hashTable.Add("4", "+81593487538");
            hashTable.Add("5", "dadhfds");
            hashTable.Add("6", "dadhfsds");
            hashTable.Add("7", "dadsshfds");
            hashTable.Add("8", "dadds");
            hashTable.Delete("vadim");
            hashTable.Delete("1");
            hashTable.Delete("2");
            hashTable.Delete("3");
            hashTable.Delete("4");
            hashTable.Delete("5");
            hashTable.Delete("6");
            hashTable.Find("3");
            hashTable.PrintTable();
           



          





        }
    }
}
