import React, { Component } from 'react'
import { get } from '../fetch/get'

const host = 'https://localhost:44338/api/'

export default (WrappedComponent, relativeUrl, paras) => {
    class AjaxActions extends Component {
        constructor() {
            super()
            this.state = { data: null }
        }

        componentWillMount() {
            this._loadData({
                relativeUrl: this.props.relativeUrl,
                args: (this.props.args === undefined ? '' : this.props.args),
                page: {pageIndex: this.props.pageIndex,pageSize: this.props.pageSize}
            });
        }
        handlerTableChange(paras){
            this._loadData({
                relativeUrl: this.props.relativeUrl,
                args: (this.props.args === undefined ? '' : this.props.args) + (paras.args === undefined ? '' : paras.args),
                page: {pageIndex: paras.pageIndex, pageSize: paras.pageSize}
            });
        }
        _loadData(paras) {
            let pageIndex = paras.page.pageIndex;
            let pageSize = paras.page.pageSize;
            let strParas = 'pageIndex=' + pageIndex + '&pageSize=' + pageSize + paras.args;
            get(host+ paras.relativeUrl+ '/?' + strParas)
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
                <WrappedComponent
                    {...this.props}
                    data={this.state.data}
                    loading={this.state.loading}
                    pageIndex={this.state.pageIndex}
                    pageSize={this.state.pageSize}
                    totalSize={this.state.totalSize}
                    onTableChange={this.handlerTableChange.bind(this)}
                    />
            )
        }
    }
    return AjaxActions
}
