import React, { Component } from 'react';
import { Switch} from 'react-router-dom'
import Header from "./Components/Header";
import Home from './Components/Home';
import Pages from './Components/Pages'
import Footer from './Components/Footer'
import LoginForm from './Components/LoginForm'
import Unauthorized from './Components/Unauthorized'
import {
  BrowserRouter as Router,
  Route
} from 'react-router-dom';
import "./Styles/Index.css";
import NotFound from "./Components/NotFound"
import AddPageForm from './Components/AddPageForm';
import { library } from '@fortawesome/fontawesome-svg-core'
import { faStroopwafel, faTrash, faPlay } from '@fortawesome/free-solid-svg-icons'
import RegisterForm from './Components/RegisterForm';
import Verify from './Components/Verify';
import PageDetails from './Components/PageDetails';
import PageSplitList from './Components/pageSplitter/PageSplitList';
import DifferenceDetails from './Components/DifferenceDetails';

library.add(faStroopwafel)
library.add(faTrash)
library.add(faPlay)

class App extends Component {
  render() {
    return (
      <Router>
         <div className="app">
          <Header />
          <Switch>
            <Route exact path="/" component={Home} />
            <Route path="/login" component={LoginForm}/>
            <Route path="/register" component={RegisterForm}/>
            <Route path="/Pages" component={Pages}/>
            <Route path="/Page/:pageId" component={PageDetails}/>
            <Route path="/Difference/:id" component={DifferenceDetails}/>
            <Route path="/unauthorized" component={Unauthorized}/>
            <Route path="/AddPage" component={AddPageForm}/>
            <Route path="/Split/:url" component={PageSplitList}/>
            <Route path="/verify/:token" component={Verify}/>
            <Route path="*" component={NotFound} />
          </Switch>
          <Footer/>
        </div>
      </Router>
    );
  }
}

export default App;
