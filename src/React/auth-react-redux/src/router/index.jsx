import React from 'react'
import {
    BrowserRouter as Router,
    Route
  } from 'react-router-dom'

import Login from '../containers/Login'
import MenuBarContainer from '../containers/MenuBar'
import Home from '../components/Home'

class RouteMap extends React.Component {
    render(){
      return (
        <Router>
          <div>
            <Route exact path="/" component = {Home} />
            <Route path="/Login" component={Login} />
            <Route path="/Menu" component={MenuBarContainer} />
          </div>
        </Router>
      )
    }
  }
  export default RouteMap