# Instant_message
The prototype allows people to speak together. They can create private discussions with many users through different chatrooms.

The chat is implemented as a Client / Server application and used the TCP/IP protocol.

At the beginning of the connection, the users authentificate themselves. They can connect themselves with their account, or if they do not have one, they can create it.

After that, the users can join an existing chatroom, the discussion group of their friends for example. They can also create a new chatroom.

![Alt text|center](https://github.com/ClaireGouessant/Instant_message/blob/master/connection.PNG)

The users are then added to the discussion. They will receive every messages sended in their chatrooms and they will send message to every chatters of the chatrooms.

![Alt text|center](https://github.com/ClaireGouessant/Instant_message/blob/master/Discussion.PNG)

## How to launch the project?
To start the project, in a first time you must start normaly the program (or you can start an instance of the server). In a second time, you have to start as many Client instances as users you want. To open a new client instance in Visual Studio: Solution Explorer >> Right click on Client >> Debug >> New Instance.
![Alt text|center](https://github.com/ClaireGouessant/Instant_message/blob/master/StartClient.png)
