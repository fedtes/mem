import { Route, Redirect } from "react-router";
import { APIContext } from "./APIProvider";
import * as React from "react";

interface IPrivateRoute {
    gotoLogin:boolean
}

export class PrivateRoute extends React.Component<any,IPrivateRoute> {

    private children: any;
    private rest: any;

    static contextType = APIContext

    public constructor(props:any) {
        super(props);
        this.state = { gotoLogin: false };
        let { children, ...rest } = props;
        this.children = children;
        this.rest = rest;
    }

    async componentDidMount() {
        if (await this.context.ping()) {
            this.setState({ gotoLogin: false });
        } else {
            this.setState({ gotoLogin: true });
        }
    }

    render() {
        if (this.context.hasLoggedUser())
            return (<Route {...this.rest} render={({ location }) => this.children} />)
        else if (this.state.gotoLogin)
            return(<Redirect to={{ pathname: "/login", state: { from: location } }} />)
        else
            return (<Loader />)
    }

}

function Loader() {
    return (
        <div>I'm loading</div>
    );
};