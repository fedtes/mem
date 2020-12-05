import { Route, Redirect } from "react-router";
import { useAPI } from "./APIProvider";
import * as React from "react";


export function PrivateRoute({ children, ...rest }) {
    const api = useAPI();
    const _render = async ({ location }) => {
        if (await api.ping())
            return children;
        else
            return (<Redirect to={{ pathname: "/login", state: { from: location } }} />)
    }
    return (
        <Route {...rest} render={_render} />
    );
}