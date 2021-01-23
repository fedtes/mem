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

        private static NoteModel[] mock = new NoteModel[]
            {
                new NoteModel() {ID=1, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Arancione, DateCreated=DateTime.Now.AddDays(-2), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=2, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Nessuno, DateCreated=DateTime.Now.AddDays(-2), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=3, Customer="NO MAC", CustomerCleaned="NOMAC", Classification=NoteClassification.Blu, DateCreated=DateTime.Now.AddDays(0), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=4, Customer="NO MAC", CustomerCleaned="NOMAC", Classification=NoteClassification.Blu, DateCreated=DateTime.Now.AddDays(-2), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=5, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Nessuno, DateCreated=DateTime.Now.AddDays(-1), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=6, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Blu, DateCreated=DateTime.Now.AddDays(+1), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=7, Customer="NO MAC", CustomerCleaned="NOMAC", Classification=NoteClassification.Rosso, DateCreated=DateTime.Now.AddDays(0), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=8, Customer="NO MAC",CustomerCleaned="NOMAC", Classification=NoteClassification.Nessuno, DateCreated=DateTime.Now.AddDays(0), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=9, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Nessuno, DateCreated=DateTime.Now.AddDays(-1), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=10, Customer="Color8", CustomerCleaned="COLOR8", Classification=NoteClassification.Nessuno, DateCreated=DateTime.Now.AddDays(-1), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=11, Customer="Color8", CustomerCleaned="COLOR8", Classification=NoteClassification.Giallo, DateCreated=DateTime.Now.AddDays(0), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=12, Customer="Color8", CustomerCleaned="COLOR8", Classification=NoteClassification.Verde, DateCreated=DateTime.Now.AddDays(0), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=13, Customer="A.C.M.E", CustomerCleaned="ACME", Classification=NoteClassification.Arancione, DateCreated=DateTime.Now.AddDays(1), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=14, Customer="JoJo", CustomerCleaned="JOJO", Classification=NoteClassification.Giallo, DateCreated=DateTime.Now.AddDays(0), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=15, Customer="JoJo", CustomerCleaned="JOJO", Classification=NoteClassification.Verde, DateCreated=DateTime.Now.AddDays(-2), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=16, Customer="JoJo", CustomerCleaned="JOJO", Classification=NoteClassification.Giallo, DateCreated=DateTime.Now.AddDays(-4), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=17, Customer="JoJo", CustomerCleaned="JOJO", Classification=NoteClassification.Viola, DateCreated=DateTime.Now.AddDays(-2), Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."}
            };

        internal bool DeleteNote(int id)
        {
            return true;
        }

        public NoteModel[] GetNotes(NoteSearchModel search)
        {
            var _search = CleanSearchString(search.searchString);
            DateTime dateFrom;
            DateTime dateTo;
            switch (search.filterDate.ToUpperInvariant())
            {
                case "TODAY":
                    dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                    break;
                case "YESTERDAY":
                    dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                    dateFrom = dateFrom.AddDays(-1);
                    dateTo = dateTo.AddDays(-1);
                    break;
                case "TOMORROW":
                    dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                    dateFrom = dateFrom.AddDays(1);
                    dateTo = dateTo.AddDays(1);
                    break;
                case "ALL":
                    dateFrom = DateTime.MinValue;
                    dateTo = DateTime.MaxValue;
                    break;
                default:
                    var yyyy = int.Parse(search.filterDate.Replace("-", "").Substring(0, 4)); 
                    var mm = int.Parse(search.filterDate.Replace("-", "").Substring(4, 2)); 
                    var dd = int.Parse(search.filterDate.Replace("-", "").Substring(6, 2));
                    dateFrom = new DateTime(yyyy, mm, dd);
                    dateTo = new DateTime(yyyy, mm, dd, 23, 59, 59);
                    break;
            }

            /* MOCK */
            return mock
                .Where(x => (String.IsNullOrEmpty(_search) || x.CustomerCleaned == _search) && x.DateCreated>=dateFrom && x.DateCreated <= dateTo)
                .ToArray();
        }

        public NoteModel GetNote(int id)
        {
            return mock.First(x => x.ID == id);
        }

        public int SetNote(NoteModel note)
        {
            if (note.ID < 0)
            {
                var n = new NoteModel()
                {
                    Text = note.Text,
                    Customer = note.Customer,
                    CustomerCleaned = CleanSearchString(note.Customer),
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    Classification = note.Classification,
                    ID = mock.Max(x => x.ID) + 1
                };

                mock = mock.Append(n).ToArray();
                return n.ID;
            }
            else
            {
                var n = GetNote(note.ID);
                n.Text = note.Text;
                n.Customer = note.Customer;
                n.CustomerCleaned = CleanSearchString(note.Customer);
                n.Classification = note.Classification;
                n.DateUpdated = DateTime.Now;
                return n.ID;
            }  
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
