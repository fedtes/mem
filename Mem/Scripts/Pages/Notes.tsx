import * as React from 'react';
import { APIContext } from '../APIProvider';
import Page from './Page';

export default class Notes extends React.Component {

    render() {
        return (
            <Page>
                <Toolbar />
                <div className="container">
                    <div className="row">
                        <div className="col">
                            <div className="form-group">
                                <label>Search by customer</label>
                                <input type="text" className="form-control" id="customer"></input>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <NoteList></NoteList>
                    </div>
                </div>
            </Page>
        );
    };
}

function Toolbar() {
    return (
        <nav className="navbar navbar-expand navbar-dark bg-dark">
            <div className="collapse navbar-collapse">
                <ul className="navbar-nav ml-auto">
                    <li className="nav-item ">
                        <div className="nav-link">Options</div>
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

    public setSearch(search:string) {
        this.setState({ ...this.state, search: search, isLoading: (this.state.search !== search) });
        this.refresh();
    }

    private refresh() {
        this.context.getNotes(this.state.search)
            .then(n => this.setState({ isLoading: false, items: n, search: this.props.search }));
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
                    {this.state.items.map(x => <NoteListItem id={x.id} customer={x.customer} text={x.text} onSelection={this.onSelection} />)}
                </div>
            );
        }
    };
};


function NoteListItem(props:any) {
    const id = props.id;
    return (
        <div className="row" onClick={ props.onSelection(id) }>
            <div className="col">
                <div>{props.customer}</div>
                <div>{props.text}</div>
            </div>
        </div>
    )

}