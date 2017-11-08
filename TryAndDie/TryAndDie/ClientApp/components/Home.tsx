import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { DefaultSearch } from './DefaultSearch';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public redirectToGetAll() {
        window.location.assign("/search/GetItems");
    }

    public render() {
        return <section id="main-section">
            <div id="search-setting">bla bla</div>
            <div id="search-bar-wrapper">
            <div id="search-bar">
                <h1>SEARCH THE BEST PRICE</h1>
                <p>...and be happy</p>
                <div id="searchBar">
                    <p><input type="text" onKeyUp={this.redirectToGetAll} /></p>
                </div>
            </div>
        </div>
    </section>;
    }
}
//<DefaultSearch />