using Mem.Models;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mem.Services
{
    public class NoteService
    {

        private NoteModel[] mock = new NoteModel[]
            {
                new NoteModel() {ID=1, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Arancione, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=2, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Nessuno, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=3, Customer="NO MAC", CustomerCleaned="NOMAC", Classification=NoteClassification.Blu, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=4, Customer="NO MAC", CustomerCleaned="NOMAC", Classification=NoteClassification.Blu, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=5, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Nessuno, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=6, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Blu, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=7, Customer="NO MAC", CustomerCleaned="NOMAC", Classification=NoteClassification.Rosso, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=8, Customer="NO MAC",CustomerCleaned="NOMAC", Classification=NoteClassification.Nessuno, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=9, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Nessuno, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=10, Customer="Color8", CustomerCleaned="COLOR8", Classification=NoteClassification.Nessuno, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=11, Customer="Color8", CustomerCleaned="COLOR8", Classification=NoteClassification.Giallo, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=12, Customer="Color8", CustomerCleaned="COLOR8", Classification=NoteClassification.Verde, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=13, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Arancione, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=14, Customer="JoJo", CustomerCleaned="JOJO", Classification=NoteClassification.Giallo, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=15, Customer="JoJo", CustomerCleaned="JOJO", Classification=NoteClassification.Verde, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=16, Customer="JoJo", CustomerCleaned="JOJO", Classification=NoteClassification.Giallo, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=17, Customer="JoJo", CustomerCleaned="JOJO", Classification=NoteClassification.Viola, DateCreated=DateTime.Now, Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."}
            };

        internal bool DeleteNote(int id)
        {
            return true;
        }

        public NoteModel[] GetNotes(string search)
        {
            var _search = CleanSearchString(search);


            /* MOCK */
            if (string.IsNullOrEmpty(_search))
                return mock;
            else
                return mock.Where(x => x.CustomerCleaned == _search).ToArray();

        }

        public NoteModel GetNote(int id)
        {
            return mock.First(x => x.ID == id);
        }

        public bool SetNote(NoteModel note)
        {
            return true;
        }

        public (String, String)[] GetSuggestions(string search)
        {
            var s = CleanSearchString(search);

            /* MOCK */
            return mock.Distinct(new ByCustomer())
                .Select(x => (x.Customer, x.CustomerCleaned))
                .Where(x => x.ToTuple().Item2.Contains(s))
                .ToArray();
        }

        private const string ESCAPE = "[^a-zA-Z0-9]";

        private string CleanSearchString(string search)
        {
            var s1 = (String.IsNullOrEmpty(search) ? "" : search)
                .Replace('ò', 'o')
                .Replace('à', 'a')
                .Replace('ù', 'u')
                .Replace('è', 'e')
                .Replace('é', 'e')
                .Replace('ì', 'i');

            return Regex.Replace(s1, ESCAPE, String.Empty).ToUpperInvariant();
        }

        
    }
}
