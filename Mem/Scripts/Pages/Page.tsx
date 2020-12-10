import * as React from "react";


export default function Page({ children, ...rest }){
    return (
        <div>
            { children }
        </div>    
    );
};

