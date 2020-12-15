import * as React from 'react';
import { APIContext } from '../APIProvider';
import Page from './Page';
import { TagField } from '../Component/TagField';

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
                            <Toolbar />
                        </div>
                    </div>
                    <div className="row">
                        <div className="col">
                            <TagField onSearchStringChange={ this.onSearchStringChange } label="Search by customer"></TagField>
                        </div>
                    </div>
                    <div className="row">
                        <NoteList ref={this.noteList}></NoteList>
                    </div>
                </div>
            </Page>
        );
    };
}

function Toolbar() {
    return (
        <nav className="navbar navbar-expand navbar-dark" style={{ "background-color": "#ff5a1d" }}>
            <div className="collapse navbar-collapse">
                <ul className="navbar-nav">
                    <li className="nav-item ">
                        <div className="nav-link icon-ico-home"></div>
                    </li>
                </ul>
                <ul className="navbar-nav ml-auto">
                    <li className="nav-item ">
                        <div className="nav-link icon-ico-filter"></div>
                    </li>
                </ul>
            </div>
        </nav>
    );
}

class NoteList extends React.Component<any,any> {
    static contextType = APIContext

    constructor(props:any) {
        super(props);
        this.state = { isLoading: true, items:[], search: "" };
    }
  
    private onSelection(id: number) {
        console.debug("Selected " + id);
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
                <div className="container app-note-list">
                    {this.state.items.map(x => <NoteListItem id={x.id} customer={x.customer} text={x.text} onSelection={this.onSelection} />)}
                </div>
            );
        }
    };
};


function NoteListItem(props:any) {
    const id = props.id;
    return (
        <div className="row app-note-list-item"  onClick={ props.onSelection(id) }>
            <div className="col">
                <div className="app-customer-label">{props.customer}</div>
                <div>{props.text}</div>
            </div>
        </div>
    )

}