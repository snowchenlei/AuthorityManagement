import React, { Component } from 'react';

class UserComponent extends React.Component {
    render() {
        return (
            <div class="panel-group" id="accordion">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <span style={{ fontSize: '16px' }}>查询条件</span>
                        <span class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#searchPanel" style={{ float: 'right' }}>
                                <i id="searchTitle" class="glyphicon glyphicon-menu-up"></i>
                            </a>
                        </span>
                    </div>
                    <div id="searchPanel" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="control-label col-md-1" for="txt_search_phoneNumber">手机号</label>
                                <div class="col-md-2">
                                    <input type="text" class="form-control" id="txt_search_phoneNumber" />
                                </div>
                                <label class="control-label col-md-1" for="txt_search_userName">用户名</label>
                                <div class="col-md-2">
                                    <input type="text" class="form-control" id="txt_search_userName" />
                                </div>                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

export default UserComponent;