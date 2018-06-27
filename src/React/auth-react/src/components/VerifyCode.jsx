import React, { Component } from 'react'
import PropTypes from 'prop-types'

export default class VerifyCode extends Component { 
    static propTypes = {
        imgSrc: PropTypes.string,
        onChangeVerifyCode: this.PropTypes.func
    }

    handlerClick(){
        if (this.props.onChangeVerifyCode) {
            this.props.onChangeVerifyCode();
        //    let imgSrc = this.props.onChangeVerifyCode();
        //    this.setState({ imgSrc: imgSrc })
        }
        this.setState({ imgSrc: this.props.imgSrc })
    }

    render(){
        <div>
            <span className='comment-field-name'>验证码：</span>
                <div className='comment-field-input'>
                    <img 
                        src={this.PropTypes.imgSrc}
                        onClick={this.handlerClick.bind(this)} />
                </div>
        </div>
    }
}