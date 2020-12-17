import * as React from 'react'
import {BrowserRouter as Router, Switch, Route, Link, Redirect } from "react-router-dom";
import Login from './Pages/Login';
import Notes from './Pages/Notes';
import { API } from './APIProvider';
import { PrivateRoute } from './PrivateRoute';
import { NoteDetail } from './Pages/NoteDetail';


export default class App extends React.Component {
    render() {
        return (
            <API>
                <Router>
                    <Switch>
                        <Route path="/login">
                            <Login />
                        </Route>
                        <PrivateRoute path="/notes/:id">
                            <NoteDetail />
                        </PrivateRoute>
                        <PrivateRoute path="/notes">
                            <Notes />
                        </PrivateRoute>
                        <Route path="/">
                            <Redirect to={{ pathname:"/notes" } } ></Redirect>
                        </Route>
                    </Switch>
                </Router>
            </API>
        );
    };
}