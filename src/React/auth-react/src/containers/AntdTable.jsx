import React from 'react';
import WrapWithLoadData from '../higher_components/WrapWithLoadData';
import { Table, Input, Icon, Button, Popconfirm } from 'antd';

const rowSelection = {
    onChange: (selectedRowKeys, selectedRows) => {
      console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
    },
    getCheckboxProps: record => ({
      disabled: record.name === 'Disabled User', // Column configuration not to be checked
      name: record.name,
    }),
  };

class AntdTableContainer extends React.Component{
    handleTableChange = (pagination, filters, sorter) => {
        if(this.props.onTableChange){
            this.props.onTableChange({ 
                pageIndex: pagination.current,
                pageSize: pagination.pageSize,
                //args:sortField === undefined ? '' : '&sort='+sortField+'&order='+sortOrder
            });
        }
      }
      //每页记录数改变
      handlerShowSizeChange(current, pageSize) {
        if(this.props.onTableChange){
            this.props.onTableChange({ 
                pageIndex: current, 
                pageSize: pageSize,
                //args:sortField === undefined ? '' : '&sort='+sortField+'&order='+sortOrder
            });
        }
      }
      //页面跳转
      handlerPageChange(pageNumber){
          console.log(pageNumber);
      }
      handlerItemRender(current, type, originalElement){
        // if (type === 'prev') {
        //     return <a>Previous</a>;
        //   } if (type === 'next') {
        //     return <a>Next</a>;
        //   }
          return originalElement;
      }
    render(){
        return(
            <Table 
                rowKey='ID'
                bordered
                dataSource={this.props.data}
                rowSelection={rowSelection} 
                columns={this.props.columns}
                loading={this.props.loading}
                pagination={{
                    defaultCurrent: 1,                                                  //默认的当前页数
                    total: this.props.totalSize,                                        //数据总数
                    pageSizeOptions: [5, 10, 20, 50, 100, 500],                            //指定每页可以显示多少条         
                    //hideOnSinglePage: true,                                             //只有一页时是否隐藏分页器
                    showTotal: (total, range) => `共 ${total} 条记录，当前显示第 ${range[0]}至${range[1]} `,    //分页信息显示
                    itemRender: this.handlerItemRender.bind(this),                      //分页样式
                    showSizeChanger: true,                                              //是否可以改变 pageSize
                    onShowSizeChange: this.handlerShowSizeChange.bind(this),            //pageSize 变化的回调
                    showQuickJumper: true,                                              //是否可以快速跳转至某页
                    onChange: this.handlerPageChange.bind(this),                        //页码改变的回调，参数是改变后的页码及每页条数
                }}
                onChange={this.handleTableChange}
            />
        )
    }
}

AntdTableContainer = WrapWithLoadData(AntdTableContainer);
export default AntdTableContainer;