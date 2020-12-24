import * as React from "react";
import { useParams, useHistory } from "react-router";
import Page from "./Page";
import { Toolbar } from "../Component/Toolbar";
import { TagField } from "../Component/TagField";
import { useAPI } from "../APIProvider";
import { Modal } from "../Component/Modal";

interface IDetailState {
    loadingId: number,
    id: number,
    customer: string,
    text: string,
    isDirty: boolean
}

export function NoteDetail() {
    const backModal = React.useRef(null);
    const { id } = useParams();
    const history = useHistory();
    const [state, setState] = React.useState<IDetailState>({
        loadingId: id,
        id: null,
        customer: null,
        text: null,
        isDirty: false
    });

    const api = useAPI();

    const onBackClick = () => {
        if (!state.customer || state.customer === "") {

            const p = new Promise<number>((res, rej) => {

                backModal.current.onclose = res;
                backModal.current.show();

            }).then(id => {
                backModal.current.hide();
                if (id === 1) {
                    api.deleteNote(state.id)
                        .then(() => history.push("/notes"));
                }
            });
            
        } else if (state.isDirty) {
            api.setNote({ id: state.id, customer: state.customer, text: state.text })
                .then(() => history.push("/notes"));
        } else {
            history.push("/notes");
        }
    };

    const onCustomerChange = (value: string) => {
        setState({ ...state, customer: value, isDirty: true })
    };

    const onTextChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        const v = e.currentTarget.value;
        setState({ ...state, text: v, isDirty: true })
    };


    if (state.loadingId) {
        api.getNote(state.loadingId)
            .then(v => setState({ ...v, loadingId: null }));
        return (<div>Caricamento...</div>);
    }
    else {
        return (
            <Page>
                <Modal ref={backModal}
                    text = "Nessun cliente inserito per questa nota!!"
                    options={[{ id: 1, label: "Scarta Nota", type: "danger" }, { id: 2, label: "Indietro", type: "light" }]} ></Modal>
                <div className="container">
                    <div className="row">
                        <div className="col app-toolbar-wrapper">
                            <Toolbar
                                leftCmd={<div className="nav-link icon-ico-back" onClick={onBackClick}></div>}
                                righCmd={<div className="nav-link icon-ico-delete"></div>}
                            />
                        </div>
                    </div>
                    <div className="row">
                        <div className="col">
                            <TagField label="Customer" onSearchStringChange={onCustomerChange} allowAddNew={false} initialSearch={state.customer}></TagField>
                        </div>
                    </div>
                    <div className="row app-note-detail">
                        <div className="col">
                            <textarea style={{ height: "100%" }} className="form-control" value={state.text} onChange={onTextChange}></textarea>
                        </div>
                    </div>
                </div>
            </Page>
        );
    }
}