import React from 'react';
import { Table } from 'antd';

class StandardTable extends React.Component {
    handleRowSelectChange = (selectedRowKeys, selectedRows) => {
        console.log('handleRowSelectChange');
      };
    handleTableChange = (pagination, filters, sorter) => {
        if (this.props.onChange) {
            // this.props.onChange({ 
            //     pageIndex: pagination.current,
            //     pageSize: pagination.pageSize,
            //     //args:sortField === undefined ? '' : '&sort='+sortField+'&order='+sortOrder
            // });
            this.props.onChange(pagination, filters, sorter);
        }
    }
    render() {
        const rowSelection = {
            onChange: this.handleRowSelectChange,
            getCheckboxProps: record => ({
              disabled: record.disabled,
            }),
          };

        return (
            <Table
                bordered
                rowKey={this.props.rowKey || "ID"}
                dataSource={this.props.data}
                rowSelection={rowSelection}
                columns={this.props.columns}
                loading={this.props.loading}
                pagination={this.props.pagination}
                onChange={this.handleTableChange}
            />
        )
    }
}

export default StandardTable;