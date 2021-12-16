using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

<<<<<<< Updated upstream
namespace Schigebiet.Models {
    public class Message {
        public string Header { get; set; }
        public string MessageText { get; set; }
        public string Solution { get; set; }


        public Message() : this("", "", "") { }

        public Message(string header, string message) : this(header, message, "") { }

        public Message(string header, string message, string solution) {
            this.Header = header;
            this.MessageText = message;
            this.Solution = solution;
=======
namespace Schigebiet.Models
{
    public class Message
    {
        public string MessageText { get; set; }
        public string Header { get; set; }
        public string Solution { get; set; }


        public Message() : this("", "", "")
        {

        }
        public Message(string h, string m) : this(h, m, "")
        {

        }
        public Message(string h, string m, string s)
        {
            this.Header = h;
            this.MessageText = m;
            this.Solution = s;
>>>>>>> Stashed changes
        }
    }
}
