import React, { Component } from 'react'
import PropTypes from 'prop-types';


export default class Menu extends Component {
    static propTypes = {
        nav: PropTypes.object.isRequired,
        index: PropTypes.number
    }

    _loadList(navs){
        let navContent = [];
        navs.forEach(nav => {
            navContent.push(
                <li className="dropdown active">
                    <a className="dropdown-toggle" href={nav.Url}><i className={nav.IconName} />{nav.Name}</a>
                </li>
            )
            if(nav.Children.length > 0){
                navContent.push(this._loadList(navs.Children))
            }
        });
        return (
            <ul className="dropdown-menu">
                {navContent}
            </ul>
        )
    }

    handlerClick(){

    }

    render() {
        // const nav = this.props.nav;
        // let navContent;
        // if (nav.Children.length > 0) {
        //     navContent = this._loadList(nav.Children)
        // }
        return (
            // <li 
            //     className="dropdown active"
            //     onClick={this.handlerClick.bind(this)}>
            //     <a className="dropdown-toggle" href={nav.Url}><i className={nav.IconName} />{nav.Name}</a>
            //     {navContent}
            // </li>
            <li 
                className="dropdown active"
                onClick={this.handlerClick.bind(this)}>
                <a className="dropdown-toggle">系统管理</a>
                <ul className="dropdown-menu">
                    <li className="dropdown active">
                        <a className="dropdown-toggle">用户管理</a>
                    </li>
                    <li className="dropdown active">
                        <a className="dropdown-toggle">角色管理</a>
                    </li>
                </ul>
            </li>
        )
    }
}