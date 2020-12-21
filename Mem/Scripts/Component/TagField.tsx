import * as React from "react";
import { useAPI } from "../APIProvider";

interface IState {
    isDirty?:boolean,
    searchString: string,
    suggestions: string[];
    focused:boolean
}

interface ITagFieldProps {
    onSearchStringChange?: (v: string) => void,
    initialSearch?: string,
    allowAddNew?: boolean,
    label:string
}

export function TagField(props: ITagFieldProps) {
    const textInput = React.useRef(null);
    const [state, setState] = React.useState<IState>({
        searchString: (props.initialSearch ? props.initialSearch : ""),
        suggestions: [],
        focused: false
    });

    const api = useAPI();

    const onInput = (e: React.FormEvent<HTMLInputElement>) => {
        const s = e.currentTarget.value;        
        api.getSuggestion(s)
            .then(r => setState({ ...state, searchString: s, suggestions: r, isDirty: true }));
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

    const shouldShowAdd = () => {
        return state.isDirty && props.allowAddNew && state.searchString.length > 0 &&  state.suggestions.filter(v => v === state.searchString).length === 0;
    }

    return (
        <div className="form-group" style={ {position:"relative"} }>
            <label>{props.label}</label>
            <input ref={textInput} type="text" value={state.searchString} className="form-control" onInput={onInput} onFocus={onFocus} onBlur={onBlur}></input>

            <div className="suggestion-container" style={{display: (state.focused? "unset": "none")}}>
                <div className="container">

                    {state.suggestions.map((s) => (<div className="row" app-value={s} onClick={onItemClick}><div className="col">{s}</div></div>))}

                    <div className="row add-tag" style={{ display: (shouldShowAdd() ? "inherit" : "none") }}>
                        <div className="col"><span>Aggiungi</span></div>
                    </div> 
                </div>
            </div>

        </div>
    );
}