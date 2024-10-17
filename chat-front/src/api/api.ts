import axios, { AxiosPromise, AxiosResponse } from "axios"

const BaseURL = "http://localhost:5154";
const ChatURL = "http://localhost:5154/Chat"
const UserURL = "http://localhost:5154/User"

const getChats = (): Promise<AxiosResponse> => {
    return axios.get(ChatURL + "/getChats");
}

const getUserChats = (userID: string): Promise<AxiosResponse> => {
    return axios.get(ChatURL + `getUserChats?userId=${userID}`);
}

const getUserChats = (userID: string) => {
    axios.get(ChatURL + `getUserChats?userId=${userID}`);
}