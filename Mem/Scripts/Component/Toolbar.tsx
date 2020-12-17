import * as React from "react";



export function Toolbar({ leftCmd, righCmd, ...rest }) {
    const render = (cmp, isRight?) => {
        if (cmp)
            return (<ul className={isRight ? 'navbar-nav ml-auto' : 'navbar-nav'}><li className="nav-item ">{cmp}</li></ul>);
        else
            return (<ul className="navbar-nav"></ul>)
    };

    return (
        <nav className="navbar navbar-expand navbar-dark" style={{ "background-color": "#ff5a1d" }}>
            <div className="collapse navbar-collapse">
                {render(leftCmd)}
                {render(righCmd, true)}
            </div>
        </nav>
    );
}