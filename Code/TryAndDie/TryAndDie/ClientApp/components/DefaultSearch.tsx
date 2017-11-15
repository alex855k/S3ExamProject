import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import 'isomorphic-fetch';
//import { DataTable } from 'react-data-components';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
//import 'node_modules/react-bootstrap-table/dist/react-bootstrap-table-all.min.css';


interface DefaultSearchInterface {
    items: Item[];
    loading: boolean;
    filteredList: Item[];
    order: string;
    table: any;
    value: any;
    //selectRowProp: any;
    itemsToCompare: number[];
    //expanding: number[];
}

class BSTable extends React.Component {
    constructor(props) {
        super(props);


    }
    props: {
        data: any;
    };

    

    private formatToLink(cell, row) {
        return "<button onClick=window.location.href='" + cell + "' type='button' target='_blank'>PRODUCT LINK</button>";
        //return "<button onclick='" + this.getLinkComponent(cell) + "'> Click me</button >"; 
    }

    render() {
        //this.props = myprops;
        if (this.props.data !== null) {
            return (
                <BootstrapTable data={this.props.data}>

                    <TableHeaderColumn dataAlign="center" headerAlign="center" width="100" dataFormat={this.formatToLink} dataField='url' isKey>URL</TableHeaderColumn>
                    <TableHeaderColumn dataAlign="center" headerAlign="center" width="100" dataField='name'>Name</TableHeaderColumn>
                    <TableHeaderColumn dataAlign="center" headerAlign="center" width="100" dataField='price' >Price</TableHeaderColumn>
                </BootstrapTable>);
        } else {
            return (<p>?</p>);
        }
    }
}

//var myprops;
var selectedItems: number[] = [];
//var items: Item[];
//var selectedItems = [];
export class DefaultSearch extends React.Component<RouteComponentProps<{}>, DefaultSearchInterface> {

    public constructor(props) {
        super(props);
        //console.log(this.props);
        //myprops = props;
        //this.onRowSelect = this.onRowSelect.bind(this);
        this.TestFunction = this.TestFunction.bind(this);
        this.state = {
            items: [],
            loading: true,
            filteredList: [],
            order: 'desc',
            table: BootstrapTable,
            value: "default",
            itemsToCompare: [],

            //selectRowProp : {
            //    mode: 'checkbox',
            //    ////onSelect: this.onRowSelect,
            //    bgColor: function (row, isSelect) {
            //        //newArray = [];
            //        if (isSelect) {
                        
            //            ////alert(row.code);   
                        

            //            selectedItems.push(row.code);
            //            //something = newArray;
            //            //alert(something);
            //            //newArray = [...selectedItems, row.code];
            //            //selectedItems = newArray;
            //            return "blue";
            //        }
            //        return null;
            //    }

            //}
        };


        fetch('api/Search/GetItems')
            .then(response => response.json() as Promise<Item[]>)
            .then(data => {
                //items = data;
                //myprops = data;
                this.setState({ items: data, loading: false });
            });

    }
    
    private SortName(table: BootstrapTable) {


        if (this.state.order === 'desc') {
            table.handleSort('asc', 'name');
            this.setState({ order: 'asc' });
        } else {
            table.handleSort('desc', 'name');
            this.setState({ order: 'desc' });
        }
    }

    private SortList(input: string, table) {
       
        switch (input) {
            case "default":
                break;
            case "Name":
                this.SortName(table);
                for (var e of selectedItems) {
                    alert(e);
                }
                selectedItems = [];
                break;
        }
    }

    private renderShowsTotal(start, to, total) {
        return (
            <p style={{ color: 'blue' }}>
                From {start} to {to}, totals is {total}&nbsp;&nbsp;(its a customize text)
            </p>
        );
    }

    //private onRowSelect(row, isSelected, e) {
    //    //alert(row.name);
    //    //var newArray;
    //    //if (isSelected) {
    //    //    newArray = [...this.state.itemsToCompare, row.code];
    //    //    this.setState({ itemsToCompare: newArray });
    //    //    alert(this.state.itemsToCompare.length);
    //    //}
    //    //if (this.state.itemsToCompare.length == 2) {
    //    //    this.state.itemsToCompare.map(function (item, i) {
    //    //        alert(item);
    //    //    });
    //    //    this.setState({ itemsToCompare: [] });
    //    //}
    //    var newArray = this.state.itemsToCompare.slice();
    //    newArray.push(row.code);
    //    this.setState({ itemsToCompare: newArray });

    //    alert(this.state.itemsToCompare.length);

    //}
    isExpandableRow(row) {
        //if (row.id < 3) return true;
        //else return false;
        return true;
    }

    expandComponent(row) {
        
        return (
            <BSTable data={row.suppliers} />
        );
        
        
    }

    //expandColumnComponent({ isExpandableRow, isExpanded }) {
    //    let content = '';

    //    if (isExpandableRow) {
    //        content = (isExpanded ? '(-)' : '(+)');
    //    } else {
    //        content = ' ';
    //    }
    //    return (
    //        <div> {content} </div>
    //    );
    //}

    private imageFormatter(cell, row) {
        return "<img height='30' width='30' src='" + cell + "'/>";
    }

    private TestFunction() {
        //var something = this.state.items.length;
        //alert("the number is " + something);
        //alert("fuck");
        for (var i of this.state.items) {
            for (var s of i.suppliers) {
                alert(s.name);
            }
        }
        //alert(myprops.data);
    }

    renderItemsList(items: Item[]) {
        var table;
        var selectElement;
        const opt = {
            page: 2,  // which page you want to show as default
            sizePerPageList: [{
                text: '5', value: 5
            }, {
                text: '10', value: 10
            }, {
                text: 'All', value: items.length
            }], // you can change the dropdown list for size per page
            sizePerPage: 5,  // which size per page you want to locate as default
            pageStartIndex: 0, // where to start counting the pages
            paginationSize: 3,  // the pagination bar size.
            prePage: 'Prev', // Previous page button text
            nextPage: 'Next', // Next page button text
            firstPage: 'First', // First page button text
            lastPage: 'Last', // Last page button text
            paginationPosition: 'bottom',
            noDataText: 'No result found',
            expandRowBgColor: 'rgb(242, 255, 163)',
            //expanding: this.state.expanding

        };

        //<button onClick={this.TestFunction}>fucking click</button>
        //<select
        //    ref={(select) => selectElement = select}
        //    onChange={() => { this.SortList(selectElement.options[selectElement.selectedIndex].value, table) }}>
        //    <option value="default" selected>--OPTIONS--</option>
        //    <option value="Name">SORT BY NAME </option>
        //</select>

        return (<div>
            <div id="searchBar">
                <input type="text" onKeyPress={this.redirectToGetAll} />

            </div>
            <BootstrapTable
                ref={(BootstrapTable) => table = BootstrapTable}
                striped hover condensed
                data={items}
                borderd={true}
                scrollTop={'Bottom'}
                pagination={true}
                options={opt}
                //search={true}
                //selectRow={this.state.selectRowProp}
                expandableRow={this.isExpandableRow}
                expandComponent={this.expandComponent}
                //expandColumnOptions={{
                //    expandColumnVisible: true,
                //    expandColumnComponent: this.expandColumnComponent,
                //    columnWidth: 50
                //}}>
                >
                <TableHeaderColumn dataAlign="center" headerAlign="center" width="150" dataFormat={this.imageFormatter} dataField='image' isKey>Image</TableHeaderColumn>
                <TableHeaderColumn dataAlign="center" headerAlign="center" width="150" dataField='name'>Name</TableHeaderColumn>
                <TableHeaderColumn dataAlign="center" headerAlign="center" width="150" dataField='description'>Description</TableHeaderColumn>
                <TableHeaderColumn dataAlign="center" headerAlign="center" width="150" dataField='lowest'>Lowest Price</TableHeaderColumn>

            </BootstrapTable>
            </div>
            );
    }

    public redirectToGetAll() {
        window.location.assign("/search/GetItems");
    }

    public redirectToPrevious() {

    }

    public redirectToNext() {

    }

    public render() {
        let contents = this.renderItemsList(this.state.items);

        return (
            
                <div id="result">
                    {contents}
                </div>
                    
            
            
        );
    }

    
}

interface Item {
    image: URL;
    name: string;
    description: string;
    lowest: number;
    suppliers: Supplier[];
}

interface Supplier {
    url: URL;
    name: string;
    price: number;
}

//interface Column {
//    title: string;
//    prop: string;
//}

//let contents = this.state.loading
//    ? <p><em>Loading...</em></p>
//    : DefaultSearch.renderItemsList(this.state.items);


//private static renderItemsList(items: Item[]) {

//    return (<table className='table'>

//        <tr>
//            <th>Price</th>
//            <th>Name</th>
//            <th>Supplier</th>
//            <th>Code</th>
//        </tr>


//        {items.map(item =>
//            <tr key={item.code}>
//                <td>{item.price}</td>
//                <td>{item.name}</td>
//                <td>{item.supplier}</td>
//                <td>{item.code}</td>
//            </tr>
//        )}

//    </table>);
//}

//<section id="main-section">
//    <div id="search-setting">bla bla</div>
//    <div id="search-bar-wrapper">
//        <div id="search-bar">
//            <h1>SEARCH THE BEST PRICE</h1>
//            <p>...and be happy</p>
//            <div id="searchBar">
//                <p><input type="text" onKeyUp={this.redirectToGetAll} /></p>
//            </div>
//        </div>
//        <div id="result">
//            {contents}
//        </div>
//        <span>
//            <button onClick={this.redirectToPrevious}>BACK</button>
//            <button onClick={this.redirectToNext}>NEXT</button>
//        </span>
//    </div>
//</section>

//return (<div>
//    <select ref={(input) => this.setState({ value: input })} onChange={() => this.SortList(this.state.value, this.state.table)}>
//        <option value="default" selected>SORT LIST</option>
//        <option value="Name">SORT BY NAME </option>
//    </select>

//    <BootstrapTable ref={(input) => this.setState({ table: input })} striped hover condensed data={items} borderd={true} options={{ noDataText: 'No result was found' }} scrollTop={'Bottom'}>
//        <TableHeaderColumn width="150" isKey={true} dataField='code'>Code</TableHeaderColumn>
//        <TableHeaderColumn width="150" dataField='name'>Name</TableHeaderColumn>
//        <TableHeaderColumn width="150" dataField='price'>Price</TableHeaderColumn>
//        <TableHeaderColumn width="150" dataField='supplier'>Supplier</TableHeaderColumn>
//    </BootstrapTable>
//</div>
//);

//var table;
//var selectElement;
//return (<div>
//    <select ref={(select) => selectElement = select} onChange={() => { this.Lol(selectElement.options[selectElement.selectedIndex].value) }}>
//        <option value="default" selected>SORT LIST</option>
//        <option value="Name">SORT BY NAME </option>
//    </select>

//    <BootstrapTable ref={(BootstrapTable) => table = BootstrapTable} striped hover condensed data={items} borderd={true} options={{ noDataText: 'No result was found' }} scrollTop={'Bottom'}>
//        <TableHeaderColumn width="150" isKey={true} dataField='code'>Code</TableHeaderColumn>
//        <TableHeaderColumn width="150" dataField='name'>Name</TableHeaderColumn>
//        <TableHeaderColumn width="150" dataField='price'>Price</TableHeaderColumn>
//        <TableHeaderColumn width="150" dataField='supplier'>Supplier</TableHeaderColumn>
//    </BootstrapTable>
//</div>
//);