import React from 'react'
import PropTypes from 'prop-types'

import { Form, Icon, Input, Button, Checkbox } from 'antd';

import styles from '../wwwroot/css/login.css'

const FormItem = Form.Item;

class NormalLoginForm extends React.Component {
    static propTypes = {
        onSubmit: PropTypes.func
    }

    componentWillMount (){
        //this.username.focus();
    }    

  handleSubmit = (e) => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        if (this.props.onSubmit) {
            this.props.onSubmit({
                username: values.username,
                password: values.password
            })
        }
      }else{
        console.log('Received values of form: ', values);
      }
    });    
  }
  
  render() {
    const { getFieldDecorator } = this.props.form;
    return (
        <Form onSubmit={this.handleSubmit} className={styles.loginForm}>
        <h2 className={ styles.loginFormTitle }>登陆</h2>
        <FormItem>
            {getFieldDecorator('username', {
                rules: [{ required: true, message: '请输入用户名!' }],
            })(
                <Input 
                    prefix={<Icon type="user" style={{ color: 'rgba(0,0,0,.25)' }} />} 
                    placeholder="用户名"
                    ref={(username) => this.username = username} />
            )}
        </FormItem>
        <FormItem>
            {getFieldDecorator('password', {
                rules: [{ required: true, message: '请输入密码!' }],
            })(
                <Input prefix={<Icon type="lock" style={{ color: 'rgba(0,0,0,.25)' }} />} type="password" placeholder="密码" />
            )}
        </FormItem>
        <FormItem>
            {getFieldDecorator('remember', {
                valuePropName: 'checked',
                initialValue: true,
            })(
                <Checkbox>记住我</Checkbox>
            )}
            <Button type="primary" htmlType="submit" className={styles.loginFormButton}>
                登陆
            </Button>
        </FormItem>
    </Form>
    );
  }
}

const WrappedNormalLoginForm = Form.create()(NormalLoginForm);

export default WrappedNormalLoginForm;