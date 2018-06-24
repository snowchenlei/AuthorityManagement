import React from 'react'
import {
    BrowserRouter as Router,
    Route
  } from 'react-router-dom'

import Login from '../containers/Login'
import NavigateListContainer from '../containers/navigation/NavigateList'
import App from '../App'

class RouteMap extends React.Component {
    render(){
      return (
        <Router>
          <div>
            <Route exact path="/" component = {App} />
            <Route path="/Login" component={Login} />
            <Route path="/Nav" component={NavigateListContainer} />
          </div>
        </Router>
      )
    }
  }
  export default RouteMap