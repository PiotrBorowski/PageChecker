import {combineReducers} from 'redux'
import userReducer from './userReducer'

const combinedReducer = combineReducers({
    user: userReducer
})

export default combinedReducer