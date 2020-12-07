import * as React from 'react';
import { useAPI } from '../APIProvider';
import { useHistory } from 'react-router';

export default function Login() {
    const api = useAPI();
    const history = useHistory();
    return (
        <div>
            login here
            <input type="button" onClick={() => api.ping().then(x => history.push("/"))} />
        </div>
    );
}