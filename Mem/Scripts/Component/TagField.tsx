import * as React from "react";
import { useAPI } from "../APIProvider";

interface IState {
    searchString: string,
    suggestions: string[];
}

export function TagField(props: any) {
    const [state, setState] = React.useState<IState>({searchString:"", suggestions:[]});
    const api = useAPI();

    const onInput = (e:React.FormEvent<HTMLInputElement>) => {
        const s = e.currentTarget.value;
        if (s.length >= 3) {
            api.getSuggestion(s)
                .then(r => setState({ ...state, searchString: s, suggestions: r }));

        } else {
            setState({...state, searchString: s });
        }
    }

    return (
        <div className="form-group">
            <label>{props.label}</label>
            <input type="text" className="form-control" onInput={onInput}></input>

        </div>
    );
}