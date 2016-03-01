using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_training.Utils
{
    public class IdGenerator
    {
        private readonly IDictionary<string, int> _ids = new Dictionary<string, int>();
        public int GetNextIdFor(string collection)
        {
            if(!_ids.ContainsKey(collection))
            {
                _ids[collection] = 0;
            }

            return ++_ids[collection];
        }
    }
}
