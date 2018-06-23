import React, { Component } from 'react'
import { GetVerifyCode } from '../fetch/acount/Account'

import VerifyCodeComponent from '../components/VerifyCode'

class VerifyContainer extends Component {
    handleChangeVerifyCode() {
        _loadVerifyCode();
    }

    constructor(){
        super()
        this.state = {
            imgSrc: 'https://localhost:44338/api/account',
            staticImgSrc:'https://localhost:44338/api/account'
        }
    }

    componentWillMount(){
        _loadVerifyCode();
    }

    _loadVerifyCode(imgSrc){
        //let imgSrc = GetVerifyCode();
        var random = Math.random();
        this.setState({imgSrc: this.state.staticImgSrc+random})
    }

    render() {
        return (
            <VerifyCodeComponent
                imgSrc = {this.state.imgSrc}
                onChangeVerifyCode={this.handleChangeVerifyCode.bind(this)} />
        )
    }
}

export default VerifyContainer



