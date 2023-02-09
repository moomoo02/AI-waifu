import React, { useState } from 'react'
import styles from "@chatscope/chat-ui-kit-styles/dist/default/styles.min.css";
import {
  MainContainer,
  ChatContainer,
  MessageList,
  Message,
  MessageInput,
} from "@chatscope/chat-ui-kit-react";
import axios from 'axios';


function Chat() {
    const [messages, setMessages] = useState([]);
    const handleSend = async text => {

        // Logger user (sender)
        // const currentUserId = "123";
    
        // const message = new ChatMessage({
        //   id: "",
        //   content: text,
        //   contentType: MessageContentType.TextHtml,
        //   senderId: currentUserId,
        //   direction: MessageDirection.Outgoing,
        //   status: MessageStatus.Sent
        // });
    
        // sendMessage({
        //   message,
        //   conversationId: activeConversation.id,
        //   senderId: currentUserId,
        // });

        //Render outgoing message on client
        setMessages((currentMessages) => ([...currentMessages, {direction: "outgoing", content: text}]))
        console.log(text + " Was sent");

        //Recieve a response from backend
        const requestUrl = '/text-text/' + text;
        axios.get(requestUrl).then((response) => {
            var data = response.data;
            setMessages((currentMessages) => ([...currentMessages, {direction: data.direction, content: data.content}]));
            // console.log("Now synthesizing to: " + audioFile);
          });
         
    
    
    };

  return (
    <div style={{ position: "relative", height: "100%", width: "100%", top: "0", left: 0 }}>
    <MainContainer>
      <ChatContainer>
        <MessageList>
            {messages.map(m => <Message key={m.id} model={{
              type: "text",
              payload: m.content,
              direction: m.direction,
              
            }} />)}
        </MessageList>
        <MessageInput placeholder="Type message here" onSend={handleSend} />
      </ChatContainer>
    </MainContainer>
    </div>
  )
}

export default Chat