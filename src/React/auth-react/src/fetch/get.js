import 'whatwg-fetch'
import 'es6-promise'

export function get(url){
  return fetch(url,{
    method:'GET',
    credentials:'include',
    //mode: "cros",
    headers:{
      // 'Accept':'application/json, text/plain,*/*'
      'Content-Type':'application/json'
    }
  })
}
