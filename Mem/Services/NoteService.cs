using Mem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mem.Services
{
    public class NoteService
    {
        public NoteModel[] GetNotes(string search)
        {
            var _search = search.Trim('"');
            var asd = new NoteModel[]
            {
                new NoteModel() {ID=1, Customer="ACME", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=2, Customer="ACME", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=3, Customer="NOMAC", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=4, Customer="NOMAC", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=5, Customer="ACME", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=6, Customer="ACME", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=7, Customer="NOMAC", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=8, Customer="NOMAC", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=9, Customer="ACME", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=10, Customer="COLOR8", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=11, Customer="COLOR8", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=12, Customer="COLOR8", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=13, Customer="ACME", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=14, Customer="JOJO", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=15, Customer="JOJO", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=16, Customer="JOJO", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."},
                new NoteModel() {ID=17, Customer="JOJO", Text="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et purus quam. Nulla quis neque vel nulla egestas lacinia. Quisque sagittis, arcu non molestie blandit."}
            };

            if (string.IsNullOrEmpty(_search))
                return asd;
            else
                return asd.Where(x => x.Customer.ToUpperInvariant() == _search.ToUpperInvariant()).ToArray();

        }

        public String[] GetSuggestions(string search)
        {
            var x = new string[]
            {
                "ACME",
                "NOMAC",
                "COLOR8",
                "JOJO"
            };

            return x.Where(y => y.ToUpperInvariant().Contains(search.ToUpperInvariant())).ToArray();
        }
    }
}
