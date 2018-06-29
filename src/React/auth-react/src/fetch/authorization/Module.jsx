import { get } from '../get'

const host = 'https://localhost:44338/api/'

export function GetModules(paras){
    const result = get(host+ 'module/?' + paras)
    return result;
}