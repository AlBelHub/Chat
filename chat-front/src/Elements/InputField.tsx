import { HubConnection } from "@microsoft/signalr";
import React, { FormEvent } from "react";
import { useEffect } from "react";
import { Message } from "../Types/types";

interface InputProps {
  chatId: string;
  connection: HubConnection;
}

export default function InputField({ chatId, connection }: InputProps) {
  const userId = "5064817829";

  const SendButtonClick = async () => {
    const inputElement: HTMLInputElement | null =
      document.getElementById("input");
    console.log(inputElement?.value);

    fetch(
      `http://localhost:5174/Chat/addMsg?chatId=${chatId}&userId=${userId}`,
      {
        method: "POST",
        body: `"${inputElement?.value}"`,
        headers: {
          "Content-Type": "application/json;charset=utf-8",
        },
      }
    )
      .then((resp) => console.log(resp))
      .catch((err) => console.log(err));

    inputElement.value = "";
  };

  useEffect(() => {
    const OnInputClick = (e: KeyboardEvent) => {
      if (e.code === "Enter" || e.code === "NumpadEnter") {
        e.preventDefault();

        fetch(
          `http://localhost:5174/Chat/addMsg?chatId=${chatId}&userId=${userId}`,
          {
            method: "POST",
            body: `"${e.target.value}"`,
            headers: {
              "Content-Type": "application/json;charset=utf-8",
            },
          }
        )
          .then((resp) => console.log(resp))
          .catch((err) => console.log(err));

        // connection.send("ReceiveMsg", e.target.value).then(e => console.log("INPUT: " + e));

        connection.invoke("SendMessage",
          {
            userId: userId,
            body: e.target.value
          }
        )
        .then(() => console.log("Message sent"))
        .catch((err) => console.error("Error sending message: " + err));

        e.target.value = "";
      }
    };
    document.addEventListener("keydown", OnInputClick);
    return () => {
      document.removeEventListener("keydown", OnInputClick);
    };
  }, [chatId]);

  return (
    <div className="input-wrapper">
      <div className="input">
        <input type="input" className="input_element" id="input" />
      </div>
      <div className="input_send-button" onClick={() => SendButtonClick()} />
    </div>
  );
}
