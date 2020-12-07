import { Route, Redirect } from "react-router";
import { useAPI } from "./APIProvider";
import * as React from "react";


export function PrivateRoute({ children, ...rest }) {
    const api = useAPI();
    const _render = ({ location }) => {
        if (api.hasLoggedUser())
            return children;
        else
            return (<Redirect to={{ pathname: "/login", state: { from: location } }} />)
    }
    return (
        <Route {...rest} render={_render} />
    );
}