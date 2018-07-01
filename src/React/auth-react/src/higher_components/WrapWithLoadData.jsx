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
                paras: this.props.paras
            });
        }
        // handleChange(pageIndex, pageSize) {
        //     this._loadData({
        //         pageIndex: pageIndex,
        //         pageSize: pageSize
        //     });
        // }
        _loadData(paras) {
            console.log(paras.relativeUrl)
            let pageIndex = 1;//paras.pageIndex;
            let pageSize = 5;//paras.pageSize;
            let strParas = 'pageIndex=' + pageIndex + '&pageSize=' + pageSize;
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
                    data={this.state.data}
                    columns={this.props.columns}
                    loading={this.state.loading}
                    pageIndex={this.state.pageIndex}
                    pageSize={this.state.pageSize}
                    totalSize={this.state.totalSize}/>
            )
        }
    }
    return AjaxActions
}
