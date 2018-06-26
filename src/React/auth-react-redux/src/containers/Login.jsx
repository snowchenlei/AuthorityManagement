import React, { Component } from 'react'
import { PostLogin } from '../fetch/acount/Account'

import LoginComponent from '../components/Login'

class LoginContainer extends Component {
    // contextTypes = {
    //     router: React.PropTypes.isRequired
    // } 
    handleSubmitLogin(comment) {
        // 登陆数据的验证
        if (!comment) return
        if (!comment.username) return alert('请输入用户名')
        if (!comment.password) return alert('请输入密码')
        PostLogin(comment.username, comment.password, ).then(response =>
            response.json().then(json => ({ json, response }))
        ).then(({ json, response }) => {
            if(json.State > 0){            
                console.log(json.Message)
                localStorage.setItem("user", JSON.stringify(json.Data));
                this.props.history.push('/')
            }else{
                console.log(json.Message)
            }
        }).then(
            response => response,
            error => error
        )
    }

    render() {
        return (
            <LoginComponent
                onSubmit={this.handleSubmitLogin.bind(this)} />
        )
    }
}

export default LoginContainer