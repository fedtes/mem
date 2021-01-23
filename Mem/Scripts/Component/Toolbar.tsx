import * as React from "react";



export function Toolbar({ leftCmd, righCmd, midCmd, ...rest }) {
    return (
        <nav className="navbar navbar-expand navbar-dark" style={{ "background-color": "#ff5a1d" }}>
            <div className="collapse navbar-collapse">
                {(leftCmd ? <ul className='navbar-nav'><li className="nav-item ">{leftCmd}</li></ul> : <ul className="navbar-nav"></ul>)}
                {(midCmd ? <ul className='navbar-nav ml-auto mr-auto'><li className="nav-item ">{midCmd}</li></ul> : <ul className="navbar-nav  ml-auto mr-auto"></ul>)}
                {(righCmd ? <ul className='navbar-nav'><li className="nav-item ">{righCmd}</li></ul> : <ul className="navbar-nav"></ul>)}
            </div>
        </nav>
    );
}