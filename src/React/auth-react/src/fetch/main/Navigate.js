import { get } from '../get'

const host = 'https://localhost:44338/api/'
export function GetNavigation(){
    const result = get(host+ 'navigation')
    return result;
    // .then(
    //     response => response,
    //     error => error
    // );
}