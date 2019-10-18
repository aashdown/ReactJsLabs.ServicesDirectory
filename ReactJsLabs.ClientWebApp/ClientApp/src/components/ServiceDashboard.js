import React, { Component } from 'react';

export class ServiceDashboard extends Component {
    static displayName = ServiceDashboard.name;

    constructor(props) {
        super(props);

        this.state = {
            serviceDirectoryUrl: process.env.REACT_APP_SERVICE_DIRECTORY_URL,
            loading: true,
            serviceDirectory: null
        };
    }

    componentDidMount() {
        fetch(this.state.serviceDirectoryUrl + "/api/services")
            .then(response => response.json())
            .then(data => this.setState({ serviceDirectory: data }));

        this.setState({ loading: false });
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderServiceList();

        return (
            <div>
                <h1>Service Directory</h1>
                {contents}
            </div>
        );
    }

    renderServiceList() {
        return (
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Service Name</th>
                        <th>Endpoint</th>
                        <th>Last Update</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        this.state.serviceDirectory != null && this.state.serviceDirectory.services.length > 0
                            ? this.state.serviceDirectory.services.map(s =>
                                <tr>
                                    <td>{s.serviceName}</td>
                                    <td>{s.endpoint}</td>
                                    <td>{s.lastUpdate}</td>
                                    <td>{s.status}?</td>
                                </tr>
                            )
                            : <tr><td colSpan={4}><i>No Data</i></td></tr>
                    }
                </tbody>
            </table>
        );
    }
}