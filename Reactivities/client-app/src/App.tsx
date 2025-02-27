import React, { Component } from "react";
import logo from "./logo.svg";
import "./App.css";
import axios from "axios";

class App extends Component {
  state = { values: [] };

  componentDidMount() {
    axios.get("http://localhost:5000/api/values/").then(response => {
      this.setState({
        values: response.data
      });
    });
  }
  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <ul>
            {this.state.values.map((val: any) => (
              <li key={val.id}>
                {val.id}: {val.name}
              </li>
            ))}
          </ul>
        </header>
      </div>
    );
  }
}

export default App;
