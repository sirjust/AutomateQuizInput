using System.Collections.Generic;

namespace AutomateQuizInput
{
    public interface IReader
    {
        IEnumerable<string> ReadDocument(string path);
    }
}