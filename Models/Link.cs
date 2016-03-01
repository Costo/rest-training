using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_training.Models
{
    public class Link
    {
        public Link()
        {

        }

        public Link(string href, string rel)
        {
            Href = href;
            Rel = rel;
        }

        public string Href { get; set; }
        public string Rel { get; set; }
    }
}
