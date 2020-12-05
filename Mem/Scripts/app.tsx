import * as React from 'react'
import {BrowserRouter as Router, Switch, Route, Link, Redirect } from "react-router-dom";
import Login from './Pages/Login';
import Memos from './Pages/Memos';
import { API } from './APIProvider';
import { PrivateRoute } from './PrivateRoute';


export default class App extends React.Component {
    render() {
        return (
            <API>
                <Router>
                    <Switch>
                        <Route path="/login"><Login /></Route>
                        <PrivateRoute path="/memos"><Memos /></PrivateRoute>
                        <Route path="/">
                            <Redirect to={ { pathname:"/memos" } } ></Redirect>
                        </Route>
                    </Switch>
                </Router>
            </API>
        );
    };
}