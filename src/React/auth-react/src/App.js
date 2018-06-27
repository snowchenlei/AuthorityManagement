import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';

import MenuBar from './containers/MenuBar'
import Fotter from './components/Fotter'
import Header from './components/Header'
import RouteMap from './router/index'

class App extends Component {
  render() {
    return (
      <div>
        <Header>
          <h1>Welcome</h1>
        </Header>
        <div style={{ paddingTop: '50px' }}>
          <div style={{ display: 'inline-block' }}>
            <MenuBar />
          </div>
          <div style={{ display: 'inline-block' }}>
            <RouteMap />
            <Fotter />
          </div>
        </div>
      </div>
    );
  }
}

export default App;
