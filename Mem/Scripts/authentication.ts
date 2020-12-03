import * as $ from 'jquery';
import { AnonymousPost } from './rest';




export async function login(username:string, password:string) {
    return await AnonymousPost("User", "Login", { usr: username, pwd: password })
        .then(v => {
            if ("success" === v.status) {
                return v.data;
            } else {
                throw new Error("Login failed");
            }
        });
}

export function signout() {
    let x = (res, rej) => {
        setTimeout(res(), 1000);
    };

    return new Promise(x);
}