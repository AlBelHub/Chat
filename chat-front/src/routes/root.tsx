import axios from "axios";
import React, { useState, useEffect } from "react";

import { Chat, Message, User } from "../Types/types";

import "../styles/main.css";
import LeftPanel from "../Elements/LeftPanel";
import ChatField from "../Elements/ChatField";

export default function Root() {
  const [users, setUsers] = useState<User[]>();

  useEffect(() => {
    fetch("http://localhost:5154/User/get", {
      method: "GET",
    })
      .then((resp) => resp.json())
      .then((res) => setUsers(res));
  }, []);


  return (
    <>
      <div className="dev-flex-temp">
        <LeftPanel />
      </div>
    </>
  );
}
