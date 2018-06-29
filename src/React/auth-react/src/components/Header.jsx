import React, { Component } from 'react';
import FontAwesome from 'react-fontawesome'
import styles from '../wwwroot/css/main.css'


export default class Header extends Component{
    render(){
        return (
            <div className={styles.headbar}>
                <a href='/' className={styles.headbarBrand}>
                <div className={styles.headTitle}>
                    <span><FontAwesome className={styles.menuIcon} name='area-chart'/><span>权限管理系统</span></span>
                </div>
                </a>
            </div>
        )
    }
}