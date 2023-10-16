using System;

namespace WebApplication1.Models
{
    public class Pagination<T>
    {
        public int total;
        public int page;
        public int per_page;
        public bool has_next;
        public bool has_prev;
        public List<T> results;
    }
}
