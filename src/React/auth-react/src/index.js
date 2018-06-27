import React from 'react';
import ReactDOM from 'react-dom';

//import App from './App'
import RouteMap from './router/index'
import './index.css';
import registerServiceWorker from './registerServiceWorker';

ReactDOM.render(
    <RouteMap />, 
    document.getElementById('root'));
registerServiceWorker();
