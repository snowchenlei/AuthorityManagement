import React, { Component } from 'react'
import PropTypes from 'prop-types'

import NormalLoginForm from './NormalLoginForm'
import styles from '../wwwroot/css/login.css'

export default class Login extends Component {
    static propTypes = {
        onSubmit: PropTypes.func
    }

    //元素初始化完成
    componentDidMount() {
        document.body.className = styles.loginBody;
    }

    handleSubmit(comment) {
        if (this.props.onSubmit) {
            this.props.onSubmit(comment)
        }
    }

    render() {
        return (
            <NormalLoginForm
                onSubmit={this.handleSubmit.bind(this)}
            />

            // <div className={styles.bg}>
            // <div className="container">
            //     <div className='form row'>
            //         <h3 className="form-title" style={{ textAlign: 'center' }}>登陆</h3>
            //         <div className="center-block">
            //             <div className="form-group">
            //                 <div className="input-group">
            //                     <div className='input-group-addon'><i className="glyphicon glyphicon-user"></i></div>
            //                     <input
            //                         type="text"
            //                         className="form-control required"
            //                         placeholder="请输入用户名"
            //                         ref={(username) => this.username = username}
            //                         value={this.state.username}
            //                         onChange={this.handleUsernameChange.bind(this)} />
            //                 </div>
            //             </div>
            //             <div className='form-group'>
            //                 <div className="input-group">
            //                     <div className='input-group-addon'><i className="glyphicon glyphicon-lock"></i></div>
            //                     <input
            //                         type="password"
            //                         className="form-control required"
            //                         placeholder="请输入密码"
            //                         value={this.state.password}
            //                         onChange={this.handlePasswordChange.bind(this)} />
            //                 </div>
            //             </div>
            //             <div className="form-group">
            //                 {/* <button type="submit" class="btn btn-success pull-right" name="submit">登录</button> */}
            //                 <Button
            //                     type='submit'
            //                     bsStyle="info"
            //                     className="btn btn-success form-control"
            //                     onClick={this.handleSubmit.bind(this)}
            //                     style={{ marginTop: '10px' }}>
            //                     登陆
            //                 </Button>
            //             </div>
            //         </div>
            //     </div>
            // </div>
            // </div>            
        )
    }
}