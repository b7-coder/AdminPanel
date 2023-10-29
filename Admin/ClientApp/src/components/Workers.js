import React, { Component } from 'react';

export class Workers extends Component {

  constructor(props) {
    super(props);
    this.state = { 
        currentCount: 0,
        fullname : "",
        age: 0,
        who: "",
        workersList: [],

    };
    
  }

  componentDidMount() {
    fetch('https://localhost:7008/Workers/LookWorkers')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log(data)
            this.setState({
                workersList: data
            });
        })
        .catch(error => {
            console.log('error');
        });
}


  handleChange = (event) => {
    this.setState({ [event.target.name]: event.target.value });
  }
  handleSubmit = async () => {
    const { fullname, age, who } = this.state;

    try {
      const response = await fetch(`https://localhost:7008/Workers/AddWorkers?fullname=${fullname}&age=${age}&who=${who}`, {
        method: 'POST',
      }).then(()=>{


        this.setState({ fullname: "" });
        this.setState({ age: 0 });
        this.setState({ who: "" });
        this.componentDidMount();

      });

      const data = await response.json();
      this.setState({ response: data, error: null });
    } catch (error) {
      this.setState({ response: null, error: error.message || 'Что-то пошло не так' });
    }
  }


  deleteSumbit = async (id) => {
    try {
        const response = await fetch(`https://localhost:7008/Workers/DeleteWorkers?id=${id}`, {
            method: 'DELETE',
        }).then(()=>{
            this.componentDidMount();
        })
        } catch (error) {
          this.setState('Что-то пошло не так');
        }
      }

  render() {
    
    return (
      <div>
        <div className='add'>
            <input type='text' placeholder='fullname' name='fullname' value={this.state.fullname} onChange={this.handleChange}/> <br/>
            <input type='text' placeholder='age' name='age' value={this.state.age} onChange={this.handleChange}/> <br/>
            <input type='text' placeholder='who' name='who' value={this.state.who} onChange={this.handleChange}/> <br/>
            <button onClick={this.handleSubmit}>Add</button>
        </div>
        <div className='look'>
            {this.state.workersList.map((row)=>(
                <div>
                    <h1>{row.fullName}</h1>
                    <h1>{row.age}</h1>
                    <h1>{row.who}</h1>
                    <button onClick={() => this.deleteSumbit(row.id)}>delete</button>
                    <br/>
                </div>
            ))}
        </div>
      </div>
    );
  }
}
