import React, { Component } from 'react';
import { Switch } from 'react-router-dom'
import Header from "./Components/Header";
import Home from './Components/Home';
import {
  BrowserRouter as Router,
  Route
} from 'react-router-dom';
import "./Styles/Header.css";
import NotFound from "./Components/NotFound"

class App extends Component {
  render() {
    return (
      <Router>
         <div className="app">
          <Header />
          <Switch>
            <Route exact path="/" component={Home} />
            <Route path="*" component={NotFound} />
          </Switch>
        </div>
      </Router>
    );
  }
}

export default App;
