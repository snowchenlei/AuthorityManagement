import React from 'react';
import PropTypes from 'prop-types'

import { Panel, Form, FormGroup, ControlLabel, FormControl } from 'react-bootstrap';
import FontAwesome from 'react-fontawesome';
import BootstrapTable from 'react-bootstrap-table-next';
import paginationFactory from 'react-bootstrap-table2-paginator';
import overlayFactory from 'react-bootstrap-table2-overlay';

import styles from '../../wwwroot/css/main.css'

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
const RemotePagination = ({ data, columns, loading, page, sizePerPage, totalSize, onTableChange }) => (
    <div>
    <BootstrapTable
        remote
        loading={loading}
        keyField="ID"
        data={data}
        columns={columns}
        rowClasses={rowClasses}
        pagination={paginationFactory({
            page: page,
            sizePerPage: sizePerPage,
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
        overlay={ overlayFactory({ spinner: true, background: 'rgba(192,192,192,0.3)' }) }
    />
    </div>
);

class ModuleComponent extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            expanded: false,
            loading: false
        }
    }
    //点击搜索框
    handlerExpanded() {
        this.setState({ expanded: !this.state.expanded })
        //this.angle.transform = 'rotatez(' + 180 + 'deg)';
    }

    render() {
        return (
            <div>
                {/* <Panel className='panel-info' defaultExpanded='true'> // 默认显示 */}
                <Panel className='panel-info'>
                    <Panel.Heading>
                        <Panel.Title toggle>
                            <span onClick={this.handlerExpanded.bind(this)}>
                                <FontAwesome name='search' className={styles.menuIcon} />查询条件
                                <FontAwesome ref={this.angle} name={this.state.expanded ? 'angle-up' : 'angle-down'} className={styles.menuIcon} style={{ float: 'right', fontSize: '24px' }} />
                                {/* <FontAwesome ref={this.angle} name='angle-down' className={styles.menuIcon} style={{ float: 'right' }}/> */}
                            </span>
                        </Panel.Title>
                    </Panel.Heading>
                    <Panel.Body collapsible>
                        <Form inline>
                            <FormGroup controlId="formInlineModuleName">
                                <ControlLabel>模块名称</ControlLabel>
                                {' '}
                                <FormControl type="text" placeholder="模块名称" />
                            </FormGroup>
                            {' '}
                        </Form>
                    </Panel.Body>
                </Panel>
            </div>
        )
    }
}
export default ModuleComponent;