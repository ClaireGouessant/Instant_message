using System;
using System.Collections;
using System.Collections.Generic;

namespace Chat_Project
{
    public class TextGestTopics : TopicsManager
    {
        Hashtable TopicChatrooms;

        public TextGestTopics()
        {
            TopicChatrooms = new Hashtable();
        }

        public void createTopic(string topic)
        {
            TopicChatrooms.Add(topic, new TextChatRoom(topic));
        }

        public TextChatRoom joinTopic(string topic)
        {
            if (TopicChatrooms.ContainsKey(topic))
            {
                return (TextChatRoom)TopicChatrooms[topic];
            }
            else
            {
                Console.WriteLine("Topic not found!");
                return null;
            }
        }

        public List<string> listTopics()
        {
            List<string> topics = new List<string>();
            foreach (string topic in TopicChatrooms.Keys)
            {
                topics.Add(topic);
            }
            return topics;
        }

        public bool IsInside(string s) // Chatroom already exist?
        {
            return TopicChatrooms.Contains(s);
        }
    }
}
