import { get } from '../get'

const host = 'http://localhost:44338/api/'
export function GetNavigation(){
    const result = get(host+ 'navigation').then(response =>
        response.json().then(json => ({ json, response }))
    ).then(({ json, response }) => {
        if (!response.ok) {
            if(json.State == 1) {
                
            } else {
                console.log(json.Message)
            }
            return Promise.reject(json);
        }
        return json;
    })
    // .then(
    //     response => response,
    //     error => error
    // );
    return {};
}