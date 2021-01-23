using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mem.Models
{

    public enum NoteClassification
    {
        Nessuno = 0,
        Arancione = 1,
        Blu = 2,
        Giallo = 3,
        Rosso = 4,
        Verde = 5,
        Viola = 6
    }

    public class NoteModel
    {
        public int ID;
        public string Customer;
        public string CustomerCleaned;
        public DateTime DateCreated;
        public DateTime DateUpdated;
        public NoteClassification Classification;
        public string Text;
    }

    public class ByCustomer : IEqualityComparer<NoteModel>
    {
        public bool Equals(NoteModel x, NoteModel y)
        {
            return x.CustomerCleaned == y.CustomerCleaned;
        }

        public int GetHashCode(NoteModel obj)
        {
            return obj.CustomerCleaned.GetHashCode();
        }
    }

    public class CustomerModel
    {
        public string Customer;
        public string CustomerCleaned;
    }

    public class NoteSearchModel
    {
        public string filterDate { get; set; }
        public string searchString { get; set; }
    }


}
