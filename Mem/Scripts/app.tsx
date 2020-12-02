import * as React from 'react'
import {BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import Login from './Login';
import Memos from './Memos';



export default class App extends React.Component {
    render() {
        return (
            <div>
                <span> hello</span>
                <Router>
                    <Switch>
                        <Route path="/login"><Login /></Route>
                        <Route path="/memos"><Memos /></Route>
                    </Switch>
                </Router>
            </div>
        );
    };
}