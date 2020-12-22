import * as React from "react";
import { useAPI } from "../APIProvider";

interface ITag {
    tag: string,
    tagCleaned:string
}

interface IState {
    isDirty?:boolean,
    tag: ITag,
    suggestions: ITag[];
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
        tag: { tag: (props.initialSearch ? props.initialSearch : ""), tagCleaned: "" },
        suggestions: [],
        focused: false
    });

    const api = useAPI();

    const onInput = (e: React.FormEvent<HTMLInputElement>) => {
        const s = e.currentTarget.value;   
        const newTag = { ...state.tag, tag: s };
        api.getSuggestion(s)
            .then(r => setState({ ...state, tag: newTag, suggestions: r, isDirty: true }));
    };

    const onFocus = () => setState({ ...state, focused: true });

    let timeout = null;

    const onBlur = () => {
        const onBlurExec = () => {
            setState({ ...state, focused: false });
            if (props.onSearchStringChange) props.onSearchStringChange(state.tag.tag);
        };
        timeout = setTimeout(onBlurExec, 250)
    };

    const onItemClick = (e: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
        if (timeout) clearTimeout(timeout);
        const v = e.currentTarget.getAttribute("app-value");
        const selection = state.suggestions[parseInt(v)];
        setState({ ...state, focused: false, tag: selection });
        if (props.onSearchStringChange) props.onSearchStringChange(selection.tag);
    };

    const shouldShowAdd = () => {
        return state.isDirty
            && props.allowAddNew
            && state.tag.tag.length > 0
            && state.suggestions.filter(v => v.tagCleaned === state.tag.tagCleaned).length === 0;
    }

    return (
        <div className="form-group" style={ {position:"relative"} }>
            <label>{props.label}</label>
            <input ref={textInput} type="text" value={state.tag.tag} className="form-control" onInput={onInput} onFocus={onFocus} onBlur={onBlur}></input>

            <div className="suggestion-container" style={{display: (state.focused? "unset": "none")}}>
                <div className="container">

                    {state.suggestions.map((s, i) => (<div className="row" app-value={i.toString()} onClick={onItemClick}><div className="col">{s.tag}</div></div>))}

                    <div className="row add-tag" style={{ display: (shouldShowAdd() ? "inherit" : "none") }}>
                        <div className="col"><span>Aggiungi</span></div>
                    </div> 
                </div>
            </div>

        </div>
    );
}