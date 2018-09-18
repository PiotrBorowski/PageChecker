import TokenHelper from '../helpers/tokenHelper'

//action creator
export const setUserAuth = (isAuthenticated) => {
    return{
        type: "SET_USER_AUTH",
        isAuthenticated
    };
}

export const setUsername = (username) => {
    return{
        type: "SET_USERNAME",
        username
    }
}

export const CheckUserToken = () => {
    return dispatch => {
        if(!TokenHelper.CheckToken()){
            console.log("brak usera")
            dispatch(setUserAuth(null));
        }
        else{
            console.log("jest user")
            dispatch(setUserAuth(true))
            dispatch(setUsername(localStorage.getItem('username')))
        }
    }
}