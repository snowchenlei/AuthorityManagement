import 'whatwg-fetch'
import 'es6-promise'

// function obj2params(obj) {
//   var result = '';
//   var item;
//   for(item in obj){
//     result += '&' + item + '=' + encodeURIComponent(obj[item])
//   }

//   if(result){
//     result = result.slice(1)
//   }
//   return result;
// }

//send post request
export function post(url , paramsObj) {
  return fetch(url,{
    method:'POST',
    //mode:'cors',
    credentials: 'include',
    xhrFields: {
      withCredentials:true //配置http跨域请求中携带cookie
    },
    headers:{
      //'Accept':'application/json, text/plain, */*',
      'Content-Type':'application/json'
    },
    body:JSON.stringify(paramsObj)//obj2params(paramsObj)
  });
}
