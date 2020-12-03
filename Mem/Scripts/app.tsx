import * as React from 'react'
import {BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import Login from './Pages/Login';
import Memos from './Pages/Memos';
import { ProvideAuth, PrivateRoute } from './AuthComponent';



export default class App extends React.Component {
    render() {
        return (
            <ProvideAuth >
                <Router>
                    <Switch>
                        <Route path="/login"><Login /></Route>
                        <PrivateRoute path="/memos"><Memos /></PrivateRoute>
                    </Switch>
                </Router>
            </ ProvideAuth>
        );
    };
}