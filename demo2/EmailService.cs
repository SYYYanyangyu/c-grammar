using System;
using System.Collections.Generic;
using System.Text;

namespace demo2
{
    public class EmailService
    {
        public void SendMessage(string message , string receive)
        {
            Console.WriteLine($"Email Sent to {receive} with {message}");
        }
    }
}
