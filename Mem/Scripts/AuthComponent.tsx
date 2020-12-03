import { useContext, createContext, useState } from "react";
import { login, signout } from './authentication';
import * as React from "react";
import { BrowserRouter as Router, Switch, Route, Link, Redirect, useHistory, useLocation } from "react-router-dom";

const authContext = createContext<any>(null);

export interface IClaim {

}

function useProvideAuth() {
    const [claim, setClaim] = useState<IClaim>(null);
    const _signin = (username: string, password: string) => login(username, password).then(v => setClaim(v))

    return {
        claim,
        login: _signin,
        signout
    };
}

export function useAuth() {
    return useContext(authContext);
}

type Props = {
    children: React.ReactNode
};

export function ProvideAuth({ children }: Props) {
    const auth = useProvideAuth();
    return (
        <authContext.Provider value={auth}>
            {children}
        </authContext.Provider>
    );
}


export function PrivateRoute({ children, ...rest }) {
    let auth = useAuth();
    return (
        <Route {...rest} render={({ location }) => auth.user ? ( children ) : ( <Redirect to={{ pathname: "/login", state: { from: location }}}/> )} />
    );
}