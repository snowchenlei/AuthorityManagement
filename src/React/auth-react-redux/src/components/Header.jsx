import React, { Component } from 'react';

import styles from '../wwwroot/css/main.css'


export default class Header extends Component{
    render(){
        console.log(styles.headBody)
        return (
            <div className={styles.headBody}>
                <span>权限管理后台系统</span>
            </div>
        )
    }
}