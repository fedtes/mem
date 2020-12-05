﻿import * as $ from 'jquery';
import * as React from "react";
import { createContext } from 'react';

const APIContext = createContext<APIProvider>(null);

type Props = {
    children: React.ReactNode
};

export function useAPI() {
    return React.useContext(APIContext);
}

export function API({ children }: Props) {
    const api = new APIProvider();
    return (
        <APIContext.Provider value={api} >
            { children }
        </APIContext.Provider>
    );
}

export interface IClaim {
    token: string
    loggeduser: string,
    username: string
}

export class APIProvider {

    private origin: string;
    private app_name: string;
    private refresh_url: string = "user/RefreshToken";
    private login_page_url: string = "login";
    private login_url: string = "user/login";
    private ping_url: string = "user/ping";

    public constructor() {
        this.origin = window.location.origin;
    }

    private url(url: string): string { return this.origin + "/" + (this.app_name ? this.app_name + "/" : "") + url;  }


    private claim: IClaim = {
        loggeduser: "",
        token: "",
        username:""
    };

    public async ping() {
        return this.get<boolean>(this.url(this.ping_url), {});
    }

    public login(username, password) {
        const action = new Promise<any>((res, rej) => {
            $.ajax({
                type: "POST",
                url: this.login_url,
                dataType: "json",
                contentType: "application/json; charset=UTF-8",
                data: { usr: username, pwd: password },
                success: function (data: any) {
                    res(data);
                },
                error: function (xhr: any) {
                    rej(xhr);
                }
            });
        });

        return action.then(claim => {
            this.setClaim(claim);
            return "";
        }).catch(x => {
            switch (x.status) {
                case 401:
                    return "Unauthorize";
                default:
                    return "Error";
            }
        });
    };

    private setClaim(claim: any) {
        this.claim = {...claim};
    };

    private get<TResult>(url: string, queryString: any): Promise<TResult> {

        const action = () => {
            return new Promise<TResult>((res, rej) => {
                $.ajax({
                    type: "GET",
                    url: url,
                    dataType: "json",
                    contentType: "application/json; charset=UTF-8",
                    data: queryString,
                    headers: {
                        Authorization: "bearer " + this.claim.token
                    },
                    success: function (data: any) {
                        res(data);
                    },
                    error: function (xhr: any) {
                        rej(xhr);
                    }
                });
            });
        };

        return this.call<TResult>(action);
    };

    private post<TResult>(url: string, params: any): Promise<TResult> {

        const action = () => {
            return new Promise<TResult>((res, rej) => {
                $.ajax({
                    type: "POST",
                    url: url,
                    dataType: "json",
                    contentType: "application/json; charset=UTF-8",
                    data: params,
                    headers: {
                        Authorization: "bearer " + this.claim.token
                    },
                    success: function (data: any) {
                        res(data);
                    },
                    error: function (xhr: any) {
                        rej(xhr);
                    }
                });
            });
        };

        return this.call<TResult>(action);
    };

    private put<TResult>(url: string, params: any): Promise<TResult> {

        const action = () => {
            return new Promise<TResult>((res, rej) => {
                $.ajax({
                    type: "PUT",
                    url: url,
                    dataType: "json",
                    contentType: "application/json; charset=UTF-8",
                    data: params,
                    headers: {
                        Authorization: "bearer " + this.claim.token
                    },
                    success: function (data: any) {
                        res(data);
                    },
                    error: function (xhr: any) {
                        rej(xhr);
                    }
                });
            });
        };

        return this.call<TResult>(action);
    };

    private delete<TResult>(url: string, params: any): Promise<TResult> {

        const action = () => {
            return new Promise<TResult>((res, rej) => {
                $.ajax({
                    type: "DELETE",
                    url: url,
                    dataType: "json",
                    contentType: "application/json; charset=UTF-8",
                    data: params,
                    headers: {
                        Authorization: "bearer " + this.claim.token
                    },
                    success: function (data: any) {
                        res(data);
                    },
                    error: function (xhr: any) {
                        rej(xhr);
                    }
                });
            });
        };

        return this.call<TResult>(action);
    };

    private refresh_token() {
        const action = new Promise<string>((res, rej) => {
            $.ajax({
                type: "POST",
                url: this.refresh_url,
                dataType: "json",
                contentType: "application/json; charset=UTF-8",
                success: function (data: any) {
                    res(data);
                },
                error: function (xhr: any) {
                    rej(xhr);
                }
            });
        });

        return action.then(t => {
            this.claim.token = t;
            this.setClaim(this.claim);
            return "OK";
        }).catch(() => {
            console.error("LOGOUT!!");
            this.setClaim(null);
            window.location.replace(this.login_page_url);
            return "KO";
        });
    };

    private call<TResult>(action: () => Promise<TResult>): Promise<TResult> {
        const onSuccess = (data: TResult) => data;

        const onFailure = async (x: any) => {
            switch (x.status) {
                case 401:

                    if ("OK" === await this.refresh_token()) {
                        return this.call(action);
                    }

                    break;

                default:
                    throw Error("[" + x.status + "] - " + x.responseText);
            }
        };

        return action().then(onSuccess, onFailure);
    };
}