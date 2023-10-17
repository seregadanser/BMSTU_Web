using System;

namespace WebApplication1.Models
{
    public class Pagination<T>
    {
        public int total { get; set; } = 0;
        public int page { get; set; } = 0;
        public int per_page { get; set; } = 0;
        public bool has_next { get; set; } = false;
        public bool has_prev { get; set; } = false;
        public List<T> results { get; set; } = null;
    }
}
