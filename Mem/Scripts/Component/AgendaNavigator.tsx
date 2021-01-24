import * as React from "react";
import * as DatePicker from "../../node_modules/react-datepicker/dist/index.js";
import { useParams, useHistory } from "react-router";
import { appPath } from "../app";

function addDays(date:Date, days:number) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
}

function stringToDate(dateString: string) {
    switch (dateString.toUpperCase()) {
        case "TODAY":
            return new Date();
            break;
        case "YESTERDAY":
            return addDays(new Date(), -1);
            break;
        case "TOMORROW":
            return addDays(new Date(), 1);
            break;
        case "ALL":
            break;
        default:
            let x = dateString.replace(/-/g, "");
            let yyyy = x.substring(0, 4);
            let mm = x.substring(4, 6);
            let dd = x.substring(6, 8);
            return new Date(parseInt(yyyy), parseInt(mm), parseInt(dd));
    }
}

export function dateToString(date: Date) {
    const str = (d: Date) => {
        let yyyy = "" + d.getFullYear();
        let mm = "" + d.getMonth();
        mm = mm.length === 1 ? "0" + mm : mm;
        let dd = "" + d.getDate();
        dd = dd.length === 1 ? "0" + dd : dd;
        return yyyy + mm + dd;
    };
    let now = new Date();
    const _today = str(now);
    const _tomorrow = str(addDays(now,1));
    const _yesterday = str(addDays(now, -1));
    const _date = str(date);

    if (_today === _date)
        return "TODAY";
    else if (_tomorrow === _date)
        return "TOMORROW";
    else if (_yesterday === _date)
        return "YESTERDAY";
    else
        return _date;
}

export function AgendaNavigator(props: any) {
    const history = useHistory();

    const NavInput:any = ({ value, onClick }) => {
        return (<button onClick={onClick}>{props.datestring}</button>);
    };

    const onChange = (date) => {
        props.dateNavChange(date);
    };

    return (
        <DatePicker.default
            selected={stringToDate(props.datestring)}
            onChange={onChange}
            customInput={<NavInput/> } />
    );
}


