
export default function userReducer (state={username: "unknown", isAuthenticated: false}, action){
    switch(action.type){
        case "SET_USER_AUTH": {
           state = {...state, 
            isAuthenticated: action.isAuthenticated
            }
            break;
        }
        case "SET_USERNAME": {
            state ={...state,
            username: action.username}
        }
        break;
    }

    return state;
}