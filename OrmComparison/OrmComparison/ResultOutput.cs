using System;
using System.IO;

namespace OrmComparison
{
    public class ResultOutput : IDisposable
    {
        private readonly FileStream _stream;
        private readonly StreamWriter _writer;
        
        public ResultOutput()
        {
            _stream = new FileStream($"results{DateTime.Now:yyyyMMdd-hhmmss}.csv", FileMode.CreateNew);
            _writer = new StreamWriter(_stream);
        }

        public void Write(string value)
        {
            Console.Write(value);
            _writer.Write(value);
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
            _writer.WriteLine(value);
        }

        public void WriteLine()
        {
            Console.WriteLine();
            _writer.WriteLine();
        }

        public void Dispose()
        {
            _writer.Dispose();
            _stream.Dispose();
        }
    }
}