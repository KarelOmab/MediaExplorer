using MediaInfoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaExplorer
{
    public class Parameter
    {
        public int Index { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public StreamKind StreamKind { get; set; }

        public Parameter(int i, string k, string d, string v, StreamKind sk)
        {
            Index = i;
            Key = k;
            Description = d;
            Value = v;
            StreamKind = sk;
        }
    }
}
