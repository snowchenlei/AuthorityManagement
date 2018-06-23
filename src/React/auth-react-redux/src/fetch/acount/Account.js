import { get } from '../get'
import { post } from '../post'

const host = 'http://localhost:44338/api/'
export function PostLogin(username, password, history) {
    post(host + 'account/login', {
        username: username,
        password: password
    }).then(response =>
        response.json().then(json => ({ json, response }))
    ).then(({ json, response }) => {
        if(json.State > 0){            
            console.log(json.Message)
            history.push('/')
        }else{
            console.log(json.Message)
        }
    }).then(
        response => response,
        error => error
    )
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