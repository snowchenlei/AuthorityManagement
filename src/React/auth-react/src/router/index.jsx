import React from 'react'
import {
  BrowserRouter as Router,
  Route,
  Link
} from 'react-router-dom'

import Login from '../containers/Login'
import MenuBarContainer from '../containers/MenuBar'
import Home from '../components/Home'
import MenuBar from '../containers/MenuBar'
import Fotter from '../components/Fotter'
import Header from '../components/Header'

class RouteMap extends React.Component {
  render() {
    return (
      <Router>
        <div>
          <Header>
            <h1>Welcome</h1>
          </Header>
          <div style={{ paddingTop: '50px' }}>
            <div style={{ display: 'inline-block' }}>
              <MenuBar />
            </div>
            <div style={{ display: 'inline-block' }}>
              <div>
                <Route exact path="/" component={Home} />
                <Route path="/Login" component={Login} />
                <Route path="/Menu" component={MenuBarContainer} />
              </div>
              <Fotter />
            </div>
          </div>
        </div>
      </Router>
    )
  }
}
export default RouteMap