using System;
using System.Collections.Generic;
using System.Text;

namespace demo3
{
    public class MessageWriter
    {
        public void Write(string message)
        {
            Console.WriteLine($"MessageWriter.Write(message: \"{message}\")");
        }
    }
}
