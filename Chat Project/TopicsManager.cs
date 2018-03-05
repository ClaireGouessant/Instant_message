using System.Collections.Generic;

namespace Chat_Project
{
    public interface TopicsManager
    {
        List<string> listTopics();

        TextChatRoom joinTopic(string topic);

        void createTopic(string topic);

        bool IsInside(string s);
    }
}
