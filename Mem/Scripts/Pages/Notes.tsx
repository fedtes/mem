import * as React from 'react';
import { APIContext, useAPI } from '../APIProvider';
import Page from './Page';
import { TagField } from '../Component/TagField';
import { useHistory } from 'react-router';
import { Toolbar } from '../Component/Toolbar';
import { func } from 'prop-types';
import { appPath } from '../app';

export default class Notes extends React.Component {

    private noteList: React.RefObject<NoteList>;

    constructor(props: any) {
        super(props);
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
                                leftCmd={<div className="nav-link icon-ico-home"></div>}
                                righCmd={<div className="nav-link icon-ico-filter"></div>}
                            />
                        </div>
                    </div>
                    <div className="row">
                        <div className="col">
                            <TagField onSearchStringChange={ this.onSearchStringChange } label="Search by customer"></TagField>
                        </div>
                    </div>
                    <div className="row app-note-list">
                        <NoteList ref={this.noteList}></NoteList>
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
        this.state = { isLoading: true, items:[], search: "" };
    }

    public setSearch(search: string) {
        this.context.getNotes(search)
            .then(n => this.setState({ isLoading: false, items: n, search: search }));
    }

    public refresh() {
        this.context.getNotes(this.state.search)
            .then(n => this.setState({ isLoading: false, items: n, search: this.state.search }));
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
        <div className="row app-note-list-item" onClick={() => history.push(appPath("/notes/" + id)) }>
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
            .then(id => history.push(appPath("/notes/" + id)));
    };
    return (
        <div className="app-button-new" onClick={onClick}><span className="icon-ico-add"></span></div>
    )
}