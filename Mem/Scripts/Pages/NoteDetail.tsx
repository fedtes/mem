import * as React from "react";
import { useParams } from "react-router";
import Page from "./Page";
import { Toolbar } from "../Component/Toolbar";
import { TagField } from "../Component/TagField";
import { useAPI } from "../APIProvider";

interface IDetailState {
    loadingId: number,
    id: number,
    customer: string,
    text:string
}

export function NoteDetail() {
    const { id } = useParams();
    const [state, setState] = React.useState<IDetailState>({ loadingId: id, id: null, customer: null, text: null });
    const api = useAPI();

    if (state.loadingId) {
        api.getNote(state.loadingId)
            .then(v => setState({ ...v, loadingId: null }));
        return (<div>Caricamento...</div>);
    }
    else {
        return (
            <Page>
                <div className="container">
                    <div className="row">
                        <div className="col app-toolbar-wrapper">
                            <Toolbar
                                leftCmd={<div className="nav-link icon-ico-back"></div>}
                                righCmd={<div className="nav-link icon-ico-delete"></div>}
                            />
                        </div>
                    </div>
                    <div className="row">
                        <div className="col">
                            <TagField label="Customer"></TagField>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col">
                            <textarea className="form-control"></textarea>
                        </div>
                    </div>
                </div>
            </Page>
        );
    }
}