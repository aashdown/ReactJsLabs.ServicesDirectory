import * as React from 'react';
import './Greetings.css';;

export class Greetings extends React.Component {
    static displayName = Greetings.name;

    constructor(props){
        super(props);

        this.state = {
            serviceDirectoryUrl: process.env.REACT_APP_SERVICE_DIRECTORY_URL,
            name: null,
            greetingServiceUrl: null,
            greetingText: null
        };

        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleSubmit(event){
        fetch(this.state.serviceDirectoryUrl + "/api/services/GreetingService")
            .then(response => response.json())
            .then(greetingService => this.setState({greetingServiceUrl: greetingService.endpoint}))
            .then(() => {
                fetch(this.state.greetingServiceUrl + "/api/greeting?name=" + this.state.name)
                    .then(response => response.text())
                    .then(greeting => this.setState({greetingText: greeting}))
            });

        event.preventDefault();
    }

    render(){
        let contents = this.state.greetingText !== null
            ? this.renderGreeting()
            : this.renderForm();

            return (
                <div>
                    <h1>Greetings</h1>
                    {contents}
                </div>
            );
    }

    renderGreeting(){
        return(
            <div className="greetingText">
                {this.state.greetingText}
            </div>
        );
    }

    renderForm(){
        return(
            <form onSubmit={this.handleSubmit}>
                <table cellPadding={5}>
                    <tr>
                        <td>Please enter your name</td>
                        <td>
                            <input name="name" type="text" onChange={(e) => this.setState({name: e.target.value})} />
                        </td>
                        <td>
                            <button type="submit">Submit</button>
                        </td>
                    </tr>
                </table>
            </form>
        );
    }
}