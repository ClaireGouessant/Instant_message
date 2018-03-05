namespace Chat_Project
{
    public interface ChatRoom
    {
        void join(Chatter c);

        string getTopic();

        int count();

        Chatter chatter(int i);
    }
}
