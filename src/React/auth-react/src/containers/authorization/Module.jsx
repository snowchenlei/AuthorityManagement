import React from 'react';

import ModuleComponent from '../../components/authorization/Module'
import { GetModules } from '../../fetch/authorization/Module'

const columns = [
    { dataField: 'ID', text: '主键', hidden: true },
    { dataField: 'Name', text: '模块名称' },
    { dataField: 'Url', text: '请求地址' }, 
    { dataField: 'IconName', text: '图标名称' }, 
    { dataField: 'Sort', text: '排序', sort: true }, 
    { dataField: 'ParentID', text: '父ID', hidden: true }, 
    { dataField: 'ParentName', text: '父模块'},
    { dataField: 'AddTime', text: '添加时间', sort: true }
];
class ModuleContainer extends React.Component {
    constructor() {
        super()
        this.state = {
          data: [],
          totalSize: 0,
          pageIndex: 1,
          pageSize:5,
          loading: false
        }
      }

    componentWillMount() {
        this._loadModule(this.state.pageIndex,this.state.pageSize);
    }
    handleChange(pageIndex, pageSize){
        this._loadModule(pageIndex, pageSize);
    }
    _loadModule(pageIndex, pageSize) {
        let paras ='pageIndex='+pageIndex+'&pageSize='+pageSize;
        GetModules(paras)
            .then(response =>
                response.json().then(json => ({ json, response }))
            ).then(({ json, response }) => {
                if (response.ok) {
                    if (json.State === 1) {
                        this.setState({ 
                            data: json.Data.rows,
                            totalSize: json.Data.total,
                            pageIndex: pageIndex,
                            pageSize: pageSize,
                            loading: false
                        });
                    } else {
                        console.log(json.Message)
                    }
                } else {
                    console.log(response)
                }
            });
    }

    render() {
        return (
            <ModuleComponent 
                columns={columns}
                data={this.state.data}
                totalSize={this.state.totalSize}
                loading={this.state.loading}
                pageIndex={this.state.pageIndex}
                pageSize={this.state.pageSize}
                onTableChange={this.handleChange.bind(this)}/>
        )
    }
}
export default ModuleContainer