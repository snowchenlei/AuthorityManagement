import React from 'react';
import ReactDOM from 'react-dom';

import './index.css';
import './wwwroot/lib/bootstrap-3.3.7-dist/css/bootstrap.min.css';
import './wwwroot/lib/bootstrap-3.3.7-dist/css/bootstrap-theme.min.css';

import RouteMap from './router/index'

ReactDOM.render(
    <RouteMap />, 
    document.getElementById('root')
);
//registerServiceWorker();
