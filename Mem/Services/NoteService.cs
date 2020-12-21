using Mem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mem.Services
{
    public class NoteService
    {

        private NoteModel[] mock = new NoteModel[]
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

        public NoteModel[] GetNotes(string search)
        {
            var _search = search.Trim('"');

            if (string.IsNullOrEmpty(_search))
                return mock;
            else
                return mock.Where(x => x.Customer.ToUpperInvariant() == _search.ToUpperInvariant()).ToArray();

        }

        public NoteModel GetNote(int id)
        {
            return mock.First(x => x.ID == id);
        }

        public bool SetNote(NoteModel note)
        {
            return true;
        }

        public String[] GetSuggestions(string search)
        {
            var _s = String.IsNullOrEmpty(search) ? "" : search;

            var x = new string[]
            {
                "ACME",
                "NOMAC",
                "COLOR8",
                "JOJO"
            };

            return x.Where(y => y.ToUpperInvariant().Contains(_s.ToUpperInvariant())).ToArray();
        }
    }
}
