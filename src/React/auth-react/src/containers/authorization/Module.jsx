import React from 'react';

import ModuleComponent from '../../components/authorization/Module'
import { GetModules } from '../../fetch/authorization/Module'
//import Table from '../BootstrapTable'
import Table from '../AntdTable'
import WrapShowBootstrapTable from '../../higher_components/WrapShowBootstrapTable'

// const columns = [
//     { dataField: 'ID', text: '主键', hidden: true },
//     { dataField: 'Name', text: '模块名称' },
//     { dataField: 'Url', text: '请求地址' },
//     { dataField: 'IconName', text: '图标名称' },
//     { dataField: 'Sort', text: '排序', sort: true },
//     { dataField: 'ParentID', text: '父ID', hidden: true },
//     { dataField: 'ParentName', text: '父模块' },
//     { dataField: 'AddTime', text: '添加时间', sort: true }
// ];
const columns = [
    { dataIndex: 'ID', title: '主键', hidden: true },
    { dataIndex: 'Name', title: '模块名称' },
    { dataIndex: 'Url', title: '请求地址' },
    { dataIndex: 'IconName', title: '图标名称' },
    { dataIndex: 'Sort', title: '排序', sorter: true },
    { dataIndex: 'ParentID', title: '父ID', hidden: true },
    { dataIndex: 'ParentName', title: '父模块' },
    { dataIndex: 'AddTime', title: '添加时间', sort: true }
];
const apiName = 'module';
class ModuleContainer extends React.Component {
    constructor() {
        super()
        this.state = {
            data: [],
            totalSize: 0,
            pageIndex: 1,
            pageSize: 5,
            loading: false
        }
    }

    // componentWillMount() {
    //     this._loadModule({
    //         pageIndex: this.state.pageIndex,
    //         pageSize: this.state.pageSize
    //     });
    // }
    // handleTableChange(pageIndex, pageSize) {
    //     this.setState({pageIndex,pageSize});
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

    render() {
        return (
            <div>
                <ModuleComponent />
                <Table
                    columns={columns}
                    relativeUrl='module'
                    pageIndex= {this.state.pageIndex}
                    pageSize= {this.state.pageSize}
                    />
            </div>
        )
    }
}
export default ModuleContainer