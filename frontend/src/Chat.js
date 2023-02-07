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
    const [messages, setMessages] = useState([{direction: "outgoing", content: "Decide whether a Tweet's sentiment is positive, neutral, or negative."},
                                              {direction: "outgoing", content: "Tweet: I hated the new Batman movie!"}]);
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
        axios.get('/text-text').then((response) => {
            const dataStr = JSON.stringify(response.data);
            setMessages((currentMessages) => ([...currentMessages, {direction: "incoming", content: dataStr}]));
          })
    
        console.log(text + " Was sent");
    
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