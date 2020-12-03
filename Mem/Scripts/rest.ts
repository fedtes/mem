import * as $ from 'jquery';


const base_url = window.location.origin;

function compose(controller: string, action: string, id?: string, queryString?: any) {
    let params = null;
    if (queryString) {
        
    } else {

    }
    return base_url + "/" + controller + "/" + action + (id ? "/" + id : "") + (params ? "?" + params : "");
}

interface IHttpResp {
    data: any,
    status: string,
    xhr: XMLHttpRequest
}

export async function AnonymousPost(controller: string, action: string, payload: any) {
    let req = new Promise<IHttpResp>((res) => { $.post(compose(controller, action), payload, res, "json") });
    return await req.then(r => { return { data: r.data, status: r.status }; });
}