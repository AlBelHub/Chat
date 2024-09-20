import React, { useEffect, useRef } from "react";
import { Message } from "../Types/Message";

import "../styles/lay.scss";

interface ChatMessageProps {
  msg: Message;
}

export default function ChatMessage({ msg }: ChatMessageProps) { 

  return (
    <div className="msg-fullWidth">
      <div className="msg">
        <div className="msg_img"></div>
        <div className="msg_container">
          <div className="msg_name">{msg.userId}</div>
          <div className="msg_body">{msg.body}</div>
        </div>
      </div>
    </div>
  );
}
