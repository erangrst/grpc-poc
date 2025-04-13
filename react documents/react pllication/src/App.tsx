import React, { useState } from 'react';
import logo from './logo.svg';
import './App.css';
import { GreeterClient } from './proto/greeter_grpc_web_pb';

function App() {
  // const [name, setName] = useState("");
  const [greeting, setGreeting] = useState('');
  // const [streamMessages, setStreamMessages] = useState([]);

  const onClickGreet = () => {

    const client = new GreeterClient('https://localhost:7001', null, null);

    // Unary Call: SayHello
    const request = new proto.helloworld.HelloRequest();

    // const request = new helloworld.HelloRequest();
    // const request = new HelloRequest();
    console.log('request', { request })
    request.setName('HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH-888888');
    console.log('request 1', { request })

    client.sayHello(request, {}, (err, response) => {
      if (err) {
        console.error('Unary Call Error:', err);
      } else {
        console.log('Unary Call response:', { resp: response.getMessage() });

        setGreeting(response.getMessage());
      }
    });


  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React 4
        </a>
        <button onClick={onClickGreet}>greet</button>
      </header>
    </div>
  );
}

export default App;
