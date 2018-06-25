import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { Button } from 'react-bootstrap';

export default class Login extends Component {
    static propTypes = {
        onSubmit: PropTypes.func,
        onUserNameInputBlur: PropTypes.func
    }

    constructor() {
        super()
        this.state = {
            username: '',
            password: ''
        }
    }

    //元素初始化完成
    componentDidMount() {
        this.username.focus()
    }

    handleUsernameChange(event) {
        this.setState({
            username: event.target.value
        })
    }

    handlePasswordChange(event) {
        this.setState({
            password: event.target.value
        })
    }


    handleSubmit() {
        if (this.props.onSubmit) {
            this.props.onSubmit({
                username: this.state.username,
                password: this.state.password
            })
        }
        this.setState({ username: this.state.username, password: this.state.password })
    }

    render() {
        return (
            <div className="container">
                <div className="form row">
                    <h3 className="form-title">登陆</h3>
                    <div className="center-block">
                        <div className="form-group">
                            <div className="input-group">
                                <div className='input-group-addon'><i className="glyphicon glyphicon-user"></i></div>
                                <input
                                    type="text"
                                    className="form-control required"
                                    placeholder="请输入用户名"
                                    ref={(username) => this.username = username}
                                    value={this.state.username}
                                    onChange={this.handleUsernameChange.bind(this)} />
                            </div>
                        </div>
                        <div className='form-group'>
                            <div className="input-group">
                                <div className='input-group-addon'><i className="glyphicon glyphicon-lock"></i></div>
                                <input
                                    type="password"
                                    className="form-control required"
                                    placeholder="请输入密码"
                                    value={this.state.password}
                                    onChange={this.handlePasswordChange.bind(this)} />
                            </div>
                        </div>
                        <div className="form-group">
                            {/* <button type="submit" class="btn btn-success pull-right" name="submit">登录</button> */}
                            <Button
                                type='submit'
                                bsStyle="info"
                                className="btn btn-success form-control"
                                onClick={this.handleSubmit.bind(this)}>
                                登陆
                                </Button>
                        </div>
                    </div>
                </div>
            </div>
            // <div>
            //     <span className='comment-field-name'>用户名：</span>
            //     <div className='comment-field-input'>
            //         <input
            //             ref={(username) => this.username = username}
            //             value={this.state.username}
            //             onChange={this.handleUsernameChange.bind(this)} />
            //     </div>
            //     <span className='comment-field-name'>密码：</span>
            //     <div className='comment-field-input'>
            //         <input type='password'
            //             value={this.state.password}
            //             onChange={this.handlePasswordChange.bind(this)} />
            //     </div>
            //     <div className='comment-field-button'>
            //         <Button bsStyle="info"
            //             onClick={this.handleSubmit.bind(this)}>
            //             登陆
            //         </Button>
            //         {/* <button
            //             onClick={this.handleSubmit.bind(this)} >
            //             登陆
            //         </button> */}
            //     </div>
            // </div>
        )
    }
}