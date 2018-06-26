import React, { Component } from 'react';
import PropTypes from 'prop-types';

import { Link } from 'react-router-dom';
import { Menu } from 'antd';
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
            for (var i = 0; i < menu.Children.length; i++) {
                let child = menu.Children[i];
                if (child.Children.length > 0) {
                    childContents.push(this._loadMenu(child, i));
                } else {
                    childContents.push(
                        <Menu.Item key={menu.ID}>
                            <Link to={child.Url}>{child.Name}</Link>
                        </Menu.Item>
                    )
                }
            }
            menuContents.push(
                <SubMenu key={menu.ID} title={<span><i className={menu.IconName} /><span>{menu.Name}</span></span>}>
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
            < Menu
                onClick={this.handleClick}
                style={{ width: 200 }
                }
                defaultSelectedKeys={['1']}
                defaultOpenKeys={['sub1']}
                mode="inline"
            >
                {this.props.data.map((menu, i) =>
                    this._loadMenu(menu, i)
                )}
            </Menu>
        );
    }
}

export default MenuBarComponent