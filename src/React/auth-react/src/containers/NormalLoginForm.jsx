import React from 'react';

import NormalLoginForm  from '../components/NormalLoginForm';
import { PostLogin } from '../fetch/acount/Account'

export default class NormalLoginFormContainer extends React.Component{
    handleSubmitLogin(comment) {
        // 登陆数据的验证
        if (!comment) return
        if (!comment.username) return alert('请输入用户名')
        if (!comment.password) return alert('请输入密码')
        PostLogin(comment.username, comment.password, ).then(response =>
            response.json().then(json => ({ json, response }))
        ).then(({ json, response }) => {
            if (this.props.onSuccess) {
                this.props.onSuccess(json, response);
            }
            // if(json.State > 0){
            //     message.success(json.Message)
            //     localStorage.setItem("user", JSON.stringify(json.Data));
            //     this.props.history.push('/')
            // }else{
            //     message.error(json.Message)
            // }
        }).then(
            response => response,
            error => error
        )
    }

    render() {
        return (
            <NormalLoginForm
                onSubmit={this.handleSubmitLogin.bind(this)} />
        )
    }
}