import React, { Component, PropTypes } from 'react';
import ReactDOM from 'react-dom';

import Navigate from './Navigate'

class NavigateListComponent extends Component {
    // static propTypes = {
    //     onReload: PropTypes.func,
    //     data: PropTypes.any
    // }

    render() {
        return (
        <nav className='navbar navbar-default navbar-fixed-top'>
            <div className="container">
                <ul className="nav navbar-nav">
                    {this.props.data.map((nav, i) =>
                        <Navigate
                            nav={nav}
                            key={i}
                            index={i} />
                    )}
                </ul>
            </div>
        </nav >
        )
    }
}

export default NavigateListComponent