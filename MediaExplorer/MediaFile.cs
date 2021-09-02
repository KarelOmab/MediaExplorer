using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaExplorer
{
    public class MediaFile
    {
        public string Name { get; set; }
        public List<Parameter> lParams = new List<Parameter>();

        public MediaFile(string n)
        {
            Name = n;
        }
    }
}
