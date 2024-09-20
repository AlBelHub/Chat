import React, { useRef } from "react";

import { useState, useEffect } from "react";
import { Chat, Message } from "../Types/types";
import "../styles/lay.scss";
import InputField from "./InputField";
import ChatMessage from "./ChatMessage";

export default function ChatField({ chatId }) {
  const [msg, setMsg] = useState<Message[]>();

  const RemoveChat = () => {
    fetch(`http://localhost:5154/Chat/removeChat?chatId=${chatId}`, {
      method: "POST",
    })
      .then((resp) => resp.json())
      .then((res) => console.log("first"));
  };

  useEffect(() => {
    fetch(`http://localhost:5154/Chat/getChatMsgs?chatID=${chatId}`, {
      method: "GET",
    })
      .then((resp) => resp.json())
      .then((res) => setMsg(res));
  }, [chatId]);

  const divScrollRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    const divScroll: HTMLDivElement | null = divScrollRef.current;
    if (divScroll) {
      divScroll.scrollTop = divScroll.scrollHeight;
    }
  }, [msg]);

  return (
    <div className="backgroundImage">
      <div className="chat-container">
        <div className="chat-container_info">
          <p>{chatId}</p>
          <div className="removeButton" onClick={() => RemoveChat()}>remove</div>
        </div>

        <div className="msgs-container">
          <div className="msgs-container_bounds" ref={divScrollRef}>
            {chatId && (
              <>
                {msg?.map((msg) => (
                  <ChatMessage msg={msg} key={msg.id} />
                ))}
              </>
            )}
          </div>
        </div>

        <InputField chatId={chatId} />
      </div>
    </div>
  );
}
