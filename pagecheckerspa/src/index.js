import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import {Provider} from 'react-redux'
import { createStore, applyMiddleware} from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension';
import thunk from 'redux-thunk';
import "jquery/dist/jquery.min.js";
import "bootstrap/dist/js/bootstrap.min.js";
import "bootstrap/dist/css/bootstrap.min.css"
import "@fortawesome/fontawesome-svg-core"
import "@fortawesome/free-solid-svg-icons"
import 'font-awesome/css/font-awesome.min.css';
import combinedReducer from './Reducers/combinedReducer'
import { faWindowMinimize } from '@fortawesome/free-solid-svg-icons';

const store = createStore(combinedReducer,
    composeWithDevTools(
        applyMiddleware(thunk)
    )
);

ReactDOM.render(<Provider store = {store}>
                    <App />
                </Provider>, document.getElementById('root'));

