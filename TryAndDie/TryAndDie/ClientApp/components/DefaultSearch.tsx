import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import 'isomorphic-fetch';

interface DefaultSearchInterface {
    items: Item[];
    loading: boolean;
    filteredList: Item[]
}

export class DefaultSearch extends React.Component<RouteComponentProps<{}>, DefaultSearchInterface> {
    constructor() {
        super();
        this.state = {
            items: [],
            loading: true,
            filteredList: []
        };


        fetch('api/Search/GetItems')
            .then(response => response.json() as Promise<Item[]>)
            .then(data => {
                this.setState({ items: data, loading: false });
            });

    }

    private static renderItemsList(items: Item[]) {


        return (<table className='table'>

            <tr>
                <th>Price</th>
                <th>Name</th>
                <th>Supplier</th>
                <th>Code</th>
            </tr>


            {items.map(item =>
                <tr key={item.code}>
                    <td>{item.price}</td>
                    <td>{item.name}</td>
                    <td>{item.supplier}</td>
                    <td>{item.code}</td>
                </tr>
            )}

        </table>);
    }

    public redirectToGetAll() {
        window.location.assign("/search/GetItems");
    }

    public redirectToPrevious() {

    }

    public redirectToNext() {

    }

    public render() {
        let contents = DefaultSearch.renderItemsList(this.state.items);

        return (
            <section id="main-section">
                <div id="search-setting">bla bla</div>
                <div id="search-bar-wrapper">
                    <div id="search-bar">
                        <h1>SEARCH THE BEST PRICE</h1>
                        <p>...and be happy</p>
                        <div id="searchBar">
                        <p><input type="text" onKeyUp={this.redirectToGetAll} /></p>
                        </div>
                    </div>
                    <div id="result">
                        {contents}
                    </div>
                    <span>
                        <button onClick={this.redirectToPrevious}>BACK</button>
                        <button onClick={this.redirectToNext}>NEXT</button>
                    </span>
                </div>
            </section>
            
            
        );
    }

    
}

//let contents = this.state.loading
//    ? <p><em>Loading...</em></p>
//    : DefaultSearch.renderItemsList(this.state.items);
interface Item {
    code: number;
    name: string;
    supplier: string;
    price: number;
}