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
  import Footer from './components/Footer'
  import Header from './components/Header'

  class App extends React.Component {
    render() {
      return (        
        <Router>
          <div>
            <Header>
              <h1>Welcome</h1>
            </Header>
            <div>
              {/* <div style={{ float: 'left' }}> */}
              <div className='pull-left' style={{ position: 'fixed', height: '100%', backgroundColor: '#e8e8e8' }}>
                <MenuBar />
              </div>
              <div style={{ paddingLeft: '200px' }}>
                <div style={{ padding: '15px' }}>
                  <Route exact path="/" component={Module} />
                  <Route path="/User" component={User} />
                  <Route path="/Role" component={Role} />
                  <Route path="/Module" component={Module} />
                  <Route path="/ModuleElement" component={ModuleElement} />
                </div>
                <Footer />
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
