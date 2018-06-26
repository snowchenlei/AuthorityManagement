import React, { Component } from 'react';

import MenuBar from '../containers/MenuBar'
import Content from './Content'
import Fotter from './Fotter'

class Container extends Component {
    render() {
        return (
            <div style={{ paddingTop: '50px' }}>
                <div className='pull-left' style={{ backgroundColor: '#f2f2f2' }}>
                    <MenuBar/>
                </div>
                <div className='pull-left'>
                    <Content />
                    <Fotter />
                </div>
            </div>
        )
    }
}

export default Container;