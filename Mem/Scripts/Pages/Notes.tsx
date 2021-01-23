import * as React from 'react';
import { APIContext, useAPI } from '../APIProvider';
import Page from './Page';
import { TagField } from '../Component/TagField';
import { useHistory } from 'react-router';
import { Toolbar } from '../Component/Toolbar';
import { appPath } from '../app';
import * as he from "he";

export default class Notes extends React.Component {

    private noteList: React.RefObject<NoteList>;
    private date: string;


    constructor(props: any) {
        super(props);
        this.date = props.match.params.date.toUpperCase();
        this.onSearchStringChange = this.onSearchStringChange.bind(this);
        this.noteList = React.createRef<NoteList>();
    }

    private onSearchStringChange(searchString:string) {
        this.noteList.current.setSearch(searchString);
    };


    render() {
        return (
            <Page>
                <div className="container">
                    <div className="row">
                        <div className="col app-toolbar-wrapper">
                            <Toolbar
                                leftCmd={<div className="nav-link icon-ico-back"></div>}
                                midCmd={<div className="nav-link">{he.encode(this.date)}</div>}
                                righCmd={<div className="nav-link icon-ico-forward"></div>}
                            />
                        </div>
                    </div>
                    <div className="row">
                        <div className="col">
                            <TagField onSearchStringChange={ this.onSearchStringChange } label="Search by customer"></TagField>
                        </div>
                    </div>
                    <div className="row app-note-list">
                        <NoteList date={this.date} ref={this.noteList}></NoteList>
                        <ButtonNew></ButtonNew>
                    </div>
                </div>
            </Page>
        );
    };
}


class NoteList extends React.Component<any,any> {
    static contextType = APIContext

    constructor(props:any) {
        super(props);
        const date = props.date;
        this.state = { isLoading: true, items: [], searchString: "", filterDate: date };
    }

    public setSearch(search: string) {
        this.context.getNotes({filterDate: this.state.filterDate, searchString: search})
            .then(n => this.setState({ ...this.state, isLoading: false, items: n, searchString: search }));
    }

    public refresh() {
        this.context.getNotes({ filterDate: this.state.filterDate, searchString: this.state.search})
            .then(n => this.setState({ ...this.state, isLoading: false, items: n, searchString: this.state.search }));
    }

    componentDidMount() {
        this.refresh();
    }

    render() {
        if (this.state.isLoading) {
            return <div>Caricamento...</div>
        } else {
            return (
                <div className="container">
                    {this.state.items.map(x => <NoteListItem id={x.id} customer={x.customer} text={x.text} />)}
                </div>
            );
        }
    };
};


function NoteListItem(props: any) {
    const history = useHistory();
    const id = props.id;
    return (
        <div className="row app-note-list-item" onClick={() => history.push(appPath("/notes/detail/" + id)) }>
            <div className="col">
                <div className="app-customer-label">{props.customer}</div>
                <div>{props.text}</div>
            </div>
        </div>
    )

}


function ButtonNew(props: any) {
    const history = useHistory();
    const api = useAPI();
    const onClick = () => {
        api.createNote({ id: -1, customer: "", text: ""})
            .then(id => history.push(appPath("/notes/detail/" + id)));
    };
    return (
        <div className="app-button-new" onClick={onClick}><span className="icon-ico-add"></span></div>
    )
}