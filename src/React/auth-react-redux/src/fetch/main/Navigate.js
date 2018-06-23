import { get } from '../get'

export function GetNavigation(){
    const result = get(host+ 'account');
    return result;
}