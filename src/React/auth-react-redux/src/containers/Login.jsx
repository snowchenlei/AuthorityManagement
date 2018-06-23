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
        PostLogin(comment.username, comment.password, this.props.history)
        //this.props.history.push('/Home')
    }

    render() {
        return (
            <LoginComponent
                onSubmit={this.handleSubmitLogin.bind(this)} />
        )
    }
}

export default LoginContainer