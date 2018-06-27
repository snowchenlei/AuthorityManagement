import React, { Component } from 'react'
import { message } from 'antd';

//import LoginComponent from '../components/Login'
import NormalLoginFormContainer from './NormalLoginForm'
import styles from '../wwwroot/css/login.css'

class LoginContainer extends Component {
    //增加背景
    componentWillMount () {
        document.body.className = styles.loginBody;
    }
    //删除背景
    componentWillUnmount() {
        document.body.className = "";
    }
    handlerSuccess(json, response){
        if(json.State > 0){
            message.success(json.Message)
            localStorage.setItem("user", JSON.stringify(json.Data));
            this.props.history.push('/')
        }else{
            message.error(json.Message)
        }
    }

    render() {
        return (
            <NormalLoginFormContainer 
                onSuccess={this.handlerSuccess.bind(this)}/>
        )
    }
}

export default LoginContainer