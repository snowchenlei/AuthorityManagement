import React, { Component } from 'react';

import Container from './/Container'
import Header from './Header'

class Home extends Component {
    render() {
        return (
            <div>
                <Header>
                    <h1>Welcome</h1>
                </Header>
                <Container />               
            </div>
        )
    }
}

export default Home;