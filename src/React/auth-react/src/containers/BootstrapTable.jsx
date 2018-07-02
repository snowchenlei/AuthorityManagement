import React from 'react';
import WrapShowBootstrapTable from '../higher_components/WrapShowBootstrapTable';
import WrapWithLoadData from '../higher_components/WrapWithLoadData';

import BootstrapTable from 'react-bootstrap-table-next';
import paginationFactory from 'react-bootstrap-table2-paginator';
import overlayFactory from 'react-bootstrap-table2-overlay';
import { cpus } from 'os';

//行样式
const rowClasses = (row, rowIndex) => {
    let classes = null;
    if (rowIndex % 2 === 1) {
        classes = 'f9f9f9';
    }
    return classes;
};
//分页
const customTotal = (from, to, size) => (
    <span className="react-bootstrap-table-pagination-total">
        显示第 {from} 到第 {to + 1} 条记录，总共 {size}  条记录
    </span>
);
//远程分页
const RemotePagination = ({ data, columns, loading, pageIndex, pageSize, totalSize, onTableChange }) => (
    <div>
        <BootstrapTable
            remote={ { 
                pagination: true, 
                sort: true 
            } }
            loading={loading}
            keyField="ID"
            data={data}
            columns={columns}
            rowClasses={rowClasses}
            pagination={paginationFactory({
                page: pageIndex,
                sizePerPage: pageSize,
                totalSize: totalSize,
                firstPageText: '<<',
                prePageText: '<',
                nextPageText: '>',
                lastPageText: '>>',
                nextPageTitle: '首页',
                prePageTitle: '上一页',
                firstPageTitle: '下一页',
                lastPageTitle: '尾页',
                showTotal: true,
                paginationTotalRenderer: customTotal,
            })}
            onTableChange={onTableChange}
            overlay={overlayFactory({ spinner: true, background: 'rgba(192,192,192,0.3)' })}
        />
    </div>
);


class BootstrapTableContainer extends React.Component{
    isUndefined(arg){
        return arg === undefined ? '' : arg;
    }
    handleTableChange = (type, { sortField, sortOrder, data, page, sizePerPage }) => {
        if(this.props.onTableChange){
            let pageIndex;
            let pageSize;
            if(type === 'pagination'){
                pageIndex = page;
                pageSize = sizePerPage;
            }else{
                pageIndex = 1;
                pageSize = sizePerPage;
            }

            this.props.onTableChange({ 
                pageIndex: pageIndex, 
                pageSize: pageSize,
                args:sortField === undefined ? '' : '&sort='+sortField+'&order='+sortOrder
            });
        }
    }
    
    render(){
        return(
            <RemotePagination
                data={this.props.data}
                columns={this.props.columns}
                loading={this.props.loading}
                pageIndex={this.props.pageIndex}
                pageSize={this.props.pageSize}
                totalSize={this.props.totalSize}
                onTableChange={this.handleTableChange.bind(this)} 
            />
        )
    }
}
//BootstrapTableContainer = WrapShowBootstrapTable(BootstrapTableContainer);
BootstrapTableContainer = WrapWithLoadData(BootstrapTableContainer);
export default BootstrapTableContainer;