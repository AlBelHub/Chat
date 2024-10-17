import React, { useMemo } from "react";

import { useState, useEffect } from "react";
import { Chat } from "../Types/types";
import ChatField from "./ChatField";

import "../styles/lay.scss";

export default function LeftPanel() {
  const [chats, setChats] = useState<Chat[]>();
  const [selectedChat, setSelectedChat] = useState<Chat>();

  const setChat = (chats: Chat[], selectedChat: Chat) => {
    setSelectedChat(chats.filter((e) => e.id === selectedChat?.id)[0]);
  };

  useEffect(() => {
    fetchChats();
  }, []);

  const fetchChats = (): void => {
    fetch("http://localhost:5174/Chat/getChats", {
      method: "GET",
    })
      .then((resp) => resp.json())
      .then((res : Chat[]) => setChats(res));
  };

  const addChat = (ChatName: string): void => {

    if (ChatName == null) {
      ChatName = "Without Name"
    }

    fetch(
      `http://localhost:5174/Chat/createChat?OwnerId=5064817829&chatName=${ChatName}`,
      {
        method: "POST",
      }
    )
      .then((res) => console.log(res.blob()))
      .then((res) => fetchChats());
  };

  return (
    <>
      <div className="left-panel">

      <div className="left-panel_head-content">
      <div className="button" onClick={() => addChat("Created from button")}>
          add chat
        </div>

        <div className="button" onClick={() => console.log("not implemented")}>
          remove all chats
        </div>
      </div>

        <div className="chat-outer">
          {chats &&
            chats.map((chat : Chat) => (
              <div
                className="chat-outer_inner chat-outer_chatElement"
                key={chat.id}
                onClick={() => setChat(chats, chat)}
              >
                <p>{chat.chatName ? chat.chatName : "No name"}</p>
              </div>
            ))}
        </div>
      </div>

      {selectedChat ? (
        <div>
          <ChatField chatId={selectedChat.id} />
        </div>
      ) : (
        <div className="chat-container">EMPTY</div>
      )}
    </>
  );
}
