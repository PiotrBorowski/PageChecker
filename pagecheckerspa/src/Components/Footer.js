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
                    <p>&copy; {new Date().getFullYear()} Copyright: Piotr Borowski</p>
                    <div className="footer-icons">
                            <a className="footer-icons" href="https://github.com/PiotrBorowski" target="_blank" rel="noopener noreferrer"> 
                                <FontAwesomeIcon icon={brands.faGithub} size="2x" color="white"/>
                            </a> 
                </div>
            </div>
        </footer>
        )
    }
}