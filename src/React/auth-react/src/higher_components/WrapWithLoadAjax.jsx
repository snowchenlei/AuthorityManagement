import React, { Component } from 'react'

export default (WrappedComponent, loadFunc) => {
    class AjaxActions extends Component {
        constructor() {
            super()
            this.state = { data: null }
        }

        componentWillMount() {
            let data = loadFunc();
            try {
                this.setState({ data: data })
            } catch (e) {
                this.setState({ data })
            }
        }

        render() {
            return (
                <WrappedComponent
                    data={this.state.data}
                    {...this.props} />
            )
        }
    }
    return AjaxActions
}
