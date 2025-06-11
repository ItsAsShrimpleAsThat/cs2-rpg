using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.csinterop
{
    public struct ChatMessage
    {
        public string author;
        public string message;

        public ChatMessage(string author, string message)
        {
            this.author = author;
            this.message = message;
        }
    }
}
