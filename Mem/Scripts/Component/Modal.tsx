import * as React from "react";

/**
 * option type: danger, light (default)
 **/
export interface IModalProps {
    text: string
    options: { id: number, label: string, type?: string }[],
}

interface IModalState {
    visible:boolean
}

export class Modal extends React.Component<IModalProps, IModalState> {

    constructor(props: IModalProps) {
        super(props);
        this.state = { visible: false };
        this.show = this.show.bind(this);
        this.hide = this.hide.bind(this);
    }

    public onclose: (id: number) => void;

    public show() {
        this.setState({ visible: true });
    }

    public hide() {
        this.setState({ visible: false });
    }

    render() {
        return (
            <div className="app-modal" onClick={() => this.onclose(-1)} style={{ display: (this.state.visible ? "inherit" : "none") }}>
                <div className="container">
                    <div className="row">
                        <div className="col">
                            <div className="container app-modal-content">
                                <div className="row app-modal-text">
                                    <div className="col">
                                        <div>{this.props.text}</div>
                                    </div>
                                </div>
                                {
                                    this.props.options.map(v => {
                                        return (<div className="row">
                                            <div className="col">
                                                <button style={{display:"block", width:"100%"}} onClick={() => this.onclose(v.id)} className={"mx-auto btn " + (v.type ? "btn-" + v.type : "btn-light")}>{v.label}</button>
                                            </div>
                                        </div>)
                                    })
                                }
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
       
    };
}