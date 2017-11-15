import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { DefaultSearch } from './DefaultSearch';

interface ParametersInterface {
    searchInput: string;
    redirectToMethod: boolean;
}

export class Home extends React.Component<RouteComponentProps<{}>, ParametersInterface> {

    constructor() {
        super();
        this.redirectToGetAll = this.redirectToGetAll.bind(this);
        this.onTextChange = this.onTextChange.bind(this);
        this.state = {
            searchInput: "",
            redirectToMethod: false

        };
    }

    public redirectToGetAll(e) {
        if (e.key === "Enter") {
            //window.location.assign("/search/getitems");
            //var input = this.state.searchInput;
            //fetch('http://localhost:47107/' + input, {
            //    method: 'get'
            //})
            this.setState({ redirectToMethod: true });
        }
    }

    private onTextChange(e) {
        this.setState({ searchInput: e.target.value });
    }

    public render() {

        //var parameter = this.state.searchInput;
        //if (this.state.redirectToMethod) {
        //    return <Redirect to="/search/getitems/:parameter" />

        //}
        //else {
        //    return (
        //        <div id="searchBar">

        //            <input type="text" onKeyPress={this.redirectToGetAll} onChange={this.onTextChange} />

        //        </div>
        //    );
        //}
        
            
           return<Redirect to="/search/getitems" />
        


    }
}
//<DefaultSearch />


//<section id="main-section">
//    <div id="search-setting">bla bla</div>
//    <div id="search-bar-wrapper">
//        <div id="search-bar">
//            <h1>SEARCH THE BEST PRICE</h1>
//            <p>...and be happy</p>
//            <div id="searchBar">
//                <p><input type="text" onLoadStart={this.redirectToGetAll} /></p>
//            </div>
//        </div>
//    </div>
//</section>;