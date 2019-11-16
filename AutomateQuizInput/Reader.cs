using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateQuizInput
{
    public class Reader : IReader
    {
        public IEnumerable<string> ReadDocument(string path) => (File.ReadAllLines(path));
    }
}
