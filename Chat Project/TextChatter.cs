using System;

namespace Chat_Project
{
    public class TextChatter : Chatter
    {
        private String name;

        public TextChatter(string name)
        {
            this.name = (String)name;
        }

        public string getAlias()
        {
            return name;
        }
    }
}
