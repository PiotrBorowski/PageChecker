export default function pageReducer(state=[], action){
    switch(action.type){
        case "GET_PAGES":
            return [
                ...action.pages
            ]

        case "DELETE_PAGE":
            return [
                ...state.filter(page => page.pageId !== action.pageId)
            ]
            

        default:
            return state;
    }
}