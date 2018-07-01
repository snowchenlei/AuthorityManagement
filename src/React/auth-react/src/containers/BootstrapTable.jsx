import React from 'react';
import WrapShowBootstrapTable from '../higher_components/WrapShowBootstrapTable';
import WrapWithLoadData from '../higher_components/WrapWithLoadData';

class BootstrapTableContainer extends React.Component{
    // componentWillMount() {
    //     this._loadModule({
    //         pageIndex: this.state.pageIndex,
    //         pageSize: this.state.pageSize
    //     });
    // }
    // handleChange(pageIndex, pageSize) {
    //     this._loadModule({
    //         pageIndex: pageIndex,
    //         pageSize: pageSize
    //     });
    // }
    // _loadModule(paras) {
    //     let pageIndex = paras.pageIndex;
    //     let pageSize = paras.pageSize;
    //     let strParas = 'pageIndex=' + pageIndex + '&pageSize=' + pageSize;
    //     GetModules(strParas)
    //         .then(response =>
    //             response.json().then(json => ({ json, response }))
    //         ).then(({ json, response }) => {
    //             if (response.ok) {
    //                 if (json.State === 1) {
    //                     this.setState({
    //                         data: json.Data.rows,
    //                         totalSize: json.Data.total,
    //                         pageIndex: pageIndex,
    //                         pageSize: pageSize,
    //                         loading: false
    //                     });
    //                 } else {
    //                     console.log(json.Message)
    //                 }
    //             } else {
    //                 console.log(response)
    //             }
    //         });
    // }
    render(){
        return(
            <div/>
        )
    }
}
BootstrapTableContainer = WrapShowBootstrapTable(BootstrapTableContainer);
BootstrapTableContainer = WrapWithLoadData(BootstrapTableContainer);
export default BootstrapTableContainer;