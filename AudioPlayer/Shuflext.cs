using Audioplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public static class ShufleExtension // GenericPlayerHomework
    {
        public static Random rndAA = new Random(); // GenericPlayerHomework
        public static CompareHelper Comp = new CompareHelper(); // GenericPlayerHomework
        public static List<T> ExtenShufle<T>(this List<T> list) where T:ItemPlaying // GenericPlayerHomework
        {

            List<T> newList = new List<T>(); // GenericPlayerHomework

            for (int i = 0; i < list.Count + 1000; i++) // GenericPlayerHomework
            {
                int index = rndAA.Next(0, list.Count); // GenericPlayerHomework
                if (!newList.Contains(list[index])) // GenericPlayerHomework
                {
                    newList.Add(list[index]); // GenericPlayerHomework
                }
                else if (newList.Contains(list[index])) // GenericPlayerHomework
                {
                    continue;
                }
            }
            return newList; // GenericPlayerHomework
        }
        public static List<T> ExtenSortByTitle<T>(this List<T> oldList, GenericPlayer<T> player) where T: ItemPlaying // GenericPlayerHomework
        {
            List<T> sortedSongsList = new List<T>(); // GenericPlayerHomework
            List<string> titleList = new List<string>(); // GenericPlayerHomework
            foreach (T item in oldList) // GenericPlayerHomework
            {
                titleList.Add(item.Title); // GenericPlayerHomework
            }
            titleList.Sort(Comp); // GenericPlayerHomework
            foreach (string item in titleList) { player.SkinForm.Render("rrr  " + item); } //WriteLine("rrr  " + item)
            for (int i = 0; i < titleList.Count; i++) // GenericPlayerHomework
            {
                foreach (T t in oldList) // GenericPlayerHomework
                {
                    if (titleList[i] == t.Title) // GenericPlayerHomework
                    {
                        sortedSongsList.Add(t); // GenericPlayerHomework
                    }
                    else if (titleList[i] != t.Title) // GenericPlayerHomework
                    {
                        continue; // GenericPlayerHomework
                    }
                }
            }
            return sortedSongsList; // GenericPlayerHomework
        }
    }
}
