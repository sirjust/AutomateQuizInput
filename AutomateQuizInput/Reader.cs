using System.Collections.Generic;
using System.IO;

namespace AutomateQuizInput
{
    public class Reader : IReader
    {
        public IEnumerable<string> ReadDocument(string path) => (File.ReadAllLines(path));
    }
}
