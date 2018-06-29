import React, { Component } from 'react';
import PropTypes from 'prop-types';

import { Link } from 'react-router-dom';
import { Menu } from 'antd';
import FontAwesome from 'react-fontawesome'

import styles from '../wwwroot/css/main.css'
const SubMenu = Menu.SubMenu;

class MenuBarComponent extends Component {
    static propTypes = {
        onReload: PropTypes.func,
        data: PropTypes.any
    }

    _loadMenu(menu, i) {
        let menuContents = [];
        if (menu.Children != null && menu.Children.length > 0) {
            let childContents = [];
            for (var j = 0; j < menu.Children.length; j++) {
                let child = menu.Children[j];
                if (child.Children.length > 0) {
                    childContents.push(this._loadMenu(child, j));
                } else {
                    childContents.push(
                        <Menu.Item key={child.ID}>
                            <Link to={child.Url}>{child.Name}</Link>
                        </Menu.Item>
                    )
                }
            }
            menuContents.push(
                // <SubMenu key={menu.ID} title={<span><Icon type={menu.IconName} style={{ color: 'rgba(0,0,0,.25)' }} /><span>{menu.Name}</span></span>}>
                // <SubMenu key={menu.ID} title={<span><i className={`glyphicon ${menu.IconName}`} /><span>{menu.Name}</span></span>}>
                <SubMenu key={menu.ID} title={<span><FontAwesome className={styles.menuIcon} name={menu.IconName} /><span>{menu.Name}</span></span>}>
                    {childContents}
                </SubMenu>
            )
        }
        return menuContents;
    }
    handleClick() {

    }
    render() {
        return (
            <Menu
                onClick={this.handleClick}
                style={{ width: 200 }
                }
                defaultSelectedKeys={['0']}
                defaultOpenKeys={['sub0']}
                mode="inline"
                theme="light"
            >
                {this.props.data.map((menu, i) =>
                    this._loadMenu(menu, i)
                )}
            </Menu>
        );
    }
}

export default MenuBarComponent