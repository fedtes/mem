import * as React from 'react';
import { useAPI } from '../APIProvider';
import { useHistory } from 'react-router';

export default function Login() {
    const [value, setFormValue] = React.useState({username:"", password:""});
    const api = useAPI();
    const history = useHistory();
    const onLoginClick = () => {
        api.login(value.username, value.password)
            .then(r => {
                if ("" === r)
                    history.push("/");
                else
                    $('#login_form > form-group:last-child > div:last-child').css("display", "unset");
            })
    };
    return (
        <div id="login_form" className="mx-auto" style={{ width:"455px" }}>
            <div className="form-group">
                <label>Username</label>
                <input type="text" className="form-control" id="in_username"
                    onInput={e => setFormValue({ ...value, username: e.currentTarget.value  }) } ></input>
            </div>
            <div className="form-group">
                <label >Password</label>
                <input type="password" className="form-control" id="in_password"
                    onInput={e => setFormValue({ ...value, password: e.currentTarget.value, })} ></input>
                <div style={{ display: "none" }}>wrong username or password</div>
            </div>
            <button className="btn btn-primary" onClick={onLoginClick}>Login</button>
        </div>
    );
}