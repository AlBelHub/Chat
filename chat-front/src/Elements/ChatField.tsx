import React, { useMemo, useRef } from "react";

import { useState, useEffect } from "react";
import { Chat, Message } from "../Types/types";
import "../styles/lay.scss";
import InputField from "./InputField";
import ChatMessage from "./ChatMessage";

import {
  HttpTransportType,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";

export default function ChatField({ chatId }) {
  const [msg, setMsg] = useState<Message[]>();

  const RemoveChat = () => {
    fetch(`http://localhost:5174/Chat/removeChat?chatId=${chatId}`, {
      method: "POST",
    })
      .then((resp) => resp.json())
      .then((res) => console.log("first"));
  };

  useEffect(() => {
    fetch(`http://localhost:5174/Chat/getChatMsgs?chatID=${chatId}`, {
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

  const chatIdMemo = useMemo(() => chatId, [chatId]);
  const msgMemo = useMemo(() => msg, [msg]);

  //signalR

  const connection = new HubConnectionBuilder()
    .withUrl("http://localhost:5174/hub", {
      skipNegotiation: true,
      transport: HttpTransportType.WebSockets,
    })
    .build();

  connection.on("ReceiveMsg", (hmsg) => {
    console.log(hmsg)
    setMsg(
      [
        ...msg,
        {
          body: hmsg
        }
      ]
    )
  });

  connection
    .start()
    .catch((err) => console.log("HUB ERROR: " + err));

  //=======

  return (
    <div className="backgroundImage">
      <div className="chat-container">
        <div className="chat-container_info">
          <p>{chatId}</p>
          <div className="removeButton" onClick={() => RemoveChat()}>
            remove
          </div>
        </div>

        <div className="msgs-container">
          <div className="msgs-container_bounds" ref={divScrollRef}>
            {chatId && (
              <>
                {msgMemo?.map((msgMemo) => (
                  <ChatMessage msg={msgMemo} key={msgMemo.id} />
                ))}
              </>
            )}
          </div>
        </div>

        <InputField chatId={chatIdMemo} connection={connection} />
      </div>
    </div>
  );
}
