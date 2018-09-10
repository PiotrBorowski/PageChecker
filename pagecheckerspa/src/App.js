import React, { Component } from 'react';
import { Switch } from 'react-router-dom'
import Header from "./Components/Header";
import Home from './Components/Home';
import Pages from './Components/Pages'
import Footer from './Components/Footer'
import {
  BrowserRouter as Router,
  Route
} from 'react-router-dom';
import "./Styles/Index.css";
import NotFound from "./Components/NotFound"
import AddPageForm from './Components/AddPageForm';
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faStroopwafel } from '@fortawesome/free-solid-svg-icons'

library.add(faStroopwafel)

class App extends Component {
  render() {
    return (
      <Router>
         <div className="app">
          <Header />
          <Switch>
            <Route exact path="/" component={Home} />
            <Route path="/AddPage" component={AddPageForm}/>
            <Route path="/Pages" component={Pages}/>
            <Route path="*" component={NotFound} />
          </Switch>
          <Footer/>
        </div>
      </Router>
    );
  }
}

export default App;
