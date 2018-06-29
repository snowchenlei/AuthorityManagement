import React, { Component } from 'react';

import styles from '../index.css';

export default class Footer extends Component{
    render(){
        return (
                // <div className='navbar-fixed-bottom footer-content' style={{ textAlign: 'center', padding: '8px', borderTop: '3px double #e5e5e5', 
                //     marginLeft: '12px', marginRight: '12px', bottom: '0px', left: '200px' }}>
                <div className={`navbar-fixed-bottom ${styles.footerContent}`}>
                    <div style={{ fontSize: '18px' }}>
                        <strong className="text-primary">Snow</strong>
                        &nbsp;权限管理 &copy; 2018-2018
                    </div>
                </div>
        )
    }
}