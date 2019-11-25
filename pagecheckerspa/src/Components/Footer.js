import React,{Component} from 'react'
import "../Styles/Index.css"
import "../Styles/Footer.css"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import * as brands from "@fortawesome/fontawesome-free-brands"

export default class Footer extends Component{
    render(){
        return(
        <footer className="footer">
            <div className="container">
                    <div className="footer-icons">
                    {new Date().getFullYear()} Piotr Borowski 
                            <a className="footer-icons" href="https://github.com/PiotrBorowski" target="_blank" rel="noopener noreferrer"> 
                             <FontAwesomeIcon icon={brands.faGithub} size="2x" color="white"/>
                            </a> 
                    </div>
            </div>
        </footer>
        )
    }
}