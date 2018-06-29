import React, { Component } from 'react';

import MenuBar from '../containers/MenuBar'
import Content from './Welcome'
import Footer from './Footer'

class Container extends Component {
    render() {
        return (
            <div style={{ paddingTop: '50px' }}>
                <div style={{ display: 'inline-block' }}>
                    <MenuBar/>
                </div>
                <div style={{ display: 'inline-block' }}>
                    <Content />
                    <Footer />
                </div>
            </div>
        )
    }
}

export default Container;