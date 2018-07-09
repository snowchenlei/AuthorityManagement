import React from 'react';
import ReactDOM from 'react-dom';
import {
  BrowserRouter as Router,
  Route,
  Link
} from 'react-router-dom'

import registerServiceWorker from './registerServiceWorker';
//import Welcome from './components/Home'
import MenuBar from './containers/MenuBar'
import User from './containers/authorization/User'
import Role from './containers/authorization/Role'
import Module from './containers/authorization/Module'
import ModuleElement from './containers/authorization/ModuleElement'
import zhCN from 'antd/lib/locale-provider/zh_CN';
import { LocaleProvider } from 'antd';
import Footer from './components/Footer'
import Header from './components/Header'

class App extends React.Component {
  render() {
    return (
      <LocaleProvider locale={zhCN}>
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
                <div style={{ fontSize: '18px', textAlign: 'center', borderTop: '3px double #e5e5e5', paddingTop: '10px', marginTop: '20px' }}>
                  <strong className="text-primary">Snow</strong>
                  &nbsp;权限管理 &copy; 2018-2018
                </div>
              </div>
              {/* <Footer /> */}
            </div>
          </div>
        </div>
      </Router>
      </LocaleProvider>
    )
  }
}

ReactDOM.render(
  <App />,
  document.getElementById('root'));
registerServiceWorker();
