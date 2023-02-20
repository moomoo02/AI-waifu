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
import { API_URL } from "./api";
import ReactGA from "react-ga";

axios.defaults.withCredentials = true;

function Chat({setSpeak, setEmotion}) {
    const [messages, setMessages] = useState([]);
    const handleSend = async text => {

        //Render outgoing message on client
        setMessages((currentMessages) => ([...currentMessages, {direction: "outgoing", content: text}]))
        console.log(text + " Was sent");

        //Recieve a response from backend
        const requestUrl = API_URL + '/text-text/' + text;
        axios.get(requestUrl).then((response) => {
            var data = response.data;
            setMessages((currentMessages) => ([...currentMessages, {direction: data.direction, content: data.content}]));
            setEmotion(data.emotion);
            setSpeak(true);
            console.log(data.emotion);
          });
          

          ReactGA.event({
            category: "Chat",
            action: "Message Sent",
          })
         
    
    
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