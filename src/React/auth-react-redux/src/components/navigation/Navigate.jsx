import React, { Component, PropTypes } from 'react'

export default class Navigate extends Component {
    // static propTypes = {
    //     nav: PropTypes.object.isRequired,
    //     index: PropTypes.number
    // }

    render() {
        const nav = this.props.nav
        return (
            <li>
                if(nav.Children.length > 0){
                    <a href={nav.Url}><i className={nav.IconName} />{nav.Name}</a>
                }else{
                    <ul className="nav navbar-nav">
                        <li>
                            <a href={nav.Url}><i className={nav.IconName} />{nav.Name}</a>
                        </li>
                    </ul>
                }
            </li>
        )
    }
}