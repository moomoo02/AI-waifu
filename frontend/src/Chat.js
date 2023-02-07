import React from 'react'
import styles from "@chatscope/chat-ui-kit-styles/dist/default/styles.min.css";
import {
  MainContainer,
  ChatContainer,
  MessageList,
  Message,
  MessageInput,
} from "@chatscope/chat-ui-kit-react";

function Chat() {
  return (
    <div style={{ position: "relative", height: "100%", width: "100%", top: "0", left: 0 }}>
    <MainContainer>
      <ChatContainer>
        <MessageList>

        </MessageList>
        <MessageInput placeholder="Type message here" />
      </ChatContainer>
    </MainContainer>
    </div>
  )
}

export default Chat