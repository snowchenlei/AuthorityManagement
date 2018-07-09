import React from 'react';

import { Table, Form, Button, Row, Col, Input, Select, DatePicker, Modal } from 'antd'
import { queryModules, queryModuleParents } from '../../services/api'
import StandardTable from '../../containers/StandardTable';
import WrapWithLoadData from '../../higher_components/WrapWithLoadData';
const FormItem = Form.Item;
const { RangePicker } = DatePicker;
const Option = Select.Option;
let selectedRowKeys = [];
const columns = [
    { dataIndex: 'ID', title: '主键', hidden: true },
    { dataIndex: 'Name', title: '模块名称', editable: true },
    { dataIndex: 'Url', title: '请求地址', editable: true },
    { dataIndex: 'IconName', title: '图标名称', editable: true },
    { dataIndex: 'Sort', title: '排序', sorter: true, editable: true },
    { dataIndex: 'ParentID', title: '父ID', hidden: true },
    { dataIndex: 'ParentName', title: '父模块', editable: true },
    { dataIndex: 'AddTime', title: '添加时间', sort: true }
];
class ModuleContainer extends React.Component {
    constructor() {
        super()
        this.state = {
            data: [],
            totalSize: 0,
            formValues: {},
            selectData: []
        }
    }

    //页面加载完成
    componentWillMount() {
        queryModuleParents().then(json => {
            if (json.State == 1) {
                this.setState({
                    selectData: json.Data
                });
            } else {
                console.log(json.Message)
            }
        })
        queryModules({
            pageIndex: 1,
            pageSize: 10
        }).then(json => {
            if (json.State == 1) {
                this.setState({
                    data: json.Data.rows,
                    totalSize: json.Data.total
                });
            } else {
                console.log(json.Message)
            }
        })
    }

    //表格改变
    handleStandardTableChange = (pagination, filtersArg, sorter) => {
        const { formValues } = this.state;
        const params = {
            pageIndex: pagination.current,
            pageSize: pagination.pageSize,
            ...formValues,
        };
        if (sorter.field) {
            params.sort = sorter.field;
            params.order = sorter.order == 'descend' ? 'DESC' : 'ASC';
        }
        queryModules(params)
            .then(json => {
                if (json.State == 1) {
                    this.setState({
                        data: json.Data.rows,
                        totalSize: json.Data.total
                    });
                } else {
                    console.log(json.Message)
                }
            })
    }

    //搜索
    handleFormSearch = e => {
        e.preventDefault();
        const { form } = this.props;
        form.validateFields((err, fieldsValue) => {
            console.log(fieldsValue)
            if (err) return;

            const values = {
                ...fieldsValue,
                updatedAt: fieldsValue.updatedAt && fieldsValue.updatedAt.valueOf(),
            };
            this.setState({
                formValues: values,
            });
            queryModules({
                pageIndex: 1,
                pageSize: 5,
                ...values
            }).then(json => {
                if (json.State == 1) {
                    this.setState({
                        data: json.Data.rows,
                        totalSize: json.Data.total
                    });
                } else {
                    console.log(json.Message)
                }
            })
        });
    };

    //重置
    handleFormReset = () => {
        const { form } = this.props;
        form.resetFields();
        this.setState({
            formValues: {},
        });
        queryModules({
            pageIndex: 1,
            pageSize: 10
        }).then(json => {
            if (json.State == 1) {
                this.setState({
                    data: json.Data.rows,
                    totalSize: json.Data.total
                });
            } else {
                console.log(json.Message)
            }
        });
    };

    //删除
    handlerDelete(){

    }

    handlerSelectChange = (selectedRowKeys) => {
        console.log('selectedRowKeys changed: ', selectedRowKeys);
        this.setState({ selectedRowKeys });
      }

    checkChecked(arr){
        if (arr.length <= 0) {
            toastr.warning("请至少选择一行数据");
            return false;
        }
        if (arr.length > 1) {
            toastr.warning("只能编辑一行数据");
            return false;
        }
        return true;
    }

    //确认框
    confirm() {
        Modal.confirm({
            title: '确认删除',
            content: '确定要删除吗？',
            maskClosable: true,
            okText: '确认',
            cancelText: '取消',
            onOk: this.handlerDelete
        });
    }

    //搜索框
    renderSimpleForm() {
        const { getFieldDecorator } = this.props.form;
        const options = this.state.selectData.map(s => <Option key={s.ID}>{s.Name}</Option>);
        return (
            <Form onSubmit={this.handleFormSearch} className="ant-advanced-search-form">
                <Row gutter={{ md: 8, lg: 24, xl: 48 }}>
                    <Col md={6} sm={24}>
                        <FormItem label="模块名称">
                            {getFieldDecorator('moduleName')(<Input placeholder="请输入模块名称" />)}
                        </FormItem>
                    </Col>
                    <Col md={6} sm={24}>
                        <FormItem label="父模块">
                            {getFieldDecorator('parentId')(
                                <Select
                                    showSearch
                                    placeholder={'请选择父模块'}
                                    filterOption={(input, option) =>
                                        option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
                                >
                                    {options}
                                </Select>
                            )}
                        </FormItem>
                    </Col>
                    <Col md={12} sm={24}>
                        <FormItem label="添加时间">
                            <RangePicker
                                showTime={{ format: 'HH:mm' }}
                                format="YYYY-MM-DD HH:mm"
                                placeholder={['开始时间', '结束时间']}
                            // onChange={onChange}
                            // onOk={onOk}
                            />
                        </FormItem>
                    </Col>
                    <Col md={6} sm={24}>
                        <span>
                            <Button type="primary" htmlType="submit">
                                查询
                  </Button>
                            <Button style={{ marginLeft: 8 }} onClick={this.handleFormReset}>
                                重置
                  </Button>
                        </span>
                    </Col>
                </Row>
            </Form>
        );
    }
    render() {
        // const rowSelection = {
        //     selectedRowKeys,
        // }
        return (
            <div>
                {/* <ModuleComponent /> */}
                {this.renderSimpleForm()}
                <Button
                    onClick={this.handlerDelete}
                >
                    删除
                </Button>
                <StandardTable
                    //loading={this.state.loading}
                    data={this.state.data}
                    columns={columns}
                    pagination={{
                        defaultCurrent: 1,                                                  //默认的当前页数
                        total: this.state.totalSize,                                        //数据总数
                        pageSizeOptions: ['5', '10'],                            //指定每页可以显示多少条         
                        //hideOnSinglePage: true,                                             //只有一页时是否隐藏分页器
                        showTotal: (total, range) => `共 ${total} 条记录，当前显示第 ${range[0]}至${range[1]} `,    //分页信息显示
                        //itemRender: this.handlerItemRender.bind(this),                      //分页样式
                        showSizeChanger: true,                                              //是否可以改变 pageSize
                        //onShowSizeChange: this.handlerShowSizeChange.bind(this),            //pageSize 变化的回调
                        showQuickJumper: true,                                              //是否可以快速跳转至某页
                        //onChange: this.handlerPageChange.bind(this),                        //页码改变的回调，参数是改变后的页码及每页条数
                    }}
                    rowSelection={rowSelection}
                    onChange={this.handleStandardTableChange}
                />
            </div>
        )
    }
}
//ModuleContainer = WrapWithLoadData(ModuleContainer);
ModuleContainer = Form.create()(ModuleContainer);
export default ModuleContainer