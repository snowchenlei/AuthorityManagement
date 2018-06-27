import { get } from '../get'
import { post } from '../post'

const host = 'https://localhost:44338/api/'
export function PostLogin(username, password) {
    var result = post(host + 'account/login', {
        username: username,
        password: password
    })
    return result;
}

export function GetVerifyCode(){
    const result = get(host+ 'account')
        .then(response =>
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
        }).then(
            response => response,
            error => error
        );
}