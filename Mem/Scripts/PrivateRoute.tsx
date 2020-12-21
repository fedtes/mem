import { Route, Redirect, withRouter } from "react-router";
import { APIContext } from "./APIProvider";
import * as React from "react";

class PrivateRoute extends Route {
    static contextType = APIContext

    public constructor(props: any) {
        super(props);
        this.state = { gotoLogin: false };
    }

    async componentDidMount() {
        if (await this.context.ping()) {
            this.setState({ gotoLogin: false });
        } else  {
            this.setState({ gotoLogin: true });
        }
    }

    render() {
        if (this.context.hasLoggedUser()) {
            return super.render();
        } else if (this.state.gotoLogin) {
            return <Redirect to='/login' />;
        } else
            return (<Loader />)
    }
};


function Loader() {
    return (
        <div>I'm loading</div>
    );
};

export default PrivateRoute;