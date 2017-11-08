using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TryAndDie.Model
{
    public class ItemModel
    {
        public string Category { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public Uri Link { get; set; }
    }
}
