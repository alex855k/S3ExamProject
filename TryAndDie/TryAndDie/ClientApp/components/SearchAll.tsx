import * as React from 'react';
import { RouteComponentProps } from 'react-router';

//import { Redirect } from 'react-router-dom';
//import { withRouter } from 'react-router';
//import { BrowserRouter as Router } from 'react-router-dom';
//import {Route} from 'react-router-dom';


export class SearchAll extends React.Component<{}, {}> {
    constructor() {
        super();

    }

    public redirectToGetAll() {
        window.location.assign("/search/GetItems");
    }

    public render() {
        return (
            <div id="searchBar">
                <p><input type="text" onKeyUp={this.redirectToGetAll} /></p>
                
            </div>


        );
    }


}

