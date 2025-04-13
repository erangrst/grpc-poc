import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import { GreeterClient } from './grpcprotos/greeter_grpc_web_pb';

function App() {
  // const [name, setName] = useState("");
  const [greeting, setGreeting] = useState('');
  const [streamMessages, setStreamMessages] = useState<any[]>([]);


  useEffect(() => {
    // Initialize gRPC client
    const client = new GreeterClient('https://localhost:7001', null, null);

    // Server Streaming Call: SayHelloStream
    const streamRequest = new proto.helloworld.HelloRequest();

    streamRequest.setName('Streaming User React');

    const stream = client.sayHelloStream(streamRequest, {});

    stream.on('data', (response: any) => {
      console.log('%c onData', 'background-color: yellow', { response: response.getMessage() })

      setStreamMessages((prevMessages: any) => [
        ...prevMessages,
        response.getMessage(),
      ]);
    });

    stream.on('error', (err: any) => {
      console.error('Stream Error:', err);
    });

    return () => {
      stream.cancel();
    };
  }, []);

  const onClickGreet = () => {

    const client = new GreeterClient('https://localhost:7001', null, null);

    // Unary Call: SayHello
    const request = new proto.helloworld.HelloRequest();
    console.log('request', { request })
    request.setName('HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH-11');
    console.log('request 1', { request })

    client.sayHello(request, {}, (err, response) => {
      if (err) {
        console.error('Unary Call Error:', err);
      } else {
        console.log('Unary Call response:', { resp: response.getMessage() });

        setGreeting(response.getMessage());
      }
    });

    // Unary Call: SayHello
    const request2 = new proto.helloworld.HelloRequest2();
    console.log('request2', { request2 })
    request2.setName('REQ 22-11');
    request2.setId('REQ 22-11');
    console.log('request2 1', { request2 })

    client.sayHello2(request2, {}, (err, response) => {
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
