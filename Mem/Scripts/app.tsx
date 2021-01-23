import * as React from 'react'
import {Router, Switch, Route, Link, Redirect, withRouter } from "react-router-dom";
import Login from './Pages/Login';
import Notes from './Pages/Notes';
import { API } from './APIProvider';
import PrivateRoute from './PrivateRoute';
import { NoteDetail } from './Pages/NoteDetail';
import { createBrowserHistory } from 'history';

const history = createBrowserHistory();

export function appPath(path: string) {
    const root = (window as any).appRoot;
    return (root && root !== "" ? "/" + root + path : path);
};

export default class App extends React.Component {
    render() {
        return (
            <API>
                <Router history={history}>
                    <Switch>
                        <Route path={ appPath("/login")}>
                            <Login />
                        </Route>
                        <PrivateRoute path={ appPath("/notes/detail/:id")}>
                            <NoteDetail />
                        </PrivateRoute>
                        <PrivateRoute path={ appPath("/notes/:date")}>
                            {withRouter(Notes)}
                        </PrivateRoute>
                        <Route path={appPath("/")}>
                            <Redirect to={{ pathname: appPath("/notes/today") } } ></Redirect>
                        </Route>
                    </Switch>
                </Router>
            </API>
        );
    };
}