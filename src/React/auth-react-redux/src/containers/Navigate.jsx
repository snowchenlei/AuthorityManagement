import React, { Component } from 'react'

import { GetNavigation } from '../fetch/main/Navigate'

class NavigateContainer extends Component{
    componentWillMount () {
        // componentWillMount 生命周期中初始化用户名
        this._loadUsername()
      }

      _loadUsername () {
        GetNavigation()
      }
}
