import React, { Component, PropTypes } from 'react'

import NavigateListComponent from '../../components/navigation/NavigateList'

import { GetNavigation } from '../../fetch/main/Navigate'

//import WrapWithLoadAjax from '../higher_components/WrapWithLoadAjax'

class NavigateListContainer extends Component {
  // static propTypes = {
  //   data: PropTypes.any,
  // }
  constructor() {
    super()
    this.state = {
      navigation: ''
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
    let navigation = GetNavigation();
    this.setState({ navigation: navigation });
  }

  render() {
    return(
      <div>
        <NavigateListComponent
          //data={this.props.data}
          data={this.state.data}
          onReload={this.handleReload.bind(this)} />
      </div>
    )
  }
}

//NavigateContainer = wrapWithLoadData(NavigateContainer, GetNavigation())
export default NavigateListContainer
