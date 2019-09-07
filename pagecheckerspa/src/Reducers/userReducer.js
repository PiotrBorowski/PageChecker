
export default function userReducer (state={username: "unknown", isAuthenticated: false}, action){
    switch(action.type){
        case "SET_USER_AUTH": {
           return {
               ...state, 
                isAuthenticated: action.isAuthenticated
            }
        }
        case "SET_USERNAME": {
            return {
                ...state,
                username: action.username
            }
        }
        default:
            return state;
    }

}