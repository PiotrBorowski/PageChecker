import axios from 'axios'

export default class TokenHelper{

static setTokenInHeader(token){
    if(token){
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
    }
    else{
        delete axios.defaults.headers.common['Authorization'];
    }
}
static setTokenInLocalStorage(token){
    if(token){
        localStorage.setItem('jwtToken', token);
    }
    else{
        localStorage.removeItem('jwtToken');
    }
}

static getToken(){
    return localStorage.getItem('jwtToken');
}

static CheckToken(){
    if(this.getToken()){
        this.setTokenInHeader(this.getToken())
        return true;
    }
        return false;
}

}