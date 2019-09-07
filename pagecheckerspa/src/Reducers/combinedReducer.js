import {combineReducers} from 'redux'
import userReducer from './userReducer'
import pageReducer from './pageReducer';

const combinedReducer = combineReducers({
    user: userReducer,
    pages: pageReducer
})

export default combinedReducer