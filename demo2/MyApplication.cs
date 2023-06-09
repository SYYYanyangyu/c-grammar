using System;
using System.Collections.Generic;
using System.Text;

namespace demo2
{
    public class MyApplication
    {
        private EmailService emailService = new EmailService();

        public void ProcessMessage (string message , string receive)
        {
            this.emailService.SendMessage (message , receive);
        } 
    }
}
