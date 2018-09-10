import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import "jquery/dist/jquery.min.js";
import "bootstrap/dist/js/bootstrap.min.js";
import "bootstrap/dist/css/bootstrap.min.css"
import "@fortawesome/fontawesome-svg-core"
import "@fortawesome/free-solid-svg-icons"
import 'font-awesome/css/font-awesome.min.css';

ReactDOM.render(<App />, document.getElementById('root'));
registerServiceWorker();
