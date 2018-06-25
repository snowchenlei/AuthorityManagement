import React, { Component } from 'react';
import ReactDOM from 'react-dom'

import MenuList from './menu/MenuList'
import Header from './Header'
import Fotter from './Fotter'

class Home extends Component {
    render() {
        return (
            <div>
                <Header>
                    <h1>Welcome</h1>
                </Header>
                <MenuList />
                <Fotter>
                    <h1>Welcome</h1>
                </Fotter>
            </div>
        )
    }
}

export default Home;