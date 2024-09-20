export interface Chat {
    chatId: string
    id: string
    ownerId: string
    usersId: string[]
    messages: Message[]
    createdAt: string
    updatedAt: string
  }

  export interface Message {
    messageId: string
    id: string
    userId: string
    body: string
    createdAt: string
    updatedAt: string
  }

  export interface User {
    userID: string
    id: string
    username: string
    password: string
    first_name: string
    last_name: string
    patronymic: string
    createdAt: string
    updatedAt: string
  }
  