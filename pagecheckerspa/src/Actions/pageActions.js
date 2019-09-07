import axios from "axios"
import {BASE_URL} from "../constants";

//action creator
export const deletePage = (pageId) => {
    return {
        type: "DELETE_PAGE",
        pageId
    }
}

export const getPages = (pages) => {
    return {
        type: "GET_PAGES",
        pages
    }
}

export const deletePageThunk = (pageId) => {
    return dispatch => {
        axios.delete(BASE_URL + "/page?pageId=" + pageId)
        .then(dispatch(deletePage(pageId)))
        .catch(err => {
            console.log(err);
        })
    }
}

export const getPageThunk = () => {
    return dispatch => {
        axios.get(BASE_URL + "/page")
        .then(response =>  {
            dispatch(getPages(response.data))
        })
        .catch(err => {
            if(err.response.status === 401){
                this.props.history.push('/unauthorized');
        }
        })
    }
}