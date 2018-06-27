import React from 'react';
import ReactDOM from 'react-dom';
import {
    BrowserRouter as Router,
    Route,
    Link
  } from 'react-router-dom'
  
  import registerServiceWorker from './registerServiceWorker';
  import Welcome from './components/Home'
  import MenuBar from './containers/MenuBar'
  import User from './containers/authorization/User'
  import Role from './containers/authorization/Role'
  import Module from './containers/authorization/Module'
  import ModuleElement from './containers/authorization/ModuleElement'
  import Fotter from './components/Fotter'
  import Header from './components/Header'

  class App extends React.Component {
    render() {
      return (        
        <Router>
          <div>
            <Header>
              <h1>Welcome</h1>
            </Header>
            <div style={{ paddingTop: '50px' }}>
              <div style={{ float: 'left' }}>
                <MenuBar />
              </div>
              <div>
                <div>
                  <Route exact path="/" component={Welcome} />
                  <Route path="/User" component={User} />
                  <Route path="/Role" component={Role} />
                  <Route path="/Module" component={Module} />
                  <Route path="/ModuleElement" component={ModuleElement} />
                </div>
                <Fotter />
              </div>
            </div>
          </div>
        </Router>
      )
    }
  }

ReactDOM.render(
    <App />, 
    document.getElementById('root'));
registerServiceWorker();
