import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor (props) {
    super(props);
    this.state = { 
        forecasts: [], 
        loading: true,
        serviceDirectoryUrl: process.env.REACT_APP_SERVICE_DIRECTORY_URL,
        weatherServiceBaseUrl: null
      };

      let serviceDirRequestUrl = this.state.serviceDirectoryUrl + "/api/services/WeatherService";

      fetch(serviceDirRequestUrl)
          .then(response => response.json())
          .then(data => this.setState({ weatherServiceBaseUrl: data.endpoint }))
          .then(() => {

              let weatherForecastRequetUrl = this.state.weatherServiceBaseUrl + "/api/SampleData/WeatherForecasts";

              fetch(weatherForecastRequetUrl)
                  .then(response => response.json())
                  .then(data => this.setState({ forecasts: data, loading: false }));
          });   
  }

  static renderForecastsTable (forecasts) {
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.dateFormatted}>
              <td>{forecast.dateFormatted}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1>Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }
}
