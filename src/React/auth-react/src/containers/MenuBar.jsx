import React, { Component } from 'react'
import PropTypes from 'prop-types';

import MenuBarComponent from '../components/MenuBar'

import { GetNavigation } from '../fetch/main/Navigate'

//import WrapWithLoadAjax from '../higher_components/WrapWithLoadAjax'

class MenuBarContainer extends Component {
  static propTypes = {
    data: PropTypes.any,
  }
  constructor() {
    super()
    this.state = {
      data: []
    }
  }

  componentWillMount() {
    // componentWillMount 生命周期中初始化用户名
    this._loadNavigation()
  }

  handleReload() {
    // _loadNavigation();
  }

  //加载导航
  _loadNavigation() {
    GetNavigation()
      .then(response =>
        response.json().then(json => ({ json, response }))
      ).then(({ json, response }) => {
        if (response.ok) {
          if (json.State === 1) {
            this.setState({ data: json.Data });
          } else {
            console.log(json.Message)
          }
        }else{
          console.log(response)
        }
      });
  }

  render() {
    return (
      <MenuBarComponent
        data={this.state.data}
        onReload={this.handleReload.bind(this)} />
    )
  }
}
export default MenuBarContainer
