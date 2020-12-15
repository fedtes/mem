import * as React from "react";
import { useAPI } from "../APIProvider";

interface IState {
    searchString: string,
    suggestions: string[];
    focused:boolean
}

export function TagField(props: any) {
    const textInput = React.useRef(null);
    const [state, setState] = React.useState<IState>({searchString:"", suggestions:[], focused:false});
    const api = useAPI();

    const onInput = (e: React.FormEvent<HTMLInputElement>) => {
        const s = e.currentTarget.value;
        if (s.length >= 2) {
            api.getSuggestion(s)
                .then(r => setState({ ...state, searchString: s, suggestions: r }));

        } else {
            setState({ ...state, searchString: s });
        }
    };

    const onFocus = () => setState({ ...state, focused: true });

    let timeout = null;

    const onBlur = () => {
        timeout = setTimeout(() => setState({ ...state, focused: false }), 250)
    };

    const onItemClick = (e: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
        if (timeout) clearTimeout(timeout);
        const v = e.currentTarget.getAttribute("app-value");
        setState({ ...state, focused: false, searchString: v });
        if (props.onSearchStringChange) props.onSearchStringChange(v);
    };


    return (
        <div className="form-group" style={ {position:"relative"} }>
            <label>{props.label}</label>
            <input ref={textInput} type="text" value={state.searchString} className="form-control" onInput={onInput} onFocus={onFocus} onBlur={onBlur}></input>

            <div className="suggestion-container" style={{display: (state.focused? "unset": "none")}}>
                <div className="container">
                    <div className="label" style={{ display: (state.suggestions.length !== 0 ? "unset" : "none") }}>forse indendevi...</div>
                    {state.suggestions.map((s) => (<div className="row" app-value={s} onClick={ onItemClick }><div className="col">{s}</div></div>)) }
                </div>
            </div>

        </div>
    );
}