
export default function userReducer (state={username: "unknown", isAuthenticated: false, role: "unknown"}, action){
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
                username: action.username,
                role: action.role
            }
        }
        default:
            return state;
    }

}